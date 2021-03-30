using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResidentAPI.Models;
namespace ResidentAPI.Repositories
{
    public class ResRepos:IResRepos
    {
        private readonly CommunityGateDatabaseContext _context;
        public ResRepos(CommunityGateDatabaseContext context)
        {
            this._context = context;
        }
        public ResRepos()
        {

        }

    }
}
