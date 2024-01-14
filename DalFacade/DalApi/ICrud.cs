using DO;
/// <summary>
/// Interface for basic CRUD (Create, Read, Update, Delete) operations for objects of type T.
/// </summary>
/// <typeparam name="T">The type of objects being manipulated.</typeparam>
namespace DalApi;
public interface ICrud<T> where T : class
{
    /// <summary>
    /// Creates a new item of type T in the data storage.
    /// </summary>
    /// <param name="item">The item to be created.</param>
    /// <returns>The ID of the newly created item.</returns>
    int Create(T item);

    /// <summary>
    /// Retrieves an item of type T from the data storage by its ID.
    /// </summary>
    /// <param name="id">The ID of the item to retrieve.</param>
    /// <returns>The item of type T if found; otherwise, null.</returns>
    T? Read(int id);

    /// <summary>
    /// Reads an object of type T from the data source based on the provided filter.
    /// </summary>
    /// <param name="filter">A function that takes an object of type T and returns a boolean indicating whether it meets the filtering criteria.</param>
    /// <returns>
    /// The first object of type T that satisfies the filter, or null if no such object is found.
    /// </returns>
    T? Read(Func<T, bool> filter);

    /// <summary>
    /// Reads all objects of type T from the data source based on an optional filter.
    /// </summary>
    /// <param name="filter">
    /// An optional filter function that takes an object of type T and returns a boolean indicating whether it meets the filtering criteria.
    /// If not provided (null), all objects of type T will be returned.
    /// </param>
    /// <returns>
    /// An IEnumerable of objects of type T that satisfy the filter if provided, 
    /// or all objects of type T if no filter is specified.
    /// </returns>
    IEnumerable<T?> ReadAll(Func<T, bool>? filter = null);

    /// <summary>
    /// Updates an existing item of type T in the data storage.
    /// </summary>
    /// <param name="item">The updated item to replace the existing one.</param>
    void Update(T item);

    /// <summary>
    /// Deletes an item of type T from the data storage by its ID.
    /// </summary>
    /// <param name="id">The ID of the item to delete.</param>
    void Delete(int id);

}

