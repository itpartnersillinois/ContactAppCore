using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactAppCore.Data;
using ContactAppCore.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactAppCore.Api {

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase {
        private IContactRepository contactRepository;

        public EmployeeController(IContactRepository contactRepository) {
            this.contactRepository = contactRepository;
        }

        [HttpGet("ByName/{netid}")]
        public async Task<EmployeeProfile> GetEmployeeByName(string netid) {
            var nameShortened = netid.Replace("@illinois.edu", "");
            var returnValue = await contactRepository.ReadAsync(c => c.EmployeeProfiles.Include(ep => ep.Jobs).ThenInclude(j => j.Office).Include(ep => ep.EmployeeLinks).Include(ep => ep.EmployeeActivities).FirstOrDefault(p => p.Title == nameShortened));
            return returnValue == null ? new EmployeeProfile() : returnValue;
        }

        [HttpGet("ById/{id}")]
        public async Task<EmployeeProfile> GetEmployeeByName(int id) {
            var returnValue = await contactRepository.ReadAsync(c => c.EmployeeProfiles.Include(ep => ep.Jobs).ThenInclude(j => j.Office).Include(ep => ep.EmployeeLinks).Include(ep => ep.EmployeeActivities).FirstOrDefault(p => p.Id == id));
            return returnValue == null ? new EmployeeProfile() : returnValue;
        }

        [HttpGet("ByNameOrCreate/{netid}")]
        public async Task<EmployeeProfile> GetEmployeeByNameOrCreate(string netid) {
            var nameShortened = netid.Replace("@illinois.edu", "");
            var returnValue = await contactRepository.ReadAsync(c => c.EmployeeProfiles.Include(ep => ep.Jobs).ThenInclude(j => j.Office).Include(ep => ep.EmployeeLinks).Include(ep => ep.EmployeeActivities).FirstOrDefault(p => p.Title == nameShortened));
            if (returnValue == null) {
                var employee = new EmployeeProfile {
                    Title = User.Identity.Name.Replace("@illinois.edu", ""),
                    IsActive = true,
                    EmployeeActivities = new List<EmployeeActivity>(),
                    Jobs = new List<JobProfile>()
                };
                _ = await contactRepository.CreateAsync(employee);
                return employee;
            }
            return returnValue;
        }
    }
}