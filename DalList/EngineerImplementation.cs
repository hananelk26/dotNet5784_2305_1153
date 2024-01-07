

namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(int id)
    {
        foreach (var Engineer in DataSource.Engineers)
        {
            if (Engineer.Id == id)
            {
                return Engineer;
            }
        }
        return null;
    }

    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    public void Update(Engineer item)
    {
        foreach (var Engineer in DataSource.Engineers)
        {
            if (Engineer.Id == item.Id)
            {
                DataSource.Engineers.Remove(Engineer);
                DataSource.Engineers.Add(item);
                break;
            }
        }
        throw new Exception("Engineer object with such ID does not exist");
    }

}
