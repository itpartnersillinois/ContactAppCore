using System.Linq;
using System.Threading.Tasks;
using ContactAppCore.Data;
using ContactAppCore.Data.Models;
using ContactAppCore.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactAppCore.Api {

    [Route("api/[controller]")]
    [ApiController]
    public class EditAreaController : ControllerBase {
        private IContactRepository contactRepository;
        private SecurityHelper securityHelper;

        public EditAreaController(IContactRepository contactRepository, SecurityHelper securityHelper) {
            this.contactRepository = contactRepository;
            this.securityHelper = securityHelper;
        }

        [HttpPost("Add")]
        public async Task<int> Add([FromForm] string title) {
            if (!securityHelper.IsFullAdmin(User)) {
                return default;
            }
            if (string.IsNullOrWhiteSpace(title)) {
                return default;
            }
            var area = new Area(title);
            await LogHelper.CreateLog(contactRepository, "Adding Area", User.Identity.Name, "", JsonConvert.SerializeObject(area));
            return await contactRepository.CreateAsync(area);
        }

        [HttpPost("AddOffice")]
        public async Task<int> AddOffice([FromForm] string title, [FromForm] int areaId) {
            if (!securityHelper.AllowArea(User, areaId)) {
                return default;
            }
            if (string.IsNullOrWhiteSpace(title)) {
                return default;
            }
            var office = new Office(title, areaId);
            await LogHelper.CreateLog(contactRepository, "Adding Office to Area " + areaId.ToString(), User.Identity.Name, "", JsonConvert.SerializeObject(office));
            return await contactRepository.CreateAsync(office);
        }

        [HttpGet("{id}")]
        public async Task<Area> Get(int id) {
            if (!securityHelper.AllowOffice(User, id)) {
                return default;
            }
            return await contactRepository.ReadAsync(c => c.Areas.SingleOrDefault(o => o.Id == id));
        }

        [HttpPost("Update")]
        public async Task<int> Update([FromBody] dynamic json) {
            var jsonObject = (dynamic) JObject.Parse(json.ToString());
            int id = int.Parse(jsonObject.id.ToString());
            if (!securityHelper.AllowArea(User, id)) {
                return default;
            }
            var originalObject = await contactRepository.ReadAsync(c => c.Areas.SingleOrDefault(a => a.Id == id));
            var isFullAdmin = securityHelper.IsFullAdmin(User);
            await LogHelper.CreateLog(contactRepository, "Updating Area " + originalObject.Id.ToString(), User.Identity.Name, JsonConvert.SerializeObject(originalObject), json.ToString());

            return await contactRepository.UpdateAsync(new Area {
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
                InternalOnly = isFullAdmin ? JsonHelper.TranslateBoolean(jsonObject.internalonly) : originalObject.InternalOnly,
                AllowBeta = isFullAdmin ? JsonHelper.TranslateBoolean(jsonObject.allowbeta) : originalObject.AllowBeta,
                AllowPeople = isFullAdmin ? JsonHelper.TranslateBoolean(jsonObject.allowpeople) : originalObject.AllowPeople,
                PeopleRefreshUrl = isFullAdmin ? jsonObject.peoplerefreshurl : originalObject.PeopleRefreshUrl,
            });
        }
    }
}