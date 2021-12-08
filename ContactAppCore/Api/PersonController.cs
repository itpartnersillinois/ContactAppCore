using ContactAppCore.Data;
using ContactAppCore.Data.Models;
using ContactAppCore.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactAppCore.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private IContactRepository contactRepository;
        private SecurityHelper securityHelper;

        public PersonController(IContactRepository contactRepository, SecurityHelper securityHelper)
        {
            this.contactRepository = contactRepository;
            this.securityHelper = securityHelper;
        }

        [HttpGet("Admin")]
        public async Task<IEnumerable<Person>> GetAdmin()
        {
            if (!securityHelper.IsFullAdmin(User))
            {
                return default;
            }
            return await contactRepository.ReadAsync(c => c.People.Where(p => p.IsFullAdmin).OrderBy(p => p.Title).ToList());
        }

        [HttpGet("Area/{areaId}")]
        public async Task<IEnumerable<Person>> GetArea(int areaId)
        {
            if (!securityHelper.AllowArea(User, areaId))
            {
                return default;
            }
            return await contactRepository.ReadAsync(c => c.People.Where(p => p.AreaId == areaId).OrderBy(p => p.Title).ToList());
        }

        [HttpGet("Office/{officeId}")]
        public async Task<IEnumerable<Person>> GetOffice(int officeId)
        {
            if (!securityHelper.AllowOffice(User, officeId))
            {
                return default;
            }
            return await contactRepository.ReadAsync(c => c.People.Where(p => p.OfficeId == officeId).OrderBy(p => p.Title).ToList());
        }
    }
}