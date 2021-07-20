using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAppCore.Data.Models
{
    public enum OfficeTypeEnum
    {
        IT, HR, Business, Facilities, Academic
    }

    public class Office : BaseDataItem
    {
        public string Address { get; set; }

        public IEnumerable<Person> Admins { get; set; }

        public Area Area { get; set; }

        public int AreaId { get; set; }

        public string Audience { get; set; }

        public string Building { get; set; }

        public string BuildingCode { get; set; }

        public string City { get; set; }

        public string Email { get; set; }

        public string ExternalUrl { get; set; }

        public string HoursFriday { get; set; }

        public string HoursMessage { get; set; }

        public string HoursMonday { get; set; }

        public string HoursSaturday { get; set; }

        public string HoursSunday { get; set; }

        public string HoursThursday { get; set; }

        public string HoursTuesday { get; set; }

        public string HoursWednesday { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        public string InternalNotes { get; set; }
        public bool InternalOnly { get; set; }
        public int InternalOrder { get; set; }
        public string InternalUrl { get; set; }

        public string Notes { get; set; }
        public OfficeTypeEnum OfficeType { get; set; }
        public string Phone { get; set; }
        public string SearchTerms { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public string TicketUrl { get; set; }
        public string ZipCode { get; set; }
    }
}