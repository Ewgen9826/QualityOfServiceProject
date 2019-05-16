using System.Collections.Generic;

namespace QualityOfServiceApp.Models
{
    public class CriteriaEvaluation
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Rating> Ratings { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }

        public CriteriaEvaluation()
        {
            Ratings = new List<Rating>();
        }
    }
}
