using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

internal class MainClockImplementation : IMainClock
{
    public DateTime? GetMainClock()
    {
        XElement root = XMLTools.LoadListFromXMLElement("data-config");
        if (root.Element("MainClock")!.Value == "")
        {
            return null;
        }
        return DateTime.Parse(root.Element("MainClock")!.Value);
    }


    public void SetMainClock(DateTime? date)
    {
        XElement root = XMLTools.LoadListFromXMLElement("data-config");
        root.Element("MainClock")!.Value = date.ToString();
        XMLTools.SaveListToXMLElement(root, "data-config");
    }

    public void reset()
    {
        DateTime? temp = DateTime.Now.Date;
        XElement root = XMLTools.LoadListFromXMLElement("data-config");
        root.Element("MainClock")!.Value = temp.ToString();
        XMLTools.SaveListToXMLElement(root, "data-config");
    }

    public void addDay(int day)
    {
        DateTime temp = (DateTime)GetMainClock();
        temp = (DateTime)temp;
        temp = temp.AddDays(day);
        SetMainClock(temp);

    }

    public void addYear(int y)
    {
        DateTime temp = (DateTime)GetMainClock();
        temp = (DateTime)temp;
        temp = temp.AddYears(y);
        SetMainClock(temp);
    }

    public void addHour(int h)
    {
        DateTime temp = (DateTime)GetMainClock();
        temp = (DateTime)temp;
        temp = temp.AddHours(h);
        SetMainClock(temp);
    }
}
