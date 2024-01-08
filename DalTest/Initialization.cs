
namespace DalTest;
using DalApi;
using DO;
public static class Initialization
{
    const int MIN = 200000000;
    const int MAX = 400000000;

    private static ITask? s_dalTask; //stage 1
    private static IEngineer? s_dalEngineer; //stage 1
    private static IDependency? s_dalDependency; //stage 1

    private static readonly Random s_rand = new();


    private static void createEngineers()
    {
        string[] engineerNames =
        {
            "Dani Levi", "Eli Amar", "Yair Cohen",
            "Ariela Lev", "Dina Klein"
        };

        string[] engineerEmail =
        {
            "DaniLevi@gmail.com", "EliAmar@gmail.com", "YairCohen@gmail.com",
            "ArielaLev@gmail.com", "DinaKlein@gmail.com"
        };

        int i = 0;
        foreach (var _name in engineerNames)
        {

            int _id;
            do
                _id = s_rand.Next(MIN, MAX);
            while (s_dalEngineer!.Read(_id) != null);

            string _email;
            _email = engineerEmail[i];

            double _cost = (double)s_rand.Next(100,1000);

            EngineerExperience _level = (EngineerExperience)s_rand.Next(0,4);

            Engineer newEng = new(_id, _email, _cost, _name, _level);

            s_dalEngineer!.Create(newEng);
            i++;
        }
    }

    private static void createTask()
    {
        (string,string)[] names = new (string,string)[] { ( "t3", "Project Kickoff Meeting" ), ( "t4", "RequirZement Elicitation and Analysis" ),
            ( "g5", "Feasibility Study" ), ( "H5", "Project Planning" ), ( "h8", "System Architecture Design" ), ( "h1", "Prototyping" ),
            ("B3", "Database Design" ), ( "b4", "Development Environment Setup" ), ( "b5", "Development Environment Setup" ), 
            ( "B6", "Initial Coding" ), ( "b7", "Unit Testing" ), ( "b8", "Integration Testing" ), ( "b9", "User Interface (UI) Development" ),
            ( "V1", "User Acceptance Testing (UAT) Preparation" ), ( "V2", "Beta Release" ), ( "g1", "UAT Execution" ), 
            ( "g2", "Refinement and Bug Fixing" ), ( "g3", "Documentation Update" ), ( "g4", "Deployment Planning" ), ( "g6", "Production Deployment" ) };


        foreach (var (name1,name2) in names)
        {
         
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            DateTime _bdt = start.AddDays(s_rand.Next(range));

            Task newTask = new Task(0,name1,name2,_bdt);
            s_dalTask!.Create(newTask);
        }

    }
  

    private static void createDependency()
    {
        Dependency[] deps =
        {
            new Dependency(1,2,1),
            new Dependency(2,3,2),
            new Dependency(3,3,1),
            new Dependency(4,4,3),
            new Dependency(5,5,4),
            new Dependency(6,6,5),
            new Dependency(7,7,6),
            new Dependency(8,8,7),
            new Dependency(9,9,8),
            new Dependency(10,10,9),
            new Dependency(11,11,10),
            new Dependency(12,12,11),
            new Dependency(13,13,12),
            new Dependency(14,14,13),
            new Dependency(15,15,14),
            new Dependency(16,16,15),
            new Dependency(17,17,16),
            new Dependency(18,18,17),
            new Dependency(19,19,18),
            new Dependency(20,20,19),
            new Dependency(21,20,18),
            new Dependency(22,20,17),
            new Dependency(23,20,16),
            new Dependency(24,10,1),
            new Dependency(25,9,2),
            new Dependency(26,9,3),
            new Dependency(27,11,3),
            new Dependency(28,11,5),
            new Dependency(29,12,6),
            new Dependency(30,12,5),
            new Dependency(31,12,4),
            new Dependency(32,13,5),
            new Dependency(33,13,6),
            new Dependency(34,13,4),
            new Dependency(35,15,4),
            new Dependency(36,15,2),
            new Dependency(37,7,2),
            new Dependency(38,16,10),
            new Dependency(39,16,1),
            new Dependency(40,16,3),
        };
        foreach (var dep in deps)
        {
            s_dalDependency!.Create(dep);
        }
    }


    public static void Do(IEngineer? dalEngineer,ITask? dalTask,IDependency? dalDependency) 
    {
        s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        s_dalDependency = dalDependency ?? throw new NullReferenceException("DAL can not be null!");

        createEngineers();
        createTask();
        createDependency();
    }
}
