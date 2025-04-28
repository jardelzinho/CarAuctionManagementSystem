using System.Globalization;

namespace CarAuctionManagementSystem.Domain.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsValidGregorianYear(this ushort year)
        {
            var calendar = new GregorianCalendar();
            return year >= calendar.MinSupportedDateTime.Year &&
                   year <= calendar.MaxSupportedDateTime.Year;
        }
    }
}
