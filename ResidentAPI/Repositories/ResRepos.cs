using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityGateClient.Models.ViewModels;
using ResidentAPI.Models;
using ResidentAPI.Models.ViewModel;
namespace ResidentAPI.Repositories
{
    public class ResRepos:IResRepos
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

        public async Task<Residents> PostResidents(Residents item)
        {
            Residents resident = null;
            if (item == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                resident = new Residents() {
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

        public async Task<Residents> UpdateResidents(Residents item, int id)
        {
            Residents resident = await _context.Residents.FindAsync(id);
            resident.ResidentName = item.ResidentName;
            resident.ResidentEmail = item.ResidentEmail;
            resident.ResidentType = item.ResidentType;
            resident.ResidentMobileNo = item.ResidentMobileNo;
            resident.ResidentPassword = item.ResidentPassword;
            resident.ResidentHouseNo = item.ResidentHouseNo;
            resident.ResidentWallet = item.ResidentWallet;
            resident.IsApproved = item.IsApproved;
            await _context.SaveChangesAsync();

            return resident;
        }

        public OneForAll GetResidentAtAGlance(int id)
        {
            var tables = new OneForAll
            {
                visitors = _context.Visitors.Where(t => t.ResidentId == id).ToList(),
                //services = _context.Services.Where(t => t.ResidentId == id).ToList(),
                //friendsAndFamily = _context.FriendsAndFamily.Where(t => t.ResidentId == id).ToList(),
                //DashboardPosts = _context.DashBoardPosts.Where(t => t.ResidentId == id).ToList(),
                complaints = _context.Complaints.Where(t => t.ResidentId == id && t.ComplaintStatus=="Unresolved").ToList(),
                payments = _context.Payments.Where(t => t.ResidentId == id && t.PaymentStatus == "Requested").ToList()
            };
            return tables;
        }

    }
}
