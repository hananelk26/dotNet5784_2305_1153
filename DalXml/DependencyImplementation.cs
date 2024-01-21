

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
            
        int newId = DataSource.Config.NextDependencyId;
        Dependency newObject = item with { Id = newId };
        DataSource.Dependencies.Add(newObject);
        return newId;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Dependency? Read(int id)
    {
        throw new NotImplementedException();
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Dependency item)
    {
        throw new NotImplementedException();
    }
}
