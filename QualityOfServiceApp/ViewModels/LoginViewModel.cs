using QualityOfServiceApp.Helpers;
using System.Windows.Input;

namespace QualityOfServiceApp.ViewModels
{
    public class LoginViewModel:BaseViewModel, IPageViewModel
    {
        private ICommand goToAdminPageCommand;
        public ICommand GoToAdminPageCommand => goToAdminPageCommand ?? (goToAdminPageCommand = new RelayCommand(x =>
        {
            Mediator.Notify("GoToAdminPage", null);
        }));

        public LoginViewModel()
        {
            
        }
    }
}
