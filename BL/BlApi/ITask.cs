
namespace BlApi;

public interface ITask
{
    /// <summary>
    /// Reads all tasks based on an optional filter.
    /// </summary>
    /// <param name="filter">An optional filter function.</param>
    /// <returns>An IEnumerable of Task objects.</returns>
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null);


    public IEnumerable<BO.TaskInList> ReadAllTaskInList(Func<BO.Task, bool>? filter = null);
    

    /// <summary>
    /// Reads a task by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the task.</param>
    /// <returns>The Task object if found; otherwise, null.</returns>
    public BO.Task? Read(int id);

    /// <summary>
    /// Creates a new task and returns its unique identifier.
    /// </summary>
    /// <param name="boTask">The Task object to be created.</param>
    /// <returns>The unique identifier of the newly created task.</returns>
    public int Create(BO.Task boTask);

    /// <summary>
    /// Updates an existing task.
    /// </summary>
    /// <param name="boTask">The Task object with updated information.</param>
    public void Update(BO.Task boTask);

    /// <summary>
    /// Deletes a task by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the task to be deleted.</param>
    public void Delete(int id);

    /// <summary>
    /// Updates the start time of a task by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the task to be updated.</param>
    /// <param name="t">The new start time for the task.</param>
    public void UpdateStartTask(int id, DateTime t);

    /// <summary>
    /// Deletes all tasks.
    /// </summary>
    public void DeleteAll();

    public void PutDatesOnAllExistingTasks(DateTime? DateOfStartProject);

    public DateTime? EndDateOfProject(DateTime? DateOfStartProject);
}
