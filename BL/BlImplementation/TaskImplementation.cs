
using BlApi;
using BO;
using System.Data;
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
            throw new BO.BlAlreadyExistsException($"Task with ID={boTask.Id} already exists"/*, ex*/);
        }
    }

    public void Delete(int id)
    {
        var t = _dal.Task.Read(id);
        if (t == null) { throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist"); }
        var de = _dal.Dependency.ReadAll();
        foreach( var d in de) 
        {
            if (id == d.DependsOnTask)
                throw "there is taska that depend in this task";
        }
        _dal.Task.Delete(id);
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
            Dependencies = depe(dalTask) ,
            RequiredEfforTime = dalTask.RequiredEffortTime,
            StartDate = dalTask.StartDate,
            ScheduledDate = dalTask.ScheduledDate,
            ForecastDate = ofrecastDate(dalTask),
            DeadLineDate = dalTask.DeadlineDate,
            CompleteDate = dalTask.CompleteDate,
            Deliverables = dalTask.Deliverables,
            Remarks = dalTask.Remarks,
             Engineer = en(dalTask),
            Complexyity = (BO.EngineerExperience)dalTask.Copmlexity!,

        };
        

        return boTask;


    }

    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        IEnumerable<DO.Task> dalTasks = _dal.Task.ReadAll()!;
        IEnumerable<BO.Task> boTasks = dalTasks.Select(dalTask => new BO.Task()
        {
            Id = dalTask.Id,
            Description = dalTask.Description,
            Alias = dalTask.Alias,
            CreatedAtDate = dalTask.createdAtDate,
            Status = status(dalTask),
            Dependencies = depe(dalTask) ,
            RequiredEfforTime = dalTask.RequiredEffortTime,
            StartDate = dalTask.StartDate,
            ScheduledDate = dalTask.ScheduledDate,
            ForecastDate = ofrecastDate(dalTask),
            DeadLineDate = dalTask.DeadlineDate,
            CompleteDate = dalTask.DeadlineDate,
            Deliverables = dalTask.Deliverables,
            Remarks = dalTask.Remarks,
            Engineer =en(dalTask) ,
            Complexyity = (BO.EngineerExperience)dalTask.Copmlexity!,


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

            throw new BO.BlDoesNotExistException($"Engineer with ID={task.Id} does Not exist");
        }

    }

    public void UpdateStartTask(int id, DateTime tim)
    {
        var ta = _dal.Task.ReadAll();
        var task = _dal.Task.Read(id);
        var dep  = _dal.Dependency.ReadAll();
        foreach(var d in dep)
        {
            if(id == d.DependsOnTask)
            {
               foreach(var t in ta)
                {
                    if(t.Id== id)
                    {
                        if (t.ScheduledDate == null)
                            throw "";
                        if(t.DeadlineDate > tim)
                            throw "":
                    }
                    
                }

            }
        }
        _dal.Task.Update(task with { ScheduledDate = tim });
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

    private DateTime? ofrecastDate(DO.Task item)
    {
        DateTime? finish;
        finish =(DateTime)( item.ScheduledDate > item.StartDate ?  item.ScheduledDate : item.StartDate)! ;
        finish = finish + item.RequiredEffortTime;
        return finish;
    }

    private List<BO.TaskInList> depe(DO.Task item)
    {
        var dep = _dal.Dependency.ReadAll();
        var ta = _dal.Task.ReadAll();
        List<BO.TaskInList> lis = new List<BO.TaskInList>();
        foreach(var t  in dep)
        {
            if(t!.DependentTask==item.Id)
            {
                foreach (var a in ta)
                {
                    if (a.Id == t.DependsOnTask)
                    {
                        var al = item.Alias;
                        var ds = item.Description;
                        var ti = status(a);
                        lis.Add(new TaskInList() { Id = (int)t.DependsOnTask!, Alias = al, Description = ds, Status = ti });
                    }
                }
            }
        }

        return lis;

    }

    private BO.EngineerInTask en(DO.Task item)
    {
        var theEn = item.EngineerId;
        var eng = _dal.Engineer.ReadAll();
        foreach(var a in eng)
        {
            if(theEn == a.Id)
            {
                return new BO.EngineerInTask() {  Id = a.Id, Name = a.Name };
            }
        }

        return null;

    }
}
