using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WPF_LoginForm;
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
        private Visibility stackPanelVisibility = Visibility.Hidden;
        private UserModel selectedUser;
       

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

        public Visibility StackPanelVisibility
        {
            get
            {
                return stackPanelVisibility;
            }

            set
            {
                stackPanelVisibility = value;
                OnPropertyChanged(nameof(StackPanelVisibility));
            }
        }
        public UserModel SelectedUser
        {
            get { return selectedUser; }
            set
            {
                if (selectedUser != value)
                {
                    selectedUser = value;
                    OnPropertyChanged(nameof(SelectedUser));
                }
            }
        }
        

        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();
            LoadCurrentUserData();
            CheckCurrentAccessLevel();
        }

        private void CheckCurrentAccessLevel()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);

            if (user.Status == "admin")
            {
                stackPanelVisibility = Visibility.Visible;
            }
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

                Colorss = new ObservableCollection<ColorInfo>
            {
                new ColorInfo { Name = "Red", Color = Colors.Red },
                new ColorInfo { Name = "ForestGreen", Color = Colors.ForestGreen },
                new ColorInfo { Name = "Blue", Color = Colors.Blue },
                new ColorInfo { Name = "Yellow", Color = Colors.Yellow },
                new ColorInfo { Name = "IndianRed", Color = Colors.IndianRed },
                new ColorInfo { Name = "BlueViolet", Color = Colors.BlueViolet },
                new ColorInfo { Name = "Sienna", Color = Colors.Sienna },
                new ColorInfo { Name = "DimGray", Color = Colors.DimGray },
            };

            }
            else
            {
                CurrentUserAccount.DisplayName="Invalid user, not logged in";
                //Hide child views.
            }
        }

        public ObservableCollection<ColorInfo> Colorss { get; set; }
        
       
    }
}
