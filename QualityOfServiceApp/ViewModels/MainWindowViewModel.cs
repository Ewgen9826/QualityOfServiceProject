﻿using QualityOfServiceApp.Helpers;
using QualityOfServiceApp.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QualityOfServiceApp.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Commands
        private ICommand goToHomePageCommand;
        public ICommand GoToHomePageCommand => goToHomePageCommand ?? (goToHomePageCommand = new RelayCommand(x =>
        {
            Mediator.Notify("GoToHomePage", null);
        }));

        private ICommand exitCommand;
        public ICommand ExitCommand => exitCommand ?? (exitCommand = new RelayCommand(x =>
        {
            Application.Current.Shutdown();
        }));

        private ICommand minWindowCommand;
        public ICommand MinWindowCommand => minWindowCommand ?? (minWindowCommand = new RelayCommand(x =>
          {
              WindowState = WindowState.Minimized;
          }));

        private ICommand maxWindowCommand;
        public ICommand MaxWindowCommand => maxWindowCommand ?? (maxWindowCommand = new RelayCommand(x =>
         {
             WindowState = WindowState.Maximized;
         }));

        private ICommand normalWindowCommand;
        public ICommand NormalWindowCommand => normalWindowCommand ?? (normalWindowCommand = new RelayCommand(x =>
        {
            WindowState = WindowState.Normal;
        }));

        #endregion

        private WindowState windowState;
        public WindowState WindowState
        {
            get => windowState;
            set
            {
                windowState = value;
                OnPropertyChanged("WindowState");
                OnPropertyChanged("IsMaxWindow");
                OnPropertyChanged("IsMinWindow");
            }
        }
        public Visibility IsMaxWindow => WindowState == WindowState.Maximized ? Visibility.Visible : Visibility.Collapsed;
        public Visibility IsMinWindow => WindowState == WindowState.Maximized ? Visibility.Collapsed : Visibility.Visible;
        private IPageViewModel currentPageViewModel;
        private List<IPageViewModel> pageViewModels;

        private string header;
        public string Header
        {
            get => header;
            set
            {
                header = value;
                OnPropertyChanged("Header");
            }
        }

        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (pageViewModels == null)
                    pageViewModels = new List<IPageViewModel>();

                return pageViewModels;
            }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return currentPageViewModel;
            }
            set
            {
                currentPageViewModel = value;
                OnPropertyChanged("CurrentPageViewModel");
            }
        }

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
            IPageViewModel page = CurrentPageViewModel;
            page.UpdateBinding();
        }

        private void GoToHomePage(object obj)
        {
            Header = "Главная";
            ChangeViewModel(PageViewModels[0]);
        }

        private void GoToQuestionnairePage(object obj)
        {
            ChangeViewModel(PageViewModels[1]);
        }

        private void GoToLoginPage(object obj)
        {
            Header = "Страница входа в систему";
            ChangeViewModel(PageViewModels[2]);
        }
        private void GoToAdminPage(object obj)
        {
            Header = "Административная панелль";
            ChangeViewModel(PageViewModels[3]);
        }
        private void GoToBanksPage(object obj)
        {
            Header = "Управление банками";
            ChangeViewModel(PageViewModels[4]);
        }
        private void GoToServicePage(object obj)
        {
            Header = "Управление услугами банков";
            ChangeViewModel(PageViewModels[5]);
        }
        private void GoToBankResultPage(object obj)
        {
            Header = "Результаты по отдельным банкам";
            ChangeViewModel(PageViewModels[6]);
        }

        private void GoToOverallResultPage(object obj)
        {
            Header = "Общие результаты";
            ChangeViewModel(PageViewModels[7]);
        }
        private void GoToReportResultPage(object obj)
        {
            Header = "Отчет";
            ChangeViewModel(PageViewModels[8]);
        }
        public MainWindowViewModel()
        {
            // Add available pages and set page
            PageViewModels.Add(new HomeViewModel());
            PageViewModels.Add(new QuestionnaireViewModel());
            PageViewModels.Add(new LoginViewModel());
            PageViewModels.Add(new AdminViewModel());
            PageViewModels.Add(new BanksViewModel());
            PageViewModels.Add(new ServiceViewModel());
            PageViewModels.Add(new BankResultViewModel());
            PageViewModels.Add(new OverallResultViewModel());
            PageViewModels.Add(new ReportViewModel());

            CurrentPageViewModel = PageViewModels[0];
            Header = "Главная";

            Mediator.Subscribe("GoToHomePage", GoToHomePage);
            Mediator.Subscribe("GoToLoginPage", GoToLoginPage);
            Mediator.Subscribe("GoToQuestionnairePage", GoToQuestionnairePage);
            Mediator.Subscribe("GoToAdminPage", GoToAdminPage);
            Mediator.Subscribe("GoToBanksPage", GoToBanksPage);
            Mediator.Subscribe("GoToServicePage", GoToServicePage);
            Mediator.Subscribe("GoToBankResultPage", GoToBankResultPage);
            Mediator.Subscribe("GoToOverallResultPage", GoToOverallResultPage);
            Mediator.Subscribe("GoToReportPage", GoToReportResultPage);
        }
    }
}
