using ContactAppCore.Data;
using ContactAppCore.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactAppCore.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IContactRepository contactRepository;

        public EmployeeController(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        [HttpGet("ByName/{netid}")]
        public async Task<EmployeeProfile> GetEmployeeByName(string netid)
        {
            var nameShortened = netid.Replace("@illinois.edu", "");
            var returnValue = await contactRepository.ReadAsync(c => c.EmployeeProfiles.Include(ep => ep.Jobs).Include(ep => ep.EmployeeLinks).Include(ep => ep.EmployeeActivities).FirstOrDefault(p => p.Title == nameShortened));
            return returnValue == null ? new EmployeeProfile() : returnValue;
        }
    }
}