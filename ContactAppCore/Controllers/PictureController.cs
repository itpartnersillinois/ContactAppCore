using System.Linq;
using System.Threading.Tasks;
using ContactAppCore.Data;
using ContactAppCore.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactAppCore.Controllers {

    [Route("[controller]")]
    [AllowAnonymous]
    public class PictureController : Controller {
        private IContactRepository contactRepository;
        private PhotoHelper photoHelper;

        public PictureController(IContactRepository contactRepository, PhotoHelper photoHelper) {
            this.contactRepository = contactRepository;
            this.photoHelper = photoHelper;
        }

        [Route("{netid}")]
        [HttpGet]
        public async Task<IActionResult> ByUsername(string netid) => await Index(netid);

        [HttpGet]
        public async Task<IActionResult> Index(string netid) {
            var nameShortened = netid.Replace("@illinois.edu", "").ToLowerInvariant();
            var returnValue = await contactRepository.ReadAsync(c => c.EmployeeProfiles.FirstOrDefault(p => p.Title == nameShortened));
            return await photoHelper.GetPhoto(returnValue?.PhotoUrl);
        }
    }
}