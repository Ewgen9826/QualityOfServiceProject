using System.Collections;
using System.Collections.Generic;

namespace QualityOfServiceApp.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int BankId { get; set; }
        public Bank Bank { get; set; }

        public ICollection<CriteriaEvaluation> CriteriaEvaluations { get; set; }
        public Service()
        {
            CriteriaEvaluations = new List<CriteriaEvaluation>();
        }

    }
}
