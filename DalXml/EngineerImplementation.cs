

namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Xml.Linq;

/// <summary>
/// Represents a class for managing Engineer objects using XML serialization.
/// Implements the IEngineer interface.
/// </summary>
internal class EngineerImplementation : IEngineer
{
    /// <summary>
    /// The XML file name for storing Engineer objects.
    /// </summary>
    readonly string s_engineer_xml = "engineers";

    /// <summary>
    /// Creates a new Engineer object and adds it to the XML file.
    /// </summary>
    /// <param name="item">The Engineer object to be created.</param>
    /// <returns>The ID of the newly created Engineer object.</returns>
    public int Create(Engineer item)
    {
        Engineer eng;
        eng = Read(item.Id)!;

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

    /// <summary>
    /// Deletes an Engineer object from the XML file based on its ID.
    /// </summary>
    /// <param name="id">The ID of the Engineer object to be deleted.</param>
    public void Delete(int id)
    {
        if (Read(id) == null)
        {
            throw new DalDoesNotExistException($"An Engineer object with ID = {id} does not exist.");
        }

        XElement ex = XMLTools.LoadListFromXMLElement(s_engineer_xml);
        XElement toDelete = (from item in ex.Elements()
                            where Convert.ToInt32(item.Element("Id")!.Value) == id
                            select item).FirstOrDefault()!;
        toDelete?.Remove();
        XMLTools.SaveListToXMLElement (ex, s_engineer_xml);

        
    }

    /// <summary>
    /// Reads an Engineer object from the XML file based on its ID.
    /// </summary>
    /// <param name="id">The ID of the Engineer object to be retrieved.</param>
    /// <returns>The Engineer object with the specified ID, or null if not found.</returns>
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

    /// <summary>
    /// Reads an Engineer object from the XML file based on a custom filter.
    /// </summary>
    /// <param name="filter">The filter condition for selecting an Engineer object.</param>
    /// <returns>The first Engineer object that satisfies the filter condition, or null if not found.</returns>
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

    /// <summary>
    /// Reads all Engineer objects from the XML file, optionally filtered.
    /// </summary>
    /// <param name="filter">The optional filter condition for selecting Engineer objects.</param>
    /// <returns>An IEnumerable of Engineer objects that satisfy the filter condition, or all objects if no filter is provided.</returns>
    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter)
    {
        XElement ex = XMLTools.LoadListFromXMLElement(s_engineer_xml);
        List<Engineer> toRead = (from item in ex.Elements()
                           select new Engineer()
                           {
                               Id = item.ToIntNullable("Id")!.Value,
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

    /// <summary>
    /// Updates an Engineer object in the XML file.
    /// </summary>
    /// <param name="item">The updated Engineer object.</param>
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

    public void DeleteAll()
    {
        XElement eng = new XElement("ArrayOfEngineer");
        XMLTools.SaveListToXMLElement(eng,s_engineer_xml);
    }
}
