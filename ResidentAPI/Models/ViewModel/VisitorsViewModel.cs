using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResidentAPI.Models.ViewModel
{
    public class VisitorsViewModel
    {
        public int VisitorId { get; set; }
        public string VisitorName { get; set; }
        public string VisitorResaon { get; set; }
        public DateTime? VisitStartTime { get; set; }
        public DateTime? VisitEndTime { get; set; }
        public int? ResidentId { get; set; }
        public string VisitorEntryStatus { get; set; }
        public int? EmployeeId { get; set; }

        public virtual Employees Employee { get; set; }
        public virtual Residents Resident { get; set; }
    }
}
