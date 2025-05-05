using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplicationTracker.DataAccess.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Website { get; set; }
       
        


        public List<Application> Applications { get; set; } = new();
        public List<CompanyContact> Contacts { get; set; } = new();
    }
}
