using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

internal class MainClockImplementation : IMainClock
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public void addDay(int day)
    {
        _dal.MainClock.addDay(day);
    }

    public void addHour(int h)
    {
        _dal.MainClock.addHour(h);
    }

    public void addYear(int y)
    {
        _dal.MainClock.addYear(y);
    }

    public DateTime? GetMainClock()
    {
        return _dal.MainClock.GetMainClock();
    }

    public void reset()
    {
        _dal.MainClock.reset();
    }


    public void SetMainClock(DateTime? date)
    {
        _dal.MainClock.SetMainClock(date);
    }
}
