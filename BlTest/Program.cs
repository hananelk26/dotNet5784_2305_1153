
using DalApi;

namespace BlTest;

public class Program
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();



    private static void createEng()
    {
        Console.WriteLine("Enter Id:");
        int IdEng = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter Email:");
        string EmailEng = Console.ReadLine()!;
        Console.WriteLine("Enter Cost:");
        double CostEng = double.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter Name");
        string NameEng = Console.ReadLine()!;
        Console.WriteLine("Enter experience");
        BO.EngineerExperience experienceEng = (BO.EngineerExperience)int.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter The ID of the current task");
        int IdOfCurrentTask = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter The Alias of the current task");
        string AliasOfCurrentTask = Console.ReadLine()!;
        BO.Engineer eng = new BO.Engineer()
        {
            Name = NameEng,
            Id = IdEng,
            Cost = CostEng,
            Level = experienceEng,
            Email = EmailEng,
            Task = new BO.TaskInEngineer()
            {
                Id = IdOfCurrentTask,
                Alias = AliasOfCurrentTask
            }


        };
        int IdEngineer = s_bl.Engineer.Create(eng);
        Console.WriteLine(IdEngineer);

    }
    private static void readEng()
    {
        Console.WriteLine("Enter Id");
        int id = int.Parse(Console.ReadLine()!);
        BO.Engineer x = s_bl.Engineer.Read(id)!;
        Console.WriteLine(x);
       

    }
    private static void readAllEng()
    {
        foreach (var eng in s_bl.Engineer.ReadAll())
        {
            Console.WriteLine(eng);
        }

    }
    private static void updateEng()
    {
        Console.WriteLine("Enter Id:");
        int IdEng = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter Email:");
        string EmailEng = Console.ReadLine()!;
        Console.WriteLine("Enter Cost:");
        double CostEng = double.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter Name");
        string NameEng = Console.ReadLine()!;
        Console.WriteLine("Enter experience");
        BO.EngineerExperience experienceEng = (BO.EngineerExperience)int.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter The ID of the current task");
        int IdOfCurrentTask = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter The Alias of the current task");
        string AliasOfCurrentTask = Console.ReadLine()!;
        BO.Engineer eng = new BO.Engineer()
        {
            Name = NameEng,
            Id = IdEng,
            Cost = CostEng,
            Level = experienceEng,
            Email = EmailEng,
            Task = new TaskInEngineer()
            {
                Id = IdOfCurrentTask,
                Alias = AliasOfCurrentTask
            }
        };

        s_bl.Engineer.Update(eng);

    }
    private static void deleteEng()
    {
        Console.WriteLine("Enter Id of the Engineer");
        int Id = int.Parse(Console.ReadLine()!);
        s_bl.Engineer.Delete(Id);

    }




    private static void createTask()
    {
        Console.WriteLine("Enter ID:");
        int IdTask = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter string Alias:");
        string aliasTask = Console.ReadLine()!;
        Console.WriteLine("Enter Description:");
        string DescriptionTask = Console.ReadLine()!;
        Console.WriteLine("Enter created Date:");
        DateTime createdAtDateTask = DateTime.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter Required Effort Time");
        TimeSpan? RequiredEffortTimeTask = TimeSpan.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter Copmlexity:");
        BO.EngineerExperience? CopmlexTask = (BO.EngineerExperience)(int.Parse(Console.ReadLine()!));
        Console.WriteLine("Enter StartDate:");
        DateTime? StartDateTask = DateTime.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter Scheduled Date");
        DateTime? ScheduledDateTask = DateTime.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter DeadlineDate:");
        DateTime? DeadlineDateTask = DateTime.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter CompleteDate:");
        DateTime? CompleteDateTask = DateTime.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter Deliverables:");
        string? DeliverablesTask = Console.ReadLine();
        Console.WriteLine("Enter Remarks:");
        string? RemarksTask = Console.ReadLine();
        Console.WriteLine("Enter the ID of the engineer working on the task:");
        int EngineerId = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter the Name of the engineer working on the task:");
        string EngineerName = Console.ReadLine()!;

        BO.Status StatusOfTheTask;

        if (CompleteDateTask != null)
        {
            StatusOfTheTask = BO.Status.Done;
        }
        else if (StartDateTask != null)
        {
            StatusOfTheTask = BO.Status.OnTrack;
        }
        else if (ScheduledDateTask != null)
        {
            StatusOfTheTask = BO.Status.Scheduled;
        }
        else
        {
            StatusOfTheTask = BO.Status.Unscheduled;
        }

        List<BO.TaskInList> DependenciesOfTask = new List<BO.TaskInList>();
        Console.WriteLine("Enter task IDs that the current task depends on ,to finish press -1");
        int num = int.Parse(Console.ReadLine()!);

        while (num != -1)
        {
            var tsk = s_bl.Task.ReadAll(t => t.Id == num).FirstOrDefault();
            if (tsk == null)
            {
                throw new BO.BlDoesNotExistException($"No task with ID ={num} exists");
            }


            BO.TaskInList temp = new BO.TaskInList()
            {
                Id = tsk.Id,
                Alias = tsk.Alias,
                Description = tsk.Description,
                Status = tsk.Status
            };
            DependenciesOfTask.Add(temp);

            num = int.Parse(Console.ReadLine()!);
        }

        BO.Task tsk = new BO.Task()
        {
            Id = IdTask,
            Alias = aliasTask,
            Description = DescriptionTask,
            CreatedAtDate = createdAtDateTask,
            Status = StatusOfTheTask,
            Dependencies = DependenciesOfTask,
            RequiredEfforTime = RequiredEffortTimeTask,
            StartDate = StartDateTask,
            ScheduledDate = ScheduledDateTask,
            ForecastDate = StartDateTask + RequiredEffortTimeTask,
            DeadLineDate = DeadlineDateTask,
            CompleteDate = CompleteDateTask,
            Deliverables = DeliverablesTask,
            Remarks = RemarksTask,
            Complexyity = CopmlexTask,
            Engineer = new BO.EngineerInTask()
            {
                Id = EngineerId,
                Name = EngineerName

            }

        };



        int id = s_bl.Task!.Create(tsk);
        Console.WriteLine($"The ID task is:{id}");

    }
    private static void readTask()
    {
        Console.WriteLine("Enter Id of the task");
        int id = int.Parse(Console.ReadLine()!);
        BO.Task x = s_bl.Task.Read(id)!;
        Console.WriteLine(x);
    }
    private static void readAllTask()
    {
        foreach (var tsk in s_bl.Task.ReadAll())
        {
            Console.WriteLine(tsk);
        }
    }
    private static void updateTask()
    {

        Console.WriteLine("Enter ID:");
        int IdTask = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter string Alias:");
        string aliasTask = Console.ReadLine()!;
        Console.WriteLine("Enter Description:");
        string DescriptionTask = Console.ReadLine()!;
        Console.WriteLine("Enter created Date:");
        DateTime createdAtDateTask = DateTime.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter Required Effort Time");
        TimeSpan? RequiredEffortTimeTask = TimeSpan.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter Copmlexity:");
        BO.EngineerExperience? CopmlexTask = (BO.EngineerExperience)(int.Parse(Console.ReadLine()!));
        Console.WriteLine("Enter StartDate:");
        DateTime? StartDateTask = DateTime.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter Scheduled Date");
        DateTime? ScheduledDateTask = DateTime.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter DeadlineDate:");
        DateTime? DeadlineDateTask = DateTime.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter CompleteDate:");
        DateTime? CompleteDateTask = DateTime.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter Deliverables:");
        string? DeliverablesTask = Console.ReadLine();
        Console.WriteLine("Enter Remarks:");
        string? RemarksTask = Console.ReadLine();
        Console.WriteLine("Enter the ID of the engineer working on the task:");
        int EngineerId = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter the Name of the engineer working on the task:");
        string EngineerName = Console.ReadLine()!;

        BO.Status StatusOfTheTask;

        if (CompleteDateTask != null)
        {
            StatusOfTheTask = BO.Status.Done;
        }
        else if (StartDateTask != null)
        {
            StatusOfTheTask = BO.Status.OnTrack;
        }
        else if (ScheduledDateTask != null)
        {
            StatusOfTheTask = BO.Status.Scheduled;
        }
        else
        {
            StatusOfTheTask = BO.Status.Unscheduled;
        }

        List<BO.TaskInList> DependenciesOfTask = new List<BO.TaskInList>();
        Console.WriteLine("Enter task IDs that the current task depends on ,to finish press -1");
        int num = int.Parse(Console.ReadLine()!);

        while (num != -1)
        {
            var tsk = s_bl.Task.ReadAll(t => t.Id == num).FirstOrDefault();
            if (tsk == null)
            {
                throw new BO.BlDoesNotExistException($"No task with ID ={num} exists");
            }


            BO.TaskInList temp = new BO.TaskInList()
            {
                Id = tsk.Id,
                Alias = tsk.Alias,
                Description = tsk.Description,
                Status = tsk.Status
            };
            DependenciesOfTask.Add(temp);

            num = int.Parse(Console.ReadLine()!);
        }

        BO.Task tsk = new BO.Task()
        {
            Id = IdTask,
            Alias = aliasTask,
            Description = DescriptionTask,
            CreatedAtDate = createdAtDateTask,
            Status = StatusOfTheTask,
            Dependencies = DependenciesOfTask,
            RequiredEfforTime = RequiredEffortTimeTask,
            StartDate = StartDateTask,
            ScheduledDate = ScheduledDateTask,
            ForecastDate = StartDateTask + RequiredEffortTimeTask,
            DeadLineDate = DeadlineDateTask,
            CompleteDate = CompleteDateTask,
            Deliverables = DeliverablesTask,
            Remarks = RemarksTask,
            Complexyity = CopmlexTask,
            Engineer = new BO.EngineerInTask()
            {
                Id = EngineerId,
                Name = EngineerName

            }

        };



        s_bl.Task!.Update(tsk);

    }

    private static void deleteTask()
    {
        Console.WriteLine("Enter Id of the Task");
        int Id = int.Parse(Console.ReadLine()!);
        s_bl.Task.Delete(Id);
    }

    private static void UpdateStartTask()
    {


    }

    private static void printMainMenu()
    {
        Console.WriteLine("Press 0 to exit");
        Console.WriteLine("Press 1 to Engineer");
        Console.WriteLine("Press 2 to Task");
    }

    private static void PrintSubMenuOfEngineer()
    {
        Console.WriteLine("Press 1 to exit");
        Console.WriteLine("Press 2 to Create");
        Console.WriteLine("Press 3 to Read");
        Console.WriteLine("Press 4 to ReadAll");
        Console.WriteLine("Press 5 to UpData");
        Console.WriteLine("Press 6 to Delete");
    }

    private static void PrintSubMenuOfTask()
    {
        Console.WriteLine("Press 1 to exit");
        Console.WriteLine("Press 2 to Create");
        Console.WriteLine("Press 3 to Read");
        Console.WriteLine("Press 4 to ReadAll");
        Console.WriteLine("Press 5 to UpData");
        Console.WriteLine("Press 6 to Delete");
        Console.WriteLine("Press 7 to UpdateStartTask");
    }


    private static void SubMenu(string entity)
    {
        if (entity == "Engineer")
        {
            PrintSubMenuOfEngineer();
            SubMenuOfEngineer();
        }
        else
        {
            PrintSubMenuOfTask();
            SubMenuOfTask();
        }
    }

    private static void SubMenuOfEngineer()
    {
        int ch = int.Parse(Console.ReadLine()!);
        switch (ch)
        {
            case 1:
                return;

            case 2:

                createEng();
                break;

            case 3:

                readEng();
                break;

            case 4:

                readAllEng();
                break;

            case 5:

                updateEng();
                break;

            case 6:

                deleteEng();
                break;

            default:
                break;
        }

    }

    private static void SubMenuOfTask()
    {
        int ch = int.Parse(Console.ReadLine()!);
        switch (ch)
        {
            case 1:
                return;

            case 2:

                createTask();
                break;

            case 3:

                readTask();
                break;

            case 4:

                readAllTask();
                break;

            case 5:

                updateTask();
                break;

            case 6:

                deleteTask();
                break;

            case 7:

                UpdateStartTask();
                break;

            default:
                break;
        }

    }
    static void main(string[] args)
    {
        Console.Write("Would you like to create Initial data? (Y/N)");
        string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
        if (ans == "Y")
            DalTest.Initialization.Do();

        int choice = 0;
        do
        {
            printMainMenu();
            choice = int.Parse(Console.ReadLine()!);
            switch (choice)
            {
                case 0:
                    break;
                case 1:
                    try
                    {
                        SubMenu("Engineer");
                        break;
                    }
                    catch (Exception me)
                    {

                        Console.WriteLine(me.Message);
                    }
                    break;

                case 2:
                    try
                    {
                        SubMenu("Task");
                        break;
                    }
                    catch (Exception me)
                    {
                        Console.WriteLine(me.Message);
                        break;
                    }

                default:
                    break;
            }


        } while (choice != 0);


    }


}
