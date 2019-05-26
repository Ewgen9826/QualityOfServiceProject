using Microsoft.Win32;
using QualityOfServiceApp.Helpers;
using QualityOfServiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace QualityOfServiceApp.ViewModels
{
    class ReportViewModel : BaseViewModel, IPageViewModel
    {
        private ICommand goToBankResultPageCommand;
        public ICommand GoToBankResultPageCommand => goToBankResultPageCommand ?? (goToBankResultPageCommand = new RelayCommand(x =>
        {
            Mediator.Notify("GoToOverallResultPage", null);
        }));
        private ICommand saveToExcelCommand;
        public ICommand SaveToExcelCommand => saveToExcelCommand ?? (saveToExcelCommand = new RelayCommand(x =>
        {
            SaveToExcel();
        }));
        private ApplicationContext context;
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
        private string resultSignificanceMessage;
        public string ResultSignificanceMessage
        {
            get => resultSignificanceMessage;
            set
            {
                resultSignificanceMessage = value;
                OnPropertyChanged("ResultSignificanceMessage");
            }
        }

        private void GetOverallIndicators()
        {
            using (context = new ApplicationContext())
            {
                var ratings = context.Ratings.ToList();
                if (ratings.Count() == 0) return;
                OverallExpectation = ratings.Average(r => r.Expectation);
                overallPerception = ratings.Average(r => r.Perception);
                OverallSignificance = ratings.Average(r => r.Significance);
                QualityFactor = OverallPerception - OverallExpectation;
                SetResutlMessage(QualityFactor);
                SetResultSignificanceMessage();
            }
        }

        private void SetResutlMessage(double qualityFactor)
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
        private void SetResultSignificanceMessage()
        {
            using (context = new ApplicationContext())
            {
                var maxValue = context.Ratings.Max(r => r.Significance);
                if (maxValue == 0) return;
                var significances = context.Ratings.Where(r => r.Significance == maxValue).ToList();
                var categories = new HashSet<CategoryEvaluation>();
                foreach (var significance in significances)
                {
                    context.Entry(significance).Reference(r => r.CriteriaEvaluation).Load();
                    context.Entry(significance.CriteriaEvaluation).Reference(c => c.CategoryEvaluation).Load();
                    categories.Add(significance.CriteriaEvaluation.CategoryEvaluation);
                }
                var message = "";
                foreach (var category in categories)
                {
                    message += category.Name + ", ";
                }
                ResultSignificanceMessage = $"Наиболее значимым для экспертов являются критерии группы: {message}";
            }
        }

        private void SaveToExcel()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                var model = new SaveToExcelModel
                {
                    OverallExpectation = this.OverallExpectation,
                    OverallPerception = this.OverallPerception,
                    OverallSignificance = this.OverallSignificance,
                    QualityFactor = this.QualityFactor,
                    ResultMessage = this.ResultMessage,
                    ResultSignificanceMessage = this.ResultSignificanceMessage
                };
                ExcelExportHelper.Export(saveFileDialog.FileName, model);
            }
        }
        public void UpdateBinding()
        {
            GetOverallIndicators();

        }
    }
}
