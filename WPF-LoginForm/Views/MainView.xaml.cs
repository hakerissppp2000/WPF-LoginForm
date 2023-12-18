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
            


            string connectionString = "Server=(local);Database=Guka_4; Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Username, Name, Lastname, Email, Status FROM [dbo].[User]";

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


        private void colorGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (colorGrid.SelectedItem != null)
            {
                var selectedColorInfo = (ColorInfo)colorGrid.SelectedItem;
                // Use the selectedColorInfo in your logic
                // for example:
                clr = selectedColorInfo.Color;
                SolidColorBrush brush = new SolidColorBrush(clr);

                // Установка значения свойства "Background" стиля "BorderStyle" в созданный объект brush
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

                // Установка значения свойства "Background" стиля "BorderStyle" в созданный объект brush
                Resources["BorderStyle"] = new Style(typeof(Border))
                {
                    Setters = { new Setter(Border.BackgroundProperty, brush) }
                };
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
