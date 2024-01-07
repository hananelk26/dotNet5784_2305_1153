

namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        //if(DataSource.Engineers.Contains(item.Id))
        bool notFounded = true;
        foreach (Engineer x in DataSource.Engineers)
        {
            if (x.Id == item.Id)
            {
                notFounded = false;
            }
        }

        if (notFounded)
        {
            DataSource.Engineers.Add(item);
            return item.Id;
        }
        throw new Exception("An object of type T with such an ID already exists");
    }


    public void Delete(int id)
    {
        bool flag = false;
        foreach (Engineer item in DataSource.Engineers)
        {
            if (item.Id == id)
            {
                DataSource.Engineers.Remove(item);
                flag = true;
            }

        }
        if (!flag) { throw new Exception("An object of type T with such an ID does not exist"); }
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
