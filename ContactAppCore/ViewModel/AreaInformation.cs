using ContactAppCore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAppCore.ViewModel
{
    public class AreaInformation
    {
        public AreaInformation(Area area, bool useInternal, OfficeTypeEnum? officeType)
        {
            AreaType = area.AreaType.ToString();
            Audience = area.Audience;
            ExternalUrl = area.ExternalUrl;
            Id = area.Id;
            InternalUrl = area.InternalUrl;
            Notes = area.Notes;
            Title = area.Title;
            Offices = area.Offices == null ? null :
                useInternal ? area.Offices.Where(o => o.IsActive && (officeType == null || o.OfficeType == officeType)).OrderBy(o => o.InternalOrder).ThenBy(o => o.Title).Select(o => new OfficeInformation(o)).ToList() :
                area.Offices.Where(o => !o.InternalOnly && o.IsActive && (officeType == null || o.OfficeType == officeType)).OrderBy(o => o.InternalOrder).ThenBy(o => o.Title).Select(o => new OfficeInformation(o)).ToList();
        }

        public string AreaType { get; set; }

        public string Audience { get; set; }

        public string ExternalUrl { get; set; }

        public int Id { get; set; }

        public string InternalUrl { get; set; }

        public string Notes { get; set; }

        public IEnumerable<OfficeInformation> Offices { get; set; }
        public string Title { get; set; }
    }
}