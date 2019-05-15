using System.Data.Entity;

namespace QualityOfServiceApp.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {
            Database.CreateIfNotExists();
        }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<CategoryEvaluation> CategoryEvaluations { get; set; }
        public DbSet<CriteriaEvaluation> CriteriaEvaluations { get; set; }
        public DbSet<Rating> Ratings { get; set; }


    }
}
