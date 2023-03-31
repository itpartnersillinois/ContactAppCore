using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ContactAppCore.Data;
using Microsoft.EntityFrameworkCore;

namespace ContactAppCore.Helpers {

    public class JobHelper {
        private IContactRepository contactRepository;

        public JobHelper(IContactRepository contactRepository) {
            this.contactRepository = contactRepository;
        }

        public async Task<string> ProcessJob(int employeeId, int officeId) {
            try {
                var url = await contactRepository.ReadAsync(c => c.Offices.Include(o => o.Area).FirstOrDefault(o => o.Id == officeId)?.Area.PeopleRefreshUrl);
                if (!string.IsNullOrWhiteSpace(url)) {
                    var netId = await contactRepository.ReadAsync(c => c.EmployeeProfiles.FirstOrDefault(ep => ep.Id == employeeId)?.Title);
                    var client = new HttpClient();
                    _ = await client.GetStringAsync(url.Replace("{netid}", netId));
                    return "Update called";
                }
                return string.Empty;
            } catch (Exception e) {
                return e.Message;
            }
        }
    }
}