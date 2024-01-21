namespace DO;

/// <summary>
/// Represents an exception thrown when attempting to access or perform an operation
/// on a data access layer (DAL) entity that does not exist.
/// </summary>
[Serializable]
public class DalDoesNotExistException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DalDoesNotExistException"/> class
    /// with an optional error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public DalDoesNotExistException(string? message) : base(message) { }
}

/// <summary>
/// Represents an exception thrown when attempting to create a new data access layer (DAL) entity,
/// but an entity with the same identifier or unique key already exists.
/// </summary>
[Serializable]
public class DalAlreadyExistsException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DalAlreadyExistsException"/> class
    /// with an optional error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public DalAlreadyExistsException(string? message) : base(message) { }
}

/// <summary>
/// Represents an exception thrown when the deletion of a data access layer (DAL) entity is not possible
/// or allowed under certain conditions.
/// </summary>
[Serializable]
public class DalDeletionImpossible : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DalDeletionImpossible"/> class
    /// with an optional error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public DalDeletionImpossible(string? message) : base(message) { }
}


public class DalXMLFileLoadCreateException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DalXMLFileLoadCreateException"/> class
    /// with an optional error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public DalXMLFileLoadCreateException(string? message) : base(message) { }
}
