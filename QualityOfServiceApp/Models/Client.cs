using System.Collections;
using System.Collections.Generic;

namespace QualityOfServiceApp.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string Education { get; set; }
        public string SocialGroup { get; set; }

        public ICollection<Bank> Banks { get; set; }

        public Client()
        {
            Banks = new List<Bank>();
        }
    }
}
