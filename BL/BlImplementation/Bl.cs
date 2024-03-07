

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
    public ITask Task => new TaskImplementation(this);

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

    public void addDay(int day)
    {
        s_Clock= s_Clock.AddDays(day);
    }

    public void addYear(int y)
    {
        s_Clock= s_Clock.AddYears(y);
    }

    public void addHour(int h)
    {
        s_Clock = s_Clock.AddHours(h);
    }

    public void resetClock()
    {
        s_Clock = DateTime.Now;
            
    }

    private static DateTime s_Clock = DateTime.Now.Date;
    public DateTime Clock { get { return s_Clock; } private set { s_Clock = value; } }



}
