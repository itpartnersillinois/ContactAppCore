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
            await contactRepository.CreateAsync(new Log { IsActive = true, Title = "Employee CV Delete " + originalObject.Id.ToString() });

            var path = fileHelper.DeleteCv(User.Identity.Name);

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
            await contactRepository.CreateAsync(new Log { IsActive = true, Title = "Employee Photo Delete " + originalObject.Id.ToString() });

            var path = fileHelper.DeletePhoto(User.Identity.Name);

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
                await contactRepository.CreateAsync(new Log { IsActive = true, Title = "Employee " + originalObject.Id.ToString(), Name = User.Identity.Name, OldData = JsonConvert.SerializeObject(originalObject), NewData = json.ToString() });

                return await contactRepository.UpdateAsync(new EmployeeProfile
                {
                    Id = originalObject.Id,
                    Title = originalObject.Title,
                    Biography = jsonObject.biography,
                    CVUrl = originalObject.CVUrl,
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
            await contactRepository.CreateAsync(new Log { IsActive = true, Title = "Employee CV " + originalObject.Id.ToString(), Name = User.Identity.Name });

            var path = fileHelper.AddCv(file.OpenReadStream(), User.Identity.Name, file.FileName);

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
            await contactRepository.CreateAsync(new Log { IsActive = true, Title = "Employee Photo " + originalObject.Id.ToString(), Name = User.Identity.Name });

            var path = fileHelper.AddPhoto(file.OpenReadStream(), User.Identity.Name, file.FileName);
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
    }
}