using System.Linq;
using System.Threading.Tasks;
using ContactAppCore.Data;
using ContactAppCore.Data.Models;
using ContactAppCore.Helpers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactAppCore.Api {

    [Route("api/[controller]")]
    [ApiController]
    public class EditPersonController : ControllerBase {
        private IContactRepository contactRepository;
        private SecurityHelper securityHelper;

        public EditPersonController(IContactRepository contactRepository, SecurityHelper securityHelper) {
            this.contactRepository = contactRepository;
            this.securityHelper = securityHelper;
        }

        [HttpPost("AddFullAdmin")]
        public async Task<int> AddFullAdmin([FromForm] string name) {
            if (!securityHelper.IsFullAdmin(User)) {
                return default;
            }
            await LogHelper.CreateLog(contactRepository, "Security Add Full Admin", User.Identity.Name, "", name);
            return await contactRepository.CreateAsync(new Person(name));
        }

        [HttpPost("AddToArea")]
        public async Task<int> AddToArea([FromForm] string name, [FromForm] int areaId) {
            if (!securityHelper.AllowArea(User, areaId)) {
                return default;
            }
            await LogHelper.CreateLog(contactRepository, "Security Add Admin To Area " + areaId, User.Identity.Name, "", name);
            return await contactRepository.CreateAsync(new Person(name, areaId));
        }

        [HttpPost("AddToOffice")]
        public async Task<int> AddToOffice([FromForm] string name, [FromForm] int officeId) {
            if (!securityHelper.AllowOffice(User, officeId)) {
                return default;
            }
            await LogHelper.CreateLog(contactRepository, "Security Add Admin To Office " + officeId, User.Identity.Name, "", name);
            return await contactRepository.CreateAsync(new Person(name, null, officeId, true));
        }

        [HttpPost("AddToOfficeLimited")]
        public async Task<int> AddToOfficeLimited([FromForm] string name, [FromForm] int officeId) {
            if (!securityHelper.AllowOffice(User, officeId)) {
                return default;
            }
            await LogHelper.CreateLog(contactRepository, "Security Add Limited Admin To Office " + officeId, User.Identity.Name, "", name);
            return await contactRepository.CreateAsync(new Person(name, null, officeId));
        }

        [HttpPost("Delete/{id}")]
        public async Task<int> Delete(int id) {
            // TODO Open this up to more people, but still restrict user information
            if (!securityHelper.IsFullAdmin(User)) {
                return default;
            }
            var person = await contactRepository.ReadAsync(c => c.People.FirstOrDefault(p => p.Id == id));
            if (person == null) {
                return default;
            }
            await LogHelper.CreateLog(contactRepository, "Security Delete Admin", User.Identity.Name, person.Title);
            return await contactRepository.DeleteAsync(new Person { Id = id });
        }
    }
}