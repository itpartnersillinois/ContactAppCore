﻿using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ContactAppCore.Data;
using Microsoft.EntityFrameworkCore;

namespace ContactAppCore.Helpers {

    public class SecurityHelper {
        private IContactRepository contactRepository;

        public SecurityHelper(IContactRepository contactRepository) {
            this.contactRepository = contactRepository;
        }

        public bool AllowArea(ClaimsPrincipal claim, int id) {
            if (claim == null || claim.Identity == null || string.IsNullOrWhiteSpace(claim.Identity.Name)) {
                return false;
            }
            return contactRepository.Read(c => c.People.Any(p => p.Title == claim.Identity.Name && p.IsActive && (p.IsFullAdmin || p.AreaId == id)));
        }

        public async Task<bool> AllowAreaForOffice(ClaimsPrincipal claim, int id) {
            if (claim == null || claim.Identity == null || string.IsNullOrWhiteSpace(claim.Identity.Name)) {
                return false;
            }
            var areaId = contactRepository.Read(c => c.Offices.Single(o => o.Id == id).AreaId);
            return await contactRepository.ReadAsync(c => c.People.Any(p => p.Title == claim.Identity.Name && p.IsActive && (p.IsFullAdmin || p.AreaId == areaId)));
        }

        public bool AllowOffice(ClaimsPrincipal claim, int id) {
            if (claim == null || claim.Identity == null || string.IsNullOrWhiteSpace(claim.Identity.Name)) {
                return false;
            }
            var areaId = contactRepository.Read(c => c.Offices.Single(o => o.Id == id).AreaId);
            return contactRepository.Read(c => c.People.Any(p => p.Title == claim.Identity.Name && p.IsActive && (p.IsFullAdmin || p.AreaId == areaId || p.OfficeId == id)));
        }

        public bool AllowProfileEdit(ClaimsPrincipal claim, int id) {
            if (claim == null || claim.Identity == null || string.IsNullOrWhiteSpace(claim.Identity.Name)) {
                return false;
            }
            return contactRepository.Read(c => c.People.Any(p => p.Title == claim.Identity.Name && p.IsActive &&
                (p.IsFullAdmin || (p.CanEditAllPeopleInUnit && !p.OfficeId.HasValue) || (p.CanEditAllPeopleInUnit && p.OfficeId == id))));
        }

        public bool IsCurrentUser(ClaimsPrincipal claim, string username) {
            if (claim == null || claim.Identity == null || string.IsNullOrWhiteSpace(claim.Identity.Name)) {
                return false;
            }
            if (claim.Identity.Name == username + "@illinois.edu") {
                return true;
            }
            var officeIds = contactRepository.Read(c => c.EmployeeProfiles.Include(e => e.Jobs).Where(p => p.Title == username && p.IsActive).SelectMany(p => p.Jobs.Select(j => j.OfficeId))).ToList();
            return contactRepository.Read(c => c.People.Any(p => p.Title == claim.Identity.Name && p.IsActive && (p.IsFullAdmin || (p.CanEditAllPeopleInUnit && !p.OfficeId.HasValue) || (p.CanEditAllPeopleInUnit && officeIds.Contains(p.OfficeId ?? 0)))));
        }

        public bool IsFullAdmin(ClaimsPrincipal claim) {
            if (claim == null || claim.Identity == null || string.IsNullOrWhiteSpace(claim.Identity.Name)) {
                return false;
            }
            return contactRepository.Read(c => c.People.Any(p => p.Title == claim.Identity.Name && p.IsActive && p.IsFullAdmin));
        }
    }
}