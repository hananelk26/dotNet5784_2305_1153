
namespace DalTest;
using DalApi;
using DO;
using System.Security.Cryptography;
using System.Xml.Linq;

/// <summary>
/// Class responsible for initializing the application with sample data.
/// </summary>
public static class Initialization
{
    const int MIN = 200000000;
    const int MAX = 400000000;

    private static IDal? s_dal;

    private static readonly Random s_rand = new();



    /// <summary>
    /// Creates engineers with sample data.
    /// </summary>
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
            {
                _id = s_rand.Next(MIN, MAX);
            }
            while (s_dal!.Engineer.Read(_id) != null);
            // while (s_dalEngineer!.Read(_id) != null);
            string _email;
            _email = engineerEmail[i];

            double _cost = (double)s_rand.Next(100,1000);

            EngineerExperience _level = (EngineerExperience)s_rand.Next(1,5);

            Engineer newEng = new(_id, _email, _cost, _name, _level);

            //s_dalEngineer!.Create(newEng);
            s_dal!.Engineer.Create(newEng);
            i++;
        }
    }

    /// <summary>
    /// Creates tasks with sample data.
    /// </summary>
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
            int temp = s_rand.Next(0, 60 * 30 * 24 * 2);
            TimeSpan RequiredEffortTime = TimeSpan.FromMinutes(temp);
            int temp2 = s_rand.Next(1, 5); 
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            DateTime _bdt;
            _bdt = (DateTime)s_dal.MainClock.GetMainClock();
            Task newTask = new Task(0,name1,name2,_bdt,RequiredEffortTime,(DO.EngineerExperience)temp2);
            //s_dalTask!.Create(newTask);
            s_dal!.Task.Create(newTask);
        }


    }

    /// <summary>
    /// Creates dependencies between tasks with sample data.
    /// </summary>

    private static void createDependency()
    {
        Dependency[] deps =
        {
            new Dependency(1,2,1),
            new Dependency(2,6,4),
            new Dependency(3,6,3),
            new Dependency(4,6,2),
            new Dependency(5,6,1),
            new Dependency(6,6,5),
            new Dependency(7,8,6),
            new Dependency(8,8,7),
            new Dependency(9,8,2),
            new Dependency(10,11,2),
            new Dependency(24,11,4),
            new Dependency(25,11,1),
            new Dependency(26,11,8),
            new Dependency(27,11,3),
            new Dependency(28,11,5),
            new Dependency(11,11,10),
            new Dependency(12,12,11),
            new Dependency(29,12,6),
            new Dependency(30,12,5),
            new Dependency(31,12,4),
            new Dependency(32,13,5),
            new Dependency(33,13,6),
            new Dependency(34,13,4),
            new Dependency(13,13,12),
            new Dependency(14,14,13),
            new Dependency(15,14,8),
            new Dependency(16,16,15),
            new Dependency(35,16,2),
            new Dependency(36,16,4),
            new Dependency(37,16,5),
            new Dependency(38,16,10),
            new Dependency(39,16,1),
            new Dependency(40,16,3),
            new Dependency(17,16,2),
            new Dependency(18,20,8),
            new Dependency(19,20,2),
            new Dependency(23,20,16),
            new Dependency(20,20,19),
            new Dependency(21,20,18),
            new Dependency(22,20,17),
        };
        foreach (var dep in deps)
        {
           // s_dalDependency!.Create(dep);
           s_dal!.Dependency.Create(dep);
        }
    }

    public static void resetDataConfig()
    {
        s_dal = DalApi.Factory.Get;
        s_dal.Time.reset();

        XElement? ex = null;

        const string s_xml_dir = @"..\xml\";
        string filePath = $"{s_xml_dir + "data-config"}.xml";
        try
        {
            if (File.Exists(filePath))
                ex = XElement.Load(filePath);

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

    /// <summary>
    /// Initializes the application with sample data.
    /// </summary>
    public static void Do() 
    {
        
        createEngineers();
        createTask();
        createDependency();
    }
}
