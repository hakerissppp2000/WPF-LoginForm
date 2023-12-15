using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;
using WPF_LoginForm.Views;

namespace WPF_LoginForm.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
       
        //Fields
        private string _username;
        private string _name;
        private string _lastname;
        private string _email;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;

        private IUserRepository userRepository;

        //Properties
        public string Username
        {
            get
            {
                return _username;
            }

            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Lastname
        {
            get
            {
                return _lastname;
            }

            set
            {
                _lastname = value;
                OnPropertyChanged(nameof(Lastname));
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public SecureString Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }

            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public bool IsViewVisible
        {
            get
            {
                return _isViewVisible;
            }

            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

 
        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; }

       

        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; } 
        //my
        public ICommand OpenRegistrationCommand { get; private set; } 
        public ICommand BackToLogInCommand { get; private set; }

        //Constructor
        public LoginView loginWindow = LoginView.loginWindow;
        public LoginViewModel()
        {
            userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPasswordCommand = new ViewModelCommand(p => ExecuteRecoverPassCommand("", ""));
            
            OpenRegistrationCommand = new ViewModelCommand(OpenRegistration);
            BackToLogInCommand = new ViewModelCommand(BackToLogIn);
        }


        private void OpenRegistration(object sender)
        {
            IsViewVisible = false;
            //loginWindow.Visibility = Visibility.Hidden;
            Registration reg = new Registration();
            reg.Show();
        }

        private void BackToLogIn(object sender)
        {
            
            loginWindow.Visibility = Visibility.Visible;
            App.Current.MainWindow.Close();
        }

        private bool CanExecuteLoginCommand(object obj)
            {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 ||
                Password == null || Password.Length < 3)
                validData = false;
            else
                validData = true;
            return validData;
            }

        private void ExecuteLoginCommand(object obj)
        {
            var isValidUser = userRepository.AuthenticateUser(new NetworkCredential(Username, Password));
            var CurrentStatus = userRepository.GetStatus(new NetworkCredential(Username, Password));
            if (isValidUser && CurrentStatus == "user")
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Username), null);
                IsViewVisible = false;
            }
            if (isValidUser && CurrentStatus == "admin")
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Username), null);
                AdminViewxaml admin = new AdminViewxaml();
                admin.Show();
                App.Current.MainWindow.Close(); 
            }
            else
            {
                ErrorMessage = "* Invalid username or password";
            }
        }

        private void ExecuteRecoverPassCommand(string username, string email)
        {
            throw new NotImplementedException();
        }

        
    }
}
