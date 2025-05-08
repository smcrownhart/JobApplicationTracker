using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplicationTracker.DataAccess.Models
{
    public class Application
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string JobTitle { get; set; }

        public string JobDescription { get; set; }
        
        public DateTime ApplicationDate { get; set; }
        public string Status { get; set; }
        //keep track of applied, interviewed, offered, rejected

        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        public Company Company { get; set; }

       
        public List<CompanyContact> CompanyContacts { get; set; } = new();
        public List<CheckedOnApp> CheckedOnHistory { get; set; } = new();
        public List<Interviews> Interviews { get; set; } = new();
        public List<InterviewPrep> InterviewPreps { get; set; } = new();

    }
}
