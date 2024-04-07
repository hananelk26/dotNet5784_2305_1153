
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

    private static IDal? s_dal = DalApi.Factory.Get;

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
         
            string _email;
            _email = engineerEmail[i];

            double _cost = (double)s_rand.Next(100,1000);

            EngineerExperience _level = (EngineerExperience)s_rand.Next(1,5);

            Engineer newEng = new(_id, _email, _cost, _name, _level);

            s_dal!.Engineer.Create(newEng);
            i++;
        }
    }

    /// <summary>
    /// Creates tasks with sample data.
    /// </summary>
    private static void createTask()
    {
       
        (string,string)[] names = new (string,string)[] { ( "Market Research", "Gathering and analyzing data on consumer preferences" ), ( "Design Conceptualization", "Thinking about the design of the specifications and features of the electric vehicle" ),
            ( "Battery Development", "Research, development, and testing of high-performance batteries tailored for electric vehicles." ), ( "Chassis Engineering", "Designing and engineering the vehicle's chassis for structural integrity, safety, and weight distribution" ), ( "Electric Motor Development", " Research, development, and testing of efficient electric motors for propulsion." ), ( "Safety Regulations Compliance", " Ensuring the vehicle complies with safety regulations and standards set by relevant authorities." ),
            ("Prototype Fabrication", "Building prototype vehicles based on the conceptual designs for testing and validation." ), ( "Software Integration", "Developing and integrating software systems for vehicle control, monitoring, and user interface." ), ( "Supply Chain Management ", "Establishing and managing the supply chain for sourcing components and materials required for vehicle production." ), 
            ( "Assembly Line Setup", "Planning and setting up assembly lines for efficient production of electric vehicles." ), ( "Technology Assessment", "Evaluating available technologies and components" ), ( "Quality Control Implementation", " Implementing quality control measures to ensure every vehicle meets performance and safety standards." ), ( "Testing and Validation", " Conducting rigorous testing and validation procedures on prototype vehicles to ensure reliability and performance." ),
            ( "Manufacturing Optimization", "Identifying and implementing strategies to optimize manufacturing processes for increased efficiency and cost-effectiveness. " ), ( "Interior Design and Comfort", "Designing and integrating interior features for comfort, aesthetics, and functionality. " ), ( "Exterior Styling ", "Styling and designing the exterior of the vehicle for aesthetics" ), 
            ( "Logistics Planning", "Planning and organizing logistics for transportation of components, assembly, and distribution of finished vehicles." ), ( "Marketing and Branding", " Developing marketing strategies and branding initiatives to promote the electric vehicle and attract consumers." ), ( "Regulatory Approval", "Obtaining necessary approvals and certifications from regulatory bodies for the production and sale of electric vehicles." ), ( "Customer Support Setup", "Establishing customer support systems for addressing inquiries, feedback, and after-sales services." ) };


        foreach (var (name1,name2) in names)
        {
            int temp = s_rand.Next(10*24*60, 60 * 30 * 24 * 2);
            TimeSpan RequiredEffortTime = TimeSpan.FromMinutes(temp);
            int temp2 = s_rand.Next(1, 5); 
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            DateTime? _bdt;
            _bdt = (DateTime)s_dal.MainClock.GetMainClock();
            Task newTask = new Task(0,name1,name2,(DateTime)_bdt,RequiredEffortTime,(DO.EngineerExperience)temp2);
 
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
            new Dependency(2,2,11),
            new Dependency(3,3,11),
            new Dependency(4,3,6),
            new Dependency(5,4,2),
            new Dependency(6,4,6),
            new Dependency(7,5,11),
            new Dependency(8,5,3),
            new Dependency(9,6,2),
            new Dependency(10,7,4),
            new Dependency(11,7,5),
            new Dependency(12,8,5),
            new Dependency(13,8,3),
            new Dependency(14,9,1),
            new Dependency(15,9,11),
            new Dependency(16,10,7),
            new Dependency(17,10,9),
            new Dependency(18,12,10),
            new Dependency(19,13,7),
            new Dependency(20,13,6),
            new Dependency(21,14,9),
            new Dependency(22,14,10),
            new Dependency(23,15,2),
            new Dependency(24,15,6),
            new Dependency(25,16,2),
            new Dependency(26,16,6),
            new Dependency(27,17,9),
            new Dependency(28,17,10),
            new Dependency(29,18,1),
            new Dependency(30,18,16),
            new Dependency(31,19,6),
            new Dependency(32,19,12),
            new Dependency(33,20,18),
            new Dependency(34,20,8),
            new Dependency(35,20,1),
            new Dependency(36,20,2),
            new Dependency(37,20,6),
            new Dependency(38,20,15),
            new Dependency(39,20,16),
            new Dependency(40,20,17),
        };
        foreach (var dep in deps)
        {
           s_dal!.Dependency.Create(dep);
        }
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
