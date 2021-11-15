﻿using ContactAppCore.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContactAppCore.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private SecurityHelper securityHelper;

        public SecurityController(SecurityHelper securityHelper)
        {
            this.securityHelper = securityHelper;
        }

        [HttpGet("Admin")]
        public async Task<bool> GetAdmin()
        {
            return await securityHelper.IsFullAdmin(User);
        }

        [HttpGet("Area/{officeId}")]
        public async Task<bool> GetArea(int officeId)
        {
            return await securityHelper.AllowAreaForOffice(User, officeId);
        }
    }
}