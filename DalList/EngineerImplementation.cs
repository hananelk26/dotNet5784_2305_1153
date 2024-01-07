

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
        throw new NotImplementedException();
    }

    public Engineer? Read(int id)
    {
        throw new NotImplementedException();
    }

    public List<Engineer> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        throw new NotImplementedException();
    }
}
