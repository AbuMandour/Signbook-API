using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SignBookProject.Models
{
    public class PointModel
    {
        [Key]
        public string name { get; set; }
        public double APoint { get; set; }
        public double LPoint { get; set; }
    }
}
