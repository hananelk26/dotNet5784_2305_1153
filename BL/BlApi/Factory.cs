

namespace BlApi;
/// <summary>
/// Factory class responsible for creating instances of the business logic layer (BL).
/// </summary>
public static class Factory
{
    /// <summary>
    /// Gets an instance of the business logic layer (BL).
    /// </summary>
    /// <returns>An instance of the business logic layer (BL).</returns>
    public static IBl Get() => new BlImplementation.Bl();
}
