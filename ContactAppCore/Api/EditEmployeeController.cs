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
        private readonly IContactRepository _contactRepository;
        private readonly FileHelper _fileHelper;
        private readonly SecurityHelper _securityHelper;

        public EditEmployeeController(IContactRepository contactRepository, SecurityHelper securityHelper, FileHelper fileHelper) {
            _contactRepository = contactRepository;
            _securityHelper = securityHelper;
            _fileHelper = fileHelper;
        }

        [HttpPost("DeleteCV")]
        public async Task<IActionResult> DeleteCv([FromForm] int id) {
            var originalObject = await _contactRepository.ReadAsync(c => c.EmployeeProfiles.SingleOrDefault(o => o.Id == id));
            if (!_securityHelper.IsCurrentUser(User, originalObject.Title)) {
                return default;
            }
            await LogHelper.CreateLog(_contactRepository, "Deleting Employee CV " + originalObject.Id.ToString(), User.Identity.Name, "", "", id.ToString());

            var path = _fileHelper.DeleteCv(originalObject.Title);

            await _contactRepository.UpdateAsync(new EmployeeProfile {
                Id = originalObject.Id,
                Title = originalObject.Title,
                Biography = originalObject.Biography,
                CVUrl = "",
                PhotoUrl = PhotoHelper.DetermineImage(originalObject.PhotoUrl),
                PreferredName = originalObject.PreferredName,
                PreferredNameLast = originalObject.PreferredNameLast,
                PreferredPronouns = originalObject.PreferredPronouns,
                IsPhoneHidden = originalObject.IsPhoneHidden,
                LastUpdated = DateTime.Now,
                IsActive = true
            });

            return new OkObjectResult("");
        }

        [HttpPost("DeletePicture")]
        public async Task<IActionResult> DeletePicture([FromForm] int id) {
            var originalObject = await _contactRepository.ReadAsync(c => c.EmployeeProfiles.SingleOrDefault(o => o.Id == id));
            if (!_securityHelper.IsCurrentUser(User, originalObject.Title)) {
                return default;
            }
            await LogHelper.CreateLog(_contactRepository, "Deleting Employee Photo " + originalObject.Id.ToString(), User.Identity.Name, "", "", id.ToString());

            var path = _fileHelper.DeletePhoto(originalObject.Title);

            await _contactRepository.UpdateAsync(new EmployeeProfile {
                Id = originalObject.Id,
                Title = originalObject.Title,
                Biography = originalObject.Biography,
                CVUrl = originalObject.CVUrl,
                PhotoUrl = PhotoHelper.DetermineImage(""),
                PreferredName = originalObject.PreferredName,
                PreferredNameLast = originalObject.PreferredNameLast,
                PreferredPronouns = originalObject.PreferredPronouns,
                IsPhoneHidden = originalObject.IsPhoneHidden,
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
                return await _contactRepository.CreateAsync(new EmployeeProfile {
                    Title = User.Identity.Name.Replace("@illinois.edu", ""),
                    Biography = jsonObject.biography,
                    PhotoUrl = PhotoHelper.DetermineImage(""),
                    IsActive = true
                });
            } else {
                var originalObject = await _contactRepository.ReadAsync(c => c.EmployeeProfiles.SingleOrDefault(o => o.Id == id));

                if (!_securityHelper.IsCurrentUser(User, originalObject.Title)) {
                    return default;
                }
                await LogHelper.CreateLog(_contactRepository, "Editing Employee " + originalObject.Id.ToString(), User.Identity.Name, JsonConvert.SerializeObject(originalObject).ToString(), json.ToString(), id.ToString());

                return await _contactRepository.UpdateAsync(new EmployeeProfile {
                    Id = originalObject.Id,
                    Title = originalObject.Title,
                    Biography = jsonObject.biography,
                    IsPhoneHidden = jsonObject.isPhoneHidden,
                    CVUrl = originalObject.CVUrl,
                    PrimaryProfile = originalObject.PrimaryProfile,
                    PhotoUrl = PhotoHelper.DetermineImage(originalObject.PhotoUrl),
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
            var originalObject = await _contactRepository.ReadAsync(c => c.EmployeeProfiles.SingleOrDefault(o => o.Id == id));
            if (!_securityHelper.IsCurrentUser(User, originalObject.Title)) {
                return default;
            }
            await LogHelper.CreateLog(_contactRepository, "Updating Employee CV " + originalObject.Id.ToString(), User.Identity.Name, "", "", id.ToString());

            var path = _fileHelper.AddCv(file.OpenReadStream(), originalObject.Title, file.FileName);

            await _contactRepository.UpdateAsync(new EmployeeProfile {
                Id = originalObject.Id,
                Title = originalObject.Title,
                Biography = originalObject.Biography,
                CVUrl = path.Result,
                PhotoUrl = PhotoHelper.DetermineImage(originalObject.PhotoUrl),
                PreferredName = originalObject.PreferredName,
                PreferredNameLast = originalObject.PreferredNameLast,
                PreferredPronouns = originalObject.PreferredPronouns,
                IsPhoneHidden = originalObject.IsPhoneHidden,
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
                var originalObject = await _contactRepository.ReadAsync(c => c.EmployeeProfiles.SingleOrDefault(o => o.Id == id));

                if (!_securityHelper.IsCurrentUser(User, originalObject.Title)) {
                    return default;
                }
                await LogHelper.CreateLog(_contactRepository, "Editing Employee from Job Profile " + originalObject.Id.ToString(), User.Identity.Name, JsonConvert.SerializeObject(originalObject).ToString(), json.ToString(), id.ToString());
                var biography = jsonObject.biography.ToString();
                if (!string.IsNullOrWhiteSpace(biography)) {
                    var originalJob = await _contactRepository.ReadAsync(c => c.JobProfiles.SingleOrDefault(o => o.Id == jobId));
                    originalJob.Biography = null;
                    _ = await _contactRepository.UpdateAsync(originalJob);
                }

                return await _contactRepository.UpdateAsync(new EmployeeProfile {
                    Id = originalObject.Id,
                    Title = originalObject.Title,
                    IsPhoneHidden = jsonObject.isPhoneHidden,
                    Biography = string.IsNullOrWhiteSpace(biography) ? originalObject.Biography : biography,
                    CVUrl = originalObject.CVUrl,
                    PrimaryProfile = originalObject.PrimaryProfile,
                    PhotoUrl = PhotoHelper.DetermineImage(originalObject.PhotoUrl),
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
            var originalObject = await _contactRepository.ReadAsync(c => c.EmployeeProfiles.SingleOrDefault(o => o.Id == id));
            if (!_securityHelper.IsCurrentUser(User, originalObject.Title)) {
                return default;
            }
            await LogHelper.CreateLog(_contactRepository, "Updating Employee Photo " + originalObject.Id.ToString(), User.Identity.Name, "", "", id.ToString());
            var area = await _contactRepository.ReadAsync(c => c.EmployeeProfiles
                .Include(c => c.Jobs).ThenInclude(j => j.Office).ThenInclude(o => o.Area)
                .FirstOrDefault(c => c.Id == id)
                .Jobs.FirstOrDefault().Office.Area);
            string errorMessage;
            var path = _fileHelper.AddPhoto(file.OpenReadStream(), originalObject.Title, file.FileName, area.PictureHeight, area.PictureWidth, out errorMessage);
            if (!string.IsNullOrWhiteSpace(errorMessage)) {
                return errorMessage + (string.IsNullOrWhiteSpace(path) ? "" : " (" + path + ")");
            }
            await _contactRepository.UpdateAsync(new EmployeeProfile {
                Id = originalObject.Id,
                Title = originalObject.Title,
                Biography = originalObject.Biography,
                CVUrl = originalObject.CVUrl,
                PhotoUrl = path,
                PreferredName = originalObject.PreferredName,
                PreferredNameLast = originalObject.PreferredNameLast,
                PreferredPronouns = originalObject.PreferredPronouns,
                IsPhoneHidden = originalObject.IsPhoneHidden,
                LastUpdated = DateTime.Now,
                IsActive = true
            });
            return path;
        }

        [HttpPost("UpdatePrimaryJob")]
        public async Task<int> UpdatePrimaryJob([FromBody] dynamic json) {
            var jsonObject = (dynamic) JObject.Parse(json.ToString());
            int id = int.Parse(jsonObject.id.ToString());
            var originalObject = await _contactRepository.ReadAsync(c => c.EmployeeProfiles.SingleOrDefault(o => o.Id == id));

            if (!_securityHelper.IsCurrentUser(User, originalObject.Title)) {
                return default;
            }
            await LogHelper.CreateLog(_contactRepository, "Editing Employee " + originalObject.Id.ToString(), User.Identity.Name, JsonConvert.SerializeObject(originalObject), json.ToString(), id.ToString());

            return await _contactRepository.UpdateAsync(new EmployeeProfile {
                Id = originalObject.Id,
                Title = originalObject.Title,
                Biography = originalObject.Biography,
                CVUrl = originalObject.CVUrl,
                PrimaryProfile = jsonObject.jobid,
                PhotoUrl = PhotoHelper.DetermineImage(originalObject.PhotoUrl),
                PreferredName = originalObject.PreferredName,
                PreferredNameLast = originalObject.PreferredNameLast,
                PreferredPronouns = originalObject.PreferredPronouns,
                IsPhoneHidden = originalObject.IsPhoneHidden,
                LastUpdated = DateTime.Now,
                IsActive = true
            });
        }
    }
}