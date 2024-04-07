using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

internal class ResetDataConfigImplamantation : IResetDataConfig
{
    public void resetDataConfig()
    {
        XElement? ex = null;

        const string s_xml_dir = @"..\xml\";
        string filePath = $"{s_xml_dir + "data-config"}.xml";
        try
        {
            if (File.Exists(filePath))
                ex = XElement.Load(filePath);

        }
        catch (Exception eex)
        {
            throw new DalXMLFileLoadCreateException($"fail to load xml file: {s_xml_dir + filePath}, {eex.Message}");
        }
        int num = 1;
        ex!.Element("NextTaskId")!.Value = num.ToString();
        ex.Element("NextDependencyId")!.Value = num.ToString();

        ex.Save(filePath);
    }
}
