using ContactAppCore.Data;
using ContactAppCore.Helpers;
using ContactAppCore.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAppCore.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ContactController : ControllerBase
    {
        private IContactRepository contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        [HttpGet("Area/{id}")]
        public async Task<AreaInformation> GetArea(int id)
        {
            var area = await contactRepository.ReadAsync(c => c.Areas.Include(a => a.Offices).Where(o => o.IsActive).SingleOrDefault(o => o.Id == id));
            return new AreaInformation(area, true, false, "", null);
        }

        [HttpGet("AreaCode/{id}")]
        public async Task<AreaInformation> GetAreaCode(string id)
        {
            var area = await contactRepository.ReadAsync(c => c.Areas.Include(a => a.Offices).Where(a => a.IsActive).SingleOrDefault(a => a.InternalCode == id));
            return new AreaInformation(area, true, false, "", null);
        }

        [HttpGet("AreaCode/External/{id}")]
        public async Task<AreaInformation> GetAreaCodeExternal(string id)
        {
            var area = await contactRepository.ReadAsync(c => c.Areas.Include(a => a.Offices).Where(a => a.IsActive && !a.InternalOnly).SingleOrDefault(a => a.InternalCode == id));
            return new AreaInformation(area, false, false, "", null);
        }

        [HttpGet("Area/External/{id}")]
        public async Task<AreaInformation> GetAreaExternal(int id)
        {
            var area = await contactRepository.ReadAsync(c => c.Areas.Include(a => a.Offices).Where(o => o.IsActive && !o.InternalOnly).SingleOrDefault(o => o.Id == id));
            return new AreaInformation(area, false, false, "", null);
        }

        [HttpGet("Office/{id}")]
        public async Task<OfficeInformation> GetOffice(int id)
        {
            var office = await contactRepository.ReadAsync(c => c.Offices.Include(a => a.Area).Where(o => o.IsActive).SingleOrDefault(o => o.Id == id));
            return new OfficeInformation(office);
        }

        [HttpGet("OfficeCode/{id}")]
        public async Task<OfficeInformation> GetOfficeCode(string id)
        {
            var office = await contactRepository.ReadAsync(c => c.Offices.Include(a => a.Area).Where(o => o.IsActive).SingleOrDefault(o => o.InternalCode == id));
            return new OfficeInformation(office);
        }

        [HttpGet("Search/Areas")]
        public async Task<IEnumerable<AreaInformation>> SearchAreas(string search = "", string areafilter = "", string officefilter = "", bool isCovid = false)
        {
            var areaType = FilterHelper.TranslateArea(areafilter);
            var officeType = FilterHelper.TranslateOffice(officefilter);
            var areas = await contactRepository.ReadAsync(c => c.Areas.Include(a => a.Offices).Where(a => a.IsActive
                && (search == "" || a.Title.Contains(search) || a.SearchTerms.Contains(search) || a.Audience.Contains(search) ||
                a.Offices.Any(o => o.Title.Contains(search)) || a.Offices.Any(o => o.SearchTerms.Contains(search)) || a.Offices.Any(o => o.Audience.Contains(search)))
                && (areaType == null || a.AreaType == areaType) && (a.Offices.Any(o => o.CovidSupport) || !isCovid))
                .OrderBy(a => a.InternalOrder).ThenBy(a => a.Title));
            return areas.ToList().Select(a => new AreaInformation(a, true, isCovid, search, officeType));
        }

        [HttpGet("Search/Areas/External")]
        public async Task<IEnumerable<AreaInformation>> SearchAreasExternal(string search = "", string areafilter = "", string officefilter = "", bool isCovid = false)
        {
            var areaType = FilterHelper.TranslateArea(areafilter);
            var officeType = FilterHelper.TranslateOffice(officefilter);
            var areas = await contactRepository.ReadAsync(c => c.Areas.Include(a => a.Offices).Where(a => a.IsActive && !a.InternalOnly
                && (search == "" || a.Title.Contains(search) || a.SearchTerms.Contains(search) || a.Audience.Contains(search) ||
                a.Offices.Any(o => o.Title.Contains(search)) || a.Offices.Any(o => o.SearchTerms.Contains(search)) || a.Offices.Any(o => o.Audience.Contains(search)))
                && (areaType == null || a.AreaType == areaType) && (a.Offices.Any(o => o.CovidSupport) || !isCovid))
            .OrderBy(a => a.InternalOrder).ThenBy(a => a.Title));
            return areas.ToList().Select(a => new AreaInformation(a, false, isCovid, search, officeType));
        }

        [HttpGet("Search/Offices")]
        public async Task<IEnumerable<OfficeInformation>> SearchOffices(string search = "", string areafilter = "", string officefilter = "", bool isCovid = false)
        {
            var areaType = FilterHelper.TranslateArea(areafilter);
            var officeType = FilterHelper.TranslateOffice(officefilter);
            var offices = await contactRepository.ReadAsync(c => c.Offices.Include(a => a.Area).Where(o => o.IsActive
                && (search == "" || o.Title.Contains(search) || o.SearchTerms.Contains(search) || o.Audience.Contains(search) ||
                o.Area.Title.Contains(search) || o.Area.SearchTerms.Contains(search) || o.Area.Audience.Contains(search))
                && (areaType == null || o.Area.AreaType == areaType)
                && (officeType == null || o.OfficeType == officeType) && (o.CovidSupport || !isCovid))
            .OrderBy(o => o.Title));
            return offices.ToList().Select(o => new OfficeInformation(o));
        }

        [HttpGet("Search/Offices/ByArea")]
        public async Task<IEnumerable<OfficeInformation>> SearchOfficesByArea(int areaId, string search = "", string officefilter = "")
        {
            var officeType = FilterHelper.TranslateOffice(officefilter);
            var offices = await contactRepository.ReadAsync(c => c.Offices.Include(a => a.Area).Where(o => o.IsActive
                && (search == "" || o.Title.Contains(search) || o.SearchTerms.Contains(search) || o.Audience.Contains(search) ||
                o.Area.Title.Contains(search) || o.Area.SearchTerms.Contains(search) || o.Area.Audience.Contains(search))
                && (o.AreaId == areaId)
                && (officeType == null || o.OfficeType == officeType))
            .OrderBy(o => o.InternalOrder).ThenBy(o => o.Title));
            return offices.ToList().Select(o => new OfficeInformation(o));
        }

        [HttpGet("Search/Offices/ByAreaCode")]
        public async Task<IEnumerable<OfficeInformation>> SearchOfficesByArea(string search, string areaCode, string officefilter)
        {
            var officeType = FilterHelper.TranslateOffice(officefilter);
            var offices = await contactRepository.ReadAsync(c => c.Offices.Include(a => a.Area).Where(o => o.IsActive
                && (search == "" || o.Title.Contains(search) || o.SearchTerms.Contains(search) || o.Audience.Contains(search) ||
                o.Area.Title.Contains(search) || o.Area.SearchTerms.Contains(search) || o.Area.Audience.Contains(search))
                && (o.Area.InternalCode == areaCode)
                && (officeType == null || o.OfficeType == officeType))
            .OrderBy(o => o.InternalOrder).ThenBy(o => o.Title));
            return offices.ToList().Select(o => new OfficeInformation(o));
        }
    }
}