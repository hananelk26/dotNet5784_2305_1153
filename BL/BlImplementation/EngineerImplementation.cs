

namespace BlImplementation;
using BlApi;
using System.Text.RegularExpressions;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;


    public int Create(BO.Engineer boEngineer)
    {
        inputValidity(boEngineer);

        DO.Engineer doEngineer = new DO.Engineer(boEngineer.Id, boEngineer.Email, boEngineer.Cost, boEngineer.Name, (DO.EngineerExperience)boEngineer.Level);
        try
        {
            int IdEngineer = _dal.Engineer.Create(doEngineer);
            return IdEngineer;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={boEngineer.Id} already exists"/*, ex*/);
        }

    }

    public void Delete(int id)
    {
        var temp = _dal.Task.ReadAll().Where(t => t!.EngineerId == id);
        if (temp.Any())
        {
            throw new BO.BlAnEngineerWhoHasATaskCannotBeDeleted("An engineer who has a task cannot be deleted");

        }

        try
        {
            _dal.Engineer.Delete(id);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist"/*, ex*/);
        }
    }

    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");
        if (condition(doEngineer)) // כלומר נמצאים בשלב 3 בלו"ז
        {
            var ret = _dal.Task.ReadAll().Where(t => t?.EngineerId == doEngineer.Id && t.CompleteDate == null).FirstOrDefault();
            int IdOfTask = ret!.Id; // RET זה המשימה של אותו מהנדס
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


    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool>? filter = null)
    {
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
                       Id = _dal.Task.ReadAll().Where(t => t?.EngineerId == item.Id && t.CompleteDate == null).FirstOrDefault()!.Id,
                       Alias = _dal.Task.ReadAll().Where(t => t?.EngineerId == item.Id && t.CompleteDate == null).FirstOrDefault()!.Alias
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
    public void Update(BO.Engineer item)
    {
        inputValidity(item);

        DO.Engineer? doEng = _dal.Engineer.Read(item.Id);

        if (doEng != null)
        {
            if (doEng.Level > (DO.EngineerExperience)item.Level)
            {
                throw new BO.BlinputValidity("There is a problem with the integrity of the level of the engineer");
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

    bool condition(DO.Engineer item)
    {
        var ret = _dal.Task.ReadAll().Where(t =>t!.EngineerId != null && t.EngineerId == item.Id && t.CompleteDate == null).FirstOrDefault();
        if (ret != null)
            return true;
        return false;
    }


    static public void inputValidity(BO.Engineer boEng)
    {
        if (!IsValidEmail(boEng.Email) || !IsValidName(boEng.Name) || boEng.Cost < 0 || boEng.Id < 0)
        {
            throw new BO.BlinputValidity("There is a problem with the integrity of the data");
        }
    }

    static bool IsValidEmail(string email)
    {
        // Regular expression for a simple email validation
        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, emailPattern);
    }

    static bool IsValidName(string name)
    {
        // Regular expression for a simple name validation
        // Allows letters, spaces, and some common special characters
        string namePattern = @"^[a-zA-Z\s\.'-]*$";
        return Regex.IsMatch(name, namePattern);
    }

}
