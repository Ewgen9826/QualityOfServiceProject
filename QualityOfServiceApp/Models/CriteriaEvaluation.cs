using System.Collections.Generic;

namespace QualityOfServiceApp.Models
{
    public class CriteriaEvaluation
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CategoryEvaluationId { get; set; }
        public CategoryEvaluation CategoryEvaluation { get; set; }

        public ICollection<Rating> Ratings { get; set; }
        
        public CriteriaEvaluation()
        {
            Ratings = new List<Rating>();
        }
    }
}
