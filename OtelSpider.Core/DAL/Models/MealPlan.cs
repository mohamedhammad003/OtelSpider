using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.DAL.Models
{
    public class MealPlan
    {
        [Key]
        public int ID { get; set; }
        [StringLength(250)]
        public string Name { get; set; }
        [StringLength(10)]
        public string Abbreviation { get; set; }
    }
}
