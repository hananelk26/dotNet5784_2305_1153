using Dal;
using DalApi;
namespace DalTest;

internal class Program
{
    private static IEngineer? s_dalEngineer = new EngineerImplementation();
    private static ITask? s_dalTask = new TaskImplementation();
    private static IDependency? s_dalDependency = new DependencyImplementation();

    private static void MainMenu()
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


    }

    private static void SubMenuTask()
    {
        PrintSubMenu();
    }

    private static void SubMenuDependency()
    {
        PrintSubMenu();
    }


    static void Main(string[] args)
    {
        Initialization.Do(s_dalEngineer, s_dalTask, s_dalDependency);
        int choice = 0;
        do
        {
            MainMenu();
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

