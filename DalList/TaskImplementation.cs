

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
        bool flag = false;
        foreach (Task item in DataSource.Tasks)
        {
            if (item.id == id) { 
                DataSource.Tasks.Remove(item);
                flag = true;
            }
            
        }
        if (!flag) { throw new Exception($"An object of type T with ID = {id} does not exist"); }
    }

    public Task? Read(int id)
    {
        foreach (var task in DataSource.Tasks)
        {
            if (task.id == id)
            {
                return task;
            }
        }
        return null;
    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }

    public void Update(Task item)
    {
        foreach (var task in DataSource.Tasks)
        {
            if(task.id == item.id)
            {
                DataSource.Tasks.Remove(task);
                DataSource.Tasks.Add(item);
                break;
            }
        }
        throw new Exception($"Task object with ID = {item.id} does not exist");
    }
}
