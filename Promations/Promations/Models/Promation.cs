using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Promations.Models
{
    public class Promation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public bool IsDeleted { get; set; }
        public int ProductID { get; set; }
        public Product product { get; set; }
    }
}
