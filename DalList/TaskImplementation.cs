

namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        int i_d = DataSource.Config.NextTaskId;
        Task newObject = item with { id = i_d };
        DataSource.Tasks.Add(newObject);
        return i_d;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task Read(int id)
    {
        throw new NotImplementedException();
    }

    public List<Task> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Task item)
    {
        throw new NotImplementedException();
    }
}
