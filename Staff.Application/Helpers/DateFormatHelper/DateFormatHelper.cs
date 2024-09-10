namespace Staff.Application.Helpers.DateFormatHelper;

public class DateFormatHelper : IDateFormatHelper
{
    public DateTime FormatDate(DateTime date)
    {
        return date.Kind != DateTimeKind.Utc ? date.ToUniversalTime() : date;
    }
}