namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System;

/// <summary>
/// Implementation of the ITask interface providing CRUD operations for Task objects.
/// </summary>
internal class TaskImplementation : ITask
{
    /// <summary>
    /// Creates a new Task object.
    /// </summary>
    /// <param name="item">The Task object to be created.</param>
    /// <returns>The ID assigned to the newly created Task object.</returns>
    public int Create(Task item)
    {
        int newId = DataSource.Config.NextTaskId;
        Task newObject = item with { Id = newId };
        DataSource.Tasks.Add(newObject);
        return newId;
    }

    /// <summary>
    /// Deletes a Task object by its ID.
    /// </summary>
    /// <param name="id">The ID of the Task object to be deleted.</param>
    /// <exception cref="Exception">Thrown if the Task object with the specified ID does not exist.</exception>
    public void Delete(int id)
    {
        if (Read(id) == null)
        {
            throw new DalDoesNotExistException($"A Task object with ID = {id} does not exist.");
        }

        DataSource.Tasks.Remove(Read(id)!);

    }

    /// <summary>
    /// Retrieves a Task object by its ID.
    /// </summary>
    /// <param name="id">The ID of the Task object to retrieve.</param>
    /// <returns>The retrieved Task object, or null if not found.</returns>
    public Task? Read(int id)
    {
        return DataSource.Tasks.FirstOrDefault(task => task.Id == id);
    }

    /// <summary>
    /// Reads a task from the data source based on the provided filter.
    /// </summary>
    /// <param name="filter">A function that takes a task and returns a boolean indicating whether the task meets the filtering criteria.</param>
    /// <returns>The first task that satisfies the filter, or null if no such task is found.</returns>
    public Task? Read(Func<Task, bool> filter)
    {
        return DataSource.Tasks.FirstOrDefault(filter);
    }

    /// <summary>
    /// Reads all tasks from the data source based on an optional filter.
    /// </summary>
    /// <param name="filter">An optional filter function that takes a task and returns a boolean indicating whether the task meets the filtering criteria.</param>
    /// <returns>
    /// An IEnumerable of tasks that satisfy the filter if provided, 
    /// or all tasks if no filter is specified.
    /// </returns>
    public IEnumerable<Task> ReadAll(Func<Task, bool>? filter)
    {
        if (filter == null)
            return DataSource.Tasks.Select(item => item);
        else
            return DataSource.Tasks.Where(filter);
    }

    /// <summary>
    /// Updates an existing Task object.
    /// </summary>
    /// <param name="item">The Task object with updated data.</param>
    /// <exception cref="Exception">Thrown if the Task object with the specified ID does not exist.</exception>
    public void Update(Task item)
    {
        if (Read(item.Id) == null)
        {
            throw new DalDoesNotExistException($"A Task object with ID = {item.Id} does not exist.");
        }
        else
        {
            DataSource.Tasks.Remove(Read(item.Id)!);
            DataSource.Tasks.Add(item);
        }
    }

    public void DeleteAll()
    {
        DataSource.Tasks.Clear();
    }
}

