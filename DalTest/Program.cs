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



    }

    private static void readTsk()
    {



    }

    private static void readAllTsk()
    {



    }

    private static void updateTsk()
    {



    }

    private static void deleteTsk()
    {



    }

    private static void createDepend()
    {



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
                    SubMenu("Engineer");
                    break;
                case 2:
                    SubMenu("Task");
                    break;
                case 3:
                    SubMenu("Dependency");
                    break;
                default:
                    break;
            }


        } while (choice != 0);


    }
}

