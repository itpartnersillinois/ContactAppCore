using ContactAppCore.Helpers;
using ContactAppCore.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContactAppCore.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityListController : ControllerBase
    {
        private ListAdminHelper listHelper;

        public SecurityListController(ListAdminHelper listHelper)
        {
            this.listHelper = listHelper;
        }

        [HttpGet("Area/{areaId}")]
        public async Task<ItemAdminList> GetAreaItems(int areaId)
        {
            return await listHelper.GetAreaList(areaId);
        }

        [HttpGet("")]
        public async Task<ItemAdminList> GetItems()
        {
            return await listHelper.GetList();
        }
    }
}