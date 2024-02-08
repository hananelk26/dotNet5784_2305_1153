
using DalApi;
using DO;

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

        BO.Engineer eng = new BO.Engineer()
        {
            Name = NameEng,
            Id = IdEng,
            Cost = CostEng,
            Level = experienceEng,
            Email = EmailEng,
            Task = null
        };
        int IdEngineer = s_bl.Engineer.Create(eng);
        Console.WriteLine($"The ID of the engineer is: {IdEngineer}");

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
    private static void updateEngStage1()
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

        //Console.WriteLine("Enter The ID of the current task");
        //int IdOfCurrentTask = int.Parse(Console.ReadLine()!);
        //Console.WriteLine("Enter The Alias of the current task");
        //string AliasOfCurrentTask = Console.ReadLine()!;
        BO.Engineer eng = new BO.Engineer()
        {
            Name = NameEng,
            Id = IdEng,
            Cost = CostEng,
            Level = experienceEng,
            Email = EmailEng,
            Task = null
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

        Console.WriteLine("Enter Deliverables:");
        string? DeliverablesTask = Console.ReadLine();

        Console.WriteLine("Enter Remarks:");
        string? RemarksTask = Console.ReadLine();


        //BO.Status StatusOfTheTask;

        //if (CompleteDateTask != null)
        //{
        //    StatusOfTheTask = BO.Status.Done;
        //}
        //else if (StartDateTask != null)
        //{
        //    StatusOfTheTask = BO.Status.OnTrack;
        //}
        //else if (ScheduledDateTask != null)
        //{
        //    StatusOfTheTask = BO.Status.Scheduled;
        //}
        //else
        //{
        //    StatusOfTheTask = BO.Status.Unscheduled;
        //}

        List<BO.TaskInList> DependenciesOfTask = new List<BO.TaskInList>();
        Console.WriteLine("Enter task IDs that the current task depends on ,to finish press -1");
        int num = int.Parse(Console.ReadLine()!);

        while (num != -1)
        {
            var ts = s_bl.Task.ReadAll(t => t.Id == num).FirstOrDefault();
            if (ts == null)
            {
                throw new BO.BlDoesNotExistException($"No task with ID ={num} exists");
            }


            BO.TaskInList temp = new BO.TaskInList()
            {
                Id = ts.Id,
                Alias = ts.Alias,
                Description = ts.Description,
                Status = ts.Status
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
            Status = BO.Status.Unscheduled,
            Dependencies = DependenciesOfTask,
            RequiredEfforTime = RequiredEffortTimeTask,
            Deliverables = DeliverablesTask,
            Remarks = RemarksTask,
            Complexyity = CopmlexTask
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
    private static void updateTaskStage1()
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

        //Console.WriteLine("Enter StartDate:");
        //DateTime? StartDateTask = DateTime.Parse(Console.ReadLine()!);

        //Console.WriteLine("Enter Scheduled Date");
        //DateTime? ScheduledDateTask = DateTime.Parse(Console.ReadLine()!);

        //Console.WriteLine("Enter DeadlineDate:");
        //DateTime? DeadlineDateTask = DateTime.Parse(Console.ReadLine()!);

        //Console.WriteLine("Enter CompleteDate:");
        //DateTime? CompleteDateTask = DateTime.Parse(Console.ReadLine()!);

        Console.WriteLine("Enter Deliverables:");
        string? DeliverablesTask = Console.ReadLine();

        Console.WriteLine("Enter Remarks:");
        string? RemarksTask = Console.ReadLine();

        //Console.WriteLine("Enter the ID of the engineer working on the task:");
        //int EngineerId = int.Parse(Console.ReadLine()!);

        //Console.WriteLine("Enter the Name of the engineer working on the task:");
        //string EngineerName = Console.ReadLine()!;

        //BO.Status StatusOfTheTask;

        //if (CompleteDateTask != null)
        //{
        //    StatusOfTheTask = BO.Status.Done;
        //}
        //else if (StartDateTask != null)
        //{
        //    StatusOfTheTask = BO.Status.OnTrack;
        //}
        //else if (ScheduledDateTask != null)
        //{
        //    StatusOfTheTask = BO.Status.Scheduled;
        //}
        //else
        //{
        //    StatusOfTheTask = BO.Status.Unscheduled;
        //}

        List<BO.TaskInList> DependenciesOfTask = new List<BO.TaskInList>();
        Console.WriteLine("Enter task IDs that the current task depends on ,to finish press -1");
        int num = int.Parse(Console.ReadLine()!);

        while (num != -1)
        {
            var ts = s_bl.Task.ReadAll(t => t.Id == num).FirstOrDefault();
            if (ts == null)
            {
                throw new BO.BlDoesNotExistException($"No task with ID ={num} exists");
            }


            BO.TaskInList temp = new BO.TaskInList()
            {
                Id = ts.Id,
                Alias = ts.Alias,
                Description = ts.Description,
                Status = ts.Status
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
            Status = BO.Status.Unscheduled,
            Dependencies = DependenciesOfTask,
            RequiredEfforTime = RequiredEffortTimeTask,
            Deliverables = DeliverablesTask,
            Remarks = RemarksTask,
            Complexyity = CopmlexTask,
            Engineer = null

        };



        s_bl.Task!.Update(tsk);

    }

    private static void deleteTask()
    {
        Console.WriteLine("Enter Id of the Task");
        int Id = int.Parse(Console.ReadLine()!);
        s_bl.Task.Delete(Id);
    }

    private static void createDep()
    {
        Console.WriteLine("Enter the pending task ID");
        int TheDependentTaskId = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter the task ID that needs to be done first");
        int TheDependOnTaskId = int.Parse(Console.ReadLine()!);

        var x = s_bl.Task.Read(TheDependentTaskId);
        var DependencyList = new List<BO.TaskInList>();
        DependencyList = x.Dependencies;
        var temp = s_bl.Task.Read(TheDependOnTaskId);
        string DescriptionOfTask = temp.Description;
        string AliasOfTask = temp.Alias;
        var statusOfTask = temp.Status;
        DependencyList.Add(new BO.TaskInList
        {
            Id = TheDependOnTaskId,
            Description = DescriptionOfTask,
            Alias = AliasOfTask,
            Status = statusOfTask

        });

        BO.Task d = new BO.Task()
        {
            Id = x.Id,
            Description = x.Description,
            Alias = x.Alias,
            CreatedAtDate = x.CreatedAtDate,
            Dependencies = DependencyList,
            RequiredEfforTime = x.RequiredEfforTime,
            StartDate = x.StartDate,
            Deliverables = x.Deliverables,
            Remarks = x.Remarks,
            Complexyity = (BO.EngineerExperience)x.Complexyity!

        };

        s_bl.Task.Update(d);

    }

    private static void readAllDep()
    {
        var tasks = s_bl.Task.ReadAll();
        foreach (var item in tasks) 
        {
            if (item.Dependencies != null)
            {
                Console.WriteLine($"The task with ID: {item.Id} depends on the tasks with ID:");
                foreach (var it in item.Dependencies)
                {
                    Console.WriteLine($"task With ID: {it.Id}");
                }
            }
            else { Console.WriteLine($"The task with ID {item.Id} does not depend on any task"); }
        }

    }

    private static void printMainMenu()
    {
        Console.WriteLine("Press 0 to exit from the program");
        Console.WriteLine("Press 1 to Engineer");
        Console.WriteLine("Press 2 to Task");
        Console.WriteLine("Press 3 to Dependency");
        Console.WriteLine("Tap 4 to set the date the project will start and set a scheduled start date for each task.");
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

    private static void PrintSubMenuOfDependency()
    {
        Console.WriteLine("Press 1 to exit");
        Console.WriteLine("Press 2 to Create");
        Console.WriteLine("Press 3 to ReadAll");

    }

    private static void SubMenu(string entity)
    {
        if (entity == "Engineer")
        {
            SubMenuOfEngineer();
        }
        else if (entity == "Task")
        {
            SubMenuOfTask();
        }
        else if (entity == "Dependency")
        {
            SubMenuOfDependency();
        }
    }

    private static void SubMenuOfEngineer()
    {
        PrintSubMenuOfEngineer();
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

                updateEngStage1();
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
        PrintSubMenuOfTask();
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

                updateTaskStage1();
                break;

            case 6:

                deleteTask();
                break;


            default:
                break;
        }

    }

    private static void SubMenuOfDependency()
    {
        PrintSubMenuOfDependency();
        int ch = int.Parse(Console.ReadLine()!);
        switch (ch)
        {
            case 1:
                return;

            case 2:

                createDep();
                break;

            case 3:

                readAllDep();
                break;

            default:
                break;
        }

    }



    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the project");
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

                case 3:
                    try
                    {
                        SubMenu("Dependency");
                    }
                    catch (Exception me)
                    {

                        Console.WriteLine(me.Message);
                    }
                    break;

                case 4:
                    break;

                default:
                    break;
            }


        } while (choice != 0);


    }


}