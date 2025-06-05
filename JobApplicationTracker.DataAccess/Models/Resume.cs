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
    public class Resume
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ApplicationId { get; set; }

        [ForeignKey("ApplicationId")]//get around any circular logic
        [JsonIgnore]
        public Application Application { get; set; }

        public string resumeContent { get; set; }
    }
}
