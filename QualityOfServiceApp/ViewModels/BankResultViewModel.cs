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
    public class BankResultViewModel : BaseViewModel, IPageViewModel
    {
        private readonly BankRepository bankRepository;
        private readonly ApplicationContext context;

        public BankResultViewModel()
        {
            bankRepository = new BankRepository();
            context = new ApplicationContext();
        }

        private ICommand goToAdminPageCommand;
        public ICommand GoToAdminPageCommand => goToAdminPageCommand ?? (goToAdminPageCommand = new RelayCommand(x =>
        {
            Mediator.Notify("GoToAdminPage", null);
        }));

        public ObservableCollection<Bank> Banks { get; set; }
        public ObservableCollection<Service> Services { get; set; }
        public ObservableCollection<CategoryEvaluation> Categorys { get; set; }
        public ObservableCollection<CriteriaEvaluation> Criaterials { get; set; }

        private Bank bankSelect;
        public Bank BankSelect
        {
            get => bankSelect;
            set
            {
                bankSelect = value;
                SetService();
                GetIndicatorsBank();
                ServiceSelect = null;
                CategorySelect = null;
                CriaterialSelect = null;
                OnPropertyChanged("BankSelect");
            }
        }

        private Service servicekSelect;
        public Service ServiceSelect
        {
            get => servicekSelect;
            set
            {
                servicekSelect = value;
                GetIndicatorsService();
                CategorySelect = null;
                CriaterialSelect = null;
                OnPropertyChanged("ServiceSelect");
            }
        }

        private CategoryEvaluation categorySelect;
        public CategoryEvaluation CategorySelect
        {
            get => categorySelect;
            set
            {
                categorySelect = value;
                GetInidcatorsCategory();
                CriaterialSelect = null;
                SetCriterials();
                OnPropertyChanged("CategorySelect");
            }
        }

        public CriteriaEvaluation criterialSelect;
        public CriteriaEvaluation CriaterialSelect
        {
            get => criterialSelect;
            set
            {
                criterialSelect = value;
                if (criterialSelect != null)
                {
                    GetIndicatorsCriterial();
                }
                OnPropertyChanged("CriaterialSelect");
            }
        }

        private double expectation;
        public double Expectation
        {
            get => expectation;
            set
            {
                expectation = value;
                OnPropertyChanged("Expectation");
            }
        }

        private double perception;
        public double Perception
        {
            get => perception;
            set
            {
                perception = value;
                OnPropertyChanged("Perception");
            }
        }

        private double significance;
        public double Significance
        {
            get => significance;
            set
            {
                significance = value;
                OnPropertyChanged("Significance");
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
        private void SetService()
        {
            if (Services == null)
                Services = new ObservableCollection<Service>();
            Services.Clear();
            foreach (var item in BankSelect.Services)
            {
                Services.Add(item);
            }
        }
        private void SetCategorys()
        {
            if (Categorys == null)
                Categorys = new ObservableCollection<CategoryEvaluation>();
            Categorys.Clear();
            foreach (var item in context.CategoryEvaluations.Include(c => c.CriteriaEvaluations).ToList())
            {
                Categorys.Add(item);
            }
        }
        private void SetCriterials()
        {
            if (Criaterials == null)
                Criaterials = new ObservableCollection<CriteriaEvaluation>();
            Criaterials.Clear();
            if (CategorySelect == null) return;
            foreach (var item in CategorySelect.CriteriaEvaluations.ToList())
            {
                Criaterials.Add(item);
            }
        }

        private void GetIndicatorsBank()
        {
            var ratings = context.Ratings.Include(b => b.Bank)
                .Where(b => b.BankId == BankSelect.Id).ToList();
            if (ratings.Count() == 0)
            {
                Expectation = 0;
                Perception = 0;
                Significance = 0;
                return;

            }
            Expectation = ratings.Average(r => r.Expectation);
            Perception = ratings.Average(r => r.Perception);
            Significance = ratings.Average(r => r.Significance);
        }
        private void GetIndicatorsService()
        {
            context.Ratings.Load();
            if (ServiceSelect == null) return;
            var ratings = context.Ratings
                .Where(s => s.BankId == BankSelect.Id)
                .Where(s => s.ServiceId == ServiceSelect.Id).ToList();
            if (ratings.Count() == 0)
            {
                Expectation = 0;
                Perception = 0;
                Significance = 0;
                return;
            }
            Expectation = ratings.Average(r => r.Expectation);
            Perception = ratings.Average(r => r.Perception);
            Significance = ratings.Average(r => r.Significance);

        }
        private void GetInidcatorsCategory()
        {
            context.Ratings.Load();
            if (ServiceSelect == null || CategorySelect==null) return;
            var ratings = context.Ratings
                .Where(s => s.BankId == BankSelect.Id)
                .Where(s => s.ServiceId == ServiceSelect.Id)
                .Where(s=>s.CriteriaEvaluation.CategoryEvaluationId==CategorySelect.Id)
                .ToList();
            if (ratings.Count() == 0)
            {
                Expectation = 0;
                Perception = 0;
                Significance = 0;
                return;
            }
            Expectation = ratings.Average(r => r.Expectation);
            Perception = ratings.Average(r => r.Perception);
            Significance = ratings.Average(r => r.Significance);

        }
        private void GetIndicatorsCriterial()
        {
            context.Ratings.Load();
            var ratings = context.Ratings
                .Where(r => r.BankId == BankSelect.Id && r.ServiceId == ServiceSelect.Id)
                .Where(r => r.CriteriaEvaluationId == CriaterialSelect.Id).ToList();
            if (ratings.Count() == 0)
            {
                Expectation = 0;
                Perception = 0;
                Significance = 0;
                return;
            }
            Expectation = ratings.Average(r => r.Expectation);
            Perception = ratings.Average(r => r.Perception);
            Significance = ratings.Average(r => r.Significance);
        }

        public void UpdateBinding()
        {
            SetBanks();
            BankSelect = Banks.FirstOrDefault();
            SetCategorys();
        }
    }
}
