using System.Globalization;

namespace Markey.CrossCutting.Helpers
{
    public static class Convert
    {
        public static string Date(string dateString)
        {
            if (string.IsNullOrEmpty(dateString))
                return string.Empty;

            if (DateTime.TryParseExact(dateString, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return date.ToString("dd/MM/yyyy HH:mm:ss");
            }

            if (DateTime.TryParse(dateString, out date))
            {
                return date.ToString("dd/MM/yyyy HH:mm:ss");
            }

            return dateString; 
        }

        public static string Bytes(long bytes)
        {
            const long KB = 1024;
            const long MB = KB * 1024;
            const long GB = MB * 1024;

            return bytes switch
            {
                >= GB => $"{(double)bytes / GB:F2} Gb",
                >= MB => $"{(double)bytes / MB:F2} Mb",
                >= KB => $"{(double)bytes / KB:F2} Kb",
                _ => $"{bytes}"
            };
        }


    }
}

