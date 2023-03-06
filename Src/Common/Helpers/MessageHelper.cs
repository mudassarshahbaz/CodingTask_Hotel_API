

namespace Common.Helpers
{
    public static class MessageHelper
    {
        public static string SystemUnhandledException = GetValue(nameof(SystemUnhandledException));
        public static string FileNotFound = GetValue(nameof(FileNotFound));
        public static string InvalidInputData = GetValue(nameof(InvalidInputData));
        private static string GetValue(string key)
        {
            return "";//SystemErrorsResource.ResourceManager.GetString(key);
        }
        public static string Invalid(string value)
        {
            return $"Invalid {value}";
        }
        public static string NotFound(string value)
        {
            return $"{value} not found.";
        }
    }
}
