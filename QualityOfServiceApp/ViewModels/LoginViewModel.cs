using QualityOfServiceApp.Helpers;
using QualityOfServiceApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace QualityOfServiceApp.ViewModels
{
    public class LoginViewModel:BaseViewModel, IPageViewModel
    {
        private readonly List<User> users;
        public LoginViewModel()
        {
            var context = new ApplicationContext();
            users = context.Users.ToList();
        }

        private ICommand goToAdminPageCommand;
        public ICommand GoToAdminPageCommand => goToAdminPageCommand ?? (goToAdminPageCommand = new RelayCommand(x=>Login(),y=>CanActivate()));

        private string userName;
        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        private void Login()
        {
            Mediator.Notify("GoToAdminPage", null);
        }
        private bool CanActivate()
        {       
            var user = users.FirstOrDefault(u => u.UserName == this.UserName && u.Password == this.Password);
            if (user == null) return false;
            return true;
        }
    }
}
