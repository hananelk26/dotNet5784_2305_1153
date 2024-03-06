namespace BO;

/// <summary>
/// Represents a simplified view of a task, suitable for listing purposes, with basic details such as ID, description, alias, and status.
/// </summary>
public class TaskInList
{
    /// <summary>
    /// Gets the unique identifier for the task.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets or sets the description of the task.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the alias or short name for the task.
    /// </summary>
    public string Alias { get; set; }

    /// <summary>
    /// Gets or sets the current status of the task.
    /// </summary>
    public Status? Status { get; set; }

    /// <summary>
    /// help fieald for dependencies input window
    /// </summary>
    public bool IsSelected { get; set; } = false;

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string containing the task's basic details, formatted for display.</returns>
    public override string ToString() => this.ToStringProperty();
}

