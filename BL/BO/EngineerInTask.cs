namespace BO;

/// <summary>
/// Represents an engineer assigned to a task, including their ID and optionally their name.
/// </summary>
public class EngineerInTask
{
    /// <summary>
    /// Gets or sets the unique identifier for the engineer.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the engineer. The name can be null.
    /// </summary>
    /// <remarks>
    /// The name is optional and might not be provided for all engineers in tasks.
    /// </remarks>
    public string? Name { get; set; }

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string containing the engineer's ID and optionally their name, formatted for display.</returns>
    public override string ToString() => this.ToStringProperty();
}

