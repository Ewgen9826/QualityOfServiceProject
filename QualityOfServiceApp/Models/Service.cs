using System.Collections;
using System.Collections.Generic;

namespace QualityOfServiceApp.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Bank> Banks { get; set; }
        public ICollection<CriteriaEvaluation> CriteriaEvaluations { get; set; }
        public Service()
        {
            Banks = new List<Bank>();
            CriteriaEvaluations = new List<CriteriaEvaluation>();
        }

    }
}
