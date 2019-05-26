using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualityOfServiceApp.Helpers
{
    public class SaveToExcelModel
    {
        public double OverallExpectation { get; set; }
        public double OverallPerception { get; set; }
        public double OverallSignificance { get; set; }
        public double QualityFactor { get; set; }
        public string ResultMessage { get; set; }
        public string ResultSignificanceMessage { get; set; }
    }
}
