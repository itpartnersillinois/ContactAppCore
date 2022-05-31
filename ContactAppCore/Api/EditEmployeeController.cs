using System;
using System.Linq;
using System.Threading.Tasks;
using ContactAppCore.Data;
using ContactAppCore.Data.Models;
using ContactAppCore.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactAppCore.Api {

    [Route("api/[controller]")]
    [ApiController]
    public class EditEmployeeController : ControllerBase {
        private IContactRepository contactRepository;
        private FileHelper fileHelper;
        private SecurityHelper securityHelper;

        public EditEmployeeController(IContactRepository contactRepository, SecurityHelper securityHelper, FileHelper fileHelper) {
            this.contactRepository = contactRepository;
            this.securityHelper = securityHelper;
            this.fileHelper = fileHelper;
        }

        [HttpPost("DeleteCV")]
        public async Task<IActionResult> DeleteCv([FromForm] int id) {
            var originalObject = await contactRepository.ReadAsync(c => c.EmployeeProfiles.SingleOrDefault(o => o.Id == id));
            if (!securityHelper.IsCurrentUser(User, originalObject.Title)) {
                return default;
            }
            await LogHelper.CreateLog(contactRepository, "Deleting Employee CV " + originalObject.Id.ToString(), User.Identity.Name);

            var path = fileHelper.DeleteCv(originalObject.Title);

            await contactRepository.UpdateAsync(new EmployeeProfile {
                Id = originalObject.Id,
                Title = originalObject.Title,
                Biography = originalObject.Biography,
                CVUrl = "",
                PhotoUrl = originalObject.PhotoUrl,
                PreferredName = originalObject.PreferredName,
                PreferredNameLast = originalObject.PreferredNameLast,
                PreferredPronouns = originalObject.PreferredPronouns,
                LastUpdated = DateTime.Now,
                IsActive = true
            });

            return new OkObjectResult("");
        }

        [HttpPost("DeletePicture")]
        public async Task<IActionResult> DeletePicture([FromForm] int id) {
            var originalObject = await contactRepository.ReadAsync(c => c.EmployeeProfiles.SingleOrDefault(o => o.Id == id));
            if (!securityHelper.IsCurrentUser(User, originalObject.Title)) {
                return default;
            }
            await LogHelper.CreateLog(contactRepository, "Deleting Employee Photo " + originalObject.Id.ToString(), User.Identity.Name);

            var path = fileHelper.DeletePhoto(originalObject.Title);

            await contactRepository.UpdateAsync(new EmployeeProfile {
                Id = originalObject.Id,
                Title = originalObject.Title,
                Biography = originalObject.Biography,
                CVUrl = originalObject.CVUrl,
                PhotoUrl = "",
                PreferredName = originalObject.PreferredName,
                PreferredNameLast = originalObject.PreferredNameLast,
                PreferredPronouns = originalObject.PreferredPronouns,
                LastUpdated = DateTime.Now,
                IsActive = true
            });

            return new OkObjectResult("");
        }

        [HttpPost("Update")]
        public async Task<int> Update([FromBody] dynamic json) {
            var jsonObject = (dynamic) JObject.Parse(json.ToString());
            int id = int.Parse(jsonObject.id.ToString());
            if (id == 0) {
                return await contactRepository.CreateAsync(new EmployeeProfile {
                    Title = User.Identity.Name.Replace("@illinois.edu", ""),
                    Biography = jsonObject.biography,
                    IsActive = true
                });
            } else {
                var originalObject = await contactRepository.ReadAsync(c => c.EmployeeProfiles.SingleOrDefault(o => o.Id == id));

                if (!securityHelper.IsCurrentUser(User, originalObject.Title)) {
                    return default;
                }
                await LogHelper.CreateLog(contactRepository, "Editing Employee " + originalObject.Id.ToString(), User.Identity.Name, JsonConvert.SerializeObject(originalObject).ToString(), json.ToString());

                return await contactRepository.UpdateAsync(new EmployeeProfile {
                    Id = originalObject.Id,
                    Title = originalObject.Title,
                    Biography = jsonObject.biography,
                    CVUrl = originalObject.CVUrl,
                    PrimaryProfile = originalObject.PrimaryProfile,
                    PhotoUrl = originalObject.PhotoUrl,
                    PreferredName = jsonObject.firstName,
                    PreferredNameLast = jsonObject.lastName,
                    PreferredPronouns = jsonObject.pronouns,
                    LastUpdated = DateTime.Now,
                    IsActive = true
                });
            }
        }

        [HttpPost("CV")]
        public async Task<string> UpdateCv([FromForm] IFormFile file, [FromForm] int id) {
            var originalObject = await contactRepository.ReadAsync(c => c.EmployeeProfiles.SingleOrDefault(o => o.Id == id));
            if (!securityHelper.IsCurrentUser(User, originalObject.Title)) {
                return default;
            }
            await LogHelper.CreateLog(contactRepository, "Updating Employee CV " + originalObject.Id.ToString(), User.Identity.Name);

            var path = fileHelper.AddCv(file.OpenReadStream(), originalObject.Title, file.FileName);

            await contactRepository.UpdateAsync(new EmployeeProfile {
                Id = originalObject.Id,
                Title = originalObject.Title,
                Biography = originalObject.Biography,
                CVUrl = path.Result,
                PhotoUrl = originalObject.PhotoUrl,
                PreferredName = originalObject.PreferredName,
                PreferredNameLast = originalObject.PreferredNameLast,
                PreferredPronouns = originalObject.PreferredPronouns,
                LastUpdated = DateTime.Now,
                IsActive = true
            });

            return path.Result;
        }

        [HttpPost("UpdateFromProfile")]
        public async Task<int> UpdateFromProfile([FromBody] dynamic json) {
            var jsonObject = (dynamic) JObject.Parse(json.ToString());
            int id = int.Parse(jsonObject.id.ToString());
            int jobId = int.Parse(jsonObject.jobId.ToString());
            if (id == 0 || jobId == 0) {
                return 0;
            } else {
                var originalObject = await contactRepository.ReadAsync(c => c.EmployeeProfiles.SingleOrDefault(o => o.Id == id));

                if (!securityHelper.IsCurrentUser(User, originalObject.Title)) {
                    return default;
                }
                await LogHelper.CreateLog(contactRepository, "Editing Employee from Job Profile " + originalObject.Id.ToString(), User.Identity.Name, JsonConvert.SerializeObject(originalObject).ToString(), json.ToString());
                var biography = jsonObject.biography.ToString();
                if (!string.IsNullOrWhiteSpace(biography)) {
                    var originalJob = await contactRepository.ReadAsync(c => c.JobProfiles.SingleOrDefault(o => o.Id == jobId));
                    originalJob.Biography = null;
                    _ = await contactRepository.UpdateAsync(originalJob);
                }

                return await contactRepository.UpdateAsync(new EmployeeProfile {
                    Id = originalObject.Id,
                    Title = originalObject.Title,
                    Biography = string.IsNullOrWhiteSpace(biography) ? originalObject.Biography : biography,
                    CVUrl = originalObject.CVUrl,
                    PrimaryProfile = originalObject.PrimaryProfile,
                    PhotoUrl = originalObject.PhotoUrl,
                    PreferredName = jsonObject.firstName,
                    PreferredNameLast = jsonObject.lastName,
                    PreferredPronouns = jsonObject.pronouns,
                    LastUpdated = DateTime.Now,
                    IsActive = true
                });
            }
        }

        [HttpPost("Picture")]
        public async Task<string> UpdatePicture([FromForm] IFormFile file, [FromForm] int id) {
            var originalObject = await contactRepository.ReadAsync(c => c.EmployeeProfiles.SingleOrDefault(o => o.Id == id));
            if (!securityHelper.IsCurrentUser(User, originalObject.Title)) {
                return default;
            }
            await LogHelper.CreateLog(contactRepository, "Updating Employee Photo " + originalObject.Id.ToString(), User.Identity.Name);
            var area = await contactRepository.ReadAsync(c => c.EmployeeProfiles
                .Include(c => c.Jobs).ThenInclude(j => j.Office).ThenInclude(o => o.Area)
                .FirstOrDefault(c => c.Id == id)
                .Jobs.FirstOrDefault().Office.Area);
            string errorMessage;
            var path = fileHelper.AddPhoto(file.OpenReadStream(), originalObject.Title, file.FileName, area.PictureHeight, area.PictureWidth, out errorMessage);
            if (!string.IsNullOrWhiteSpace(errorMessage) || string.IsNullOrWhiteSpace(path)) {
                return errorMessage + (string.IsNullOrWhiteSpace(path) ? "" : " (" + path + ")");
            }
            await contactRepository.UpdateAsync(new EmployeeProfile {
                Id = originalObject.Id,
                Title = originalObject.Title,
                Biography = originalObject.Biography,
                CVUrl = originalObject.CVUrl,
                PhotoUrl = path,
                PreferredName = originalObject.PreferredName,
                PreferredNameLast = originalObject.PreferredNameLast,
                PreferredPronouns = originalObject.PreferredPronouns,
                LastUpdated = DateTime.Now,
                IsActive = true
            });
            return path;
        }

        [HttpPost("UpdatePrimaryJob")]
        public async Task<int> UpdatePrimaryJob([FromBody] dynamic json) {
            var jsonObject = (dynamic) JObject.Parse(json.ToString());
            int id = int.Parse(jsonObject.id.ToString());
            var originalObject = await contactRepository.ReadAsync(c => c.EmployeeProfiles.SingleOrDefault(o => o.Id == id));

            if (!securityHelper.IsCurrentUser(User, originalObject.Title)) {
                return default;
            }
            await LogHelper.CreateLog(contactRepository, "Editing Employee " + originalObject.Id.ToString(), User.Identity.Name, JsonConvert.SerializeObject(originalObject), json.ToString());

            return await contactRepository.UpdateAsync(new EmployeeProfile {
                Id = originalObject.Id,
                Title = originalObject.Title,
                Biography = originalObject.Biography,
                CVUrl = originalObject.CVUrl,
                PrimaryProfile = jsonObject.jobid,
                PhotoUrl = originalObject.PhotoUrl,
                PreferredName = originalObject.PreferredName,
                PreferredNameLast = originalObject.PreferredNameLast,
                PreferredPronouns = originalObject.PreferredPronouns,
                LastUpdated = DateTime.Now,
                IsActive = true
            });
        }
    }
}