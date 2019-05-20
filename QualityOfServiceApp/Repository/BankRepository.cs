using QualityOfServiceApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualityOfServiceApp.Repository
{
    public class BankRepository: IRepository<Bank>
    {
        private ApplicationContext context;
        public Bank Add(Bank item)
        {
            Bank bank = null;
            using (context=new ApplicationContext ())
            {
                 bank = context.Banks.Add(item);
                context.SaveChanges();

            }
            return bank;
        }

        public Bank Delete(Bank item)
        {
            Bank deleteBank = null;
            using (context = new ApplicationContext())
            {
                context.Banks.Attach(item);
                deleteBank = context.Banks.Remove(item);
                context.SaveChanges();
            }
            return deleteBank;
        }

        public  IEnumerable<Bank> GetAll()
        {
            IEnumerable<Bank> banks = null;
            using (context=new ApplicationContext ())
            {
                banks = context.Banks.Include(s=>s.Services).ToList();
            }
            return banks;
        }

        public IEnumerable<Bank> GetFillBanks()
        {
            IEnumerable<Bank> banks=null;
            using (context=new ApplicationContext ())
            {
                banks = context.Banks.ToList();
                foreach (var bank in banks)
                {
                    context.Entry(bank).Collection(s => s.Services).Load();
                    foreach (var service in bank.Services)
                    {
                        context.Entry(service).Collection(c => c.CategoryEvaluations).Load();
                        foreach (var category in service.CategoryEvaluations)
                        {
                            context.Entry(category).Collection(c => c.CriteriaEvaluations).Load();
                            foreach (var criterial in category.CriteriaEvaluations)
                            {
                                context.Entry(criterial).Collection(r => r.Ratings).Load();
                            }
                        }
                    }
                }
            }
            return banks;
        }
    }
}
