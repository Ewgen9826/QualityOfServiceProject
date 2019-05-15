using QualityOfServiceApp.Helpers;
using QualityOfServiceApp.Models;
using QualityOfServiceApp.Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;

namespace QualityOfServiceApp.ViewModels
{
    public class QuestionnaireViewModel : BaseViewModel, IPageViewModel
    {
        #region Repository init
        private readonly BankRepository bankRepository;
        private readonly ServiceRepository serviceRepository;
        private readonly DataRepository dataRepository;
        private readonly ApplicationContext context;
        public QuestionnaireViewModel()
        {
            context = new ApplicationContext();
            bankRepository = new BankRepository();
            serviceRepository = new ServiceRepository();
            dataRepository = new DataRepository();
            Banks = new ObservableCollection<Bank>();
            Services = new ObservableCollection<Service>();
            CriteriaEvaluations = new ObservableCollection<SelectRatingViewModel>();
            foreach (var item in bankRepository.GetAll())
            {
                Banks.Add(item);
            }
            foreach (var item in dataRepository.GetCriteriaEvaluations())
            {
                CriteriaEvaluations.Add(new SelectRatingViewModel { Name = item.Name, Expectation=1, Perception=2 });
            }
        }
        #endregion

        #region Commands
        private ICommand goToHomeCommand;
        public ICommand GoToHomeCommand => goToHomeCommand ?? (goToHomeCommand = new RelayCommand(x =>
        {
            Mediator.Notify("GoToHomePage", null);
        }));

        private ICommand completedCommand;
        public ICommand CompletedCommand => completedCommand ?? (completedCommand = new RelayCommand(x =>
        {
            CompletedQuestionnary();
        }));
        #endregion

        #region Notify Properti
        public ObservableCollection<Bank> Banks { get; set; }
        public ObservableCollection<Service> Services { get; set; }
        public ObservableCollection<SelectRatingViewModel> CriteriaEvaluations { get; set; }

        private Bank selectBank;
        public Bank SelectBank
        {
            get => selectBank;
            set
            {
                selectBank = value;
                SetService();
                OnPropertyChanged("SelectBank");
            }
        }

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

        private string gender;
        public string Gender
        {
            get => gender;
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }

        private string age;
        public string Age
        {
            get => age;
            set
            {
                age = value;
                OnPropertyChanged("Age");
            }
        }

        private string education;
        public string Education
        {
            get => education;
            set
            {
                education = value;
                OnPropertyChanged("Education");
            }
        }

        private string socialGroup;
        public string SocialGroup
        {
            get => socialGroup;
            set
            {
                socialGroup = value;
                OnPropertyChanged("SocialGroup");
            }
        }

        #endregion

        private void SetService()
        {
            Services.Clear();
            foreach (var item in SelectBank.Services)
            {
                Services.Add(item);
            }
        }
        private void CompletedQuestionnary()
        {
            foreach (var item in CriteriaEvaluations)
            {
                var criteria = context.CriteriaEvaluations.Include(r => r.Ratings).FirstOrDefault(c => c.Name == item.Name);
                if (criteria == null) return;
                criteria.Ratings.Add(new Rating { Expectation = item.Expectation, Perception = item.Perception });
            }
            var bank = context.Banks.Include(c => c.Clients).FirstOrDefault(b => b.Name == SelectBank.Name);
            if (bank == null) return;
            var client = new Client
            {
                Gender = this.Gender,
                Age = this.Age,
                Education = this.Education,
                SocialGroup = this.SocialGroup
            };
            bank.Clients.Add(client);
            context.SaveChanges();
        }
    }
}
