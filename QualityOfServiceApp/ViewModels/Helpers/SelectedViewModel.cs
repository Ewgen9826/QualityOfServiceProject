using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualityOfServiceApp.ViewModels
{
    public class SelectedViewModel: BaseViewModel
    {
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

        private bool isSelect;
        public bool IsSelect
        {
            get => isSelect;
            set
            {
                isSelect = value;
                OnPropertyChanged("IsSelect");
            }
        }
    }
}
