using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;
using WPF_LoginForm.Views;

namespace WPF_LoginForm
{
    /// <summary>
    /// Логика взаимодействия для EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        public string username { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public int Id { get; set; }
        public EditUserWindow()
        {
            InitializeComponent();
            
           
           
        }
        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtLastName.Text) || string.IsNullOrEmpty(txtEmail.Text))
            {
                return;
            }

            try
            {
                    SqlConnection con = new SqlConnection(@"Server=(local); Database=Guka_4; Integrated Security=True");
                    con.Open();
                    string add_data = "UPDATE [dbo].[User] SET Username = @Username, Name = @Name, LastName = @LastName, Email = @Email, Status = @Status WHERE Id=@Id";
                    SqlCommand cmd = new SqlCommand(add_data, con);

                    cmd.Parameters.AddWithValue("@Username", txtUser.Text);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Status", (StatusCombobox.SelectedItem as ComboBoxItem).Content.ToString());
                cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    txtUser.Text = "";
                    txtName.Text = "";
                    txtLastName.Text = "";
                    txtEmail.Text = "";
                    MessageBox.Show("User updated successfully");
                    this.Close();
              
            }
            catch
            {

            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUser.Text = username;
            txtName.Text = Name;
            txtLastName.Text = Lastname;
            txtEmail.Text = Email;
            if(Status == "user")
            {
                StatusCombobox.SelectedIndex = 0;
            }
            else
            {
                StatusCombobox.SelectedIndex = 1;
            }
        }
    }
}
