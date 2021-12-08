using ContactAppCore.Data;
using ContactAppCore.Data.Models;
using System.Threading.Tasks;

namespace ContactAppCore.Helpers
{
    public static class LogHelper
    {
        public static async Task<int> CreateLog(IContactRepository contactRepository, string title, string name, string oldData = "", string newData = "")
        {
            return await contactRepository.CreateAsync(new Log { IsActive = true, Title = title, Name = name, OldData = oldData, NewData = newData });
        }
    }
}