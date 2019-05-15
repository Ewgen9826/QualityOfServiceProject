using QualityOfServiceApp.Helpers;
using QualityOfServiceApp.Models;
using QualityOfServiceApp.Repository;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace QualityOfServiceApp.ViewModels
{
    public class BanksViewModel : BaseViewModel, IPageViewModel
    {
        #region Repository initial
        private readonly BankRepository repository;
        private readonly ServiceRepository serviceRepository;
        public BanksViewModel()
        {
            repository = new BankRepository();
            serviceRepository = new ServiceRepository();
            Banks = new ObservableCollection<Bank>();
            Services = new ObservableCollection<SelectedViewModel>();
            SetServiceCollection();
            SetCollection();
        }

        private void SetServiceCollection()
        {
            Services.Clear();
            foreach (var item in serviceRepository.GetAll())
            {
                Services.Add(new SelectedViewModel { Name = item.Name, IsSelect = false });
            }
        }

        #endregion

        #region Commands
        private ICommand goToAdminPageCommand;
        public ICommand GoToAdminPageCommand => goToAdminPageCommand ?? (goToAdminPageCommand = new RelayCommand(x =>
        {
            Mediator.Notify("GoToAdminPage", null);
        }));

        private ICommand addBankCommand;
        public ICommand AddBankCommand => addBankCommand ?? (addBankCommand = new RelayCommand(x =>
         {
             AddBank();
         }));

        private ICommand removeBankCommand;
        public ICommand RemoveBankCommand => removeBankCommand ?? (removeBankCommand = new RelayCommand(x =>
        {
            RemoveBank();
        }));

        private ICommand updateListCommand;
        public ICommand UpdateListCommand => updateListCommand ?? (updateListCommand = new RelayCommand(x =>
        {
            SetServiceCollection();
        }));
        #endregion

        #region Notify Propertis
        public ObservableCollection<Bank> Banks { get; set; }
        public ObservableCollection<SelectedViewModel> Services { get; set; }

        private Bank selectBank;
        public Bank SeleckBank
        {
            get => selectBank;
            set
            {
                selectBank = value;
                OnPropertyChanged("SeleckBank");
            }
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private string region;
        public string Region
        {
            get => region;
            set
            {
                region = value;
                OnPropertyChanged("Region");
            }
        }

        private string neighborhood;
        public string Neighborhood
        {
            get => neighborhood;
            set
            {
                neighborhood = value;
                OnPropertyChanged("Neighborhood");
            }
        }

        private string locality;
        public string Locality
        {
            get => locality;
            set
            {
                locality = value;
                OnPropertyChanged("Locality");
            }
        }
        #endregion

        #region Private Methods

        private void AddBank()
        {
            Bank bank = new Bank
            {
                Name = this.Name,
                Region = this.Region,
                Neighborhood = this.Neighborhood,
                Locality = this.Locality
            };
            var context = new ApplicationContext();
            foreach (var item in Services)
            {
                if (item.IsSelect)
                {
                    var service = context.Services.FirstOrDefault(s => s.Name == item.Name);
                    if (service == null) return;
                    bank.Services.Add(service);
                }
            }
            repository.Add(bank);
            repository.SaveAll();
            Banks.Add(bank);
        }
        private void RemoveBank()
        {
            if (SeleckBank == null) return;
            repository.Delete(SeleckBank);
            repository.SaveAll();
            Banks.Remove(SeleckBank);
        }
        private void SetCollection()
        {
            foreach (var item in repository.GetAll())
            {
                Banks.Add(item);
            }
        }
        #endregion
    }
}
