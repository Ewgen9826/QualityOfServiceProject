﻿using System.Collections.Generic;

namespace QualityOfServiceApp.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public double Expectation { get; set; }
        public double Perception { get; set; }

        public int CriteriaEvaluationId { get; set; }
        public CriteriaEvaluation CriteriaEvaluation { get; set; }

        public ICollection<Bank> Banks { get; set; }
        public Rating()
        {
            Banks = new List<Bank>();
        }
    }
}
