

namespace Dal;

using DalApi;
using DO;
using System.Collections;
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
        Engineer eng;
        eng = Read(item.Id)!;

        if (eng != null)
        {
            throw new DalAlreadyExistsException($"An Engineer object with ID = {item.Id} already exists.");
        }

        DataSource.Engineers.Add(item);
        return item.Id;
    }

    /// <summary>
    /// Deletes an Engineer object by its ID.
    /// </summary>
    /// <param name="id">The ID of the Engineer object to be deleted.</param>
    /// <exception cref="Exception">Thrown if the Engineer object with the specified ID does not exist.</exception>
    public void Delete(int id)
    {
        if (Read(id) == null)
        {
            throw new DalDoesNotExistException($"An Engineer object with ID = {id} does not exist.");
        }

        DataSource.Engineers.Remove(Read(id));
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
    /// Reads an engineer from the data source based on the provided filter.
    /// </summary>
    /// <param name="filter">A function that takes an engineer and returns a boolean indicating whether the engineer meets the filtering criteria.</param>
    /// <returns>The first engineer that satisfies the filter, or null if no such engineer is found.</returns>
    public Engineer? Read(Func<Engineer, bool> filter)
    {
        return DataSource.Engineers.FirstOrDefault(filter);
    }

    /// <summary>
    /// Reads all engineers from the data source based on an optional filter.
    /// </summary>
    /// <param name="filter">An optional filter function that takes an engineer and returns a boolean indicating whether the engineer meets the filtering criteria.</param>
    /// <returns>
    /// An IEnumerable of engineers that satisfy the filter if provided, 
    /// or all engineers if no filter is specified.
    /// </returns>
    public IEnumerable<Engineer> ReadAll(Func<Engineer, bool>? filter)
    {
        if (filter == null)
            return DataSource.Engineers.Select(item => item);
        else
            return DataSource.Engineers.Where(filter);
    }

    /// <summary>
    /// Updates an existing Engineer object.
    /// </summary>
    /// <param name="item">The Engineer object with updated data.</param>
    /// <exception cref="Exception">Thrown if the Engineer object with the specified ID does not exist.</exception>
    public void Update(Engineer item)
    {
        Engineer? existingEngineer = Read(item.Id); //DataSource.Engineers.FirstOrDefault(engineer => engineer.Id == item.Id);
        if (existingEngineer == null)
        {
            throw new DalDoesNotExistException($"An Engineer object with ID = {item.Id} does not exist.");
        }
        
        DataSource.Engineers.Remove(existingEngineer);
        DataSource.Engineers.Add(item);
    }

    public void DeleteAll()
    {
        DataSource.Engineers.Clear();
    }
}
