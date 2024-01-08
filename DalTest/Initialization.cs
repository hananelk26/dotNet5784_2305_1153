
namespace DalTest;
using DalApi;
using DO;
using System.Collections.Specialized;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Security.Cryptography;

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


        // אתה מוזמן לממש



        Dependency newTask = new Dependency(0);
        s_dalDependency!.Create(newTask);
    }

    
}
