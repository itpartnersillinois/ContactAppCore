using ContactAppCore.Helpers;
using ContactAppCore.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactAppCore.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private ListHelper listHelper;

        public ItemController(ListHelper listHelper)
        {
            this.listHelper = listHelper;
        }

        [HttpGet("Area/{areaId}")]
        public async Task<ItemList> GetAreaItems(int areaId)
        {
            return await listHelper.GetAreaList(User, areaId);
        }

        [HttpGet("")]
        public async Task<ItemList> GetItems()
        {
            return await listHelper.GetList(User);
        }
    }
}