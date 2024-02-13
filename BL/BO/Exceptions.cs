namespace BO;

/// <summary>
/// Exception for signaling that an attempt was made to create an entity that already exists.
/// </summary>
[Serializable]
public class BlAlreadyExistsException : Exception
{
    public BlAlreadyExistsException(string? message) : base(message) { }
    // Additional constructor can be documented similarly if uncommented.
}

/// <summary>
/// Exception for indicating that a null property was encountered where it is not allowed.
/// </summary>
[Serializable]
public class BlNullPropertyException : Exception
{
    public BlNullPropertyException(string? message) : base(message) { }
}

/// <summary>
/// Exception thrown when an attempt is made to delete an engineer who is currently assigned to a task.
/// </summary>
public class BlAnEngineerWhoHasATaskCannotBeDeleted : Exception
{
    public BlAnEngineerWhoHasATaskCannotBeDeleted(string? message) : base(message) { }
}

/// <summary>
/// Exception indicating that the requested entity does not exist.
/// </summary>
public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
}

/// <summary>
/// Exception thrown when input validation fails.
/// </summary>
public class BlinputValidity : Exception
{
    public BlinputValidity(string? message) : base(message) { }
}

/// <summary>
/// Exception indicating that a task cannot be modified or deleted because part of it depends on another component or task.
/// </summary>
public class BlPartOfTheTaskDepends : Exception
{
    public BlPartOfTheTaskDepends(string? message) : base(message) { }
}

/// <summary>
/// Exception thrown when a date-related validation fails, indicating the provided date is not acceptable.
/// </summary>
public class BLTheDateIsNotGood : Exception
{
    public BLTheDateIsNotGood(string? message) : base(message) { }
}
