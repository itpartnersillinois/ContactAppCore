using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactAppCore.CampusService;
using ContactAppCore.Data;
using ContactAppCore.Data.Models;
using ContactAppCore.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ContactAppCore.Api {

    [Route("api/[controller]")]
    [ApiController]
    public class EditJobController : ControllerBase {
        private readonly IContactRepository _contactRepository;
        private readonly DataWarehouseManager _dataWarehouseManager;
        private readonly JobHelper _jobHelper;
        private readonly SecurityHelper _securityHelper;

        public EditJobController(IContactRepository contactRepository, SecurityHelper securityHelper, JobHelper jobHelper, DataWarehouseManager dataWarehouseManager) {
            _contactRepository = contactRepository;
            _securityHelper = securityHelper;
            _jobHelper = jobHelper;
            _dataWarehouseManager = dataWarehouseManager;
        }

        [HttpPost("Add")]
        public async Task<int> Add([FromForm] string netid, [FromForm] int officeId) {
            if (!_securityHelper.AllowOffice(User, officeId)) {
                return default;
            }

            var employeeProfile = CreateEmployeeProfileIfNeeded(netid);

            _ = await LogHelper.CreateLog(_contactRepository, "Adding Job " + officeId, User.Identity.Name, "", netid);

            var job = new JobProfile {
                Title = employeeProfile.Item2,
                EmployeeProfileId = employeeProfile.Item1.Id,
                OfficeId = officeId,
                LastUpdated = DateTime.Now,
                Tags = new List<JobProfileTag>(),
                IsActive = true
            };
            var returnValue = _contactRepository.Create(job);
            _ = await _jobHelper.ProcessJob(employeeProfile.Item1.Id, officeId);
            return returnValue;
        }

        // This is here as a convenience method to repair existing employee information
        [HttpGet("AddFullName")]
        public int AddFullName(string netid) {
            _ = CreateEmployeeProfileIfNeeded(netid);
            return 0;
        }

        [HttpPost("Delete/{id}")]
        public async Task<int> Delete(int id) {
            var originalObject = await _contactRepository.ReadAsync(c => c.JobProfiles.Include(o => o.Tags).SingleOrDefault(o => o.Id == id));
            int officeId = originalObject.OfficeId;
            if (!_securityHelper.AllowOffice(User, officeId)) {
                return default;
            }
            var employeeProfileId = originalObject.EmployeeProfileId;
            await LogHelper.CreateLog(_contactRepository, "Deleting Job " + originalObject.Id.ToString(), User.Identity.Name, JsonConvert.SerializeObject(originalObject), "", employeeProfileId.ToString());
            originalObject.Tags.ToList().ForEach(t => _contactRepository.Delete(t));
            var returnValue = _contactRepository.Delete(new JobProfile {
                Id = originalObject.Id,
            });
            _ = await _jobHelper.ProcessJob(employeeProfileId, officeId);
            return returnValue;
        }

        [HttpPost("ProcessPerson/{id}")]
        public async Task<int> Process(int id) {
            var originalObject = await _contactRepository.ReadAsync(c => c.JobProfiles.SingleOrDefault(o => o.Id == id));
            int officeId = originalObject.OfficeId;
            _ = await _jobHelper.ProcessJob(originalObject.EmployeeProfileId, officeId);
            return 0;
        }

        [HttpPost("Update")]
        public async Task<int> Update([FromBody] dynamic json) {
            var jsonObject = JObject.Parse(json.ToString());
            int id = int.Parse(jsonObject.id.ToString());
            var originalObject = await _contactRepository.ReadAsync(c => c.JobProfiles.Include(o => o.Tags).SingleOrDefault(o => o.Id == id));
            int officeId = originalObject.OfficeId;
            if (!_securityHelper.AllowOffice(User, officeId)) {
                return default;
            }

            await LogHelper.CreateLog(_contactRepository, "Editing Job " + originalObject.Id.ToString(), User.Identity.Name, JsonConvert.SerializeObject(originalObject), json.ToString(), originalObject.EmployeeProfileId.ToString());
            originalObject.Tags.ToList().ForEach(t => _contactRepository.Delete(t));
            var job = new JobProfile {
                Id = originalObject.Id,
                Title = jsonObject.title,
                Biography = jsonObject.biography,
                Category = jsonObject.category,
                EmployeeProfileId = originalObject.EmployeeProfileId,
                OfficeId = originalObject.OfficeId,
                InternalOrder = jsonObject.internalorder,
                LastUpdated = DateTime.Now,
                Tags = new List<JobProfileTag>(),
                IsActive = true
            };
            var returnValue = _contactRepository.Update(job);
            _ = await _jobHelper.ProcessJob(originalObject.EmployeeProfileId, officeId);
            return returnValue;
        }

        private Tuple<EmployeeProfile, string> CreateEmployeeProfileIfNeeded(string netid) {
            var employeeProfile = _contactRepository.Read(c => c.EmployeeProfiles.SingleOrDefault(o => o.Title == netid));

            if (employeeProfile == null) {
                var listedName = _dataWarehouseManager.GetFirstAndLastName(netid);

                var newEmployeeProfile = new EmployeeProfile {
                    Title = netid,
                    ListedNameFirst = listedName.FirstName,
                    ListedNameLast = listedName.LastName,
                    IsActive = true
                };
                _contactRepository.Create(newEmployeeProfile);
                return new Tuple<EmployeeProfile, string>(employeeProfile, listedName.Title);
            } else {
                var listedName = _dataWarehouseManager.GetFirstAndLastName(netid);
                employeeProfile.ListedNameFirst = listedName.FirstName;
                employeeProfile.ListedNameLast = listedName.LastName;
                _ = _contactRepository.Update(employeeProfile);
                return new Tuple<EmployeeProfile, string>(employeeProfile, listedName.Title);
            }
        }
    }
}