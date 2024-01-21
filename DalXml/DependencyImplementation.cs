

namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Data.Common;

internal class DependencyImplementation : IDependency
{
    readonly string s_dependency_xml = "dependnecy";

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

    public void Delete(int id)
    {

        if (Read(id) == null)
        {
            throw new DalXMLFileLoadCreateException($"A Dependency object with ID = {id} does not exist.");
        }

        List<Dependency> l;
        l = XMLTools.LoadListFromXMLSerializer<Dependency>(s_dependency_xml);
        l.Remove(Read(id));
        XMLTools.SaveListToXMLSerializer<Dependency>(l, s_dependency_xml);
    }

    public Dependency? Read(int id)
    {
        List<Dependency> l;
        l = XMLTools.LoadListFromXMLSerializer<Dependency>(s_dependency_xml);
        return l.FirstOrDefault(dependency => dependency.Id == id);
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        List<Dependency> l;
        l = XMLTools.LoadListFromXMLSerializer<Dependency>(s_dependency_xml);
        return l.FirstOrDefault(filter);
    }

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
}
