using System.Linq;
using System.Threading.Tasks;
using ContactAppCore.CampusService;
using ContactAppCore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactAppCore.Api {

    [Route("api/[controller]")]
    [ApiController]
    public class SignatureController : Controller {
        private readonly IContactRepository _contactRepository;
        private readonly DataWarehouseManager _dataWarehouseManager;

        public SignatureController(IContactRepository contactRepository, DataWarehouseManager dataWarehouseManager) {
            _contactRepository = contactRepository;
            _dataWarehouseManager = dataWarehouseManager;
        }

        public async Task<string> Index() {
            var netId = User.Identity.Name.Replace("@illinois.edu", "");
            var profile = await _contactRepository.ReadAsync(c => c.EmployeeProfiles.Include(e => e.Jobs).SingleOrDefault(e => e.Title == netId));
            var job = profile.Jobs?.Count > 1 && profile.PrimaryProfile.HasValue ? profile.Jobs.SingleOrDefault(j => j.Id == profile.PrimaryProfile) : profile.Jobs?.FirstOrDefault();
            var office = await _contactRepository.ReadAsync(c => c.Offices.Include(o => o.Area).SingleOrDefault(o => o.Id == job.OfficeId));
            if (profile != null && job != null && office != null && office.Area != null) {
                var item = _dataWarehouseManager.GetDataWarehouseItem(netId);
                var url = "https://webservices.illinois.edu/webservices/js/ds/signature.html?";
                url += Set("name", profile.GenerateSignatureName(), true);
                url += Set("department1", office.Title);
                url += Set("department2", "");
                url += Set("role1", job.Title);
                url += Set("role2", "");
                url += Set("address1", item.AddressLine1);
                url += Set("address2", item.AddressLine2);
                url += Set("cityStateZip", item.CityStateZip);
                url += Set("phone", item.PhoneFull);
                url += Set("email", User.Identity.Name);
                url += Set("website1", office.Area.ExternalUrl);
                url += Set("campus", office.Area.Title);
                url += office.Area.SignatureExtension;
                return url;
            }
            return "";
        }

        private string Set(string name, object value, bool isFirst = false) {
            return (isFirst ? "" : "&") + (value == null ? name + "=" : name + "=" + value.ToString().Replace("&", "&amp;"));
        }
    }
}