

namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Implementation of the IEngineer interface providing CRUD operations for Engineer objects.
/// </summary>
internal class EngineerImplementation : IEngineer
{
    /// <summary>
    /// Creates a new Engineer object.
    /// </summary>
    /// <param name="item">The Engineer object to be created.</param>
    /// <returns>The ID assigned to the newly created Engineer object.</returns>
    public int Create(Engineer item)
    {
        bool notFound = true;
        foreach (Engineer existingEngineer in DataSource.Engineers)
        {
            if (existingEngineer.Id == item.Id)
            {
                notFound = false;
                break;
            }
        }

        if (notFound)
        {
            DataSource.Engineers.Add(item);
            return item.Id;
        }

        throw new Exception($"An Engineer object with ID = {item.Id} already exists.");
    }

    /// <summary>
    /// Deletes an Engineer object by its ID.
    /// </summary>
    /// <param name="id">The ID of the Engineer object to be deleted.</param>
    /// <exception cref="Exception">Thrown if the Engineer object with the specified ID does not exist.</exception>
    public void Delete(int id)
    {
        bool found = false;
        foreach (Engineer item in DataSource.Engineers.ToList())
        {
            if (item.Id == id)
            {
                DataSource.Engineers.Remove(item);
                found = true;
                break;
            }
        }

        if (!found)
        {
            throw new Exception($"An Engineer object with ID = {id} does not exist.");
        }
    }

    /// <summary>
    /// Retrieves an Engineer object by its ID.
    /// </summary>
    /// <param name="id">The ID of the Engineer object to retrieve.</param>
    /// <returns>The retrieved Engineer object, or null if not found.</returns>
    public Engineer? Read(int id)
    {
        return DataSource.Engineers.FirstOrDefault(engineer => engineer.Id == id);
    }

    /// <summary>
    /// Retrieves all Engineer objects.
    /// </summary>
    /// <returns>A list containing all Engineer objects.</returns>
    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    /// <summary>
    /// Updates an existing Engineer object.
    /// </summary>
    /// <param name="item">The Engineer object with updated data.</param>
    /// <exception cref="Exception">Thrown if the Engineer object with the specified ID does not exist.</exception>
    public void Update(Engineer item)
    {
        Engineer existingEngineer = DataSource.Engineers.FirstOrDefault(engineer => engineer.Id == item.Id);
        if (existingEngineer != null)
        {
            DataSource.Engineers.Remove(existingEngineer);
            DataSource.Engineers.Add(item);
        }
        else
        {
            throw new Exception($"An Engineer object with ID = {item.Id} does not exist.");
        }
    }
}
