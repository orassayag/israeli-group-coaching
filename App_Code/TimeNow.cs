using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TimeNow
/// </summary>
public static class TimeNow
{
    private static DateTime _timeNow;

    static TimeNow()
    {
        _timeNow = DateTime.Now;
    }

    public static DateTime TheTimeNow
    {
        get { return _timeNow; }
        set
        {
            if (value == default(DateTime))
            {
                return;
            }

            _timeNow = value;
        }
    }
}
