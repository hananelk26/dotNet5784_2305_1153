

namespace BlApi;
using DO;
using System;

public interface IEngineer
{
    /// <summary>
    /// Reads all engineers based on an optional filter.
    /// </summary>
    /// <param name="filter">An optional filter function.</param>
    /// <returns>An IEnumerable of Engineer objects.</returns>
    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool>? filter = null);

    /// <summary>
    /// Reads an engineer by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the engineer.</param>
    /// <returns>The Engineer object if found; otherwise, null.</returns>
    public BO.Engineer? Read(int id);

    /// <summary>
    /// Creates a new engineer and returns its unique identifier.
    /// </summary>
    /// <param name="boEngineer">The Engineer object to be created.</param>
    /// <returns>The unique identifier of the newly created engineer.</returns>
    public int Create(BO.Engineer boEngineer);

    /// <summary>
    /// Deletes an engineer by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the engineer to be deleted.</param>
    public void Delete(int id);

    /// <summary>
    /// Updates an existing engineer.
    /// </summary>
    /// <param name="item">The Engineer object with updated information.</param>
    public void Update(BO.Engineer item);

    /// <summary>
    /// Deletes all engineers.
    /// </summary>
    public void DeleteAll();
}
