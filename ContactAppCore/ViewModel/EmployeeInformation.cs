using ContactAppCore.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace ContactAppCore.ViewModel
{
    public class EmployeeInformation
    {
        public EmployeeInformation()
        {
        }

        public EmployeeInformation(JobProfile profile)
        {
            Biography = string.IsNullOrWhiteSpace(profile.Biography) ? profile.EmployeeProfile.Biography ?? "" : profile.Biography;
            CVUrl = profile.EmployeeProfile.CVUrl ?? "";
            InternalOrder = profile.InternalOrder;
            NetId = profile.EmployeeProfile.Title ?? "";
            OfficeName = profile.Office.Title;
            OfficeCode = profile.Office.InternalCode ?? "";
            Phone = profile.Phone ?? "";
            PhotoUrl = profile.EmployeeProfile.PhotoUrl ?? "";
            PreferredName = profile.EmployeeProfile.PreferredName ?? "";
            IsPrimaryProfile = (profile.Id == (profile.EmployeeProfile.PrimaryProfile ?? 0));
            EmployeeActivityInformation = profile.EmployeeProfile.EmployeeActivities.Select(ea => new EmployeeActivityInformation(ea));
            Tags = string.Join(", ", profile.Tags.Select(t => t.Title));
        }

        public string Biography { get; set; }
        public string CVUrl { get; set; }
        public IEnumerable<EmployeeActivityInformation> EmployeeActivityInformation { get; set; }
        public int InternalOrder { get; set; }
        public bool IsPrimaryProfile { get; set; }
        public string NetId { get; set; }
        public string OfficeCode { get; set; }
        public string OfficeName { get; set; }
        public string Phone { get; set; }
        public string PhotoUrl { get; set; }
        public string PreferredName { get; set; }
        public string Tags { get; set; }
    }
}