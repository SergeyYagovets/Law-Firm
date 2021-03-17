using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LawFirm.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string INN { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }
        
        public DateTime FounderDate { get; set; }
        
        public DateTime FouderUpdate { get; set; }
    }
}
