
using BlApi;
using BO;


namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public int Create(BO.Task item)
    {
        throw new NotImplementedException();
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

    public void Update(BO.Task item)
    {
        throw new NotImplementedException();
    }

    public void UpdateStartTask(int id, DateTime t)
    {
        throw new NotImplementedException();
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
