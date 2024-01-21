

namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Xml.Linq;

internal class EngineerImplementation : IEngineer
{
    readonly string s_engineer_xml = "engineer";

    public int Create(Engineer item)
    {
        Engineer eng;
        eng = Read(item.Id);

        if (eng != null)
        {
            throw new DalXMLFileLoadCreateException($"An Engineer object with ID = {item.Id} already exists.");
        }

        XElement ex;
        ex = XMLTools.LoadListFromXMLElement(s_engineer_xml);

        XElement Id = new XElement("Id",item.Id);
        XElement Email = new XElement("Email", item.Email);
        XElement Cost = new XElement("Cost", item.Cost);
        XElement Name = new XElement("Name", item.Name);
        XElement Level = new XElement("Level", item.Level);

        ex.Add(new XElement("Engineer",Id,Email,Cost,Name,Level));

        XMLTools.SaveListToXMLElement(ex, s_engineer_xml);
        
        return item.Id;
    }

    public void Delete(int id)
    {
        if (Read(id) == null)
        {
            throw new DalXMLFileLoadCreateException($"An Engineer object with ID = {id} does not exist.");
        }

        XElement ex = XMLTools.LoadListFromXMLElement(s_engineer_xml);
        XElement toDelete = from item in ex.Elements()
                            where Convert.ToInt32(item.Element("Id").Value) == id
                            select item;

        
    }

    public Engineer? Read(int id)
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        Engineer eng;
        eng = Read(item.Id)!;

        if (eng == null)
        {
            throw new DalDoesNotExistException($"An Engineer object with ID = {item.Id} does not exist.");
        }

        XElement ex;
        ex = XMLTools.LoadListFromXMLElement(s_engineer_xml);
        XElement? found = ex.Elements().FirstOrDefault(xElement => xElement.Element("Id")!.Value == item.Id.ToString());
        if (found == null) { return; }

        found.Element("Name")!.Value = item.Name;
        found.Element("Email")!.Value = item.Email;
        found.Element("Cost")!.Value = item.Cost.ToString();
        found.Element("Level")!.Value = item.Level.ToString();

        XMLTools.SaveListToXMLElement(ex, s_engineer_xml);
    }
}
