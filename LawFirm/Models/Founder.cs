using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LawFirm.Models
{
    public class Founder
    {
        [Key]
        public int FounderId { get; set; }
        [Required]
        public string INN { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime CustomerData { get; set; }
        public DateTime CustomerUpdate { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
