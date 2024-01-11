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
    /// Retrieves all items of type T from the data storage.
    /// </summary>
    /// <returns>A list of all items of type T.</returns>
    List<T> ReadAll();

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

