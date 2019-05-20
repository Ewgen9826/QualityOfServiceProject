using QualityOfServiceApp.Helpers;
using QualityOfServiceApp.Models;
using QualityOfServiceApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace QualityOfServiceApp.ViewModels
{
    public class BanksViewModel : BaseViewModel, IPageViewModel
    {
        #region Repository initial
        private readonly BankRepository bankRepository;
        private readonly ServiceRepository serviceRepository;
        private readonly ApplicationContext context;
        public BanksViewModel()
        {
            bankRepository = new BankRepository();
            serviceRepository = new ServiceRepository();
            context = new ApplicationContext();
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
                Locality = this.Locality,
                Services = new List<Service>()
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
            var newBank=context.Banks.Add(bank);
            context.SaveChanges();
            if (newBank == null) return;
            Banks.Add(newBank);
        }
        private void RemoveBank()
        {
            if (SeleckBank == null) return;
            var deleteBank = bankRepository.Delete(SeleckBank);
            if (deleteBank == null) return;
            Banks.Remove(SeleckBank);
        }
        private void SetBanks()
        {
            if (Banks == null)
                Banks = new ObservableCollection<Bank>();
            Banks.Clear();
            foreach (var item in bankRepository.GetAll())
            {
                Banks.Add(item);
            }
        }
        private void SetServices()
        {
            if (Services == null)
                Services = new ObservableCollection<SelectedViewModel>();
            Services.Clear();
            foreach (var item in serviceRepository.GetAll())
            {
                Services.Add(new SelectedViewModel { Name = item.Name, IsSelect = false });
            }
        }

        public void UpdateBinding()
        {
            SetBanks();
            SetServices();
        }
        #endregion
    }
}
