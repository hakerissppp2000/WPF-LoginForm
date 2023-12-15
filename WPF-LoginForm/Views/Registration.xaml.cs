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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
      
        public Registration()
        {
            InitializeComponent();

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this. Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e) { }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginView lg = new LoginView();
            lg.Show();
            this.Close();
        }

        private void btnLogin_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPass.Text) || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtLastName.Text) || string.IsNullOrEmpty(txtEmail.Text))
            {
                return;
            }

            try
            {
                SqlConnection con = new SqlConnection(@"Server=(local); Database=Guka_4; Integrated Security=True");
                con.Open();
                string add_data = "INSERT INTO [dbo].[User] VALUES(@Username, @Password, @Name, @LastName, @Email, 'user')";
                SqlCommand cmd = new SqlCommand(add_data, con);

                cmd.Parameters.AddWithValue("@Username", txtUser.Text);
                cmd.Parameters.AddWithValue("@Password", txtPass.Text);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                txtUser.Text = "";
                txtPass.Text = "";
                txtName.Text = "";
                txtLastName.Text = "";
                txtEmail.Text = "";
                LoginView lp = new LoginView();
                this.Close();
                lp.Show();
            }
            catch
            {

            }
        }
    }
}
