using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAppTracker.Maui.Models
{
    public class EditApplicationDTO
    {
        
            public int Id { get; set; }
            public string JobTitle { get; set; }
            public string JobDescription { get; set; }
            public DateTime ApplicationDate { get; set; }
            public string Status { get; set; }
            public int CompanyId { get; set; }
        
    }
}
