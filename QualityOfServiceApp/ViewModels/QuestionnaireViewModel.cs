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
        private ApplicationContext context;
        public QuestionnaireViewModel()
        {
            context = new ApplicationContext();
            bankRepository = new BankRepository();
            serviceRepository = new ServiceRepository();
            dataRepository = new DataRepository();
            Services = new ObservableCollection<Service>();


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

        public ObservableCollection<SelectRatingViewModel> Criterials { get; set; }


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
            if (Services == null)
                Services = new ObservableCollection<Service>();
            Services.Clear();
            foreach (var item in SelectBank.Services)
            {
                Services.Add(item);
            }
        }
        private void SetBanks()
        {
            if (Banks == null)
                Banks = new ObservableCollection<Bank>();
            Banks.Clear();
            foreach (var item in bankRepository.GetFillBanks())
            {
                Banks.Add(item);
            }
        }

        private void SetCriterials()
        {
            if (Criterials == null)
                Criterials = new ObservableCollection<SelectRatingViewModel>();
            for (int i = 0; i < context.CriteriaEvaluations.Count(); i++)
            {
                Criterials.Add(new SelectRatingViewModel());
            }
        }


        private void CompletedQuestionnary()
        {
            using (context=new ApplicationContext ())
            {
                var client = new Client
                {
                    Age = this.Age,
                    Education = this.Education,
                    Gender = this.Gender,
                    SocialGroup = this.SocialGroup
                };
                var bank = context.Banks.FirstOrDefault(b => b.Name == SelectBank.Name);
                context.Entry(bank).Collection(c => c.Clients).Load();
                bank.Clients.Add(client);
                context.Entry(bank).Collection(s => s.Services).Load();
                context.Entry(bank.Services.FirstOrDefault(c => c.Name == SelectService.Name)).Collection(c => c.CategoryEvaluations).Load();
                int j = 0;
                foreach (var category in bank.Services.FirstOrDefault(s=>s.Name==SelectService.Name).CategoryEvaluations)
                {
                    context.Entry(category).Collection(c => c.CriteriaEvaluations).Load();
                    var criterials = category.CriteriaEvaluations;
                    foreach (var c in criterials)
                    {
                        Rating r = new Rating
                        {
                            Expectation = Criterials[j].Expectation,
                            Perception = Criterials[j].Perception,
                            Significance=Criterials[j].Significance
                        };
                        r.Client = client;
                        r.CriteriaEvaluationId = c.Id;
                        r.BankId = bank.Id;
                        r.ServiceId = SelectService.Id;
                        context.Ratings.Add(r);
                        j++;
                    }
                }
                context.SaveChanges();
            }
            Mediator.Notify("GoToHomePage", null);
        }

        public void UpdateBinding()
        {
            SetBanks();
            SetCriterials();
        }
    }
}
