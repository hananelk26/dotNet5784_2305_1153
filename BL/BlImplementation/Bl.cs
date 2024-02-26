

namespace BlImplementation;
using BlApi;
using DO;
using System.Xml.Linq;

/// <summary>
/// Internal class implementing the business logic layer (BL) interface.
/// </summary>
internal class Bl : IBl
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    /// <summary>
    /// Gets an instance of the engineer Implementation.
    /// </summary>
    public IEngineer Engineer => new EngineerImplementation();

    /// <summary>
    /// Gets an instance of the task Implementation.
    /// </summary>
    public ITask Task => new TaskImplementation();

    /// <summary>
    /// Gets an instance of the time Implementation.
    /// </summary>
    public ITime Time => new TimeImplementation();

    /// <summary>
    /// Resets all entities in the data, such as engineers, tasks, and dependencies.
    /// </summary>
    public void ResetsAllEntitiesInTheData()
    {
        _dal.Engineer.DeleteAll();
        _dal.Task.DeleteAll();
        _dal.Dependency.DeleteAll();

    }

    public void InitializeDB() => DalTest.Initialization.Do();

    public void resetDataConfig()
    {
       DalTest.Initialization.resetDataConfig();
    }

}
