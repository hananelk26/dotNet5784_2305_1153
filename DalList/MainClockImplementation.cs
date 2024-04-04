using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal class MainClockImplementation : IMainClock
{
    public DateTime? GetMainClock()
    {
        return DataSource.Config.StartDateOfMainClock;
    }

    public void SetMainClock(DateTime? date)
    {
        DataSource.Config.StartDateOfMainClock = date; ;
    }

    public void reset()
    {
        DataSource.Config.StartDateOfMainClock = DateTime.Now.Date;
    }

    public void addDay(int day)
    {
        DateTime temp = (DateTime)(DataSource.Config.StartDateOfMainClock);
        temp = temp.AddDays(day);
        DataSource.Config.StartDateOfMainClock = temp;
    }

    public void addYear(int y)
    {
        DateTime temp = (DateTime)(DataSource.Config.StartDateOfMainClock);
        temp = temp.AddYears(y);
        DataSource.Config.StartDateOfMainClock = temp;
    }

    public void addMonth(int m)
    {
        DateTime temp = (DateTime)(DataSource.Config.StartDateOfMainClock);
        temp = temp.AddMonths(m);
        DataSource.Config.StartDateOfMainClock = temp;
    }
}
