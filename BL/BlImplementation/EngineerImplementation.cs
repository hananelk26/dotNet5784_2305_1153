

namespace BlImplementation;
using BlApi;
using System.Text.RegularExpressions;

internal class EngineerImplementation : IEngineer
{
    /// <summary>
    /// Data access layer instance for interacting with engineers.
    /// </summary>
    private DalApi.IDal _dal = DalApi.Factory.Get;

   


    /// <summary>
    /// Creates a new engineer in the system.
    /// </summary>
    /// <param name="boEngineer">The BO.Engineer object representing the new engineer.</param>
    /// <returns>The unique identifier of the newly created engineer.</returns>
    public int Create(BO.Engineer boEngineer)
    {
        // Validate the input before creating a new engineer
        inputValidity(boEngineer);

        // Convert BO.Engineer to DO.Engineer and create the engineer in the data access layer
        DO.Engineer doEngineer = new DO.Engineer(boEngineer.Id, boEngineer.Email, boEngineer.Cost, boEngineer.Name, (DO.EngineerExperience)boEngineer.Level);
        try
        {
            int IdEngineer = _dal.Engineer.Create(doEngineer);
            return IdEngineer;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            // Handle the exception for duplicate engineer ID
            throw new BO.BlAlreadyExistsException($"Engineer with ID={boEngineer.Id} already exists"/*, ex*/);
        }
    }

    /// <summary>
    /// Deletes an engineer by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the engineer to be deleted.</param>
    public void Delete(int id)
    {
        // Check if the engineer has any associated tasks
        var temp = _dal.Task.ReadAll().Where(t => t!.EngineerId == id);
        if (temp.Any())
        {
            // An engineer with an associated task cannot be deleted
            throw new BO.BlAnEngineerWhoHasATaskCannotBeDeleted("An engineer who has a task cannot be deleted");
        }

        try
        {
            // Delete the engineer in the data access layer
            _dal.Engineer.Delete(id);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            // Handle the exception for non-existent engineer
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist"/*, ex*/);
        }
    }

    /// <summary>
    /// Reads an engineer by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the engineer to be read.</param>
    /// <returns>The BO.Engineer object representing the read engineer.</returns>
    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            // Handle the case where the engineer does not exist
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");

        if (condition(doEngineer))// The engineer is currently working on a task.
        {
            var ret = _dal.Task.ReadAll().Where(t => t!.EngineerId != null && t.EngineerId == doEngineer.Id && t.CompleteDate == null).FirstOrDefault();
            int IdOfTask = ret!.Id;
            string AliasOfTask = ret!.Alias;

            return new BO.Engineer()
            {
                Id = id,
                Name = doEngineer.Name,
                Email = doEngineer.Email,
                Cost = doEngineer.Cost,
                Level = (BO.EngineerExperience)doEngineer.Level,
                Task = new BO.TaskInEngineer()
                {
                    Id = IdOfTask,
                    Alias = AliasOfTask
                }
            };
        }
        else
        {
            return new BO.Engineer()
            {
                Id = id,
                Name = doEngineer.Name,
                Email = doEngineer.Email,
                Cost = doEngineer.Cost,
                Level = (BO.EngineerExperience)doEngineer.Level,
                Task = null
            };
        }
    }

    /// <summary>
    /// Reads all engineers in the system.
    /// </summary>
    /// <param name="filter">An optional filter to apply to the list of engineers.</param>
    /// <returns>An IEnumerable of BO.Engineer objects representing all engineers in the system.</returns>
    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool>? filter = null)
    {
        // Read all engineers from the data access layer
        IEnumerable<BO.Engineer> ans;
        ans = (from DO.Engineer item in _dal.Engineer.ReadAll()
               select condition(item) ?
               new BO.Engineer
               {
                   Id = item.Id,
                   Cost = item.Cost,
                   Name = item.Name,
                   Email = item.Email,
                   Level = (BO.EngineerExperience)item.Level,
                   Task = new BO.TaskInEngineer()
                   {
                       Id = _dal.Task.ReadAll().Where(t =>t!.EngineerId!= null && t?.EngineerId == item.Id && t.CompleteDate == null).FirstOrDefault()!.Id,
                       Alias = _dal.Task.ReadAll().Where(t => t!.EngineerId != null && t?.EngineerId == item.Id && t.CompleteDate == null).FirstOrDefault()!.Alias
                   }
               }

               : new BO.Engineer
               {
                   Id = item.Id,
                   Cost = item.Cost,
                   Name = item.Name,
                   Email = item.Email,
                   Level = (BO.EngineerExperience)item.Level,
                   Task = null
               }
               );
        if (filter != null)
        {
            return ans.Where(filter);
        }
        return ans;
    }
    /// <inheritdoc/>
    /// <exception cref="BO.BlDoesNotExistException">Thrown if the engineer with the specified ID does not exist.</exception>
    public void Update(BO.Engineer item)
    {
        inputValidity(item);

        DO.Engineer? doEng = _dal.Engineer.Read(item.Id);

        if (doEng != null)
        {
            if (doEng.Level > (DO.EngineerExperience)item.Level)
            {
                throw new BO.BlinputValidity("An engineer's level cannot decrease");
            }
        }

        DO.Engineer doEngineer = new DO.Engineer()
        {
            Id = item.Id,
            Name = item.Name,
            Email = item.Email,
            Level = (DO.EngineerExperience)item.Level,
            Cost = item.Cost
        };
        try
        {
            _dal.Engineer.Update(doEngineer);
        }
        catch (DO.DalDoesNotExistException)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={item.Id} does Not exist");
        }

        if (item.Task != null)
        {
            var task = _dal.Task.ReadAll().Where(t => t?.Id == item.Task.Id).FirstOrDefault();
            if (task != null)
            {
                DO.Task doTask = new DO.Task()
                {
                    EngineerId = item.Id,
                    Id = task.Id,
                    Description = task.Description,
                    Alias = task.Alias,
                    createdAtDate = task.createdAtDate,
                    RequiredEffortTime = task.RequiredEffortTime,
                    Copmlexity = task.Copmlexity,
                    StartDate = task.StartDate,
                    ScheduledDate = task.ScheduledDate,
                    DeadlineDate = task.DeadlineDate,
                    CompleteDate = task.CompleteDate,
                    Deliverables = task.Deliverables,
                    Remarks = task.Remarks
                };

                try
                {
                    _dal.Task.Update(doTask);
                }
                catch (DO.DalDoesNotExistException)
                {
                    throw new BO.BlDoesNotExistException($"Task with ID={doTask.Id} does Not exist");
                }
            }
        }
    }

    /// <inheritdoc/>
    public void DeleteAll()
    {
        _dal.Engineer.DeleteAll();
    }

    /// <summary>
    /// Checks whether the engineer has an active task.
    /// </summary>
    /// <param name="item">The engineer to check.</param>
    /// <returns>True if the engineer has an active task; otherwise, false.</returns>
    bool condition(DO.Engineer item)
    {
        var ret = _dal.Task.ReadAll().Where(t => t!.EngineerId != null && t.EngineerId == item.Id && t.CompleteDate == null).FirstOrDefault();
        if (ret != null)
            return true;
        return false;
    }

    /// <summary>
    /// Validates the input data for an engineer.
    /// </summary>
    /// <param name="boEng">The engineer to validate.</param>
    /// <exception cref="BO.BlinputValidity">Thrown if there is a problem with the integrity of the data.</exception>
    static public void inputValidity(BO.Engineer boEng)
    {
        if (!IsValidEmail(boEng.Email) || !IsValidName(boEng.Name) || boEng.Cost < 0 || boEng.Id < 0)
        {
            throw new BO.BlinputValidity("There is a problem with the integrity of the data");
        }
    }

    /// <summary>
    /// Validates if the given email is in a correct format.
    /// </summary>
    /// <param name="email">The email to validate.</param>
    /// <returns>True if the email is valid; otherwise, false.</returns>
    static bool IsValidEmail(string email)
    {
        // Regular expression for a simple email validation
        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, emailPattern);
    }

    /// <summary>
    /// Validates if the given name contains only allowed characters.
    /// </summary>
    /// <param name="name">The name to validate.</param>
    /// <returns>True if the name is valid; otherwise, false.</returns>
    static bool IsValidName(string name)
    {
        // Regular expression for a simple name validation
        // Allows letters, spaces, and some common special characters
        string namePattern = @"^[a-zA-Z\s\.'-]*$";
        return Regex.IsMatch(name, namePattern);
    }
}