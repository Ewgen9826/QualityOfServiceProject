using QualityOfServiceApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualityOfServiceApp.Repository
{
    class ServiceRepository : IRepository<Service>
    {
        private readonly ApplicationContext context;

        public ServiceRepository()
        {
            context = new ApplicationContext();
        }
        public Service Add(Service item)
        {
           var service= context.Services.Add(item);
            return service;
        }

        public void Delete(Service item)
        {
            context.Services.Remove(item);
        }

        public IEnumerable<Service> GetAll()
        {
            var services = context.Services.Include(b => b.Banks).ToList();
            return services;
        }

        public bool SaveAll()
        {
            return context.SaveChanges() > 0;
        }
    }
}
