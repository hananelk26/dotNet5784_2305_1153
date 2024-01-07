namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Dependency? Read(int id)
    {
        foreach (var Dependency in DataSource.Dependencies)
        {
            if (Dependency.Id == id)
            {
                return Dependency;
            }
        }
        return null;
    }

    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencies);
    }

    public void Update(Dependency item)
    {
        foreach (var Dependency in DataSource.Dependencies)
        {
            if (Dependency.Id == item.Id)
            {
                DataSource.Dependencies.Remove(Dependency);
                DataSource.Dependencies.Add(item);
                break;
            }
        }
        throw new Exception("Dependency object with such ID does not exist");
    }
}
