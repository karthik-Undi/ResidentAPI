using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResidentAPI.Models;
using ResidentAPI.Repositories;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

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
            _log4net.Info("Get All Residents Was Called !!");
            return _context.GetAllResidents();
        }
        [HttpGet("{id}")]
        public IActionResult GetResidentById(int id)
        {
            _log4net.Info("Get Resident By ID Was Called !!");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var resident = _context.GetResidentById(id);
                _log4net.Info("Resident Of Id " + id + " Was Called");
                if (resident == null)
                {
                    return NotFound();
                }
                return Ok(resident);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostResidents(Residents item)
        {
            _log4net.Info("Post Residents Was Called !!");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var addResident = await _context.PostResidents(item);
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                    try
                    {
                        using (var response = await httpClient.PostAsync("http://localhost:26408/api/House/UpdateIsFreeHouse", content))
                        {

                            _log4net.Info("Resident House No " + item.ResidentHouseNo + " Was Sent To House API !!");
                        }
                    }
                    catch (Exception)
                    {
                        return BadRequest();
                    }
                }
                return Ok(addResident);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("AtAGlance/{id}")]
        public IActionResult GetResidentAtAGlance(int id)
        {
            _log4net.Info("Get Resident By ID Was Called !!");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var all = _context.GetResidentAtAGlance(id);
                _log4net.Info("Resident Of Id " + id + " Was Called");
                if (all == null)
                {
                    return NotFound();
                }
                return Ok(all);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }






    }
}
