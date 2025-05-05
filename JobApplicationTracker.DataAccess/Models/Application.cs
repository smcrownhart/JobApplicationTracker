using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JobApplicationTracker.DataAccess.Models
{
    public class Application
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string JobTitle { get; set; }

        public string JobDescription { get; set; }
        [Required]
        public DateTime ApplicationDate { get; set; }
        public string Status { get; set; }
        //keep track of applied, interviewed, offered, rejected

        [ForeignKey("Company")]
       
        public int CompanyId { get; set; }


        public Company Company { get; set; }

        public string? ResumePath { get; set; }
        public string? CoverLetterPath { get; set; }

       
        public List<CheckedOnApp> CheckedOnApps { get; set; } = new();

        public bool IsCheckedOn => CheckedOnApps?.Any() == true;
        public List<Interviews> Interviews { get; set; } = new();
        public List<InterviewPrep> InterviewPreps { get; set; } = new();

    }
}
