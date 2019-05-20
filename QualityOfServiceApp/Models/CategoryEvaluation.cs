using System.Collections.Generic;

namespace QualityOfServiceApp.Models
{
    public class CategoryEvaluation
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<CriteriaEvaluation> CriteriaEvaluations { get; set; }

        public ICollection<Service> Services { get; set; }

        public CategoryEvaluation()
        {
            CriteriaEvaluations = new List<CriteriaEvaluation>();
            Services = new List<Service>();
        }
    }
}
