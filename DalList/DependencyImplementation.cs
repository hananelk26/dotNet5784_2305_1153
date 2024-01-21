namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;
using System.Data.SqlTypes;



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
        int newId = DalXml.Config.NextDependencyId();
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

    /// <summary>
    /// Reads a dependency from the data source based on the provided filter.
    /// </summary>
    /// <param name="filter">A function that takes a dependency and returns a boolean indicating whether the dependency meets the filtering criteria.</param>
    /// <returns>The first dependency that satisfies the filter, or null if no such dependency is found.</returns>
    public Dependency? Read(Func<Dependency, bool> filter)
    {
        return DataSource.Dependencies.FirstOrDefault(filter);
    }


    /// <summary>
    /// Reads all dependencies from the data source based on an optional filter.
    /// </summary>
    /// <param name="filter">An optional filter function that takes a dependency and returns a boolean indicating whether the dependency meets the filtering criteria.</param>
    /// <returns>
    /// An IEnumerable of dependencies that satisfy the filter if provided, 
    /// or all dependencies if no filter is specified.
    /// </returns>
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
