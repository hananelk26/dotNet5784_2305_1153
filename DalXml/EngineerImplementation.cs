

namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            throw new DalAlreadyExistsException($"An Engineer object with ID = {item.Id} already exists.");
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
            throw new DalDoesNotExistException($"An Engineer object with ID = {id} does not exist.");
        }

        XElement ex = XMLTools.LoadListFromXMLElement(s_engineer_xml);
        XElement toDelete = (from item in ex.Elements()
                            where Convert.ToInt32(item.Element("Id").Value) == id
                            select item).FirstOrDefault();
        toDelete?.Remove();
        XMLTools.SaveListToXMLElement (ex, s_engineer_xml);

    }

    public Engineer? Read(int id)
    {
        XElement ex = XMLTools.LoadListFromXMLElement(s_engineer_xml);
        Engineer toRead = (from item in ex.Elements()
                             where Convert.ToInt32(item.Element("Id")!.Value) == id
                             select new Engineer()
                             {
                                 Id = Convert.ToInt32(item.Element("Id")!.Value),
                                 Email = item.Element("Email")!.Value,
                                 Cost = Convert.ToDouble(item.Element("Cost")!.Value),
                                 Name = item.Element("Name")!.Value,
                                 Level = (EngineerExperience)(Convert.ToInt32(item.Element("Level")!.Value))

                             }).FirstOrDefault()!;

        return toRead;
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        XElement ex = XMLTools.LoadListFromXMLElement(s_engineer_xml);
        List<Engineer> toRead = (from item in ex.Elements()
                                 select new Engineer()
                                 {
                                     Id = Convert.ToInt32(item.Element("Id")!.Value),
                                     Email = item.Element("Email")!.Value,
                                     Cost = Convert.ToDouble(item.Element("Cost")!.Value),
                                     Name = item.Element("Name")!.Value,
                                     Level = (EngineerExperience)(Convert.ToInt32(item.Element("Level")!.Value))

                                 }).ToList();
        Engineer eng = toRead.FirstOrDefault(filter)! ;
        return eng;
    } 

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter)
    {
        XElement ex = XMLTools.LoadListFromXMLElement(s_engineer_xml);
        List<Engineer> toRead = (from item in ex.Elements()
                           select new Engineer()
                           {
                               Id = Convert.ToInt32(item.Element("Id")!.Value),
                               Email = item.Element("Email")!.Value,
                               Cost = Convert.ToDouble(item.Element("Cost")!.Value),
                               Name = item.Element("Name")!.Value,
                               Level = (EngineerExperience)(Convert.ToInt32(item.Element("Level")!.Value))

                           }).ToList();

        if (filter == null)
            return toRead;
        else
            return toRead.Where(filter);
    }

    public void Update(Engineer item)
    {
        throw new NotImplementedException();
    }
}
