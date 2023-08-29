using System.Collections.Generic;
using System.Linq;
using ContactAppCore.Data.Models;

namespace ContactAppCore.ViewModel {

    public class EmployeeInformation {

        public EmployeeInformation(JobProfile profile) {
            Biography = profile.EmployeeProfile.Biography ?? string.Empty;
            JobBiography = string.IsNullOrWhiteSpace(profile.Biography) ? profile.EmployeeProfile.Biography ?? string.Empty : profile.Biography;
            Category = profile.Category ?? string.Empty;
            CVUrl = profile.EmployeeProfile.CVUrl ?? string.Empty;
            InternalOrder = profile.InternalOrder;
            NetId = profile.EmployeeProfile.Title ?? string.Empty;
            OfficeName = profile.Office.Title;
            OfficeType = profile.Office.OfficeType.ToString();
            OfficeCode = profile.Office.InternalCode ?? string.Empty;
            Phone = profile.Phone ?? string.Empty;
            PhotoUrl = profile.EmployeeProfile.PhotoUrl ?? string.Empty;
            PreferredFirstName = profile.EmployeeProfile.PreferredName ?? string.Empty;
            PreferredLastName = profile.EmployeeProfile.PreferredNameLast ?? string.Empty;
            PreferredPronouns = profile.EmployeeProfile.PreferredPronouns ?? string.Empty;
            IsPrimaryProfile = (profile.Id == (profile.EmployeeProfile.PrimaryProfile ?? 0));
            IsPhoneHidden = profile.EmployeeProfile.IsPhoneHidden ?? false;
            EmployeeActivityInformation = profile.EmployeeProfile.EmployeeActivities.Select(ea => new EmployeeActivityInformation(ea));
            Tags = string.Join(", ", profile.Tags.Select(t => t.Title));
            Title = profile.Title ?? string.Empty;
        }

        public string Biography { get; set; }
        public string Category { get; set; }
        public string CVUrl { get; set; }
        public IEnumerable<EmployeeActivityInformation> EmployeeActivityInformation { get; set; }
        public int InternalOrder { get; set; }
        public bool IsPhoneHidden { get; set; }
        public bool IsPrimaryProfile { get; set; }
        public string JobBiography { get; set; }
        public string NetId { get; set; }
        public string OfficeCode { get; set; }
        public string OfficeName { get; set; }
        public string OfficeType { get; set; }
        public string Phone { get; set; }
        public string PhotoUrl { get; set; }
        public string PreferredFirstName { get; set; }
        public string PreferredLastName { get; set; }
        public string PreferredPronouns { get; set; }
        public string Tags { get; set; }
        public string Title { get; set; }
    }
}