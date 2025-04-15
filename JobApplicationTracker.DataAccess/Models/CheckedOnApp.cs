using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplicationTracker.DataAccess.Models
{
    public class CheckedOnApp
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CheckedOnDate { get; set; }

        [ForeignKey("Application")]
        public int ApplicationId { get; set; }

        public Application Application { get; set; }
    }
}
