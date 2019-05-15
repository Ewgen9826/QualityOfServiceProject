using System.Collections.Generic;

namespace QualityOfServiceApp.Models
{
    public class CriteriaEvaluation
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Rating> Ratings { get; set; }
        public ICollection<Service> Services { get; set; }
        public CriteriaEvaluation()
        {
            Ratings = new List<Rating>();
            Services = new List<Service>();
        }
    }
}
