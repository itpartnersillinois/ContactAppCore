using System;
using System.Linq;
using System.Threading.Tasks;
using ContactAppCore.Data;
using ContactAppCore.Data.Models;
using ContactAppCore.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactAppCore.Api {

    [Route("api/[controller]")]
    [ApiController]
    public class EditEmployeeActivityController : ControllerBase {
        private readonly IContactRepository _contactRepository;
        private readonly SecurityHelper _securityHelper;

        public EditEmployeeActivityController(IContactRepository contactRepository, SecurityHelper securityHelper) {
            _contactRepository = contactRepository;
            _securityHelper = securityHelper;
        }

        [HttpPost("Add")]
        public async Task<int> AddActivity([FromBody] dynamic json) {
            var jsonObject = (dynamic) JObject.Parse(json.ToString());
            int employeeId = int.Parse(jsonObject.employeeId.ToString());
            var person = await _contactRepository.ReadAsync(c => c.EmployeeProfiles.SingleOrDefault(o => o.Id == employeeId));

            if (!_securityHelper.IsCurrentUser(User, person.Title)) {
                return default;
            }
            await LogHelper.CreateLog(_contactRepository, "Adding Employee Activity " + person.Id.ToString(), User.Identity.Name, "", json.ToString(), employeeId.ToString());

            return await _contactRepository.CreateAsync(new EmployeeActivity {
                EmployeeProfileId = employeeId,
                Title = jsonObject.name,
                InternalOrder = jsonObject.priority,
                Url = jsonObject.url,
                Type = jsonObject.type,
                YearEnded = jsonObject.yearEnded,
                YearStarted = jsonObject.yearStarted,
                LastUpdated = DateTime.Now,
                IsActive = true
            });
        }

        [HttpPost("Delete")]
        public async Task<int> DeleteActivity([FromBody] dynamic json) {
            var jsonObject = (dynamic) JObject.Parse(json.ToString());
            int employeeId = int.Parse(jsonObject.employeeId.ToString());
            var person = await _contactRepository.ReadAsync(c => c.EmployeeProfiles.SingleOrDefault(o => o.Id == employeeId));

            if (!_securityHelper.IsCurrentUser(User, person.Title)) {
                return default;
            }
            await LogHelper.CreateLog(_contactRepository, "Deleting Employee Activity " + person.Id.ToString(), User.Identity.Name, json.ToString(), employeeId.ToString());

            return await _contactRepository.DeleteAsync(new EmployeeActivity {
                Id = jsonObject.id,
                EmployeeProfileId = employeeId
            });
        }

        [HttpPost("Edit")]
        public async Task<int> EditActivity([FromBody] dynamic json) {
            var jsonObject = (dynamic) JObject.Parse(json.ToString());
            int employeeId = int.Parse(jsonObject.employeeId.ToString());
            var person = await _contactRepository.ReadAsync(c => c.EmployeeProfiles.SingleOrDefault(o => o.Id == employeeId));

            if (!_securityHelper.IsCurrentUser(User, person.Title)) {
                return default;
            }
            await LogHelper.CreateLog(_contactRepository, "Editing Employee Activity " + person.Id.ToString(), User.Identity.Name, "", json.ToString(), employeeId.ToString());

            return await _contactRepository.UpdateAsync(new EmployeeActivity {
                Id = jsonObject.id,
                EmployeeProfileId = employeeId,
                Title = jsonObject.name,
                InternalOrder = jsonObject.priority,
                Url = jsonObject.url,
                Type = jsonObject.type,
                YearEnded = jsonObject.yearEnded,
                YearStarted = jsonObject.yearStarted,
                LastUpdated = DateTime.Now,
                IsActive = true
            });
        }
    }
}