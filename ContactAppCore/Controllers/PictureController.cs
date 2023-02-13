using System.Linq;
using System.Threading.Tasks;
using ContactAppCore.Data;
using ContactAppCore.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ContactAppCore.Controllers {

    [Route("[controller]")]
    public class PictureController : Controller {
        private IContactRepository contactRepository;
        private PhotoHelper photoHelper;

        public PictureController(IContactRepository contactRepository, PhotoHelper photoHelper) {
            this.contactRepository = contactRepository;
            this.photoHelper = photoHelper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string netid) {
            var nameShortened = netid.Replace("@illinois.edu", "");
            var returnValue = await contactRepository.ReadAsync(c => c.EmployeeProfiles.FirstOrDefault(p => p.Title == nameShortened));
            return await photoHelper.GetPhoto(returnValue?.PhotoUrl);
        }
    }
}