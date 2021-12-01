using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactAppCore.Data.Models
{
    public enum OfficeTypeEnum
    {
        NotListed, IT, HR, Business, Facilities, Academic, General, Other = 9
    }

    public class Office : BaseDataItem
    {
        public Office()
        {
        }

        public Office(string title, int areaId)
        {
            IsActive = false;
            Title = title;
            AreaId = areaId;
            InternalOrder = 3;
            HoursMondayStart = "8:00 AM";
            HoursMondayEnd = "5:00 PM";
            HoursTuesdayStart = "8:00 AM";
            HoursTuesdayEnd = "5:00 PM";
            HoursWednesdayStart = "8:00 AM";
            HoursWednesdayEnd = "5:00 PM";
            HoursThursdayStart = "8:00 AM";
            HoursThursdayEnd = "5:00 PM";
            HoursFridayStart = "8:00 AM";
            HoursFridayEnd = "5:00 PM";
        }

        public string Address { get; set; }

        public virtual IEnumerable<Person> Admins { get; set; }

        public virtual Area Area { get; set; }

        public int AreaId { get; set; }

        public string Audience { get; set; }

        public string Building { get; set; }

        public string BuildingCode { get; set; }

        public string City { get; set; }

        public bool CovidSupport { get; set; }

        public string Email { get; set; }

        public string ExternalUrl { get; set; }

        public string HoursFridayEnd { get; set; }
        public string HoursFridayStart { get; set; }
        public bool HoursIncludeHolidayMessage { get; set; }
        public string HoursMessage { get; set; }

        public string HoursMondayEnd { get; set; }
        public string HoursMondayStart { get; set; }

        public string HoursSaturdayEnd { get; set; }
        public string HoursSaturdayStart { get; set; }

        public string HoursSundayEnd { get; set; }
        public string HoursSundayStart { get; set; }

        public string HoursThursdayEnd { get; set; }
        public string HoursThursdayStart { get; set; }

        public string HoursTuesdayEnd { get; set; }
        public string HoursTuesdayStart { get; set; }

        public string HoursWednesdayEnd { get; set; }
        public string HoursWednesdayStart { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        public string InternalCode { get; set; }
        public string InternalNotes { get; set; }
        public bool InternalOnly { get; set; }
        public int InternalOrder { get; set; }
        public string InternalUrl { get; set; }

        public virtual IEnumerable<JobProfile> JobProfiles { get; set; }

        public string Notes { get; set; }
        public OfficeTypeEnum OfficeType { get; set; }
        public string Phone { get; set; }
        public string Room { get; set; }
        public string SearchTerms { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public string TicketUrl { get; set; }
        public string ZipCode { get; set; }
    }
}