using QualityOfServiceApp.Helpers;
using System.Windows.Input;

namespace QualityOfServiceApp.ViewModels
{
    public class HomeViewModel : BaseViewModel, IPageViewModel
    {
        private ICommand goToQuestionnaireCommand;
        private ICommand goToLoginCommand;
        public ICommand GoToQuestionnaireCommand => goToQuestionnaireCommand ?? (goToQuestionnaireCommand = new RelayCommand(x =>
                                                                  {
                                                                      Mediator.Notify("GoToQuestionnairePage", null);
                                                                  }));
        public ICommand GoToLoginCommand => goToLoginCommand ?? (goToLoginCommand = new RelayCommand(x =>
                                                                {
                                                                    Mediator.Notify("GoToLoginPage", null);
                                                                }));

        public void UpdateBinding()
        {
        }
    }
}
