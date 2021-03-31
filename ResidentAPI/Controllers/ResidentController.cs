using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResidentAPI.Models;
using ResidentAPI.Repositories;

namespace ResidentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidentController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ResidentController));
        private readonly IResRepos _context;
        public ResidentController(IResRepos context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<Residents> GetAllResidents()
        {
            _log4net.Info("Get All Residents is Called !!");
            return _context.GetAllResidents();
        }
        [HttpGet("{id}")]
        public IActionResult GetResidentById(int id)
        {
            _log4net.Info("Get Resident By ID is Called !!");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resident = _context.GetResidentById(id);
            _log4net.Info("Resident of Id " + id + " was called");
            if (resident == null)
            {
                return NotFound();
            }
            return Ok(resident);
        }

        [HttpPost]
        public async Task<IActionResult> PostResidents(Residents item)
        {
            _log4net.Info("Post Residents is called !!");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var addResident = await _context.PostResidents(item);
            return Ok(addResident);
        }
    }
}
