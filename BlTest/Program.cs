﻿
using BlApi;
using BO;
using DalApi;
using DO;
using Microsoft.VisualBasic;
using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlTest;

/// <summary>
/// Represents the main program class.
/// </summary>
public class Program
{
    /// <summary>
    /// Represents the business logic instance.
    /// </summary>
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    /// <summary>
    /// Sets start dates for the project and tasks based on user input.
    /// </summary>
    private static void SettingDatesforProjectAndForTasks()
    {
        Console.WriteLine("Enter a start date for the project");
        DateTime StartDateOfProject = DateTime.Parse(Console.ReadLine()!);
        s_bl.Time.SetStartDate(StartDateOfProject);
        foreach (var task in s_bl.Task.ReadAll()) // Initializes all tasks that do not depend on other tasks
        {
            if (!task.Dependencies!.Any())
            {
                DateTime DateOfTask;
                do
                {
                    Console.WriteLine($"Enter a date of task with ID = {task.Id}");
                    DateOfTask = DateTime.Parse(Console.ReadLine()!);
                    if (DateOfTask > StartDateOfProject)
                    {
                        task.ScheduledDate = DateOfTask;
                        s_bl.Task.Update(task);
                    }

                } while (DateOfTask <= StartDateOfProject);
            }
        }

        List<int> IDOfTasksWithoutAScheduledDate = new List<int>(); // A list with the IDs of all tasks that depend on other tasks
        foreach (var task in s_bl.Task.ReadAll())
        {
            if (task.Dependencies!.Any())
            {
                IDOfTasksWithoutAScheduledDate.Add(task.Id);
            }
        }

        Console.WriteLine("Here is the list of all the tasks that are left to be updated with a planned start date:");
        foreach (var ID in IDOfTasksWithoutAScheduledDate)
        {
            Console.WriteLine($"Task with ID: {ID}");
        }

        while (IDOfTasksWithoutAScheduledDate.Any()) // Setting a date for all tasks that depend on other tasks
        {
            Console.WriteLine("Enter the ID of the tasjk you want to set a scheduled start date for");
            int IDOfTask;
            try
            {
                IDOfTask = int.Parse(Console.ReadLine()!);
            }
            catch (Exception me)
            {
                Console.WriteLine(me.Message);
                continue;
            }
            Console.WriteLine("Enter the scheduledDate of the task you want to set a scheduled start date for");
            DateTime DateOfTask;
            try
            {
                DateOfTask = DateTime.Parse(Console.ReadLine()!);
            }
            catch (Exception me)
            {
                Console.WriteLine(me.Message);
                continue;

            }
            if (DateOfTask <= StartDateOfProject)
            {
                Console.WriteLine("The start date must be after the project start time");
                continue;
            }

            try
            {
                s_bl.Task.UpdateStartTask(IDOfTask, DateOfTask);// may throw an exception
                IDOfTasksWithoutAScheduledDate.Remove(IDOfTask);
            }
            catch (Exception me)
            {
                Console.WriteLine(me.Message);
            }

        }
    }

    /// <summary>
    /// Creates a new engineer with user-provided information and displays the generated ID.
    /// </summary>
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

    /// <summary>
    /// Reads and displays information for an engineer based on user-provided ID.
    /// </summary>
    private static void readEng()
    {
        Console.WriteLine("Enter Id");
        int id = int.Parse(Console.ReadLine()!);
        BO.Engineer x = s_bl.Engineer.Read(id)!;
        Console.WriteLine(x);

    }

    /// <summary>
    /// Reads and displays information for all engineers.
    /// </summary>
    private static void readAllEng()
    {
        foreach (var eng in s_bl.Engineer.ReadAll())
        {
            Console.WriteLine(eng);
        }

    }

    /// <summary>
    /// Updates the information for an engineer based on user-provided details.
    /// </summary>
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

    /// <summary>
    /// Deletes an engineer based on the user-provided ID.
    /// </summary>
    private static void deleteEng()
    {
        Console.WriteLine("Enter Id of the Engineer");
        int Id = int.Parse(Console.ReadLine()!);
        s_bl.Engineer.Delete(Id);

    }

    /// <summary>
    /// Creates a new task with user-provided information and displays the generated ID.
    /// </summary>
    private static void createTask()
    {
        Console.WriteLine("Enter ID:");
        int IdTask = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Enter string Alias:");
        string aliasTask = Console.ReadLine()!;

        Console.WriteLine("Enter Description:");
        string DescriptionTask = Console.ReadLine()!;

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

        BO.Task tskk = new BO.Task()
        {
            Id = IdTask,
            Alias = aliasTask,
            Description = DescriptionTask,
            CreatedAtDate = DateTime.Now,
            Status = BO.Status.Unscheduled,
            Dependencies = DependenciesOfTask,
            RequiredEfforTime = RequiredEffortTimeTask,
            Deliverables = DeliverablesTask,
            Remarks = RemarksTask,
            Complexyity = CopmlexTask
        };



        int id = s_bl.Task!.Create(tskk);
        Console.WriteLine($"The ID task is:{id}");

    }

    /// <summary>
    /// Reads and displays information for a task based on user-provided ID.
    /// </summary>
    private static void readTask()
    {
        Console.WriteLine("Enter Id of the task");
        int id = int.Parse(Console.ReadLine()!);
        BO.Task x = s_bl.Task.Read(id)!;
        Console.WriteLine(x);
    }

    /// <summary>
    /// Reads and displays information for all tasks.
    /// </summary>
    private static void readAllTask()
    {
        foreach (var tsk in s_bl.Task.ReadAll())
        {
            Console.WriteLine(tsk);
        }
    }

    /// <summary>
    /// Updates the information for a task based on user-provided details (Stage 1).
    /// </summary>
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

        BO.Task tskk = new BO.Task()
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



        s_bl.Task!.Update(tskk);

    }

    /// <summary>
    /// Deletes a task based on the user-provided ID.
    /// </summary>
    private static void deleteTask()
    {
        Console.WriteLine("Enter Id of the Task");
        int Id = int.Parse(Console.ReadLine()!);
        s_bl.Task.Delete(Id);
    }

    /// <summary>
    /// Creates a dependency between two tasks based on user-provided IDs.
    /// </summary>
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
            Deliverables = x.Deliverables,
            Remarks = x.Remarks,
            Complexyity = (BO.EngineerExperience?)x.Complexyity!

        };

        s_bl.Task.Update(d);

    }

    /// <summary>
    /// Reads and displays information for all tasks with dependencies.
    /// </summary>
    private static void readAllDep()
    {
        var tasks = s_bl.Task.ReadAll();
        foreach (var item in tasks)
        {
            if (item.Dependencies.Any())
            {
                Console.WriteLine($"The task with ID: {item.Id} depends on the tasks with ID:");
                foreach (var it in item.Dependencies)
                {
                    Console.WriteLine($"task With ID: {it.Id}");
                }
            }
            else { Console.WriteLine($"The task with ID {item.Id} does not depend on any task"); }
            Console.WriteLine("");
        }

    }

    /// <summary>
    /// Displays the main menu options.
    /// </summary>
    private static void printMainMenu()
    {
        Console.WriteLine("Press 0 to exit from the program");
        Console.WriteLine("Press 1 to Engineer");
        Console.WriteLine("Press 2 to Task");
        Console.WriteLine("Press 3 to Dependency");
        Console.WriteLine("Tap 4 to set the date the project will start and set a scheduled start date for each task.");
    }

    /// <summary>
    /// Displays the submenu options for the Engineer entity.
    /// </summary>
    private static void PrintSubMenuOfEngineer()
    {
        Console.WriteLine("Press 1 to exit");
        Console.WriteLine("Press 2 to Create");
        Console.WriteLine("Press 3 to Read");
        Console.WriteLine("Press 4 to ReadAll");
        Console.WriteLine("Press 5 to UpData");
        Console.WriteLine("Press 6 to Delete");
    }

    /// <summary>
    /// Displays the submenu options for the Task entity.
    /// </summary>
    private static void PrintSubMenuOfTask()
    {
        Console.WriteLine("Press 1 to exit");
        Console.WriteLine("Press 2 to Create");
        Console.WriteLine("Press 3 to Read");
        Console.WriteLine("Press 4 to ReadAll");
        Console.WriteLine("Press 5 to UpData");
        Console.WriteLine("Press 6 to Delete");
    }

    /// <summary>
    /// Displays the submenu options for the Dependency entity.
    /// </summary>
    private static void PrintSubMenuOfDependency()
    {
        Console.WriteLine("Press 1 to exit");
        Console.WriteLine("Press 2 to Create");
        Console.WriteLine("Press 3 to ReadAll");

    }

    // <summary>
    /// Handles the submenu options based on the provided entity.
    /// </summary>
    /// <param name="entity">The entity for which the submenu is displayed.</param>
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

    /// <summary>
    /// Handles the submenu options for the Engineer entity.
    /// </summary>
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

                updateEng();
                break;

            case 6:

                deleteEng();
                break;

            default:
                break;
        }

    }

    /// <summary>
    /// Handles the submenu options for the Task entity.
    /// </summary>
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

    /// <summary>
    /// Handles the submenu options for the Dependency entity.
    /// </summary>
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
        Console.ForegroundColor = ConsoleColor.Cyan;
       // Console.BackgroundColor = ConsoleColor.Yellow;
        Console.WriteLine(@"                                                                                                $$\ 
                                                                                                $$ |
$$\  $$\  $$\  $$$$$$\         $$$$$$\   $$$$$$\   $$$$$$\         $$$$$$$\  $$$$$$\   $$$$$$\  $$ |
$$ | $$ | $$ |$$  __$$\        \____$$\ $$  __$$\ $$  __$$\       $$  _____|$$  __$$\ $$  __$$\ $$ |
$$ | $$ | $$ |$$$$$$$$ |       $$$$$$$ |$$ |  \__|$$$$$$$$ |      $$ /      $$ /  $$ |$$ /  $$ |$$ |
$$ | $$ | $$ |$$   ____|      $$  __$$ |$$ |      $$   ____|      $$ |      $$ |  $$ |$$ |  $$ |$$ |
\$$$$$\$$$$  |\$$$$$$$\       \$$$$$$$ |$$ |      \$$$$$$$\       \$$$$$$$\ \$$$$$$  |\$$$$$$  |$$ |
 \_____\____/  \_______|       \_______|\__|       \_______|       \_______| \______/  \______/ \__|
                                                                                                    
                                                                                                    
                                                                                                    ");
        Console.WriteLine(@" __      __     _                        _                           _           _   
 \ \    / /___ | | __  ___  _ __   ___  | |_  ___   _ __  _ _  ___  (_) ___  __ | |_ 
  \ \/\/ // -_)| |/ _|/ _ \| '  \ / -_) |  _|/ _ \ | '_ \| '_|/ _ \ | |/ -_)/ _||  _|
   \_/\_/ \___||_|\__|\___/|_|_|_|\___|  \__|\___/ | .__/|_|  \___/_/ |\___|\__| \__|
                                                   |_|            |__/               ");
        Console.WriteLine("Welcome to the project");
        Console.Write("Would you like to create Initial data? (Y/N)");
        string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
        if (ans == "Y") // Initializes all entities and resets the project start date
        {
            s_bl.ResetsAllEntitiesInTheData();
            s_bl.resetDataConfig();
            DalTest.Initialization.Do();
        }

        int choice = 0;
        if (s_bl.Time.StartDate() == null) // There is no start date for the project yet
        {

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
                        SettingDatesforProjectAndForTasks(); // Set project and task dates
                        break;

                    default:
                        break;
                }


            } while (choice != 0 && s_bl.Time.StartDate() == null); ;

            Console.WriteLine("Project execution stage:");
            executionStage();
        }
        else // We have moved to the project execution phase
        {
            Console.WriteLine("Project execution stage:");
            executionStage();
        }

    }

    /// <summary>
    /// Handles the execution stage menu and related actions.
    /// </summary>
    private static void executionStage()
    {
        int choice = 0;
        do
        {
            printMainMenuOfExecutionStage();
            foreach (var task in s_bl.Task.ReadAll())
            {
                if (task.CompleteDate == null)
                {
                    task.ForecastDate = task.StartDate > task.ScheduledDate ? task.StartDate + task.RequiredEfforTime : task.ScheduledDate + task.RequiredEfforTime;
                    s_bl.Task.Update(task);
                }
            }
            choice = int.Parse(Console.ReadLine()!);
            switch (choice)
            {
                case 0:
                    return;

                case 1:
                    try
                    {
                        SubMenu("Engineer");
                    }
                    catch (Exception me)
                    {
                        Console.WriteLine(me.Message);
                    }
                    break;

                case 2:
                    assignAnEngineerToTask();
                    break;

                case 3:
                    ReportTaskComplete();
                    break;

                case 4:
                    foreach (var item in s_bl.Task.ReadAll())
                    {
                        Console.WriteLine(item);
                    }
                    break;

                case 5:
                    foreach (var item in s_bl.Engineer.ReadAll())
                    {
                        Console.WriteLine(item);
                    }
                    break;

                case 6:
                    readAllDep();
                    break;

                case 7:
                    foreach (var item in s_bl.Engineer.ReadAll(t => t.Task != null))
                    {
                        Console.WriteLine(item);
                    }
                    break;

                case 8:
                    foreach (var item in s_bl.Engineer.ReadAll(t => t.Task == null))
                    {
                        Console.WriteLine(item);
                    }
                    break;

                case 9:
                    foreach (var item in s_bl.Task.ReadAll(t => t.Status == Status.Done))
                    {
                        Console.WriteLine(item);
                    }
                    break;

                case 10:
                    foreach (var item in s_bl.Task.ReadAll(t => t.Status != Status.Done))
                    {
                        Console.WriteLine(item);
                    }
                    break; 

                case 11:
                    foreach (var item in s_bl.Task.ReadAll(t => t.Status == Status.Scheduled))
                    {
                        Console.WriteLine(item);
                    }
                    break;

                case 12:
                    foreach (var item in s_bl.Task.ReadAll(t => t.Status == Status.OnTrack))
                    {
                        Console.WriteLine(item);
                    }
                    break;


                default:
                    break;
            }
        }
        while (choice != 0);

    }

    /// <summary>
    /// Displays the main menu options for the execution stage.
    /// </summary>
    private static void printMainMenuOfExecutionStage()
    {
        Console.WriteLine("Press 0 to exit the program");
        Console.WriteLine("press 1 for actions on engineers");
        Console.WriteLine("Press 2 to assign an engineer to the task");
        Console.WriteLine("Tap 3 to report a completed task");
        Console.WriteLine("Press 4 to print all the task");
        Console.WriteLine("Press 5 to print the list of engineers");
        Console.WriteLine("Press 6 to print the list of dependencies");
        Console.WriteLine("Press 7 to print the list of engineers currently working on a task");
        Console.WriteLine("Press 8 to print the list of available engineers");
        Console.WriteLine("Press 9 to print the list of completed tasks");
        Console.WriteLine("Press 10 to print all tasks that have not yet been completed");
        Console.WriteLine("Press 11 to print all tasks that have not yet been started");
        Console.WriteLine("Press 12 to print all tasks currently in progress");
    }

    /// <summary>
    /// Assigns an engineer to a task based on user input.
    /// </summary>
    private static void assignAnEngineerToTask()
    {
        Console.WriteLine("Enter the ID of the engineer you want to assign:");
        int IdOfEngineer = int.Parse(Console.ReadLine()!);

        try
        {
            s_bl.Engineer.Read(IdOfEngineer); // may thrown exception

            Console.WriteLine("Enter the ID of the task you want to assign an engineer to:");
            int IdOfTask = int.Parse(Console.ReadLine()!);

            var task = s_bl.Task.Read(IdOfTask);
            if (task == null) // the task with does Not exist
            {
                Console.WriteLine($"task with ID={IdOfTask} does Not exist");
            }
            else
            {
                if (/*task.Engineer == null || */task.CompleteDate == null && s_bl.Engineer.Read(IdOfEngineer)!.Task == null) // The task is free and that engineer also has no other task in progress
                {
                    var Engineer = new BO.EngineerInTask()
                    {
                        Id = IdOfEngineer,
                        Name = ""
                    };
                    task.Engineer = Engineer;
                    task.StartDate = DateTime.Now;
                    s_bl.Task.Update(task);
                }
                else
                {
                    if (task.CompleteDate != null)// The mission has already been completed
                    {
                        Console.WriteLine("The mission has already been completed");
                    }
                    else
                    {
                        Console.WriteLine("An engineer is already assigned to another task");

                    }
                }
            }
        }
        catch (Exception me)
        {
            Console.WriteLine(me.Message);
        }

    }

    /// <summary>
    /// Reports the completion of a task based on user input.
    /// </summary>
    private static void ReportTaskComplete()
    {
        Console.WriteLine("Enter the Id of task");
        int ID = int.Parse(Console.ReadLine()!);

        var task = s_bl.Task.Read(ID); // the task with does Not exist
        if (task == null)
        {
            Console.WriteLine($"task with ID={ID} does Not exist");
        }
        else 
        {
            task.CompleteDate = DateTime.Now;
            s_bl.Task.Update(task);
            Console.WriteLine("The mission is complete");

        }

    }

}