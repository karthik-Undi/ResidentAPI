using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CommunityGateClient.Models.ViewModels;
using ResidentAPI.Models;
using ResidentAPI.Models.ViewModel;
namespace ResidentAPI.Repositories
{
    public class ResRepos : IResRepos
    {
        private readonly CommunityGateDatabaseContext _context;

        public ResRepos()
        {

        }
        public ResRepos(CommunityGateDatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Residents> GetAllResidents()
        {
            return _context.Residents.ToList();
        }
        public Residents GetResidentById(int id)
        {
            Residents item = _context.Residents.Find(id);
            return item;
        }

        public Residents GetResidentByMail(string mail)
        {
            Residents item = _context.Residents.Single(res => res.ResidentEmail == mail);
            return item;
        }

        public async Task<Residents> PostResidents(Residents item)
        {
            Residents resident = null;
            if (item == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                resident = new Residents()
                {
                    ResidentName = item.ResidentName,
                    ResidentEmail = item.ResidentEmail,
                    ResidentHouseNo = item.ResidentHouseNo,
                    ResidentMobileNo = item.ResidentMobileNo,
                    ResidentPassword = item.ResidentPassword,
                    ResidentType = item.ResidentType,
                    ResidentWallet = 0,
                    IsApproved = "notApproved"
                };
                await _context.Residents.AddAsync(resident);
                await _context.SaveChangesAsync();
            }
            return resident;
        }

        public async Task<Residents> RemoveResident(int id)
        {
            Residents resident = await _context.Residents.FindAsync(id);
            if (resident == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                _context.Residents.Remove(resident);
                await _context.SaveChangesAsync();
            }
            return resident;
        }

        public async Task<Residents> ApproveResident(int id)
        {
            Residents resident = await _context.Residents.FindAsync(id);
            resident.IsApproved = "Approved";
            await _context.SaveChangesAsync();

            return resident;
        }

        public OneForAll GetResidentAtAGlance(List<VisitorsViewModel> visitor, List<ComplaintsViewModel> complaint, List<PaymentsViewModel> payment)
        {
            var tables = new OneForAll
            {
                visitors = visitor,
                complaints = complaint,
                payments = payment
            };
            return tables;
        }
        public async Task<Residents> UpdateResidentWallet(int id, int item)
        {
            Residents resident = await _context.Residents.FindAsync(id);
            resident.ResidentWallet = item;
            await _context.SaveChangesAsync();
            return resident;
        }
    }

}
