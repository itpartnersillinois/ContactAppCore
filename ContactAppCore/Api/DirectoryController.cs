using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactAppCore.Data;
using ContactAppCore.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactAppCore.Api {

    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class DirectoryController : ControllerBase {
        private IContactRepository contactRepository;

        public DirectoryController(IContactRepository contactRepository) {
            this.contactRepository = contactRepository;
        }

        [HttpGet("All")]
        public async Task<IEnumerable<EmployeeInformation>> GetAllPeople() {
            return await contactRepository.ReadAsync(c => c.JobProfiles
                .Include(j => j.EmployeeProfile).ThenInclude(e => e.EmployeeActivities)
                .Include(j => j.Tags).Include(j => j.Office)
                .Where(j => j.Office.IsActive && j.IsActive && j.EmployeeProfile.IsActive)
                .Select(j => new EmployeeInformation(j)).ToList());
        }

        [HttpGet("Area/{id}")]
        public async Task<IEnumerable<EmployeeInformation>> GetAllPeopleByArea(int id) {
            return await contactRepository.ReadAsync(c => c.JobProfiles
                .Include(j => j.EmployeeProfile).ThenInclude(e => e.EmployeeActivities)
                .Include(j => j.Tags).Include(j => j.Office)
                .Where(j => j.Office.AreaId == id && j.Office.IsActive && j.IsActive && j.EmployeeProfile.IsActive)
                .Select(j => new EmployeeInformation(j)).ToList());
        }

        [HttpGet("AreaUsernames/{id}")]
        public async Task<IEnumerable<string>> GetAllUsernamesByArea(int id) {
            return await contactRepository.ReadAsync(c => c.JobProfiles
                .Include(j => j.EmployeeProfile).ThenInclude(e => e.EmployeeActivities)
                .Include(j => j.Tags).Include(j => j.Office)
                .Where(j => j.Office.AreaId == id && j.Office.IsActive && j.IsActive && j.EmployeeProfile.IsActive)
                .Select(j => j.EmployeeNetId).ToList());
        }

        [HttpGet("Name/{username}")]
        public async Task<IEnumerable<EmployeeInformation>> GetByUsername(string username) {
            return string.IsNullOrWhiteSpace(username) ? new List<EmployeeInformation>() :
                await contactRepository.ReadAsync(c => c.JobProfiles
                    .Include(j => j.EmployeeProfile).ThenInclude(e => e.EmployeeActivities)
                    .Include(j => j.Tags).Include(j => j.Office)
                    .Where(j => j.EmployeeProfile.Title == username && j.Office.IsActive && j.IsActive && j.EmployeeProfile.IsActive)
                    .Select(j => new EmployeeInformation(j)).ToList());
        }
    }
}