using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dal
{
    internal class TimeImplementation : ITIme
    {
        public void reset()
        {
            XElement root = XMLTools.LoadListFromXMLElement("data-config");
            root.Element("StartDate")!.Value = "";
            XMLTools.SaveListToXMLElement(root, "data-config");
        }

        public void SetStartDate(DateTime date)
        {
            XElement root = XMLTools.LoadListFromXMLElement("data-config");
            root.Element("StartDate")!.Value = date.ToString();
            XMLTools.SaveListToXMLElement(root, "data-config");
        }

        public DateTime? StartDate()
        {
            XElement root = XMLTools.LoadListFromXMLElement("data-config");
            if(root.Element("StartDate")!.Value=="")
            {
                return null;
            }
            return DateTime.Parse(root.Element("StartDate")!.Value);
        }
    }
}
