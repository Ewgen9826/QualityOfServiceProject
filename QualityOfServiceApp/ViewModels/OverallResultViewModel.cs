using QualityOfServiceApp.Helpers;
using QualityOfServiceApp.Models;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;

namespace QualityOfServiceApp.ViewModels
{
    class OverallResultViewModel : BaseViewModel, IPageViewModel
    {
        private ApplicationContext context;
        private ICommand goToAdminPageCommand;
        public ICommand GoToAdminPageCommand => goToAdminPageCommand ?? (goToAdminPageCommand = new RelayCommand(x =>
        {
            Mediator.Notify("GoToAdminPage", null);
        }));

        #region Materiality
        private double m_expectation;
        public double M_Expectation
        {
            get => m_expectation;
            set
            {
                m_expectation = value;
                OnPropertyChanged("M_Expectation");
            }
        }

        private double m_perception;
        public double M_Perception
        {
            get => m_perception;
            set
            {
                m_perception = value;
                OnPropertyChanged("M_Perception");
            }
        }

        private double m_significance;
        public double M_Significance
        {
            get => m_significance;
            set
            {
                m_significance = value;
                OnPropertyChanged("M_Significance");
            }
        }

        public ObservableCollection<CriteriaEvaluation> Materialities { get; set; }

        private CriteriaEvaluation m_selectItem;
        public CriteriaEvaluation M_SelectItem
        {
            get => m_selectItem;
            set
            {
                m_selectItem = value;
                GetIndicatorsForMateriality();
                OnPropertyChanged("M_SelectItem");
            }
        }
        private void SetMaterialities()
        {
            if (Materialities == null)
                Materialities = new ObservableCollection<CriteriaEvaluation>();
            Materialities.Clear();
            using (context = new ApplicationContext())
            {
                foreach (var criterial in context.CriteriaEvaluations.Include(c=>c.CategoryEvaluation)
                    .Where(c=>c.CategoryEvaluation.Name=="Материальность").ToList())
                {
                    Materialities.Add(criterial);
                }
            }
            Materialities.Add(new CriteriaEvaluation { Name = "Общие показатели" });
        }
        private void GetIndicatorsForMateriality()
        {
            if (M_SelectItem == null) return;
            using (context = new ApplicationContext())
            {
                IEnumerable<Rating> ratings;
                if (M_SelectItem.Name == "Общие показатели")
                {
                    context.Ratings.Load();
                    context.CategoryEvaluations.Load();
                    context.CriteriaEvaluations.Load();
                    ratings = context.Ratings
                        .Where(r => r.CriteriaEvaluation.CategoryEvaluation.Name == "Материальность").ToList();
                }
                else
                {
                    ratings = context.Ratings
                                .Where(r => r.CriteriaEvaluationId == M_SelectItem.Id);

                }
                if (ratings.Count() == 0)
                {
                    M_Expectation = 0;
                    M_Perception = 0;
                    M_Significance = 0;
                }
                M_Expectation = ratings.Average(r => r.Expectation);
                M_Perception = ratings.Average(r => r.Perception);
                M_Significance = ratings.Average(r => r.Significance);
            }
        }
        #endregion

        #region Reliabilities
        private double r_expectation;
        public double R_Expectation
        {
            get => r_expectation;
            set
            {
                r_expectation = value;
                OnPropertyChanged("R_Expectation");
            }
        }

        private double r_perception;
        public double R_Perception
        {
            get => r_perception;
            set
            {
                r_perception = value;
                OnPropertyChanged("R_Perception");
            }
        }

        private double r_significance;
        public double R_Significance
        {
            get => r_significance;
            set
            {
                r_significance = value;
                OnPropertyChanged("R_Significance");
            }
        }

        public ObservableCollection<CriteriaEvaluation> Reliabilities { get; set; }

        private CriteriaEvaluation r_selectItem;
        public CriteriaEvaluation R_SelectItem
        {
            get => r_selectItem;
            set
            {
                r_selectItem = value;
                GetIndicatorsForReliability();
                OnPropertyChanged("R_SelectItem");
            }
        }
        private void SetReliabilities()
        {
            if (Reliabilities == null)
                Reliabilities = new ObservableCollection<CriteriaEvaluation>();
            Reliabilities.Clear();
            using (context = new ApplicationContext())
            {
                foreach (var criterial in context.CriteriaEvaluations.Include(c=>c.CategoryEvaluation)
                    .Where(c=>c.CategoryEvaluation.Name=="Надежность").ToList())
                {
                    Reliabilities.Add(criterial);
                }
            }
            Reliabilities.Add(new CriteriaEvaluation { Name = "Общие показатели" });
        }
        private void GetIndicatorsForReliability()
        {
            if (R_SelectItem == null) return;
            using (context = new ApplicationContext())
            {
                IEnumerable<Rating> ratings;
                if (R_SelectItem.Name == "Общие показатели")
                {
                    ratings = context.Ratings.ToList();
                }
                else
                {
                    context.Ratings.Load();
                    context.CategoryEvaluations.Load();
                    context.CriteriaEvaluations.Load();
                    ratings = context.Ratings
                        .Where(r => r.CriteriaEvaluation.CategoryEvaluation.Name == "Надежность").ToList();

                }
                if (ratings.Count() == 0)
                {
                    R_Expectation = 0;
                    R_Perception = 0;
                    R_Significance = 0;
                }
                R_Expectation = ratings.Average(r => r.Expectation);
                R_Perception = ratings.Average(r => r.Perception);
                R_Significance = ratings.Average(r => r.Significance);
            }
        }
        #endregion

        #region Responsiveness
        private double res_expectation;
        public double Res_Expectation
        {
            get => res_expectation;
            set
            {
                res_expectation = value;
                OnPropertyChanged("Res_Expectation");
            }
        }

        private double res_perception;
        public double Res_Perception
        {
            get => res_perception;
            set
            {
                res_perception = value;
                OnPropertyChanged("Res_Perception");
            }
        }

        private double res_significance;
        public double Res_Significance
        {
            get => res_significance;
            set
            {
                res_significance = value;
                OnPropertyChanged("Res_Significance");
            }
        }

        public ObservableCollection<CriteriaEvaluation> Responsiveness { get; set; }

        private CriteriaEvaluation res_selectItem;
        public CriteriaEvaluation Res_SelectItem
        {
            get => res_selectItem;
            set
            {
                res_selectItem = value;
                GetIndicatorsForResponsiveness();
                OnPropertyChanged("Res_SelectItem");
            }
        }
        private void SetResponsiveness()
        {
            if (Responsiveness == null)
                Responsiveness = new ObservableCollection<CriteriaEvaluation>();
            Responsiveness.Clear();
            using (context = new ApplicationContext())
            {
                foreach (var criterial in context.CriteriaEvaluations.Include(c => c.CategoryEvaluation)
                    .Where(c => c.CategoryEvaluation.Name == "Отзывчивость").ToList())
                {
                    Responsiveness.Add(criterial);
                }
            }
            Responsiveness.Add(new CriteriaEvaluation { Name = "Общие показатели" });
        }
        private void GetIndicatorsForResponsiveness()
        {
            if (Res_SelectItem == null) return;
            using (context = new ApplicationContext())
            {
                IEnumerable<Rating> ratings;
                if (Res_SelectItem.Name == "Общие показатели")
                {
                    ratings = context.Ratings.ToList();
                }
                else
                {
                    context.Ratings.Load();
                    context.CategoryEvaluations.Load();
                    context.CriteriaEvaluations.Load();
                    ratings = context.Ratings
                        .Where(r => r.CriteriaEvaluation.CategoryEvaluation.Name == "Отзывчивость").ToList();

                }
                if (ratings.Count() == 0)
                {
                    Res_Expectation = 0;
                    Res_Perception = 0;
                    Res_Significance = 0;
                }
                Res_Expectation = ratings.Average(r => r.Expectation);
                Res_Perception = ratings.Average(r => r.Perception);
                Res_Significance = ratings.Average(r => r.Significance);
            }
        }
        #endregion

        #region Conviction
        private double c_expectation;
        public double C_Expectation
        {
            get => c_expectation;
            set
            {
                c_expectation = value;
                OnPropertyChanged("C_Expectation");
            }
        }

        private double c_perception;
        public double C_Perception
        {
            get => c_perception;
            set
            {
                c_perception = value;
                OnPropertyChanged("C_Perception");
            }
        }

        private double c_significance;
        public double C_Significance
        {
            get => c_significance;
            set
            {
                c_significance = value;
                OnPropertyChanged("C_Significance");
            }
        }

        public ObservableCollection<CriteriaEvaluation> Convictions { get; set; }

        private CriteriaEvaluation c_selectItem;
        public CriteriaEvaluation C_SelectItem
        {
            get => c_selectItem;
            set
            {
                c_selectItem = value;
                GetIndicatorsForConviction();
                OnPropertyChanged("C_SelectItem");
            }
        }
        private void SetConvictions()
        {
            if (Convictions == null)
                Convictions = new ObservableCollection<CriteriaEvaluation>();
            Convictions.Clear();
            using (context = new ApplicationContext())
            {
                foreach (var criterial in context.CriteriaEvaluations.Include(c => c.CategoryEvaluation)
                    .Where(c => c.CategoryEvaluation.Name == "Убежденность").ToList())
                {
                    Convictions.Add(criterial);
                }
            }
            Convictions.Add(new CriteriaEvaluation { Name = "Общие показатели" });
        }
        private void GetIndicatorsForConviction()
        {
            if (C_SelectItem == null) return;
            using (context = new ApplicationContext())
            {
                IEnumerable<Rating> ratings;
                if (C_SelectItem.Name == "Общие показатели")
                {
                    context.Ratings.Load();
                    context.CategoryEvaluations.Load();
                    context.CriteriaEvaluations.Load();
                    ratings = context.Ratings
                        .Where(r => r.CriteriaEvaluation.CategoryEvaluation.Name == "Убежденность").ToList();
                }
                else
                {
                    ratings = context.Ratings
                                .Where(r => r.CriteriaEvaluationId == C_SelectItem.Id);

                }
                if (ratings.Count() == 0)
                {
                    C_Expectation = 0;
                    C_Perception = 0;
                    C_Significance = 0;
                }
                C_Expectation = ratings.Average(r => r.Expectation);
                C_Perception = ratings.Average(r => r.Perception);
                C_Significance = ratings.Average(r => r.Significance);
            }
        }
        #endregion

        #region Sympathy
        private double s_expectation;
        public double S_Expectation
        {
            get => s_expectation;
            set
            {
                s_expectation = value;
                OnPropertyChanged("S_Expectation");
            }
        }

        private double s_perception;
        public double S_Perception
        {
            get => s_perception;
            set
            {
                s_perception = value;
                OnPropertyChanged("S_Perception");
            }
        }

        private double s_significance;
        public double S_Significance
        {
            get => s_significance;
            set
            {
                s_significance = value;
                OnPropertyChanged("S_Significance");
            }
        }

        public ObservableCollection<CriteriaEvaluation> Sympathies { get; set; }

        private CriteriaEvaluation s_selectItem;
        public CriteriaEvaluation S_SelectItem
        {
            get => s_selectItem;
            set
            {
                s_selectItem = value;
                GetIndicatorsForSympathy();
                OnPropertyChanged("S_SelectItem");
            }
        }
        private void SetSympathies()
        {
            if (Sympathies == null)
                Sympathies = new ObservableCollection<CriteriaEvaluation>();
            Sympathies.Clear();
            using (context = new ApplicationContext())
            {
                foreach (var criterial in context.CriteriaEvaluations.Include(c => c.CategoryEvaluation)
                    .Where(c => c.CategoryEvaluation.Name == "Сочувствие").ToList())
                {
                    Sympathies.Add(criterial);
                }
            }
            Sympathies.Add(new CriteriaEvaluation { Name = "Общие показатели" });
        }
        private void GetIndicatorsForSympathy()
        {
            if (S_SelectItem == null) return;
            using (context = new ApplicationContext())
            {
                IEnumerable<Rating> ratings;
                if (S_SelectItem.Name == "Общие показатели")
                {
                    ratings = context.Ratings.ToList();
                }
                else
                {
                    context.Ratings.Load();
                    context.CategoryEvaluations.Load();
                    context.CriteriaEvaluations.Load();
                    ratings = context.Ratings
                        .Where(r => r.CriteriaEvaluation.CategoryEvaluation.Name == "Сочувствие").ToList();

                }
                if (ratings.Count() == 0)
                {
                    S_Expectation = 0;
                    S_Perception = 0;
                    S_Significance = 0;
                }
                S_Expectation = ratings.Average(r => r.Expectation);
                S_Perception = ratings.Average(r => r.Perception);
                S_Significance = ratings.Average(r => r.Significance);
            }
        }
        #endregion

        private double overallExpectation;
        public double OverallExpectation
        {
            get => overallExpectation;
            set
            {
                overallExpectation = value;
                OnPropertyChanged("OverallExpectation");
            }
        }

        private double overallPerception;
        public double OverallPerception
        {
            get => overallPerception;
            set
            {
                overallPerception = value;
                OnPropertyChanged("OverallPerception");
            }
        }

        private double overallSignificance;
        public double OverallSignificance
        {
            get => overallSignificance;
            set
            {
                overallSignificance = value;
                OnPropertyChanged("OverallSignificance");
            }
        }

        private double qualityFactor;
        public double QualityFactor
        {
            get => qualityFactor;
            set
            {
                qualityFactor = value;
                OnPropertyChanged("QualityFactor");
            }
        }

        private string resultMessage;
        public string ResultMessage
        {
            get => resultMessage;
            set
            {
                resultMessage = value;
                OnPropertyChanged("ResultMessage");
            }
        }

        private void GetOverallIndicators()
        {
            using (context=new ApplicationContext ())
            {
                var ratings = context.Ratings.ToList();
                if (ratings.Count() == 0) return;
                OverallExpectation = ratings.Average(r => r.Expectation);
                overallPerception = ratings.Average(r => r.Perception);
                OverallSignificance = ratings.Average(r => r.Significance);
                QualityFactor = OverallPerception - OverallExpectation;
                SetResutlMessage(QualityFactor);
            }
        }

        public void SetResutlMessage(double qualityFactor)
        {
            if (qualityFactor == 0)
            {
                ResultMessage = "Уровень восприятия и ожидания совпадает. Успешный результат.Это свидетельствует о хорошем качестве оказываемых услуг, что будет способствовать сохранению и привлечению новых клиентов";
            }
            if (qualityFactor > 0)
            {
                ResultMessage = "Уровень восприятия выше ожидания. Успешный результат. Это свидетельствует о высоком качестве оказываемых услуг, что будет способствовать сохранению и привлечению новых клиентов";
            }
            if (qualityFactor >= -1 && qualityFactor < 0)
            {
                ResultMessage = "Уровень восприятия ниже ожидания. Удовлетворительный результат. Это свидетельствует о удовлетворительном качестве оказываемых услуг, клиенты не до конца довольны качеством обслуживания, однако это не критично";
            }
            if (qualityFactor < -1)
            {
                ResultMessage = "Уровень восприятия ниже ожидания. Неудовлетворительный результат. Это свидетельствует о низком качестве оказываемых услуг, это скажется на количестве клиентов, требуется повышение качества обслуживания";
            }
        }

        public void UpdateBinding()
        {
            SetMaterialities();
            SetReliabilities();
            SetResponsiveness();
            SetSympathies();
            SetConvictions();

            M_SelectItem = Materialities.LastOrDefault();
            R_SelectItem = Reliabilities.LastOrDefault();
            Res_SelectItem = Reliabilities.LastOrDefault();
            S_SelectItem = Sympathies.LastOrDefault();
            C_SelectItem = Convictions.LastOrDefault();

            GetIndicatorsForMateriality();
            GetIndicatorsForReliability();
            GetIndicatorsForResponsiveness();
            GetIndicatorsForSympathy();
            GetIndicatorsForConviction();

            GetOverallIndicators();

        }
    }
}
