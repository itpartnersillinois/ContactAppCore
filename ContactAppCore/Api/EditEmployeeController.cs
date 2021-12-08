﻿using ContactAppCore.Data;
using ContactAppCore.Data.Models;
using ContactAppCore.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactAppCore.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditEmployeeController : ControllerBase
    {
        private IContactRepository contactRepository;
        private FileHelper fileHelper;
        private SecurityHelper securityHelper;

        public EditEmployeeController(IContactRepository contactRepository, SecurityHelper securityHelper, FileHelper fileHelper)
        {
            this.contactRepository = contactRepository;
            this.securityHelper = securityHelper;
            this.fileHelper = fileHelper;
        }

        [HttpPost("DeleteCV")]
        public async Task<IActionResult> DeleteCv([FromForm] int id)
        {
            var originalObject = await contactRepository.ReadAsync(c => c.EmployeeProfiles.SingleOrDefault(o => o.Id == id));
            if (!securityHelper.IsCurrentUser(User, originalObject.Title))
            {
                return default;
            }
            await LogHelper.CreateLog(contactRepository, "Deleting Employee CV " + originalObject.Id.ToString(), User.Identity.Name);

            var path = fileHelper.DeleteCv(originalObject.Title);

            await contactRepository.UpdateAsync(new EmployeeProfile
            {
                Id = originalObject.Id,
                Title = originalObject.Title,
                Biography = originalObject.Biography,
                CVUrl = "",
                PhotoUrl = originalObject.PhotoUrl,
                LastUpdated = DateTime.Now,
                IsActive = true
            });

            return new OkObjectResult("");
        }

        [HttpPost("DeletePicture")]
        public async Task<IActionResult> DeletePicture([FromForm] int id)
        {
            var originalObject = await contactRepository.ReadAsync(c => c.EmployeeProfiles.SingleOrDefault(o => o.Id == id));
            if (!securityHelper.IsCurrentUser(User, originalObject.Title))
            {
                return default;
            }
            await LogHelper.CreateLog(contactRepository, "Deleting Employee Photo " + originalObject.Id.ToString(), User.Identity.Name);

            var path = fileHelper.DeletePhoto(originalObject.Title);

            await contactRepository.UpdateAsync(new EmployeeProfile
            {
                Id = originalObject.Id,
                Title = originalObject.Title,
                Biography = originalObject.Biography,
                CVUrl = originalObject.CVUrl,
                PhotoUrl = "",
                LastUpdated = DateTime.Now,
                IsActive = true
            });

            return new OkObjectResult("");
        }

        [HttpPost("Update")]
        public async Task<int> UpdateBiography([FromBody] dynamic json)
        {
            var jsonObject = (dynamic)JObject.Parse(json.ToString());
            int id = int.Parse(jsonObject.id.ToString());
            if (id == 0)
            {
                return await contactRepository.CreateAsync(new EmployeeProfile
                {
                    Title = User.Identity.Name.Replace("@illinois.edu", ""),
                    Biography = jsonObject.biography,
                    IsActive = true
                });
            }
            else
            {
                var originalObject = await contactRepository.ReadAsync(c => c.EmployeeProfiles.SingleOrDefault(o => o.Id == id));

                if (!securityHelper.IsCurrentUser(User, originalObject.Title))
                {
                    return default;
                }
                await LogHelper.CreateLog(contactRepository, "Editing Employee " + originalObject.Id.ToString(), User.Identity.Name, JsonConvert.SerializeObject(originalObject).ToString(), json.ToString());

                return await contactRepository.UpdateAsync(new EmployeeProfile
                {
                    Id = originalObject.Id,
                    Title = originalObject.Title,
                    Biography = jsonObject.biography,
                    CVUrl = originalObject.CVUrl,
                    PrimaryProfile = originalObject.PrimaryProfile,
                    PhotoUrl = originalObject.PhotoUrl,
                    LastUpdated = DateTime.Now,
                    IsActive = true
                });
            }
        }

        [HttpPost("CV")]
        public async Task<string> UpdateCv([FromForm] IFormFile file, [FromForm] int id)
        {
            var originalObject = await contactRepository.ReadAsync(c => c.EmployeeProfiles.SingleOrDefault(o => o.Id == id));
            if (!securityHelper.IsCurrentUser(User, originalObject.Title))
            {
                return default;
            }
            await LogHelper.CreateLog(contactRepository, "Updating Employee CV " + originalObject.Id.ToString(), User.Identity.Name);

            var path = fileHelper.AddCv(file.OpenReadStream(), originalObject.Title, file.FileName);

            await contactRepository.UpdateAsync(new EmployeeProfile
            {
                Id = originalObject.Id,
                Title = originalObject.Title,
                Biography = originalObject.Biography,
                CVUrl = path.Result,
                PhotoUrl = originalObject.PhotoUrl,
                LastUpdated = DateTime.Now,
                IsActive = true
            });

            return path.Result;
        }

        [HttpPost("Picture")]
        public async Task<string> UpdatePicture([FromForm] IFormFile file, [FromForm] int id)
        {
            var originalObject = await contactRepository.ReadAsync(c => c.EmployeeProfiles.SingleOrDefault(o => o.Id == id));
            if (!securityHelper.IsCurrentUser(User, originalObject.Title))
            {
                return default;
            }
            await LogHelper.CreateLog(contactRepository, "Updating Employee Photo " + originalObject.Id.ToString(), User.Identity.Name);

            var path = fileHelper.AddPhoto(file.OpenReadStream(), originalObject.Title, file.FileName);
            if (path.Result == "")
            {
                return "Error - File is not sized correctly";
            }

            await contactRepository.UpdateAsync(new EmployeeProfile
            {
                Id = originalObject.Id,
                Title = originalObject.Title,
                Biography = originalObject.Biography,
                CVUrl = originalObject.CVUrl,
                PhotoUrl = path.Result,
                LastUpdated = DateTime.Now,
                IsActive = true
            });

            return path.Result;
        }

        [HttpPost("UpdatePrimaryJob")]
        public async Task<int> UpdatePrimaryJob([FromBody] dynamic json)
        {
            var jsonObject = (dynamic)JObject.Parse(json.ToString());
            int id = int.Parse(jsonObject.id.ToString());
            var originalObject = await contactRepository.ReadAsync(c => c.EmployeeProfiles.SingleOrDefault(o => o.Id == id));

            if (!securityHelper.IsCurrentUser(User, originalObject.Title))
            {
                return default;
            }
            await LogHelper.CreateLog(contactRepository, "Editing Employee " + originalObject.Id.ToString(), User.Identity.Name, JsonConvert.SerializeObject(originalObject), json.ToString());

            return await contactRepository.UpdateAsync(new EmployeeProfile
            {
                Id = originalObject.Id,
                Title = originalObject.Title,
                Biography = originalObject.Biography,
                CVUrl = originalObject.CVUrl,
                PrimaryProfile = jsonObject.jobid,
                PhotoUrl = originalObject.PhotoUrl,
                LastUpdated = DateTime.Now,
                IsActive = true
            });
        }
    }
}