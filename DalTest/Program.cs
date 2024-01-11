using Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;
namespace DalTest;

internal class Program
{
    private static IEngineer? s_dalEngineer = new EngineerImplementation();
    private static ITask? s_dalTask = new TaskImplementation();
    private static IDependency? s_dalDependency = new DependencyImplementation();
    private static void printTaskFields(DO.Task p)
    {
        Console.WriteLine($"ID is: {p.id} ");
        Console.WriteLine($"Alias is: {p.Alias} ");
        Console.WriteLine($"Description is: {p.Description} ");
        Console.WriteLine($"createdAtDate is: {p.createdAtDate} ");
        Console.WriteLine($" isMileStone is: {p.isMileStone} ");
        Console.WriteLine($"RequiredEffortTime is: {p.RequiredEffortTime} ");
        Console.WriteLine($"Copmlexity is: {p.Copmlexity} ");
        Console.WriteLine($"StartDate is: {p.StartDate} ");
        Console.WriteLine($"ScheduledDate is: {p.ScheduledDate} ");
        Console.WriteLine($"DeadlineDate is: {p.DeadlineDate} ");
        Console.WriteLine($"CompleteDate is: {p.CompleteDate} ");
        Console.WriteLine($"Deliverables is: {p.Deliverables} ");
        Console.WriteLine($"Remarks is: {p.Remarks} ");
        Console.WriteLine($"Engineerld is: {p.Engineerld} ");
    
    
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
        int ch = int.Parse(Console.ReadLine());
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
        int Id = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter Email:");
        string Email = Console.ReadLine();
        Console.WriteLine("Enter Cost:");
        double Cost = double.Parse(Console.ReadLine());
        Console.WriteLine("Enter Name");
        string Name = Console.ReadLine();
        Console.WriteLine("Enter experience");
        EngineerExperience experience = (EngineerExperience)int.Parse(Console.ReadLine());
        Engineer x =new Engineer(Id, Email, Cost, Name , experience);
       int y = s_dalEngineer.Create(x);
        Console.WriteLine(y);
    }

    private static void readEng()
    {
        Console.WriteLine("Enter Id");
       int id =  int.Parse(Console.ReadLine());
        Engineer x = s_dalEngineer.Read(id);
        printEng(x);
    }

    private static void readAllEng()
    {
        foreach(var en in  s_dalEngineer.ReadAll())
        {
            printEng(en);
        }
    }

    private static void updateEng()
    {
        Console.WriteLine("Enter Id:");
        int Id = int.Parse(Console.ReadLine());

        Engineer eng = s_dalEngineer.Read(Id);

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
        s_dalEngineer.Update(x);
    }

    private static void deleteEng()
    {
        Console.WriteLine("Enter Id");
        int y = int.Parse(Console.ReadLine());
        s_dalEngineer.Delete(y);
    }


    private static void createTsk()
    {
        Console.WriteLine("Enter ID:");
        int Id = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter string Alias:");
        string alias = Console.ReadLine();
        Console.WriteLine("Enter Description:");
        string Description = Console.ReadLine();    
        Console.WriteLine("Enter created Date:");
        DateTime createdAtDate =DateTime.Parse( Console.ReadLine());
        Console.WriteLine("Enter isMileStone:");
        bool isMail =bool.Parse( Console.ReadLine());
        Console.WriteLine("Enter Required Effort Time");
        TimeSpan? RequiredEffortTime =TimeSpan.Parse( Console.ReadLine());
        Console.WriteLine("Enter Copmlexity:");
        DO.EngineerExperience? Copmlex = (DO.EngineerExperience)(int.Parse(Console.ReadLine()));
        Console.WriteLine("Enter StartDate:");
        DateTime? StartDate = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("Enter Scheduled Date");
        DateTime? ScheduledDate = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("Enter DeadlineDate:");
        DateTime? DeadlineDate = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("Enter CompleteDate:");
        DateTime? CompleteDate = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("Enter Deliverables:");
        string? Deliverables = Console.ReadLine();
        Console.WriteLine("Enter Remarks:");
        string? Remarks = Console.ReadLine();
        Console.WriteLine("Enter Engineerld:");
        int? Engineerld = int.Parse( Console.ReadLine());   

        DO.Task p = new DO.Task(Id, alias, Description, createdAtDate, isMail, RequiredEffortTime, Copmlex, StartDate, ScheduledDate, DeadlineDate, CompleteDate, Deliverables, Remarks, Engineerld);
        int id = s_dalTask!.Create(p);
        Console.WriteLine($"The ID task is:{id}");

    }

    private static void readTsk()
    {
        Console.WriteLine("Enter ID");
        int id = int.Parse(Console.ReadLine());
        DO.Task p = s_dalTask.Read(id);
        printTaskFields(p);
    }

    private static void readAllTsk()
    {
        List<DO.Task> p = s_dalTask.ReadAll();
        foreach (var item in p)
        {
            printTaskFields(item);
        }
    }

    private static void updateTsk()
    {
        Console.WriteLine("Enter ID:");
        int Id = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter string Alias:");
        string alias = Console.ReadLine();
        Console.WriteLine("Enter Description:");
        string Description = Console.ReadLine();
        Console.WriteLine("Enter created Date:");
        DateTime createdAtDate = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("Enter isMileStone:");
        bool isMail = bool.Parse(Console.ReadLine());
        Console.WriteLine("Enter Required Effort Time");
        TimeSpan? RequiredEffortTime = TimeSpan.Parse(Console.ReadLine());
        Console.WriteLine("Enter Copmlexity:");
        DO.EngineerExperience? Copmlex = (DO.EngineerExperience)(int.Parse(Console.ReadLine()));
        Console.WriteLine("Enter StartDate:");
        DateTime? StartDate = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("Enter Scheduled Date");
        DateTime? ScheduledDate = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("Enter DeadlineDate:");
        DateTime? DeadlineDate = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("Enter CompleteDate:");
        DateTime? CompleteDate = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("Enter Deliverables:");
        string? Deliverables = Console.ReadLine();
        Console.WriteLine("Enter Remarks:");
        string? Remarks = Console.ReadLine();
        Console.WriteLine("Enter Engineerld:");
        int? Engineerld = int.Parse(Console.ReadLine());

        DO.Task p =  s_dalTask.Read(Id);
        printTaskFields(p);

        DO.Task t = new DO.Task(Id, alias, Description, createdAtDate, isMail, RequiredEffortTime, Copmlex, StartDate, ScheduledDate, DeadlineDate, CompleteDate, Deliverables, Remarks, Engineerld);
        s_dalTask.Update(t);
    }

    private static void deleteTsk()
    {
        Console.WriteLine("Enter id:");
        int id = int.Parse(Console.ReadLine());
        s_dalTask!.Delete(id);
    }

    private static void createDepend()
    {
        Console.WriteLine("Enter id:");
        int id = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter DependentTask:");
        int? DependentTask = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter DependsOnTask");
        int? DependsOnTask = int.Parse(Console.ReadLine()); 

        DO.Dependency dep = new DO.Dependency(id,DependentTask,DependsOnTask);
        s_dalDependency.Create(dep);
        Console.WriteLine($"The ID Dependency is:{id}");
    }

    private static void readDepend()
    {
        Console.WriteLine("Enter Id");
        int id = int.Parse(Console.ReadLine());
        Dependency y = s_dalDependency.Read(id);
        printDepe(y);
    }

    private static void readAllDepend()
    {
        foreach (var en in s_dalDependency.ReadAll())
        {
            printDepe(en);
        }
    }

    private static void updateDepend()
    {
        Console.WriteLine("Enter id:");
        int id = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter DependentTask:");
        int? DependentTask = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter DependsOnTask");
        int? DependsOnTask = int.Parse(Console.ReadLine());

        DO.Dependency p =  s_dalDependency.Read(id);
        printDepe(p);

        DO.Dependency dep = new DO.Dependency(id, DependentTask, DependsOnTask);
        s_dalDependency.Update(dep);

    }

    private static void deleteDepend()
    {
        Console.WriteLine("Enter Id");
        int y = int.Parse(Console.ReadLine());
        s_dalDependency.Delete(y);
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
        }
    }
    public static void printDepe(Dependency dep)
    {
        if(dep != null)
        {
            Console.WriteLine($"The Id:{dep.Id}");
            Console.WriteLine($"The DependentTask: {dep.DependentTask}");
            Console.WriteLine($"The Dependent DependsOnTask: {dep.DependsOnTask}");
        }
    }

    static void Main(string[] args)
    {
        Initialization.Do(s_dalEngineer, s_dalTask, s_dalDependency);
        int choice = 0;
        do
        {
            printMainMenu();
            choice = int.Parse(Console.ReadLine());
            switch(choice)
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

