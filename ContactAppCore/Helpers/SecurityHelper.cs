using ContactAppCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ContactAppCore.Helpers
{
    public class SecurityHelper
    {
        private IContactRepository contactRepository;

        public SecurityHelper(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public async Task<bool> AllowArea(ClaimsPrincipal claim, int id)
        {
            if (claim == null || claim.Identity == null || string.IsNullOrWhiteSpace(claim.Identity.Name))
            {
                return false;
            }
            return await contactRepository.ReadAsync(c => c.People.Any(p => p.Title == claim.Identity.Name && p.IsActive && (p.IsFullAdmin || p.AreaId == id)));
        }

        public async Task<bool> AllowOffice(ClaimsPrincipal claim, int id)
        {
            if (claim == null || claim.Identity == null || string.IsNullOrWhiteSpace(claim.Identity.Name))
            {
                return false;
            }
            var areaId = contactRepository.Read(c => c.Offices.Single(o => o.Id == id).AreaId);
            return await contactRepository.ReadAsync(c => c.People.Any(p => p.Title == claim.Identity.Name && p.IsActive && (p.IsFullAdmin || p.AreaId == areaId || p.OfficeId == id)));
        }

        public async Task<bool> IsFullAdmin(ClaimsPrincipal claim)
        {
            if (claim == null || claim.Identity == null || string.IsNullOrWhiteSpace(claim.Identity.Name))
            {
                return false;
            }
            return await contactRepository.ReadAsync(c => c.People.Any(p => p.Title == claim.Identity.Name && p.IsActive && p.IsFullAdmin));
        }
    }
}