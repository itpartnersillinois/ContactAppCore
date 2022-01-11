using ContactAppCore.Data;
using ContactAppCore.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAppCore.Helpers
{
    public class ListAdminHelper
    {
        private IContactRepository contactRepository;

        public ListAdminHelper(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public async Task<ItemAdminList> GetAreaList(int areaId)
        {
            var offices = await contactRepository.ReadAsync(c => c.Offices.Include(c => c.Admins)
                .Where(c => c.AreaId == areaId)
                .Select(c => new AreaOfficeAdminItem { Id = c.Id, Title = c.Title, Admins = c.Admins.Select(a => a.Title).ToList() }).OrderBy(a => a.Title));

            return new ItemAdminList
            {
                Areas = null,
                Offices = offices.ToList()
            };
        }

        public async Task<ItemAdminList> GetList()
        {
            var areas = await contactRepository.ReadAsync(c => c.Areas.Include(c => c.Admins)
                .Select(c => new AreaOfficeAdminItem { Id = c.Id, Title = c.Title, Admins = c.Admins.Select(a => a.Title).ToList() }).OrderBy(a => a.Title));

            return new ItemAdminList
            {
                Areas = areas.ToList(),
            };
        }
    }
}