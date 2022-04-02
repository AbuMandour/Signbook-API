using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignBookProject.Models
{
    public class BundleModel
    {
        public string userId { get; set; }
        public bool isEligible { get; set; }
        public double credit { get; set; }
    }
}
