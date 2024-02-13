using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

/// <summary>
/// Represents an engineer with specific attributes such as ID, name, email, experience level, cost, and assigned task.
/// </summary>
public class Engineer
{
    /// <summary>
    /// Gets the unique identifier for the engineer.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets or sets the name of the engineer.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the email address of the engineer.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the experience level of the engineer.
    /// </summary>
    public EngineerExperience Level { get; set; }

    /// <summary>
    /// Gets or sets the hourly cost of the engineer.
    /// </summary>
    public double Cost { get; set; }

    /// <summary>
    /// Gets or sets the current task assigned to the engineer, if any.
    /// </summary>
    /// <remarks>
    /// The task is nullable, indicating that the engineer might not always have an active task.
    /// </remarks>
    public TaskInEngineer? Task { get; set; }

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string containing the engineer's properties.</returns>
    public override string ToString() => this.ToStringProperty();
}


