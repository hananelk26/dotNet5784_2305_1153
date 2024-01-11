namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;

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
        bool found = false;
        foreach (Dependency item in DataSource.Dependencies.ToList())
        {
            if (item.Id == id)
            {
                DataSource.Dependencies.Remove(item);
                found = true;
                break;
            }
        }
        if (!found)
        {
            throw new Exception($"A Dependency object with ID = {id} does not exist.");
        }
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
    /// Retrieves all Dependency objects.
    /// </summary>
    /// <returns>A list containing all Dependency objects.</returns>
    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencies);
    }

    /// <summary>
    /// Updates an existing Dependency object.
    /// </summary>
    /// <param name="item">The Dependency object with updated data.</param>
    /// <exception cref="Exception">Thrown if the Dependency object with the specified ID does not exist.</exception>
    public void Update(Dependency item)
    {
        Dependency existingDependency = DataSource.Dependencies.FirstOrDefault(dependency => dependency.Id == item.Id);
        if (existingDependency != null)
        {
            DataSource.Dependencies.Remove(existingDependency);
            DataSource.Dependencies.Add(item);
        }
        else
        {
            throw new Exception($"A Dependency object with ID = {item.Id} does not exist.");
        }
    }
}

