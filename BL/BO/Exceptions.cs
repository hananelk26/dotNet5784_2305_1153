namespace BO;

[Serializable]
public class BlAlreadyExistsException : Exception
{
    public BlAlreadyExistsException(string? message) : base(message) { }
    //public BlAlreadyExistsException(string message, Exception innerException)
    //            : base(message, innerException) { }

}


[Serializable]
public class BlNullPropertyException : Exception
{
    public BlNullPropertyException(string? message) : base(message) { }
}

public class BlAnEngineerWhoHasATaskCannotBeDeleted : Exception
{
    public BlAnEngineerWhoHasATaskCannotBeDeleted(string? me):base(me) { }
}

public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
}

public class BlinputValidity: Exception
{
    public BlinputValidity(string? message) : base(message) { }
}

public class BlPartOfTheTaskDepends :Exception
{
    public BlPartOfTheTaskDepends(string? me):base(me) { }
}

public class BLTheDateIsNotGood : Exception
{
    public BLTheDateIsNotGood(string? me):base(me) { }
}