
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Xml.Linq;

/// <summary>
/// Represents a class for managing Task objects using XML serialization.
/// Implements the ITask interface.
/// </summary>
internal class TaskImplementation : ITask
{
    readonly string s_task_xml = "task";

    /// <summary>
    /// Creates a new Task object and adds it to the XML file.
    /// </summary>
    /// <param name="item">The Task object to be created.</param>
    /// <returns>The ID of the newly created Task object.</returns>
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

    /// <summary>
    /// Deletes a Task object from the XML file based on its ID.
    /// </summary>
    /// <param name="id">The ID of the Task object to be deleted.</param>
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
            tas.Remove(Read(id)!);
            XMLTools.SaveListToXMLSerializer<Task>(tas, s_task_xml);

        }
    }

    /// <summary>
    /// Reads a Task object from the XML file based on its ID.
    /// </summary>
    /// <param name="id">The ID of the Task object to be retrieved.</param>
    /// <returns>The Task object with the specified ID, or null if not found.</returns>
    public Task? Read(int id)
    {
        List<Task> tas;
        tas = XMLTools.LoadListFromXMLSerializer<Task>(s_task_xml);
        Task newObject = tas.FirstOrDefault(task => task.Id == id)!;
        return newObject;
       
    }

    /// <summary>
    /// Reads a Task object from the XML file based on a custom filter.
    /// </summary>
    /// <param name="filter">The filter condition for selecting a Task object.</param>
    /// <returns>The first Task object that satisfies the filter condition, or null if not found.</returns>
    public Task? Read(Func<Task, bool> filter)
    {
        List<Task> tas;
        tas = XMLTools.LoadListFromXMLSerializer<Task>(s_task_xml);
            Task newObject = tas.FirstOrDefault(filter)!;
            return newObject;
    }

    /// <summary>
    /// Reads all Task objects from the XML file, optionally filtered.
    /// </summary>
    /// <param name="filter">The optional filter condition for selecting Task objects.</param>
    /// <returns>An IEnumerable of Task objects that satisfy the filter condition, or all objects if no filter is provided.</returns>
    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        IEnumerable<Task> tas;
        tas= XMLTools.LoadListFromXMLSerializer<Task>(s_task_xml);
        if (filter == null)
            return tas.Select(item => item);
        else
            return tas.Where(filter);
    }

    /// <summary>
    /// Updates a Task object in the XML file.
    /// </summary>
    /// <param name="item">The updated Task object.</param>
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
            tas.Remove(Read(item.Id)!);
            tas.Add(item);
        }
        XMLTools.SaveListToXMLSerializer<Task>(tas, s_task_xml);

    }

    public void DeleteAll()
    {
        XElement tas = new XElement("ArrayOfTask");
        XMLTools.SaveListToXMLElement(tas, s_task_xml);
    }
}
