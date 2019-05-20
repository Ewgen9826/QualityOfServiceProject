using System.Collections;
using System.Collections.Generic;

namespace QualityOfServiceApp.Models
{
    public class Bank
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string Neighborhood { get; set; }
        public string Locality { get; set; }

        public  ICollection<Service> Services { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public  ICollection<Client> Clients { get; set; }

        public Bank()
        {
            Services = new List<Service>();
            Clients = new List<Client>();
            Ratings = new List<Rating>();
        }
    }
}
