using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using WPF_LoginForm.Views;

namespace WPF_LoginForm
{
    /// <summary>
    /// Логика взаимодействия для AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        public AddUserWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPass.Text) || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtLastName.Text) || string.IsNullOrEmpty(txtEmail.Text))
            {
                return;
            }

            try
            {

                if (!LoginExists(txtUser.Text))
                {
                    SqlConnection con = new SqlConnection(@"Server=(local); Database=Guka_4; Integrated Security=True");
                    con.Open();
                    string add_data = "INSERT INTO [dbo].[User] VALUES(@Username, @Password, @Name, @LastName, @Email, @Status)";
                    SqlCommand cmd = new SqlCommand(add_data, con);

                    cmd.Parameters.AddWithValue("@Username", txtUser.Text);
                    cmd.Parameters.AddWithValue("@Password", txtPass.Text);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Status", (StatusCombobox.SelectedItem as ComboBoxItem).Content.ToString());
                    cmd.ExecuteNonQuery();
                    con.Close();
                    txtUser.Text = "";
                    txtPass.Text = "";
                    txtName.Text = "";
                    txtLastName.Text = "";
                    txtEmail.Text = "";
                    MessageBox.Show("User added successfully");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("A user with the same username already exists", "Error");
                }
                }
            catch
            {

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        public bool LoginExists(string login)
        {
            bool exists;

            using (var connection = new SqlConnection(@"Server=(local); Database=Guka_4; Integrated Security=True"))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;

                command.CommandText = "SELECT COUNT(*) FROM [User] WHERE Username=@Login";
                command.Parameters.AddWithValue("@Login", login);

                exists = (int)command.ExecuteScalar() > 0;
            }

            return exists;
        }
    }
}
