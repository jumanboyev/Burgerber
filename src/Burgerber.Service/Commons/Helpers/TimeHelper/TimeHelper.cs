using Burgerber.Domain.Constans;

namespace Burgerber.Service.Commons.Helpers.TimeHelper;

public class TimeHelper
{
    public static DateTime GetDateTime()
    {
        var dbTime = DateTime.UtcNow;
        dbTime.AddHours(TimeConstant.UTC);
        return dbTime;
    }
}
