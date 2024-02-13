
namespace BO;

/// <summary>
/// Represents a task within a project, including its details, status, dependencies, and assignments.
/// </summary>
public class Task
{
    /// <summary>
    /// Gets or sets the unique identifier for the task.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the description of the task.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the alias or short name for the task.
    /// </summary>
    public string? Alias { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the task was created.
    /// </summary>
    public DateTime CreatedAtDate { get; set; }

    /// <summary>
    /// Gets or sets the status of the task.
    /// </summary>
    public BO.Status? Status { get; set; }

    /// <summary>
    /// Gets or sets the list of tasks that this task depends on.
    /// </summary>
    public List<BO.TaskInList>? Dependencies { get; set; }

    /// <summary>
    /// Gets or sets the estimated effort required to complete the task, in timespan format.
    /// </summary>
    public TimeSpan? RequiredEfforTime { get; set; }

    /// <summary>
    /// Gets or sets the start date of the task.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the scheduled date for the task.
    /// </summary>
    public DateTime? ScheduledDate { get; set; }

    /// <summary>
    /// Gets or sets the forecasted completion date of the task.
    /// </summary>
    public DateTime? ForecastDate { get; set; }

    /// <summary>
    /// Gets or sets the deadline date for the task.
    /// </summary>
    public DateTime? DeadLineDate { get; set; }

    /// <summary>
    /// Gets or sets the actual completion date of the task.
    /// </summary>
    public DateTime? CompleteDate { get; set; }

    /// <summary>
    /// Gets or sets the deliverables of the task.
    /// </summary>
    public string? Deliverables { get; set; }

    /// <summary>
    /// Gets or sets any remarks related to the task.
    /// </summary>
    public string? Remarks { get; set; }

    /// <summary>
    /// Gets or sets the engineer assigned to the task.
    /// </summary>
    public BO.EngineerInTask? Engineer { get; set; }

    /// <summary>
    /// Gets or sets the complexity of the task, as indicated by the engineer's experience level required.
    /// </summary>
    public BO.EngineerExperience? Complexyity { get; set; }

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string containing the task's properties.</returns>
    public override string ToString() => this.ToStringProperty();
}
