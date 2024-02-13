using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

/// <summary>
/// Represents a Data Access Layer (DAL) interface providing access to various dependencies.
/// </summary>
public interface IDal
{
    /// <summary>
    /// Gets the dependency component for managing dependencies.
    /// </summary>
    IDependency Dependency { get; }

    /// <summary>
    /// Gets the engineer component for managing engineers.
    /// </summary>
    IEngineer Engineer { get; }

    /// <summary>
    /// Gets the task component for managing tasks.
    /// </summary>
    ITask Task { get; }

    ITIme Time { get; }
}

