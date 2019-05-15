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
        private readonly ApplicationContext context;

        public BankRepository()
        {
            this.context = new ApplicationContext();
        }

        public Bank Add(Bank item)
        {
            var bank = context.Banks.Add(item);
            return bank;
        }

        public void Delete(Bank item)
        {
            context.Banks.Remove(item);
        }

        public  IEnumerable<Bank> GetAll()
        {
            var banks =  context.Banks.Include(s => s.Services).ToList();
            return banks;
        }

        public bool SaveAll()
        {
           return context.SaveChanges()>0;
        }
    }
}
