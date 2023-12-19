using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Xceed.Wpf.Toolkit.Core.Media;
using WPF_LoginForm.ViewModels;
using Xceed.Wpf.Toolkit;
using WPF_LoginForm.Models;
using System.Net;
using Xceed.Wpf.Toolkit.Primitives;

namespace WPF_LoginForm.Views
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            
            InitializeComponent();

            BDShow();



        }
        private void BDShow()
        {
            string connectionString = "Server=(local);Database=Guka_4; Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Id, Username, Name, Lastname, Email, Status FROM [dbo].[User]";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        userGrid.ItemsSource = dataTable.DefaultView;
                    }
                }
            }
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            //WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void SortByNameBt(object sender, RoutedEventArgs e)
        {
            string connectionString = "Server=(local);Database=Guka_4; Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Username, Name, Lastname, Email, Status FROM [dbo].[User] ORDER BY Name";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        userGrid.ItemsSource = dataTable.DefaultView;
                    }
                }
            }
        }

        private void SortByLastname(object sender, RoutedEventArgs e)
        {
            string connectionString = "Server=(local);Database=Guka_4; Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Username, Name, Lastname, Email, Status FROM [dbo].[User] ORDER BY Lastname";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        userGrid.ItemsSource = dataTable.DefaultView;
                    }
                }
            }
        }

        private void SortByStatus(object sender, RoutedEventArgs e)
        {
            string connectionString = "Server=(local);Database=Guka_4; Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Username, Name, Lastname, Email, Status FROM [dbo].[User] ORDER BY Status";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        userGrid.ItemsSource = dataTable.DefaultView;
                    }
                }
            }
        }

      
       
        public Color clr { get; set; }
        public Color clrtwo { get; set; }

        //фон грида цвет
        private void colorGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (colorGrid.SelectedItem != null)
            {
                var selectedColorInfo = (ColorInfo)colorGrid.SelectedItem;
                clr = selectedColorInfo.Color;
                SolidColorBrush brush = new SolidColorBrush(clr);

                Resources["BorderStyle"] = new Style(typeof(Border))
                {
                    Setters = { new Setter(Border.BackgroundProperty, brush) }
                };
            }
        }

        private void clr2_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            var selectedColorInfo = clr2.SelectedColor;


                clr = (Color)selectedColorInfo;
                SolidColorBrush brush = new SolidColorBrush(clr);

                Resources["BorderStyle"] = new Style(typeof(Border))
                {
                    Setters = { new Setter(Border.BackgroundProperty, brush) }
                };
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddUserWindow w = new AddUserWindow();
            w.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            if (userGrid.SelectedItem != null && userGrid.SelectedItem is DataRowView)
            {
                DataRowView row = (DataRowView)userGrid.SelectedItem;
                int userId = Convert.ToInt32(row["Id"]);
                Delete_User(userId);
            }
        }
 

        public string ElementiGrida(string nazvanie)
        {
           if (userGrid.SelectedItem != null && userGrid.SelectedItem is DataRowView)
            {
                DataRowView row = (DataRowView)userGrid.SelectedItem;
                string Element = row[nazvanie].ToString();
                return Element;
               
            }
             return null;
        }
        private void Delete_User(int Id)
        {
            SqlConnection connection = new SqlConnection("Server=(local);Database=Guka_4; Integrated Security=True");
            string cmd = "DELETE FROM [User] WHERE Id = @Id";
            SqlCommand deleteCommand = new SqlCommand(cmd, connection);
            deleteCommand.Parameters.AddWithValue("@Id", Id);

            try
            {
                connection.Open();
                deleteCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection("Server=(local);Database=Guka_4; Integrated Security=True");
            connection.Open();
            string cmd = "SELECT * FROM [User]";
            SqlCommand createCommand = new SqlCommand(cmd, connection);
            createCommand.ExecuteNonQuery();
            SqlDataAdapter dataAdp = new SqlDataAdapter(createCommand);
            DataTable dt = new DataTable("User");
            dataAdp.Fill(dt);
            userGrid.ItemsSource = dt.DefaultView;
            connection.Close();
        }
        //кнопка редактирования пользователя
        public void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (userGrid.SelectedItem != null && userGrid.SelectedItem is DataRowView)
            {
                DataRowView row = (DataRowView)userGrid.SelectedItem;
                userName = Convert.ToString(row["Username"]);
                name = Convert.ToString(row["Name"]);
                lastname = Convert.ToString(row["Lastname"]);
                email = Convert.ToString(row["Email"]);
                status = Convert.ToString(row["Status"]);
                id = Convert.ToInt32(row["Id"]);
                EditUserWindow ed = new EditUserWindow();
                ed.username = userName;
                ed.Name = name;
                ed.Lastname = lastname;
                ed.Email = email;
                ed.Status = status;
                ed.Id = id;
                ed.Show();
            }

        }
       public string userName { get; set; }
       public string name { get; set; }
       public string lastname { get; set; }
       public string email { get; set; }
       public string status { get; set; }
       public int id { get; set; }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri("C:\\Users\\Маргарита\\source\\repos\\Login-In-WPF-MVVM-C-Sharp-and-SQL-Server-main\\WPF-LoginForm\\Images\\back3.jpg", UriKind.Relative));

            Resources["BorderStyle"] = new Style(typeof(Border))
            {
                Setters = { new Setter(Border.BackgroundProperty, brush) }
            };
        
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri("C:\\Users\\Маргарита\\source\\repos\\Login-In-WPF-MVVM-C-Sharp-and-SQL-Server-main\\WPF-LoginForm\\Images\\back-image2.jpg", UriKind.Relative));

            Resources["BorderStyle"] = new Style(typeof(Border))
            {
                Setters = { new Setter(Border.BackgroundProperty, brush) }
            };
  
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri("C:\\Users\\Маргарита\\source\\repos\\Login-In-WPF-MVVM-C-Sharp-and-SQL-Server-main\\WPF-LoginForm\\Images\\дум тем.png", UriKind.Relative));

            Resources["BorderStyle"] = new Style(typeof(Border))
            {
                Setters = { new Setter(Border.BackgroundProperty, brush) }
            };
          
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));

                Resources["BorderStyle"] = new Style(typeof(Border))
                {
                    Setters = { new Setter(Border.BackgroundProperty, brush) }
                };
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (RamkiGrid.SelectedItem != null && RamkiGrid.SelectedItem != null)
            {
                var selectedColorInfo = (ColorInfo)RamkiGrid.SelectedItem;
                clr = selectedColorInfo.Color;
                //LinearGradientBrush brush = new LinearGradientBrush();
                //brush.StartPoint = new Point(0, 0);
                //brush.EndPoint = new Point(1, 1);
                //brush.GradientStops.Add(new GradientStop(clr, 0.0));
                //brush.GradientStops.Add(new GradientStop(clr, 1.1));
                //Resources["RamkiStyle"] = new Style(typeof(Border))
                //{
                //    Setters = { new Setter(Border.BorderBrushProperty, brush) }
                //};
                SolidColorBrush brush = new SolidColorBrush(clr);

                Resources["RamkiStyle"] = new Style(typeof(Border))
                {
                    Setters = { new Setter(Border.BorderBrushProperty, brush) }
                };
            }
        }

        private void clr3_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {

            var selectedColorInfo = clr4.SelectedColor;


            clr = (Color)selectedColorInfo;
            SolidColorBrush brush = new SolidColorBrush(clr);

            Resources["RamkiStyle"] = new Style(typeof(Border))
            {
                Setters = { new Setter(Border.BorderBrushProperty, brush) }
            };
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            ResourceDictionary lightTheme = new ResourceDictionary();
            lightTheme.Source = new Uri("Light.xaml", UriKind.Relative);
            Application.Current.Resources.MergedDictionaries.Add(lightTheme);
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            ResourceDictionary darkTheme = new ResourceDictionary();
            darkTheme.Source = new Uri("Dark.xaml", UriKind.Relative);
            Application.Current.Resources.MergedDictionaries.Add(darkTheme);

        }
    }
}