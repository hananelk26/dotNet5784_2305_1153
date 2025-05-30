﻿using Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace DalTest;

internal class Program
{
    //private static IEngineer? s_dalEngineer = new EngineerImplementation();
    //private static ITask? s_dalTask = new TaskImplementation();
    //private static IDependency? s_dalDependency = new DependencyImplementation();

    //static readonly IDal s_dal = new DalList(); //stage 2
    //static readonly IDal s_dal = new DalXml();
    static readonly IDal s_dal = Factory.Get;
    private static void printTaskFields(DO.Task p)
    {
        Console.WriteLine($"ID is: {p.Id} ");
        Console.WriteLine($"Alias is: {p.Alias} ");
        Console.WriteLine($"Description is: {p.Description} ");
        Console.WriteLine($"createdAtDate is: {p.createdAtDate} ");
        Console.WriteLine($"RequiredEffortTime is: {p.RequiredEffortTime} ");
        Console.WriteLine($"Copmlexity is: {p.Copmlexity} ");
        Console.WriteLine($"StartDate is: {p.StartDate} ");
        Console.WriteLine($"ScheduledDate is: {p.ScheduledDate} ");
       // Console.WriteLine($"DeadlineDate is: {p.DeadlineDate} ");
        Console.WriteLine($"CompleteDate is: {p.CompleteDate} ");
        Console.WriteLine($"Deliverables is: {p.Deliverables} ");
        Console.WriteLine($"Remarks is: {p.Remarks} ");
        Console.WriteLine($"Engineerid is: {p.EngineerId} ");
        Console.WriteLine();
    
    
    }
    private static void printMainMenu()
    {
        Console.WriteLine("Press 0 to exit");
        Console.WriteLine("Press 1 to Engineer");
        Console.WriteLine("Press 2 to Task");
        Console.WriteLine("Press 3 to Dependency");
    }

    private static void PrintSubMenu()
    {
        Console.WriteLine("Press 1 to exit");
        Console.WriteLine("Press 2 to Create");
        Console.WriteLine("Press 3 to Read");
        Console.WriteLine("Press 4 to ReadAll");
        Console.WriteLine("Press 5 to UpData");
        Console.WriteLine("Press 6 to Delete");
    }

    private static void SubMenu(string entity)
    {
        PrintSubMenu();
        int ch = int.Parse(Console.ReadLine()!);
        switch (ch)
        {
            case 1:
                return;

            case 2:

                if (entity == "Engineer")
                {
                    createEng();
                }
                else if (entity == "Task")
                {
                    createTsk();
                }
                else
                {
                    createDepend(); 
                }
                break;

            case 3:

                if (entity == "Engineer")
                {
                    readEng();
                }
                else if (entity == "Task")
                {
                    readTsk();
                }
                else
                {
                    readDepend();
                }
                break;

            case 4:

                if (entity == "Engineer")
                {
                    readAllEng();
                }
                else if (entity == "Task")
                {
                    readAllTsk();
                }
                else
                {
                    readAllDepend();
                }
                break;

            case 5:

                if (entity == "Engineer")
                {
                    updateEng();
                }
                else if (entity == "Task")
                {
                    updateTsk();
                }
                else
                {
                    updateDepend();
                }
                break;

            case 6:
                if (entity == "Engineer")
                {
                    deleteEng();
                }
                else if (entity == "Task")
                {
                    deleteTsk();
                }
                else
                {
                    deleteDepend();
                }
                break;

            default:
                break;
        }


    }

    private static void createEng()
    {
        Console.WriteLine("Enter Id:");
        int Id = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter Email:");
        string Email = Console.ReadLine()!;
        Console.WriteLine("Enter Cost:");
        double Cost = double.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter Name");
        string Name = Console.ReadLine()!;
        Console.WriteLine("Enter experience");
        EngineerExperience experience = (EngineerExperience)int.Parse(Console.ReadLine()!);
        Engineer x =new Engineer(Id, Email, Cost, Name , experience);
       int y = s_dal.Engineer.Create(x);
        Console.WriteLine(y);
    }

    private static void readEng()
    {
        Console.WriteLine("Enter Id");
       int id =  int.Parse(Console.ReadLine()!);
        Engineer x = s_dal.Engineer.Read(id)!;
        printEng(x);
    }

    private static void readAllEng()
    {
        foreach(var en in  s_dal.Engineer.ReadAll())
        {
            printEng(en);
        }
    }

    private static void updateEng()
    {
        Console.WriteLine("Enter Id:");
        int Id = int.Parse(Console.ReadLine()!);

        Engineer eng = s_dal.Engineer.Read(Id)!;

        Console.WriteLine("Enter Email:");
        string? Email = Console.ReadLine();
        if (Email  == null || Email == "")
        {
            Email = eng.Email;
        }

        Console.WriteLine("Enter Cost:");
        double Cost = 0;
        string? tmp = Console.ReadLine();
        if (tmp == null || tmp == "")
        {
            Cost = eng.Cost;
        }
        else 
        {
            Cost = double.Parse(tmp);
        }
        
        Console.WriteLine("Enter Name");
        string? Name = Console.ReadLine();
        if (Name == null || Name == "")
        {
            Name = eng.Name;
        }

        Console.WriteLine("Enter experience");
        EngineerExperience experience = EngineerExperience.Beginner;
        tmp = Console.ReadLine();
        if (tmp == null || tmp == "")
        {
            experience = eng.Level;
        }
        else
        {
            experience = (EngineerExperience)int.Parse(tmp);
        }

        Engineer x = new Engineer(Id, Email, Cost, Name, experience);
        s_dal.Engineer.Update(x);
    }

    private static void deleteEng()
    {
        Console.WriteLine("Enter Id");
        int y = int.Parse(Console.ReadLine()!);
        s_dal.Engineer.Delete(y);
    }


    private static void createTsk()
    {
        Console.WriteLine("Enter ID:");
        int Id = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter string Alias:");
        string alias = Console.ReadLine()!;
        Console.WriteLine("Enter Description:");
        string Description = Console.ReadLine()!;    
        Console.WriteLine("Enter created Date:");
        DateTime createdAtDate =DateTime.Parse( Console.ReadLine()!);
        //Console.WriteLine("Enter isMileStone:");
        Console.WriteLine("Enter Required Effort Time");
        TimeSpan? RequiredEffortTime =TimeSpan.Parse( Console.ReadLine()!);
        Console.WriteLine("Enter Copmlexity:");
        DO.EngineerExperience? Copmlex = (DO.EngineerExperience)(int.Parse(Console.ReadLine()!));
        Console.WriteLine("Enter StartDate:");
        DateTime? StartDate = DateTime.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter Scheduled Date");
        DateTime? ScheduledDate = DateTime.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter DeadlineDate:");
        DateTime? DeadlineDate = DateTime.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter CompleteDate:");
        DateTime? CompleteDate = DateTime.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter Deliverables:");
        string? Deliverables = Console.ReadLine();
        Console.WriteLine("Enter Remarks:");
        string? Remarks = Console.ReadLine();
        Console.WriteLine("Enter EngineerId:");
        int? EngineerId = int.Parse( Console.ReadLine()!);   

        DO.Task p = new DO.Task(Id, alias!, Description!, createdAtDate, RequiredEffortTime, Copmlex, StartDate, ScheduledDate, DeadlineDate, CompleteDate, Deliverables, Remarks, EngineerId);
        int id = s_dal.Task!.Create(p);
        Console.WriteLine($"The ID task is:{id}");

    }

    private static void readTsk()
    {
        Console.WriteLine("Enter ID");
        int id = int.Parse(Console.ReadLine()!);
        DO.Task p = s_dal.Task.Read(id)!;
        printTaskFields(p);
    }

    private static void readAllTsk()
    {
        IEnumerable<DO.Task> p = s_dal.Task.ReadAll()!;
        foreach (var item in p)
        {
            printTaskFields(item);
        }
    }

    private static void updateTsk()
    {
        Console.WriteLine("Enter ID:");
        int Id = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter string Alias:");
        string alias = Console.ReadLine()!;
        Console.WriteLine("Enter Description:");
        string Description = Console.ReadLine()!;
        Console.WriteLine("Enter created Date:");
        DateTime createdAtDate = DateTime.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter isMileStone:");
        Console.WriteLine("Enter Required Effort Time");
        TimeSpan? RequiredEffortTime = TimeSpan.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter Copmlexity:");
        DO.EngineerExperience? Copmlex = (DO.EngineerExperience)(int.Parse(Console.ReadLine()!));
        Console.WriteLine("Enter StartDate:");
        DateTime? StartDate = DateTime.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter Scheduled Date");
        DateTime? ScheduledDate = DateTime.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter DeadlineDate:");
        DateTime? DeadlineDate = DateTime.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter CompleteDate:");
        DateTime? CompleteDate = DateTime.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter Deliverables:");
        string? Deliverables = Console.ReadLine();
        Console.WriteLine("Enter Remarks:");
        string? Remarks = Console.ReadLine();
        Console.WriteLine("Enter EngineerId:");
        int? EngineerId = int.Parse(Console.ReadLine()!);

        DO.Task p =  s_dal.Task.Read(Id)!;
        printTaskFields(p);

        DO.Task t = new DO.Task(Id, alias, Description, createdAtDate, RequiredEffortTime, Copmlex, StartDate, ScheduledDate, DeadlineDate, CompleteDate, Deliverables, Remarks, EngineerId);
        s_dal.Task.Update(t);
    }

    private static void deleteTsk()
    {
        Console.WriteLine("Enter id:");
        int id = int.Parse(Console.ReadLine()!);
        s_dal.Task!.Delete(id);
    }

    private static void createDepend()
    {
        Console.WriteLine("Enter id:");
        int id = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter DependentTask:");
        int? DependentTask = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter DependsOnTask");
        int? DependsOnTask = int.Parse(Console.ReadLine()!); 

        DO.Dependency dep = new DO.Dependency(id,DependentTask,DependsOnTask);
       int cuurent= s_dal.Dependency.Create(dep);
        Console.WriteLine($"The ID Dependency is:{cuurent}");
    }

    private static void readDepend()
    {
        Console.WriteLine("Enter Id");
        int id = int.Parse(Console.ReadLine()!);
        Dependency y = s_dal.Dependency.Read(id)!;
        printDepe(y);
    }

    private static void readAllDepend()
    {
        foreach (var en in s_dal.Dependency.ReadAll())
        {
            printDepe(en!);
        }
    }

    private static void updateDepend()
    {
        Console.WriteLine("Enter id:");
        int id = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter DependentTask:");
        int? DependentTask = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter DependsOnTask");
        int? DependsOnTask = int.Parse(Console.ReadLine()!);

        DO.Dependency p =  s_dal.Dependency.Read(id)!;
        printDepe(p);

        DO.Dependency dep = new DO.Dependency(id, DependentTask, DependsOnTask);
        s_dal.Dependency.Update(dep);

    }

    private static void deleteDepend()
    {
        Console.WriteLine("Enter Id");
        int y = int.Parse(Console.ReadLine()!);
        s_dal.Dependency.Delete(y);
    }

    public static void printEng(Engineer? en)
    {
        if (en != null)
        {
            Console.WriteLine($" The Id is: {en.Id}");
            Console.WriteLine($"The Name is: {en.Name}");
            Console.WriteLine($"The Email is: {en.Email}");
            Console.WriteLine($"The Cost is: {en.Cost}");
            Console.WriteLine($"The experience is: {en.Level}");
            Console.WriteLine();
        }
    }
    public static void printDepe(Dependency dep)
    {
        if(dep != null)
        {
            Console.WriteLine($"The Id:{dep.Id}");
            Console.WriteLine($"The DependentTask: {dep.DependentTask}");
            Console.WriteLine($"The Dependent DependsOnTask: {dep.DependsOnTask}");
            Console.WriteLine();
        }
    }

    public static void resetDataConfig()
    {
        XElement ?ex = null;

        const string s_xml_dir = @"..\xml\";
        string filePath = $"{s_xml_dir + "data-config"}.xml";
        try
        {
            if (File.Exists(filePath))
               ex =  XElement.Load(filePath);
            
        }
        catch (Exception eex)
        {
            throw new DalXMLFileLoadCreateException($"fail to load xml file: {s_xml_dir + filePath}, {eex.Message}");
        }
        int num = 1;
        ex!.Element("NextTaskId")!.Value = num.ToString();
        ex.Element("NextDependencyId")!.Value = num.ToString();

        ex.Save(filePath);
        
    }

    static void Main(string[] args)
    {
        Console.Write("Would you like to create Initial data? (Y/N)"); 
        string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input"); 
        if (ans == "Y") 
        {
            s_dal.Engineer.DeleteAll();
            s_dal.Task.DeleteAll();
            s_dal.Dependency.DeleteAll();
            resetDataConfig();
            Initialization.Do();
        }
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

