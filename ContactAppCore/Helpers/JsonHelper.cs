namespace ContactAppCore.Helpers
{
    public static class JsonHelper
    {
        public static bool TranslateBoolean(dynamic item)
        {
            if (item == null)
            {
                return false;
            }
            return item.ToString() == "on" || item.ToString() == "true";
        }
    }
}