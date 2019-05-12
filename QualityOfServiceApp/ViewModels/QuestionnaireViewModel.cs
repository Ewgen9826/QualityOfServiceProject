using QualityOfServiceApp.Helpers;
using System.Windows.Input;

namespace QualityOfServiceApp.ViewModels
{
    public class QuestionnaireViewModel : BaseViewModel, IPageViewModel
    {
        private ICommand goToHomeCommand;
        public ICommand GoToHomeCommand => goToHomeCommand ?? (goToHomeCommand = new RelayCommand(x =>
        {
            Mediator.Notify("GoToHomePage", null);
        }));
    }
}
