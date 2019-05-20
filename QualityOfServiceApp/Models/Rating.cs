using System.Collections.Generic;

namespace QualityOfServiceApp.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public double Expectation { get; set; }
        public double Perception { get; set; }
        public double Significance { get; set; }

        public int CriteriaEvaluationId { get; set; }
        public CriteriaEvaluation CriteriaEvaluation { get; set; }

        public Bank Bank { get; set; }
        public int BankId { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }

        public Service Service { get; set; }
        public int ServiceId { get; set; }
    }
}
