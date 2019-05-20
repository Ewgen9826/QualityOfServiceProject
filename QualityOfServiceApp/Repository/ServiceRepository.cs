using QualityOfServiceApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace QualityOfServiceApp.Repository
{
    class ServiceRepository : IRepository<Service>
    {
        private ApplicationContext context;
        public Service Add(Service item)
        {
            Service service = null;
            using (context=new ApplicationContext ())
            {
                var categorys= context.CategoryEvaluations.ToList();
                foreach (var category in categorys)
                {
                    item.CategoryEvaluations.Add(category);
                }
                service = context.Services.Add(item);
                context.SaveChanges();
            }
            return service;
        }

        public Service Delete(Service item)
        {
            Service deleteService = null;
            using (context = new ApplicationContext())
            {
                context.Services.Attach(item);
                deleteService = context.Services.Remove(item);
                context.SaveChanges();
            }
            return deleteService;
        }

        public IEnumerable<Service> GetAll()
        {
            IEnumerable<Service> services = null;
            using (context=new ApplicationContext())
            {
                services = context.Services.ToList();
            }
            return services;
        }

    }
}
