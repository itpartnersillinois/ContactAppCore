using ContactAppCore.Data.Models;

namespace ContactAppCore.Helpers {

    public static class FilterHelper {

        public static AreaTypeEnum? TranslateArea(string s) {
            if (string.IsNullOrWhiteSpace(s)) {
                return null;
            }
            switch (s.ToLowerInvariant()) {
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

        public static OfficeTypeEnum? TranslateOffice(string s) {
            if (string.IsNullOrWhiteSpace(s)) {
                return null;
            }
            switch (s.ToLowerInvariant()) {
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

                case "research":
                    return OfficeTypeEnum.Research;

                case "studentsupport":
                case "support":
                    return OfficeTypeEnum.StudentSupport;

                case "advancement":
                    return OfficeTypeEnum.Advancement;

                case "general":
                    return OfficeTypeEnum.General;

                default:
                    return null;
            }
        }
    }
}