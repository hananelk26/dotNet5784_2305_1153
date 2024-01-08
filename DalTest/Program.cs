using Dal;
using DalApi;
using System.Diagnostics;
using System.Linq.Expressions;
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

    private static void SubMenuEngineer()
    {
        PrintSubMenu();
        int ch = int.Parse(Console.ReadLine());
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

    private static void createEng()
    {
        int Id = int.Parse(Console.ReadLine());
        string Email = Console.ReadLine();
        double Cost = double.Parse(Console.ReadLine());
        string Name = Console.ReadLine();




    }

    private static void readEng()
    {


    }

    private static void readAllEng()
    {


    }

    private static void updateEng()
    {


    }

    private static void deleteEng()
    {


    }
    

    private static void SubMenuTask()
    {
        PrintSubMenu();
        int ch = int.Parse(Console.ReadLine());
        switch (ch)
        {
            case 1:
                return;
            case 2:
                createTsk();
                break;
            case 3:
                readTsk();
                break;

            case 4:
                readAllTsk();
                break;

            case 5:
                updateTsk();
                break;

            case 6:
                deleteTsk();
                break;

            default:
                break;
        }
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


    private static void SubMenuDependency()
    {
        PrintSubMenu();
        int ch = int.Parse(Console.ReadLine());
        switch (ch)
        {
            case 1:
                return;
            case 2:
                createDepend();
                break;
            case 3:
                readDepend();
                break;

            case 4:
                readAllDepend();
                break;

            case 5:
                updateDepend();
                break;

            case 6:
                deleteDepend();
                break;

            default:
                break;
        }
    }

    private static void createDepend()
    {



    }

    private static void readDepend()
    {



    }

    private static void readAllDepend()
    {



    }

    private static void updateDepend()
    {



    }

    private static void deleteDepend()
    {



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
                    SubMenuEngineer();
                    break;
                case 2:
                    SubMenuTask();
                    break;
                case 3:
                    SubMenuDependency();
                    break;
                default:
                    break;
            }


        } while (choice != 0);


    }
}

