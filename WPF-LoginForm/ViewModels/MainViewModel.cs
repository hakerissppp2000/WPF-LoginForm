using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;

namespace WPF_LoginForm.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //Fields
        private UserAccountModel _currentUserAccount;
        private string _UserNiw;
        private string _UserName;
        private string _UserLastname;
        private string _UserStatus;
        private IUserRepository userRepository;

        public UserAccountModel CurrentUserAccount
        {
            get
            {
                return _currentUserAccount;
            }

            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }
        public string UserNow
        {
            get
            {
                return _UserNiw;
            }

            set
            {
                _UserNiw = value;
                OnPropertyChanged(nameof(UserNow));
            }
        }

        public string UserName
        {
            get
            {
                return _UserName;
            }

            set
            {
                _UserName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public string UserLastname
        {
            get
            {
                return _UserLastname;
            }

            set
            {
                _UserLastname = value;
                OnPropertyChanged(nameof(UserLastname));
            }
        }

        public string UserStatus
        {
            get
            {
                return _UserStatus;
            }

            set
            {
                _UserStatus = value;
                OnPropertyChanged(nameof(UserStatus));
            }
        }

        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();
            LoadCurrentUserData();
        }

        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                CurrentUserAccount.Username = user.Username;
                UserNow = $"Welcome, {user.Name} {user.LastName} ;)";
                UserName =$"Name: {user.Name}";
                UserLastname = $"Lastame: {user.LastName}";
                UserStatus = $"Status: {user.Status}";
                CurrentUserAccount.ProfilePicture = null;               
            }
            else
            {
                CurrentUserAccount.DisplayName="Invalid user, not logged in";
                //Hide child views.
            }
        }
    }
}
