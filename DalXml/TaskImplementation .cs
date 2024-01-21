
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Data.Common;

internal class TaskImplementation : ITask
{
    readonly string s_task_xml = "task";

    public int Create(Task item)
    {
        List<Task> tas;
        tas = XMLTools.LoadListFromXMLSerializer<Task>(s_task_xml);
        int newId = Config.NextTaskId;
        Task newObject = item with { Id = newId };
        tas.Add(newObject);
        XMLTools.SaveListToXMLSerializer<Task>(tas, s_task_xml);
        return newId;
    }

    public void Delete(int id)
    {
        if (Read(id) == null)
        {
            throw new DalDoesNotExistException($"A Task object with ID = {id} does not exist.");
        }
        else
        {

            List<Task> tas;
            tas = XMLTools.LoadListFromXMLSerializer<Task>(s_task_xml);
            tas.Remove(Read(id));
            XMLTools.SaveListToXMLSerializer<Task>(tas, s_task_xml);

        }
    }

    public Task? Read(int id)
    {
        List<Task> tas;
        tas = XMLTools.LoadListFromXMLSerializer<Task>(s_task_xml);
        Task newObject = tas.FirstOrDefault(task => task.Id == id);
        return newObject;
       
    }

    public Task? Read(Func<Task, bool> filter)
    {
        List<Task> tas;
        tas = XMLTools.LoadListFromXMLSerializer<Task>(s_task_xml);
            Task newObject = tas.FirstOrDefault(filter);
            return newObject;
    }

    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        IEnumerable<Task> tas;
        tas= XMLTools.LoadListFromXMLSerializer<Task>(s_task_xml);
        if (filter == null)
            return tas.Select(item => item);
        else
            return tas.Where(filter);
    }

    public void Update(Task item)
    {
        List<Task> tas;
        tas = XMLTools.LoadListFromXMLSerializer<Task>(s_task_xml);
        if (Read(item.Id) == null)
        {
            throw new DalDoesNotExistException($"A Task object with ID = {item.Id} does not exist.");
        }
        else
        {
            tas.Remove(Read(item.Id));
            tas.Add(item);
        }
        XMLTools.SaveListToXMLSerializer<Task>(tas, s_task_xml);

    }
}
