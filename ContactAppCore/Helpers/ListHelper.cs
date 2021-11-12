using ContactAppCore.Data;
using ContactAppCore.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ContactAppCore.Helpers
{
    public class ListHelper
    {
        private IContactRepository contactRepository;

        public ListHelper(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public async Task<ItemList> GetAreaList(ClaimsPrincipal claim, int areaId)
        {
            if (claim == null || claim.Identity == null || string.IsNullOrWhiteSpace(claim.Identity.Name))
            {
                return new ItemList { IsDeniedAccess = true };
            }
            var name = claim.Identity.Name;
            var isAdmin = await contactRepository.ReadAsync(c => c.People.Any(c => c.Title == name && (c.IsFullAdmin || c.AreaId == areaId) && c.IsActive));
            if (isAdmin)
            {
                var offices = await contactRepository.ReadAsync(c => c.Offices
                    .Where(c => c.AreaId == areaId)
                    .Select(c => new AreaOfficeItem { Id = c.Id, Title = c.Title }).OrderBy(a => a.Title));

                return new ItemList
                {
                    IsDeniedAccess = false,
                    IsFullAdmin = isAdmin,
                    Areas = null,
                    Offices = offices.ToList()
                };
            }
            else
            {
                var offices = await contactRepository.ReadAsync(c => c.People
                    .Join(c.Offices, p => p.OfficeId, o => o.Id, (p, o) => new { p, o })
                    .Where(c => c.p.Title == name && c.p.IsActive && c.o.AreaId == areaId)
                    .Select(c => new AreaOfficeItem { Id = c.o.Id, Title = c.o.Title }));

                return new ItemList
                {
                    IsDeniedAccess = false,
                    IsFullAdmin = isAdmin,
                    Areas = null,
                    Offices = offices.ToList()
                };
            }
        }

        public async Task<ItemList> GetList(ClaimsPrincipal claim)
        {
            if (claim == null || claim.Identity == null || string.IsNullOrWhiteSpace(claim.Identity.Name))
            {
                return new ItemList { IsDeniedAccess = true };
            }
            var name = claim.Identity.Name;
            var isAdmin = await contactRepository.ReadAsync(c => c.People.Any(c => c.Title == name && c.IsFullAdmin && c.IsActive));
            if (isAdmin)
            {
                var areas = await contactRepository.ReadAsync(c => c.Areas
                    .Select(c => new AreaOfficeItem { Id = c.Id, Title = c.Title }).OrderBy(a => a.Title));
                var offices = await contactRepository.ReadAsync(c => c.Offices
                    .Select(c => new AreaOfficeItem { Id = c.Id, Title = c.Title }).OrderBy(o => o.Title));

                return new ItemList
                {
                    IsDeniedAccess = false,
                    IsFullAdmin = isAdmin,
                    Areas = areas.ToList(),
                    Offices = offices.ToList()
                };
            }
            else
            {
                var areas = await contactRepository.ReadAsync(c => c.People
                    .Join(c.Areas, p => p.AreaId, a => a.Id, (p, a) => new { p, a })
                    .Where(c => c.p.Title == name && c.p.IsActive)
                    .Select(c => new AreaOfficeItem { Id = c.a.Id, Title = c.a.Title }).OrderBy(a => a.Title));
                var offices = await contactRepository.ReadAsync(c => c.People
                    .Join(c.Offices, p => p.OfficeId, o => o.Id, (p, o) => new { p, o })
                    .Join(c.Areas, co => co.o.AreaId, a => a.Id, (co, a) => new { co, a })
                    .Where(c => c.co.p.Title == name && c.co.p.IsActive)
                    .Select(c => new AreaOfficeItem { Id = c.co.o.Id, Title = $"{c.a.Title} {c.co.o.Title}" }).OrderBy(o => o.Title));

                return new ItemList
                {
                    IsDeniedAccess = false,
                    IsFullAdmin = isAdmin,
                    Areas = areas.ToList(),
                    Offices = offices.ToList()
                };
            }
        }
    }
}