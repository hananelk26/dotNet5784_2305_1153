using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

public interface IMainClock
{
    public void SetMainClock(DateTime? date);

    public DateTime? GetMainClock();

    public void reset();

    public void addDay(int day);

    public void addYear(int y);

    public void addHour(int h);

}
