
using BlApi;
using BO;
using System.Data;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace BlImplementation;

/// <summary>
/// Represents an implementation of the ITask interface.
/// </summary>
internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

 

    /// <summary>
    /// Creates a new task based on the provided BO.Task object.
    /// </summary>
    /// <param name="boTask">The BO.Task object containing task information.</param>
    /// <returns>The ID of the created task.</returns>
    public int Create(BO.Task boTask)
    {
        inputValidity(boTask);

        if (boTask.Dependencies != null)
        {
            foreach (var dependId in boTask.Dependencies!)
            {
                DO.Dependency dependency = new DO.Dependency()
                {
                    DependentTask = boTask.Id,
                    DependsOnTask = dependId.Id
                };

                var ret = _dal.Dependency.ReadAll(d => d.DependentTask == boTask.Id && d.DependsOnTask == dependId.Id).FirstOrDefault();
                if (ret == null)
                {
                    _dal.Dependency.Create(dependency);
                }
            }
        }

        DO.Task task = new DO.Task()
        {
            Id = boTask.Id,
            Description = boTask.Description!,
            Alias = boTask.Alias!,
            createdAtDate = boTask.CreatedAtDate,
            RequiredEffortTime = boTask.RequiredEfforTime,
            Copmlexity = (DO.EngineerExperience)boTask.Complexyity!,
            Deliverables = boTask.Deliverables,
            Remarks = boTask.Remarks,
        };

        try
        {
            return _dal.Task.Create(task);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={boTask.Id} already exists"/*, ex*/);
        }
    }

    /// <summary>
    /// Deletes a task with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the task to be deleted.</param>
    public void Delete(int id)
    {
        var t = _dal.Task.Read(id);
        if (t == null) { throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist"); }
        var de = _dal.Dependency.ReadAll();
        foreach (var d in de)
        {
            if (id == d!.DependsOnTask)
                throw new BO.BlPartOfTheTaskDepends("there is a task that depends on this task");
        }
        _dal.Task.Delete(id);
        foreach (var d in de)
        {
            if (id == d!.DependentTask)
                _dal.Dependency.Delete(d.Id);
        }
    }


    /// <summary>
    ///  Retrieves a task with the specified ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>The retrieved task as a BO.Task object, or null if the task does not exist.</returns>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public BO.Task? Read(int id)
    {
        var dalTask = _dal.Task.Read(id);
        if (dalTask == null) { throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist"); }
        BO.Task boTask = new BO.Task()
        {
            Id = dalTask.Id,
            Description = dalTask.Description,
            Alias = dalTask.Alias,
            CreatedAtDate = dalTask.createdAtDate,
            Status = status(dalTask),
            Dependencies = depe(dalTask),
            RequiredEfforTime = dalTask.RequiredEffortTime,
            StartDate = dalTask.StartDate,
            ScheduledDate = dalTask.ScheduledDate,
            ForecastDate = forecastDate(dalTask),
            DeadLineDate = dalTask.DeadlineDate,
            CompleteDate = dalTask.CompleteDate,
            Deliverables = dalTask.Deliverables,
            Remarks = dalTask.Remarks,
            Engineer = en(dalTask),
            Complexyity = (BO.EngineerExperience?)dalTask.Copmlexity!

        };


        return boTask;


    }

    /// <summary>
    /// Retrieves all tasks from the data access layer and optionally applies a filter.
    /// </summary>
    /// <param name="filter">An optional filter function to apply to the tasks.</param>
    /// <returns>
    /// An IEnumerable collection of BO.Task objects representing all tasks retrieved from the data access layer.
    /// If a filter is provided, only tasks that satisfy the filter conditions are included in the result.
    /// </returns>
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        IEnumerable<DO.Task?> dalTasks = _dal.Task.ReadAll()!;
        IEnumerable<BO.Task> boTasks = dalTasks.Select(dalTask => new BO.Task()
        {
            Id = dalTask.Id,
            Description = dalTask.Description,
            Alias = dalTask.Alias,
            CreatedAtDate = dalTask.createdAtDate,
            Status = status(dalTask),
            Dependencies = depe(dalTask),
            RequiredEfforTime = dalTask.RequiredEffortTime,
            StartDate = dalTask.StartDate,
            ScheduledDate = dalTask.ScheduledDate,
            ForecastDate = forecastDate(dalTask),
            DeadLineDate = dalTask.DeadlineDate,
            CompleteDate = dalTask.CompleteDate,
            Deliverables = dalTask.Deliverables,
            Remarks = dalTask.Remarks,
            Engineer = en(dalTask),
            Complexyity = (BO.EngineerExperience?)dalTask.Copmlexity!,


        });
        if (filter != null)
        {
            boTasks = boTasks.Where(filter);
        }
        return boTasks;

    }

    public IEnumerable<BO.TaskInList> ReadAllTaskInList(Func<BO.Task, bool>? filter = null)
    {
        return ReadAll(filter).Select(item => new TaskInList()
        {
            Id = item.Id,
            Description = item.Description!,
            Alias = item.Alias!,
            Status = item.Status,
        });
    }


    /// <summary>
    /// Updates an existing task in the data access layer based on the provided business object.
    /// </summary>
    /// <param name="boTask">The business object representing the task to be updated.</param>
    /// <exception cref="BO.BlDoesNotExistException">Thrown if the task with the specified ID does not exist.</exception>
    public void Update(BO.Task boTask)
    {
        inputValidity(boTask);

        var deps = _dal.Dependency.ReadAll();
        if (deps != null)
        {
            foreach (var item in deps)
            {
                if (item!.DependentTask == boTask.Id)
                {
                    _dal.Dependency.Delete(item.Id);
                }
            }
        }

        foreach (var dependId in boTask.Dependencies!)
        {
            DO.Dependency dependency = new DO.Dependency()
            {
                DependentTask = boTask.Id,
                DependsOnTask = dependId.Id
            };
            _dal.Dependency.Create(dependency);

        }


        DO.Task task = new DO.Task()
        {
            Id = boTask.Id,
            Description = boTask.Description!,
            Alias = boTask.Alias!,
            createdAtDate = boTask.CreatedAtDate,
            RequiredEffortTime = boTask.RequiredEfforTime,
            Copmlexity = (DO.EngineerExperience?)boTask.Complexyity!,
            StartDate = boTask.StartDate,
            ScheduledDate = boTask.ScheduledDate,
            DeadlineDate = boTask.DeadLineDate,
            CompleteDate = boTask.CompleteDate,
            Deliverables = boTask.Deliverables,
            Remarks = boTask.Remarks,
            EngineerId = boTask.Engineer != null ? boTask.Engineer.Id : null,

        };


        try
        {
            _dal.Task.Update(task);
        }
        catch (DO.DalDoesNotExistException)
        {

            throw new BO.BlDoesNotExistException($"Engineer with ID={task.Id} does Not exist");
        }

    }

    /// <summary>
    /// Updates the scheduled start date of a task and performs validation based on dependencies.
    /// </summary>
    /// <param name="id">The ID of the task to be updated.</param>
    /// <param name="tim">The new scheduled start date for the task.</param>
    /// <exception cref="BLTheDateIsNotGood">Thrown if the scheduled start date is not valid based on dependencies.</exception>
    public void UpdateStartTask(int id, DateTime tim)
    {
        var Tasks = _dal.Task.ReadAll();
        var task = _dal.Task.Read(id);
        var dep = _dal.Dependency.ReadAll();

        foreach (var d in dep)
        {
            if (d.DependentTask == id)
            {
                foreach (var t in Tasks)
                {
                    if (t.Id == d.DependsOnTask)
                    {
                        if (t.ScheduledDate == null)
                            throw new BLTheDateIsNotGood($"Previous task With ID = {t.Id} have not been given a start date");
                        if (t.ScheduledDate + t.RequiredEffortTime > tim)
                            throw new BLTheDateIsNotGood($"A task cannot start before the estimated end time ({t.ScheduledDate + t.RequiredEffortTime}) of the task WIt ID = {t.Id} that precedes it");
                    }

                }

            }
        }
        _dal.Task.Update(task with { ScheduledDate = tim });
    }

    public void PutDatesOnAllExistingTasks(DateTime? DateOfStartProject)
    {
        var TasksWithoutAScheduleDate = new List<DO.Task>();
        var taskToRemove = new List<DO.Task>();
        var tasks = _dal.Task.ReadAll();
        var dependencyes = _dal.Dependency.ReadAll();
       

        foreach (var task in tasks)
        {
            if(task.RequiredEffortTime == null)
            {
                throw new Exception("The time it takes to finish the task has not been updated for all tasks");
            }
        }

        // Setting a scheduled date for all tasks that do not depend on any other task
        foreach (var  Task in tasks)
        {
            bool IsTheTaskIndependentOfPreviousTasks = true;
            foreach (var dependency in dependencyes)
            {
                if (dependency.DependentTask == Task.Id)
                {
                    IsTheTaskIndependentOfPreviousTasks = false;
                }
            }

            if (IsTheTaskIndependentOfPreviousTasks)
            {
                DO.Task UpdateTask = Task with { ScheduledDate = DateOfStartProject };
                _dal.Task.Update(UpdateTask);
            }
            else
            {
                TasksWithoutAScheduleDate.Add(Task);
            }

        }

        while (TasksWithoutAScheduleDate.Any())
        {
            foreach(var Task in TasksWithoutAScheduleDate)
            {
                DateTime? Max = DateOfStartProject;
                bool ThereAreNoPreviousTasksThatDoNotHaveAScheduleDate = true;

                foreach (var Dependency in dependencyes) // Take a task from the list and check if the task has previous tasks that have not had a schedule Date updated
                {
                    if (Dependency.DependentTask == Task.Id)
                    {
                        var Tas = _dal.Task.Read((int)Dependency.DependsOnTask);//the previous task
                        if (Tas.ScheduledDate == null)
                        {
                            ThereAreNoPreviousTasksThatDoNotHaveAScheduleDate = false;
                            break;
                        }
                        else
                        {
                            Max = ((Tas.ScheduledDate + Tas.RequiredEffortTime) > Max ? (Tas.ScheduledDate + Tas.RequiredEffortTime) : Max);
                        }
                    }
                }

                if (ThereAreNoPreviousTasksThatDoNotHaveAScheduleDate)
                {
                    DO.Task UpdateTask = Task with { ScheduledDate = Max };
                    _dal.Task.Update(UpdateTask);
                    taskToRemove.Add(Task);
                }

            }
            foreach (var item in taskToRemove)
            {
                TasksWithoutAScheduleDate.Remove(item);
            } 
        }

    }

    public DateTime? EndDateOfProject(DateTime? DateOfStartProject)
    {
        var tasks = _dal.Task.ReadAll();
        DateTime? Max = DateOfStartProject;
        DateTime? Temp;
        foreach (var Task in tasks)
        {
            Temp = Task!.ScheduledDate + Task.RequiredEffortTime;
            if (Temp > Max)
            {
                Max = Temp;
            }

        }

        return Max;

    }


    /// <summary>
    /// Deletes all tasks and associated dependencies.
    /// </summary>
    public void DeleteAll()
    {
        _dal.Task.DeleteAll();
    }

    /// <summary>
    /// Determines whether the provided name is valid.
    /// </summary>
    /// <param name="name">The name to validate.</param>
    /// <returns>
    ///   <c>true</c> if the name is valid; otherwise, <c>false</c>.
    /// </returns>
    static bool IsValidName(string name)
    {
        return name != null && name.Length > 0;
    }

    /// <summary>
    /// Validates the input integrity of a Task object.
    /// </summary>
    /// <param name="boTask">The Task object to validate.</param>
    /// <exception cref="BO.BlinputValidity">Thrown when there is a problem with the integrity of the data.</exception>
    static public void inputValidity(BO.Task boTask)
    {
        if (!IsValidName(boTask.Alias!) || boTask.Id < 0)
        {
            throw new BO.BlinputValidity("There is a problem with the integrity of the data");
        }
    }

    /// <summary>
    /// Determines the status of a Task based on its properties.
    /// </summary>
    /// <param name="item">The Task object for which to determine the status.</param>
    /// <returns>The status of the Task.</returns>
    private BO.Status status(DO.Task item)
    {

        if (item.ScheduledDate == null) { return BO.Status.Unscheduled; }
        if (item.ScheduledDate != null && item.StartDate == null) { return BO.Status.Scheduled; }
        if (item.CompleteDate != null) { return BO.Status.Done; }

        else
        { return BO.Status.OnTrack; }
    }

    /// <summary>
    /// Calculates the forecasted completion date for a Task based on its scheduled date, start date, and required effort time.
    /// </summary>
    /// <param name="item">The Task object for which to calculate the forecasted completion date.</param>
    /// <returns>The forecasted completion date of the Task, or null if either the scheduled date or start date is not set.</returns>
    private DateTime? forecastDate(DO.Task item)
    {
        if (item.ScheduledDate == null /*|| item.StartDate == null*/)
        {
            return null;
        }

        DateTime? finish;
        if (item.StartDate != null)
        {
            finish = (DateTime)(item.ScheduledDate > item.StartDate ? item.ScheduledDate : item.StartDate)!;
        }
        else
        {
            finish = item.ScheduledDate;
        }
        finish = finish + item.RequiredEffortTime;
        return finish;
    }

    /// <summary>
    /// Retrieves a list of dependent tasks for a given Task.
    /// </summary>
    /// <param name="item">The Task object for which to retrieve dependent tasks.</param>
    /// <returns>A list of dependent tasks represented as TaskInList objects.</returns>

    private List<BO.TaskInList> depe(DO.Task item)
    {
        var dep = _dal.Dependency.ReadAll();
        var ta = _dal.Task.ReadAll();
        List<BO.TaskInList> lis = new List<BO.TaskInList>();
        foreach (var t in dep)
        {
            if (t!.DependentTask == item.Id)
            {
                foreach (var a in ta)
                {
                    if (a.Id == t.DependsOnTask)
                    {
                        var al = a.Alias;
                        var ds = a.Description;
                        var ti = status(a);
                        lis.Add(new TaskInList() { Id = (int)t.DependsOnTask!, Alias = al, Description = ds, Status = ti });
                    }
                }
            }
        }

        return lis;

    }

    /// <summary>
    /// Retrieves an EngineerInTask object representing the engineer associated with the given Task.
    /// </summary>
    /// <param name="item">The Task object for which to retrieve the associated engineer.</param>
    /// <returns>
    /// An EngineerInTask object representing the engineer associated with the given Task,
    /// or null if no engineer is associated.
    /// </returns>
    private BO.EngineerInTask? en(DO.Task item)
    {
        var theEn = item.EngineerId;
        if (theEn != null)
        {
            var eng = _dal.Engineer.ReadAll();
            foreach (var a in eng)
            {
                if (theEn == a.Id)
                {
                    return new BO.EngineerInTask() { Id = a.Id, Name = a.Name };
                }
            }

        }

        return null;

    }
}
