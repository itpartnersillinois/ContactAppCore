using ContactAppCore.Data;
using ContactAppCore.Data.Models;
using ContactAppCore.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAppCore.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditJobController : ControllerBase
    {
        private IContactRepository contactRepository;
        private SecurityHelper securityHelper;

        public EditJobController(IContactRepository contactRepository, SecurityHelper securityHelper)
        {
            this.contactRepository = contactRepository;
            this.securityHelper = securityHelper;
        }

        [HttpPost("Add")]
        public async Task<int> Add([FromBody] dynamic json)
        {
            var jsonObject = (dynamic)JObject.Parse(json.ToString());
            int officeId = int.Parse(jsonObject.officeId.ToString());
            if (!securityHelper.AllowOffice(User, officeId))
            {
                return default;
            }

            var employeeProfile = CreateEmployeeProfileIfNeeded(jsonObject.netid.ToString());

            await LogHelper.CreateLog(contactRepository, "Adding Job " + officeId, User.Identity.Name, "", json.ToString());

            var job = new JobProfile
            {
                Title = jsonObject.title,
                Biography = jsonObject.biography,
                EmployeeProfileId = employeeProfile.Id,
                OfficeId = officeId,
                InternalOrder = jsonObject.internalorder,
                LastUpdated = DateTime.Now,
                Tags = new List<JobProfileTag>(),
                IsActive = true
            };

            if (jsonObject.category != "")
            {
                job.Tags.Add(new JobProfileTag { IsActive = true, LastUpdated = DateTime.Now, Title = jsonObject.category });
            }
            if (jsonObject.display != "")
            {
                job.Tags.Add(new JobProfileTag { IsActive = true, LastUpdated = DateTime.Now, Title = jsonObject.display });
            }

            return await contactRepository.CreateAsync(job);
        }

        [HttpPost("Delete/{id}")]
        public async Task<int> Delete(int id)
        {
            var originalObject = await contactRepository.ReadAsync(c => c.JobProfiles.Include(o => o.Tags).SingleOrDefault(o => o.Id == id));
            int officeId = originalObject.OfficeId;
            if (!securityHelper.AllowOffice(User, officeId))
            {
                return default;
            }

            await LogHelper.CreateLog(contactRepository, "Deleting Job " + originalObject.Id.ToString(), User.Identity.Name, JsonConvert.SerializeObject(originalObject));
            originalObject.Tags.ToList().ForEach(t => contactRepository.Delete(t));
            return await contactRepository.DeleteAsync(new JobProfile
            {
                Id = originalObject.Id,
            });
        }

        [HttpPost("Update")]
        public async Task<int> Update([FromBody] dynamic json)
        {
            var jsonObject = (dynamic)JObject.Parse(json.ToString());
            int id = int.Parse(jsonObject.id.ToString());
            var originalObject = await contactRepository.ReadAsync(c => c.JobProfiles.Include(o => o.Tags).SingleOrDefault(o => o.Id == id));
            int officeId = originalObject.OfficeId;
            if (!securityHelper.AllowOffice(User, officeId))
            {
                return default;
            }

            await LogHelper.CreateLog(contactRepository, "Editing Job " + originalObject.Id.ToString(), User.Identity.Name, JsonConvert.SerializeObject(originalObject), json.ToString());
            originalObject.Tags.ToList().ForEach(t => contactRepository.Delete(t));
            var job = new JobProfile
            {
                Id = originalObject.Id,
                Title = jsonObject.title,
                Biography = jsonObject.biography,
                EmployeeProfileId = originalObject.EmployeeProfileId,
                OfficeId = originalObject.OfficeId,
                InternalOrder = jsonObject.internalorder,
                LastUpdated = DateTime.Now,
                Tags = new List<JobProfileTag>(),
                IsActive = true
            };

            if (jsonObject.category != "")
            {
                job.Tags.Add(new JobProfileTag { IsActive = true, LastUpdated = DateTime.Now, Title = jsonObject.category });
            }
            if (jsonObject.display != "")
            {
                job.Tags.Add(new JobProfileTag { IsActive = true, LastUpdated = DateTime.Now, Title = jsonObject.display });
            }

            return await contactRepository.UpdateAsync(job);
        }

        private EmployeeProfile CreateEmployeeProfileIfNeeded(string netid)
        {
            var employeeProfile = contactRepository.Read(c => c.EmployeeProfiles.SingleOrDefault(o => o.Title == netid));

            if (employeeProfile == null)
            {
                var newEmployeeProfile = new EmployeeProfile
                {
                    Title = netid,
                    IsActive = true
                };
                contactRepository.Create(newEmployeeProfile);
                return newEmployeeProfile;
            }
            else
            {
                return employeeProfile;
            }
        }
    }
}