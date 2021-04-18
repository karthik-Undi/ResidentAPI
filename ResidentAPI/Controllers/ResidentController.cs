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
using System.Net.Http.Headers;
using ResidentAPI.Models.ViewModel;

namespace ResidentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidentController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ResidentController));
        private readonly IResRepos _context;
        string BaseurlForVisitorAPI = "https://localhost:44301/";
        string BaseUrlForComplaintsAPI = "http://localhost:36224/";
        string BaseUrlForPaymentsAPI = "http://localhost:27340/";

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
        
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateResidentWallet(int id, Residents item)
        {
            _log4net.Info("Update Resiednt Wallert For Resident With Id " + id + " Was Called !!");
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                int wallet = Convert.ToInt32(item.ResidentWallet);
                var updateWallet = await _context.UpdateResidentWallet(id, wallet);
                return Ok(updateWallet);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("AtAGlance/{id}")]
        public async Task<IActionResult> GetResidentAtAGlance(int id)
        {
            List<VisitorsViewModel> visitors = new List<VisitorsViewModel>();
            List<ComplaintsViewModel> complaints = new List<ComplaintsViewModel>();
            List<PaymentsViewModel> payments = new List<PaymentsViewModel>();
            _log4net.Info("Get Resident By ID Was Called !!");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using(var clientForVisitors = new HttpClient())
                {
                    clientForVisitors.BaseAddress = new Uri(BaseurlForVisitorAPI);
                    clientForVisitors.DefaultRequestHeaders.Clear();
                    clientForVisitors.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await clientForVisitors.GetAsync("/api/Visitor/" + id);
                    if (Res.IsSuccessStatusCode)
                    {
                        var Response = Res.Content.ReadAsStringAsync().Result;
                        visitors = JsonConvert.DeserializeObject<List<VisitorsViewModel>>(Response);
                    }
                }
                using (var clientForComplaints = new HttpClient())
                {
                    clientForComplaints.BaseAddress = new Uri(BaseUrlForComplaintsAPI);
                    clientForComplaints.DefaultRequestHeaders.Clear();
                    clientForComplaints.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await clientForComplaints.GetAsync("/api/Complaints/GetComplaintByResidentId/" + id);
                    if (Res.IsSuccessStatusCode)
                    {
                        var Response = Res.Content.ReadAsStringAsync().Result;
                        complaints = JsonConvert.DeserializeObject<List<ComplaintsViewModel>>(Response);
                    }
                }
                using (var clientForPayments = new HttpClient())
                {
                    clientForPayments.BaseAddress = new Uri(BaseUrlForPaymentsAPI);
                    clientForPayments.DefaultRequestHeaders.Clear();
                    clientForPayments.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await clientForPayments.GetAsync("/api/Payments/GetPaymentsByRedsidentId/" + id);
                    if (Res.IsSuccessStatusCode)
                    {
                        var Response = Res.Content.ReadAsStringAsync().Result;
                        payments = JsonConvert.DeserializeObject<List<PaymentsViewModel>>(Response);
                    }
                }

                var all = _context.GetResidentAtAGlance(visitors,complaints,payments);
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


        [HttpPost("ApproveResident/{id}")]
        public async Task<IActionResult> ApproveResident(int id,Residents r)
        {
            _log4net.Info("Update Resiednt Wallert For Resident With Id " + id + " Was Called !!");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var ApprovedResident = await _context.ApproveResident(id);
                return Ok(ApprovedResident);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }



    }
}
