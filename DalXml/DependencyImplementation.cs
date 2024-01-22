

namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Xml.Linq;

/// <summary>
/// Represents a class for managing Dependency objects using XML serialization.
/// Implements the IDependency interface.
/// </summary>
internal class DependencyImplementation : IDependency
{
    /// <summary>
    /// The XML file name for storing Dependency objects.
    /// </summary>
    readonly string s_dependency_xml = "dependencys"; 

    /// <summary>
    /// Creates a new Dependency object and adds it to the XML file.
    /// </summary>
    /// <param name="item">The Dependency object to be created.</param>
    /// <returns>The ID of the newly created Dependency object.</returns>
    public int Create(Dependency item)
    {
        List<Dependency> l;
        l = XMLTools.LoadListFromXMLSerializer<Dependency>(s_dependency_xml);
        int newId = Config.NextDependencyId;
        Dependency newObject = item with { Id = newId };
        l.Add( newObject );
        XMLTools.SaveListToXMLSerializer<Dependency>(l, s_dependency_xml);
        return newId;
    }


    /// <summary>
    /// Deletes a Dependency object from the XML file based on its ID.
    /// </summary>
    /// <param name="id">The ID of the Dependency object to be deleted.</param>
    public void Delete(int id)
    {

        if (Read(id) == null)
        {
            throw new DalXMLFileLoadCreateException($"A Dependency object with ID = {id} does not exist.");
        }

        List<Dependency> l;
        l = XMLTools.LoadListFromXMLSerializer<Dependency>(s_dependency_xml);
        l.Remove(Read(id)!);
        XMLTools.SaveListToXMLSerializer<Dependency>(l, s_dependency_xml);
    }

    /// <summary>
    /// Reads a Dependency object from the XML file based on its ID.
    /// </summary>
    /// <param name="id">The ID of the Dependency object to be retrieved.</param>
    /// <returns>The Dependency object with the specified ID, or null if not found.</returns>
    public Dependency? Read(int id)
    {
        List<Dependency> l;
        l = XMLTools.LoadListFromXMLSerializer<Dependency>(s_dependency_xml);
        return l.FirstOrDefault(dependency => dependency.Id == id);
    }

    /// <summary>
    /// Reads a Dependency object from the XML file based on a custom filter.
    /// </summary>
    /// <param name="filter">The filter condition for selecting a Dependency object.</param>
    /// <returns>The first Dependency object that satisfies the filter condition, or null if not found.</returns>
    public Dependency? Read(Func<Dependency, bool> filter)
    {
        List<Dependency> l;
        l = XMLTools.LoadListFromXMLSerializer<Dependency>(s_dependency_xml);
        return l.FirstOrDefault(filter);
    }

    /// <summary>
    /// Reads all Dependency objects from the XML file, optionally filtered.
    /// </summary>
    /// <param name="filter">The optional filter condition for selecting Dependency objects.</param>
    /// <returns>An IEnumerable of Dependency objects that satisfy the filter condition, or all objects if no filter is provided.</returns>
    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter)
    {
        List<Dependency> l;
        l = XMLTools.LoadListFromXMLSerializer<Dependency>(s_dependency_xml);
        if (filter != null)
        {
            return l.Where(filter);
        }
        else
        {
            return l.Select(item => item);
        }
    }

    /// <summary>
    /// Updates a Dependency object in the XML file.
    /// </summary>
    /// <param name="item">The updated Dependency object.</param>
    public void Update(Dependency item)
    {

        if (Read(item.Id) == null)
        {
            throw new DalXMLFileLoadCreateException($"A Dependency object with ID = {item.Id} does not exist.");
        }

        Delete(item.Id);

        List<Dependency> l;
        l = XMLTools.LoadListFromXMLSerializer<Dependency>(s_dependency_xml);
        l.Add(item);

        XMLTools.SaveListToXMLSerializer<Dependency>(l, s_dependency_xml);
    }

    public void DeleteAll()
    {
        XElement dep = new XElement("ArrayOfDependency");
        XMLTools.SaveListToXMLElement(dep, s_dependency_xml);
    }


}


