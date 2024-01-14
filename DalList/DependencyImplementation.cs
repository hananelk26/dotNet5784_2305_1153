namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;
using System;



/// <summary>
/// Implementation of the IDependency interface providing CRUD operations for Dependency objects.
/// </summary>
internal class DependencyImplementation : IDependency
{
    /// <summary>
    /// Creates a new Dependency object.
    /// </summary>
    /// <param name="item">The Dependency object to be created.</param>
    /// <returns>The ID assigned to the newly created Dependency object.</returns>
    public int Create(Dependency item)
    {
        int newId = DataSource.Config.NextDependencyId;
        Dependency newObject = item with { Id = newId };
        DataSource.Dependencies.Add(newObject);
        return newId;
    }

    /// <summary>
    /// Deletes a Dependency object by its ID.
    /// </summary>
    /// <param name="id">The ID of the Dependency object to be deleted.</param>
    /// <exception cref="Exception">Thrown if the Dependency object with the specified ID does not exist.</exception>
    public void Delete(int id)
    {

        if (Read(id) == null)
        {
            throw new DalDoesNotExistException($"A Dependency object with ID = {id} does not exist.");
        }

        DataSource.Dependencies.Remove(Read(id));
    }

    /// <summary>
    /// Retrieves a Dependency object by its ID.
    /// </summary>
    /// <param name="id">The ID of the Dependency object to retrieve.</param>
    /// <returns>The retrieved Dependency object, or null if not found.</returns>
    public Dependency? Read(int id)
    {
        return DataSource.Dependencies.FirstOrDefault(dependency => dependency.Id == id);
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        return DataSource.Dependencies.FirstOrDefault(filter);
    }


    /// <summary>
    /// Retrieves all Dependency objects.
    /// </summary>
    /// <returns>A list containing all Dependency objects.</returns>
    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter)
    {
        if (filter != null)
        {
            return DataSource.Dependencies.Where(filter);
        }
        else
        {
            return DataSource.Dependencies.Select(item => item);
        }


        // return DataSource.Dependencies.Select(item => item);
    }

    /// <summary>
    /// Updates an existing Dependency object.
    /// </summary>
    /// <param name="item">The Dependency object with updated data.</param>
    /// <exception cref="Exception">Thrown if the Dependency object with the specified ID does not exist.</exception>
    public void Update(Dependency item)
    {

        if (Read(item.Id) == null)
        {
            throw new DalDoesNotExistException($"A Dependency object with ID = {item.Id} does not exist.");
        }

        DataSource.Dependencies.Remove(Read(item.Id));
        DataSource.Dependencies.Add(item);
    }

}
