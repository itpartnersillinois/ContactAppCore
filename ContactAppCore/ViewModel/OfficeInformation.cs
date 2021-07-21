using ContactAppCore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAppCore.ViewModel
{
    public class OfficeInformation
    {
        public OfficeInformation(Office office)
        {
            Address = office.Address;
            AreaId = office.AreaId;
            Area = office.Area?.Title;
            Audience = office.Audience;
            Building = office.Building;
            BuildingUrl = string.IsNullOrWhiteSpace(office.BuildingCode) ? string.Empty : "https://map.illinois.edu/view?ACTION=MAP&buildingId=" + office.BuildingCode;
            City = office.City;
            Email = office.Email;
            ExternalUrl = office.ExternalUrl;
            Id = office.Id;
            InternalOrder = office.InternalOrder;
            InternalUrl = office.InternalUrl;
            Notes = office.Notes;
            OfficeType = office.OfficeType.ToString();
            Phone = office.Phone;
            Room = office.Room;
            TicketUrl = office.TicketUrl;
            Title = office.Title;
            ZipCode = office.ZipCode;
        }

        public string Address { get; set; }

        public string Area { get; set; }

        public int AreaId { get; set; }

        public string Audience { get; set; }

        public string Building { get; set; }

        public string BuildingUrl { get; set; }

        public string City { get; set; }

        public string Email { get; set; }

        public string ExternalUrl { get; set; }

        public string Hours1 { get; set; }

        public string Hours2 { get; set; }

        public string Hours3 { get; set; }

        public string Hours4 { get; set; }

        public string Hours5 { get; set; }

        public string Hours6 { get; set; }

        public string Hours7 { get; set; }

        public string HoursMessage { get; set; }

        public int Id { get; set; }

        public string InternalNotes { get; set; }
        public bool InternalOnly { get; set; }
        public int InternalOrder { get; set; }
        public string InternalUrl { get; set; }

        public string Notes { get; set; }
        public string OfficeType { get; set; }
        public string Phone { get; set; }
        public string Room { get; set; }
        public string SearchTerms { get; set; }
        public string TicketUrl { get; set; }
        public string Title { get; set; }
        public string ZipCode { get; set; }
    }
}