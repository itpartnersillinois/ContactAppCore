using ContactAppCore.Data;
using ContactAppCore.Data.Models;
using ContactAppCore.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactAppCore.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditAreaController : ControllerBase
    {
        private IContactRepository contactRepository;
        private SecurityHelper securityHelper;

        public EditAreaController(IContactRepository contactRepository, SecurityHelper securityHelper)
        {
            this.contactRepository = contactRepository;
            this.securityHelper = securityHelper;
        }

        [HttpPost("Add")]
        public async Task<int> Add([FromForm] string title)
        {
            if (!securityHelper.IsFullAdmin(User).Result)
            {
                return default;
            }
            var area = new Area(title);
            await contactRepository.CreateAsync(new Log { IsActive = true, Title = "New Area", Name = User.Identity.Name, NewData = JsonConvert.SerializeObject(area) });
            return await contactRepository.CreateAsync(area);
        }

        [HttpPost("AddOffice")]
        public async Task<int> AddOffice([FromForm] string title, [FromForm] int areaId)
        {
            if (!securityHelper.AllowArea(User, areaId).Result)
            {
                return default;
            }
            var office = new Office(title, areaId);
            await contactRepository.CreateAsync(new Log { IsActive = true, Title = areaId.ToString(), Name = User.Identity.Name, NewData = JsonConvert.SerializeObject(office) });
            return await contactRepository.CreateAsync(office);
        }

        [HttpGet("{id}")]
        public async Task<Area> Get(int id)
        {
            if (!securityHelper.AllowOffice(User, id).Result)
            {
                return default;
            }
            return await contactRepository.ReadAsync(c => c.Areas.SingleOrDefault(o => o.Id == id));
        }

        [HttpPost("Update")]
        public async Task<int> Update([FromBody] dynamic json)
        {
            var jsonObject = (dynamic)JObject.Parse(json.ToString());
            int id = int.Parse(jsonObject.id.ToString());
            if (!securityHelper.AllowArea(User, id).Result)
            {
                return default;
            }
            var originalObject = await contactRepository.ReadAsync(c => c.Areas.SingleOrDefault(a => a.Id == id));
            var isFullAdmin = securityHelper.IsFullAdmin(User).Result;
            await contactRepository.CreateAsync(new Log { IsActive = true, Title = originalObject.Id.ToString(), Name = User.Identity.Name, OldData = JsonConvert.SerializeObject(originalObject), NewData = json.ToString() });

            return await contactRepository.UpdateAsync(new Area
            {
                Id = id,
                Title = jsonObject.title,
                Audience = jsonObject.audience,
                AreaType = jsonObject.areatype,
                ExternalUrl = jsonObject.externalurl,
                InternalUrl = jsonObject.internalurl,
                Notes = jsonObject.notes,
                SearchTerms = jsonObject.searchterm,
                InternalNotes = jsonObject.internalnotes,
                InternalCode = jsonObject.internalcode,
                InternalOrder = isFullAdmin ? jsonObject.internalorder : originalObject.InternalOrder,
                IsActive = JsonHelper.TranslateBoolean(jsonObject.isactive),
                InternalOnly = JsonHelper.TranslateBoolean(jsonObject.internalonly),
                AllowBeta = isFullAdmin ? JsonHelper.TranslateBoolean(jsonObject.allowbeta) : originalObject.AllowBeta,
                AllowPeople = isFullAdmin ? JsonHelper.TranslateBoolean(jsonObject.allowpeople) : originalObject.AllowPeople,
            });
        }
    }
}