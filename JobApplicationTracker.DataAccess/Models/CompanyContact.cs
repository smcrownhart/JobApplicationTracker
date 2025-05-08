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
    public class CompanyContact
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        [ForeignKey("Application")]
        public int ApplicationId { get; set; }

        [JsonIgnore]
        public Application Application { get; set; }
    }
}
