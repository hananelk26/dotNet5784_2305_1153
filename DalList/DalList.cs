﻿namespace Dal;

using DalApi;
/// <summary>
/// Represents a sealed class implementing the IDal interface.
/// </summary>
/// <remarks>
/// This class provides specific implementations for the IDependency, IEngineer, and ITask properties.
/// </remarks>
sealed internal class DalList : IDal
{

    public static IDal Instance { get; } = new DalList();
    private DalList() {; }

    /// <summary>
    /// Gets the dependency component for managing dependencies.
    /// </summary>
    public IDependency Dependency => new DependencyImplementation();

    /// <summary>
    /// Gets the engineer component for managing engineers.
    /// </summary>
    public IEngineer Engineer => new EngineerImplementation();

    /// <summary>
    /// Gets the task component for managing tasks.
    /// </summary>
    public ITask Task => new TaskImplementation();

    public ITIme Time => new TimeImplementation();

    public IMainClock MainClock => new MainClockImplementation();

    public IResetDataConfig ResetDataConfig => new ResetDataConfigImplamantation();
}

