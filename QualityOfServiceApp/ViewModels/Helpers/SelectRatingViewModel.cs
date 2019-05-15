using QualityOfServiceApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualityOfServiceApp.ViewModels
{
    public class SelectRatingViewModel:BaseViewModel
    {
        public string Name { get; set; }
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
    }
}
