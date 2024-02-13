
using System.Net.NetworkInformation;

namespace BO;

/// <summary>
/// Represents a task assigned to an engineer, identified by an ID and an alias.
/// </summary>
public class TaskInEngineer
{
    /// <summary>
    /// Gets the unique identifier for the task assigned to the engineer.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets or sets the alias or short name for the task.
    /// </summary>
    public string Alias { get; set; }

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string containing the task's ID and alias, formatted for display.</returns>
    public override string ToString() => this.ToStringProperty();
}

