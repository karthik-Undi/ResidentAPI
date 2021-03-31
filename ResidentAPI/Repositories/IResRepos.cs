using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResidentAPI.Models;

namespace ResidentAPI.Repositories
{
    public interface IResRepos
    {
        IEnumerable<Residents> GetAllResidents();
        Residents GetResidentById(int id);
        Task<Residents> PostResidents(Residents item);
        Task<Residents> RemoveResident(int id);
        Task<Residents> UpdateResidents(Residents item, int id);
    }
}
