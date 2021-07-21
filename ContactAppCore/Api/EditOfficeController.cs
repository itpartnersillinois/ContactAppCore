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
    public class EditOfficeController : ControllerBase
    {
        private IContactRepository contactRepository;
        private SecurityHelper securityHelper;

        public EditOfficeController(IContactRepository contactRepository, SecurityHelper securityHelper)
        {
            this.contactRepository = contactRepository;
            this.securityHelper = securityHelper;
        }

        [HttpGet("{id}")]
        public async Task<Office> Get(int id)
        {
            if (!securityHelper.AllowOffice(User, id).Result)
            {
                return default;
            }
            return await contactRepository.ReadAsync(c => c.Offices.SingleOrDefault(o => o.Id == id));
        }

        [HttpPost("Update")]
        public async Task<int> Update([FromBody] dynamic json)
        {
            var jsonObject = (dynamic)JObject.Parse(json.ToString());
            int id = int.Parse(jsonObject.id.ToString());
            if (!securityHelper.AllowOffice(User, id).Result)
            {
                return default;
            }
            var originalObject = await contactRepository.ReadAsync(c => c.Offices.SingleOrDefault(o => o.Id == id));

            await contactRepository.CreateAsync(new Log { IsActive = true, Title = originalObject.AreaId.ToString(), Name = User.Identity.Name, OldData = JsonConvert.SerializeObject(originalObject), NewData = json.ToString() });

            return await contactRepository.UpdateAsync(new Office
            {
                Id = id,
                AreaId = originalObject.AreaId,
                Title = jsonObject.title,
                Audience = jsonObject.audience,
                OfficeType = jsonObject.officetype,
                Email = jsonObject.email,
                ExternalUrl = jsonObject.externalurl,
                InternalUrl = jsonObject.internalurl,
                TicketUrl = jsonObject.ticketurl,
                Phone = jsonObject.phone,
                Room = jsonObject.room,
                Building = jsonObject.building,
                BuildingCode = jsonObject.buildingcode,
                Address = jsonObject.address,
                City = jsonObject.city,
                ZipCode = jsonObject.zipcode,
                HoursSundayStart = jsonObject.hourssun1,
                HoursSundayEnd = jsonObject.hourssun2,
                HoursMondayStart = jsonObject.hoursmon1,
                HoursMondayEnd = jsonObject.hoursmon2,
                HoursTuesdayStart = jsonObject.hourstue1,
                HoursTuesdayEnd = jsonObject.hourstue2,
                HoursWednesdayStart = jsonObject.hourswed1,
                HoursWednesdayEnd = jsonObject.hourswed2,
                HoursThursdayStart = jsonObject.hoursthu1,
                HoursThursdayEnd = jsonObject.hoursthu2,
                HoursFridayStart = jsonObject.hoursfri1,
                HoursFridayEnd = jsonObject.hoursfri2,
                HoursSaturdayStart = jsonObject.hourssat1,
                HoursSaturdayEnd = jsonObject.hourssat2,
                HoursMessage = jsonObject.hournotes,
                HoursIncludeHolidayMessage = JsonHelper.TranslateBoolean(jsonObject.hoursincludeholidaymessage),
                Notes = jsonObject.notes,
                SearchTerms = jsonObject.searchterm,
                InternalNotes = jsonObject.internalnotes,
                InternalCode = jsonObject.internalcode,
                InternalOrder = jsonObject.internalorder,
                IsActive = JsonHelper.TranslateBoolean(jsonObject.isactive),
                InternalOnly = JsonHelper.TranslateBoolean(jsonObject.internalonly)
            });
        }
    }
}