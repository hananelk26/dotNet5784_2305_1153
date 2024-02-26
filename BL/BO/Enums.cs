namespace BO;

/// <summary>
/// Represents the experience level of an engineer.
/// </summary>
public enum EngineerExperience
{
    None,
    /// <summary>
    /// Indicates a beginner level engineer with limited experience.
    /// </summary>
    Beginner,

    /// <summary>
    /// Represents an engineer who has surpassed the beginner stage but is not yet intermediate.
    /// </summary>
    Advanced_Beginner,

    /// <summary>
    /// Denotes an engineer with a moderate level of experience and skill.
    /// </summary>
    Intermediate,

    /// <summary>
    /// Refers to an engineer with advanced skills and experience in their field.
    /// </summary>
    Advanced,

    /// <summary>
    /// Signifies an engineer at the highest level of skill and experience.
    /// </summary>
    Expert
}

/// <summary>
/// Defines the status of a task or project.
/// </summary>
public enum Status
{
    /// <summary>
    /// The task or project has not been scheduled yet.
    /// </summary>
    Unscheduled,

    /// <summary>
    /// The task or project has been scheduled.
    /// </summary>
    Scheduled,

    /// <summary>
    /// The task or project is currently on track to meet its deadlines.
    /// </summary>
    OnTrack,

    /// <summary>
    /// The task or project has been completed.
    /// </summary>
    Done
}

