using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResidentAPI.Models.ViewModel
{
    public class ComplaintsViewModel
    {
        public int ComplaintId { get; set; }
        public int? ResidentId { get; set; }
        public string ComplaintRegarding { get; set; }
        public string ComplaintStatus { get; set; }

        public virtual Residents Resident { get; set; }
    }
}
