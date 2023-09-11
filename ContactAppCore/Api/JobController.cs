using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactAppCore.CampusService;
using ContactAppCore.Data;
using ContactAppCore.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactAppCore.Api {

    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class JobController : ControllerBase {
        private readonly IContactRepository _contactRepository;
        private readonly DataWarehouseManager _dataWarehouseManager;

        public JobController(IContactRepository contactRepository, DataWarehouseManager dataWarehouseManager) {
            _contactRepository = contactRepository;
            _dataWarehouseManager = dataWarehouseManager;
        }

        [HttpGet("audit/{netid}")]
        public async Task<IEnumerable<Log>> Audit(string netid) {
            var employee = await _contactRepository.ReadAsync(c => c.EmployeeProfiles.FirstOrDefault(e => e.Title == netid));
            return await _contactRepository.ReadAsync(c => c.Logs.Where(l => l.NetId == netid || (employee != null && l.NetId == employee.Id.ToString())).OrderByDescending(e => e.LastUpdated));
        }

        [HttpGet("audititem/{id}")]
        public async Task<Log> AuditItem(int id) {
            return await _contactRepository.ReadAsync(c => c.Logs.SingleOrDefault(l => l.Id == id));
        }

        [HttpGet("{id}")]
        public async Task<JobProfile> Get(int id) {
            return await _contactRepository.ReadAsync(c => c.JobProfiles.Include(j => j.Tags).Include(j => j.EmployeeProfile).FirstOrDefault(j => j.Id == id));
        }

        [HttpGet("area/{id}")]
        public async Task<IEnumerable<JobProfile>> GetByArea(int id) {
            return await _contactRepository.ReadAsync(c => c.JobProfiles.Include(j => j.Tags).Include(j => j.EmployeeProfile)
                .Where(j => j.IsActive && c.Offices.Where(o => o.AreaId == id).Select(o => o.Id).Contains(j.OfficeId)));
        }

        [HttpGet("name/{netid}")]
        public async Task<IEnumerable<JobProfile>> GetByNetId(string netid) {
            return await _contactRepository.ReadAsync(c => c.JobProfiles.Include(j => j.EmployeeProfile).Include(j => j.Office).Where(j => j.IsActive && j.EmployeeProfile.Title == netid).OrderBy(e => e.Title));
        }

        [HttpGet("office/{id}")]
        public async Task<IEnumerable<JobProfile>> GetByOffice(int id) {
            return await _contactRepository.ReadAsync(c => c.JobProfiles.Include(j => j.Tags).Include(j => j.EmployeeProfile).Where(j => j.IsActive && j.OfficeId == id).OrderBy(e => e.EmployeeProfile.ListedNameLast).ThenBy(e => e.EmployeeProfile.ListedNameFirst).ThenBy(e => e.EmployeeProfile.Title));
        }
    }
}