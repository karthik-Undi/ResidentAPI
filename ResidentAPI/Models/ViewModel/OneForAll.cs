using ResidentAPI.Models;
using ResidentAPI.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityGateClient.Models.ViewModels
{
    public class OneForAll
    {
        public IEnumerable<Residents> residents { get; set; }
        public IEnumerable<Employees> employees { get; set; }
        public IEnumerable<VisitorsViewModel> visitors { get; set; }
        public IEnumerable<PaymentsViewModel> payments { get; set; }
        public IEnumerable<Services> services { get; set; }
        public IEnumerable<DashBoardPosts> DashboardPosts { get; set; }
        public IEnumerable<FriendsAndFamily> friendsAndFamily { get; set; }
        public IEnumerable<HouseList> houseList { get; set; }
        public IEnumerable<ComplaintsViewModel> complaints { get; set; }
    }
}
