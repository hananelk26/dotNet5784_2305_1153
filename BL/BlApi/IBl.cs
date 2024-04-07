using BO;

namespace BlApi;

public interface IBl
{ 
    /// <summary>
    /// Gets an instance of the engineer interface.
    /// </summary>
    public IEngineer Engineer { get; }

    /// <summary>
    /// Gets an instance of the task interface.
    /// </summary>
    public ITask Task { get; }

    /// <summary>
    /// Gets an instance of the time interface.
    /// </summary>
    public ITime Time { get; }

    public IMainClock MainClock { get;  }

    /// <summary>
    /// Resets all entities in the data, such as engineers, tasks, and time.
    /// </summary>
    public void ResetsAllEntitiesInTheData();

    public void InitializeDB();
    public void resetDataConfig();

    public bool IsCircularDependency(List<TaskInList> dependencyes,int IdOfDependentTask);
}
