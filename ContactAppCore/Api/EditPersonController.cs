using ContactAppCore.Data;
using ContactAppCore.Data.Models;
using ContactAppCore.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactAppCore.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditPersonController : ControllerBase
    {
        private IContactRepository contactRepository;
        private SecurityHelper securityHelper;

        public EditPersonController(IContactRepository contactRepository, SecurityHelper securityHelper)
        {
            this.contactRepository = contactRepository;
            this.securityHelper = securityHelper;
        }

        [HttpPost("AddFullAdmin")]
        public async Task<int> AddFullAdmin([FromForm] string name)
        {
            if (!securityHelper.IsFullAdmin(User).Result)
            {
                return default;
            }
            return await contactRepository.CreateAsync(new Person(name));
        }

        [HttpPost("AddToArea")]
        public async Task<int> AddToArea(string name, int areaId)
        {
            if (!securityHelper.AllowArea(User, areaId).Result)
            {
                return default;
            }
            return await contactRepository.CreateAsync(new Person(name, areaId));
        }

        [HttpPost("AddToOffice")]
        public async Task<int> AddToOffice(string name, int officeId)
        {
            if (!securityHelper.AllowOffice(User, officeId).Result)
            {
                return default;
            }
            return await contactRepository.CreateAsync(new Person(name, officeId));
        }
    }
}