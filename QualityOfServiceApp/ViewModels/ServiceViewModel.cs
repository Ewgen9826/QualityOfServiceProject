using QualityOfServiceApp.Helpers;
using QualityOfServiceApp.Models;
using QualityOfServiceApp.Repository;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace QualityOfServiceApp.ViewModels
{
    public class ServiceViewModel: BaseViewModel, IPageViewModel
    {
        #region Service Repository initial
        private readonly ServiceRepository repository;
        public ServiceViewModel()
        {
            repository = new ServiceRepository();
        }
        #endregion

        #region Commands
        private ICommand goToAdminPageCommand;
        public ICommand GoToAdminPageCommand => goToAdminPageCommand ?? (goToAdminPageCommand = new RelayCommand(x =>
        {
            Mediator.Notify("GoToAdminPage", null);
        }));

        private ICommand addServiceCommand;
        public ICommand AddServiceCommand => addServiceCommand ?? (addServiceCommand = new RelayCommand(x =>
        {
            AddService();
        }));

        private ICommand removeServiceCommand;
        public ICommand RemoveServiceCommand => removeServiceCommand ?? (removeServiceCommand = new RelayCommand(x =>
        {
            RemoveService();
        }));


        #endregion

        #region Notify Propertis
        public ObservableCollection<Service> Services { get; set; }
        private Service selectService;
        public Service SelectService
        {
            get => selectService;
            set
            {
                selectService = value;
                OnPropertyChanged("SelectService");
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
        #endregion

        #region Private Methods
        private void AddService()
        {
            Service service = new Service
            {
                Name = this.Name
            };
            repository.Add(service);
            Services.Add(service);
        }
        private void RemoveService()
        {
            if (SelectService == null) return;
            repository.Delete(SelectService);
            Services.Remove(SelectService);
        }
        private void SetServices()
        {
            if (Services == null)
                Services = new ObservableCollection<Service>();
            Services.Clear();
            foreach (var item in repository.GetAll())
            {
                Services.Add(item);
            }
        }

        public void UpdateBinding()
        {
            SetServices();
        }
        #endregion

    }
}
