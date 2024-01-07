namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        int i_d = DataSource.Config.NextDependencyId;
        Dependency newObject = item with { Id = i_d };
        DataSource.Dependencies.Add(newObject);
        return i_d;
    }

    public void Delete(int id)
    {
        bool flag = false;
        foreach (Dependency item in DataSource.Dependencies)
        {
            if (item.Id == id)
            {
                DataSource.Dependencies.Remove(item);
                flag = true;
            }

        }
        if (!flag) { throw new Exception("An object of type T with such an ID does not exist"); }
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
