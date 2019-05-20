using QualityOfServiceApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QualityOfServiceApp.ViewModels
{
    public class AdminViewModel: BaseViewModel, IPageViewModel
    {
        private ICommand goToBanksPageCommand;
        public ICommand GoToBanksPageCommand => goToBanksPageCommand ?? (goToBanksPageCommand = new RelayCommand(x =>
        {
            Mediator.Notify("GoToBanksPage", null);
        }));

        private ICommand goToServicePageCommand;
        public ICommand GoToServicePageCommand => goToServicePageCommand ?? (goToServicePageCommand = new RelayCommand(x =>
        {
            Mediator.Notify("GoToServicePage", null);
        }));

        private ICommand goToBankResultPageCommand;
        public ICommand GoToBankResultPageCommand => goToBankResultPageCommand ?? (goToBankResultPageCommand = new RelayCommand(x =>
        {
            Mediator.Notify("GoToBankResultPage", null);
        }));

        private ICommand goToOverallResultPageCommand;
        public ICommand GoToOverallResultPageCommand => goToOverallResultPageCommand ?? (goToOverallResultPageCommand = new RelayCommand(x =>
        {
            Mediator.Notify("GoToOverallResultPage", null);
        }));

        public void UpdateBinding()
        {

        }
    }
}
