using ContactAppCore.Data;
using ContactAppCore.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAppCore.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class JobController : ControllerBase
    {
        private IContactRepository contactRepository;

        public JobController(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        [HttpGet("{id}")]
        public async Task<JobProfile> Get(int id)
        {
            return await contactRepository.ReadAsync(c => c.JobProfiles.Include(j => j.Tags).Include(j => j.EmployeeProfile).FirstOrDefault(j => j.Id == id));
        }

        [HttpGet("Area/{id}")]
        public async Task<IEnumerable<JobProfile>> GetByArea(int id)
        {
            var officeIds = await contactRepository.ReadAsync(c => c.Offices.Where(c => c.AreaId == id).Select(c => c.Id));
            return await contactRepository.ReadAsync(c => c.JobProfiles.Include(j => j.Tags).Include(j => j.EmployeeProfile).Where(j => j.IsActive && officeIds.Contains(j.OfficeId)));
        }

        [HttpGet("Office/{id}")]
        public async Task<IEnumerable<JobProfile>> GetByOffice(int id)
        {
            return await contactRepository.ReadAsync(c => c.JobProfiles.Include(j => j.Tags).Include(j => j.EmployeeProfile).Where(j => j.IsActive && j.OfficeId == id));
        }
    }
}