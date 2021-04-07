using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityGateClient.Models.ViewModels;
using ResidentAPI.Models;
using ResidentAPI.Models.ViewModel;

namespace ResidentAPI.Repositories
{
    public interface IResRepos
    {
        IEnumerable<Residents> GetAllResidents();
        Residents GetResidentById(int id);
        Task<Residents> PostResidents(Residents item);
        Task<Residents> RemoveResident(int id);
        Task<Residents> UpdateResidents(Residents item, int id);
        public OneForAll GetResidentAtAGlance(List<VisitorsViewModel> visitor, List<ComplaintsViewModel> complaint, List<PaymentsViewModel> payment);
        Task<Residents> UpdateResidentWallet(int id, int item);
    }
}
