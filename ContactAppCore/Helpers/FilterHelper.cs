using ContactAppCore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAppCore.Helpers
{
    public static class FilterHelper
    {
        public static AreaTypeEnum? TranslateArea(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return null;
            }
            switch (s.ToLowerInvariant())
            {
                case "campus":
                    return AreaTypeEnum.Campus;

                case "college":
                    return AreaTypeEnum.College;

                case "other":
                    return AreaTypeEnum.Other;

                case "research":
                    return AreaTypeEnum.Research;

                case "system":
                    return AreaTypeEnum.System;

                default:
                    return null;
            }
        }

        public static OfficeTypeEnum? TranslateOffice(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return null;
            }
            switch (s.ToLowerInvariant())
            {
                case "hr":
                case "humanresources":
                    return OfficeTypeEnum.HR;

                case "academic":
                    return OfficeTypeEnum.Academic;

                case "business":
                    return OfficeTypeEnum.Business;

                case "facilities":
                    return OfficeTypeEnum.Facilities;

                case "it":
                case "tech":
                    return OfficeTypeEnum.IT;

                default:
                    return null;
            }
        }
    }
}