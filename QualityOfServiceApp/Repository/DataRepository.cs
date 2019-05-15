using QualityOfServiceApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualityOfServiceApp.Repository
{
    public class DataRepository
    {
        private readonly ApplicationContext context;

        public DataRepository()
        {
            this.context = new ApplicationContext();
        }

        public List<CriteriaEvaluation> GetCriteriaEvaluations()
        {
            var criteriaEvaluations = context.CriteriaEvaluations.ToList();
            return criteriaEvaluations;
        }
    }
}
