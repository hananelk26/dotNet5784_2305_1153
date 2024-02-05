
using BlApi;
using System.Security.Cryptography;
using System.Text.RegularExpressions;


namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public int Create(BO.Task boTask)
    {
        inputValidity(boTask);


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
            Copmlexity = (DO.EngineerExperience)boTask.Complexyity!,
            StartDate = boTask.StartDate,
            ScheduledDate = boTask.ScheduledDate,
            DeadlineDate = boTask.DeadLineDate,
            CompleteDate = boTask.CompleteDate,
            Deliverables = boTask.Deliverables,
            Remarks = boTask.Remarks,
            EngineerId = boTask.Engineer!.Id

        };

        try
        {
            return _dal.Task.Create(task);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={boTask.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Task? Read(int id)
    {
       var dalTask =  _dal.Task.Read(id);
        if(dalTask == null) { throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist"); }
        BO.Task boTask = new BO.Task()
        {
            Id = dalTask.Id,
            Description = dalTask.Description,
            Alias = dalTask.Alias,
            CreatedAtDate = dalTask.createdAtDate,
            Status = status(dalTask),
            Dependencies = ,
            RequiredEfforTime = dalTask.RequiredEffortTime,
            StartDate = dalTask.StartDate,
            ScheduledDate = dalTask.ScheduledDate,
            ForecastDate =
            DeadLineDate = dalTask.DeadlineDate,
            CompleteDate = dalTask.DeadlineDate,
            Deliverables = dalTask.Deliverables,
            Remarks = dalTask.Remarks,
            Engineer = new EngineerInTask() ,
            Complexyity = (BO.EngineerExperience)dalTask.Copmlexity!,

        }
        

        return boTask;


    }

    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        IEnumerable<DO.Task> dalTasks = _dal.Task.ReadAll()!;
        IEnumerable<BO.Task> boTasks = dalTasks.Select(dalTask => new BO.Task
        {
            Id = dalTask.Id,
            Description = dalTask.Description,
            Alias = dalTask.Alias,
            CreatedAtDate = dalTask.createdAtDate,
            Status = status(dalTask),
            Dependencies = ,
            RequiredEfforTime = dalTask.RequiredEffortTime,
            StartDate = dalTask.StartDate,
            ScheduledDate = dalTask.ScheduledDate,
            ForecastDate =
            DeadLineDate = dalTask.DeadlineDate,
            CompleteDate = dalTask.DeadlineDate,
            Deliverables = dalTask.Deliverables,
            Remarks = dalTask.Remarks,
            Engineer = ,
            Complexyity = , 


        });
        if (filter != null)
        {
            boTasks = boTasks.Where(filter);
        }
        return boTasks;

    }

    public void Update(BO.Task boTask)
    {
        inputValidity(boTask);

        foreach (var dependId in boTask.Dependencies!)
        {
            DO.Dependency dep = _dal.Dependency.ReadAll(d => d.DependsOnTask == dependId.Id && d.DependentTask == boTask.Id).FirstOrDefault()!;
            if (dep == null)
            {
                DO.Dependency dependency = new DO.Dependency()
                {
                    DependentTask = boTask.Id,
                    DependsOnTask = dependId.Id
                };
                _dal.Dependency.Create(dependency);
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
            StartDate = boTask.StartDate,
            ScheduledDate = boTask.ScheduledDate,
            DeadlineDate = boTask.DeadLineDate,
            CompleteDate = boTask.CompleteDate,
            Deliverables = boTask.Deliverables,
            Remarks = boTask.Remarks,
            EngineerId = boTask.Engineer!.Id

        };


        try
        {
            _dal.Task.Update(task);
        }
        catch (DO.DalDoesNotExistException)
        {

            throw BO.BlDoesNotExistException($"Engineer with ID={item.Id} does Not exist");
        }






    }

    public void UpdateStartTask(int id, DateTime t)
    {
        throw new NotImplementedException();
    }

    static bool IsValidName(string name)
    {
        // Regular expression for a simple name validation
        // Allows letters, spaces, and some common special characters
        string namePattern = @"^[a-zA-Z\s\.'-]*$";
        return Regex.IsMatch(name, namePattern);
    }

    static public void inputValidity(BO.Task boTask)
    {
        if (!IsValidName(boTask.Alias!) || boTask.Id < 0)
        {
            throw new BO.BlinputValidity("There is a problem with the integrity of the data");
        }
    }

     private BO.Status status(DO.Task item)
     {
        if(item.StartDate==null) { return BO.Status.Unscheduled; }
        if (item.CompleteDate != null) {  return BO.Status.Done; }
        if(item.ScheduledDate!=null && item.StartDate == null) {  return BO.Status.Scheduled; }
        else
        { return BO.Status.OnTrack; }
     }


}
