using ContactAppCore.Data.Models;
using System.Collections.Generic;

namespace ContactAppCore.ViewModel
{
    public class OfficeInformation
    {
        private List<string> hoursList;

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
            InternalUrl = office.InternalUrl;
            Notes = office.Notes;
            OfficeType = office.OfficeType.ToString();
            Phone = office.Phone;
            Room = office.Room;
            TicketUrl = office.TicketUrl;
            Title = office.Title;
            ZipCode = office.ZipCode;
            HoursMessage = office.HoursMessage;
            if (office.HoursIncludeHolidayMessage)
            {
                HoursMessage = ("Closed on University Holidays. " + HoursMessage).Trim();
            }
            hoursList = new List<string>();
            if (!string.IsNullOrWhiteSpace(office.HoursMondayStart))
            {
                //Check Sun-Sat
                if (office.HoursMondayStart == office.HoursTuesdayStart &&
                    office.HoursMondayStart == office.HoursWednesdayStart &&
                    office.HoursMondayStart == office.HoursThursdayStart &&
                    office.HoursMondayStart == office.HoursFridayStart &&
                    office.HoursMondayStart == office.HoursSaturdayStart &&
                    office.HoursMondayStart == office.HoursSundayStart &&
                    office.HoursMondayEnd == office.HoursTuesdayEnd &&
                    office.HoursMondayEnd == office.HoursWednesdayEnd &&
                    office.HoursMondayEnd == office.HoursThursdayEnd &&
                    office.HoursMondayEnd == office.HoursFridayEnd &&
                    office.HoursMondayEnd == office.HoursSaturdayEnd &&
                    office.HoursMondayEnd == office.HoursSundayEnd)
                {
                    AddHours("Sun-Sat", office.HoursMondayStart, office.HoursMondayEnd);
                }
                //Check Mon-Fri, Sat-Sun
                else if (office.HoursMondayStart == office.HoursTuesdayStart &&
                    office.HoursMondayStart == office.HoursWednesdayStart &&
                    office.HoursMondayStart == office.HoursThursdayStart &&
                    office.HoursMondayStart == office.HoursFridayStart &&
                    office.HoursMondayEnd == office.HoursTuesdayEnd &&
                    office.HoursMondayEnd == office.HoursWednesdayEnd &&
                    office.HoursMondayEnd == office.HoursThursdayEnd &&
                    office.HoursMondayEnd == office.HoursFridayEnd)
                {
                    AddHours("Mon-Fri", office.HoursMondayStart, office.HoursMondayEnd);
                    if (!string.IsNullOrWhiteSpace(office.HoursSaturdayStart) &&
                        office.HoursSaturdayStart == office.HoursSundayStart && office.HoursSaturdayEnd == office.HoursSundayEnd)
                    {
                        AddHours("Sat-Sun", office.HoursSaturdayStart, office.HoursSaturdayEnd);
                    }
                    else
                    {
                        AddHours("Sat", office.HoursSaturdayStart, office.HoursSaturdayEnd);
                        AddHours("Sun", office.HoursSundayStart, office.HoursSundayEnd);
                    }
                }
            }
            if (hoursList.Count == 0)
            {
                AddHours("Mon", office.HoursMondayStart, office.HoursMondayEnd);
                AddHours("Tue", office.HoursTuesdayStart, office.HoursTuesdayEnd);
                AddHours("Wed", office.HoursWednesdayStart, office.HoursWednesdayEnd);
                AddHours("Thu", office.HoursThursdayStart, office.HoursThursdayEnd);
                AddHours("Fri", office.HoursFridayStart, office.HoursFridayEnd);
                AddHours("Sat", office.HoursSaturdayStart, office.HoursSaturdayEnd);
                AddHours("Sun", office.HoursSundayStart, office.HoursSundayEnd);
            }
            Hours = hoursList.ToArray();
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

        public string[] Hours { get; set; }

        public string HoursMessage { get; set; }

        public int Id { get; set; }

        public bool InternalOnly { get; set; }

        public string InternalUrl { get; set; }

        public string Notes { get; set; }

        public string OfficeType { get; set; }

        public string Phone { get; set; }

        public string Room { get; set; }

        public string TicketUrl { get; set; }

        public string Title { get; set; }

        public string ZipCode { get; set; }

        private void AddHours(string header, string time1, string time2)
        {
            if (!string.IsNullOrWhiteSpace(time1) && !string.IsNullOrWhiteSpace(time2))
            {
                hoursList.Add($"{header}: {time1}-{time2}");
            }
            else if (!string.IsNullOrWhiteSpace(time1))
            {
                hoursList.Add($"{header}: {time1}");
            }
        }
    }
}