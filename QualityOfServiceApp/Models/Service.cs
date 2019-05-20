using System.Collections;
using System.Collections.Generic;

namespace QualityOfServiceApp.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public  ICollection<Bank> Banks { get; set; }
        public  ICollection<CategoryEvaluation> CategoryEvaluations { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public Service()
        {
            Ratings = new List<Rating>();
            Banks = new List<Bank>();
            CategoryEvaluations = new List<CategoryEvaluation>();
        }

    }
}
