using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplicationTracker.DataAccess.Models
{
    public class InterviewPrep
    {
        [Key]
        public int Id { get; set; }

        public string? PrepNotes { get; set; }
        //notes, questions, etc to help prepare for the interview

        public string? CompanyNotes { get; set; }
        //notes about the company to bring up in the interview, research, talking points

        [ForeignKey("Interviews")]
        public int InterviewsId { get; set; }

        public Interviews interviews { get; set; }
    }
}
