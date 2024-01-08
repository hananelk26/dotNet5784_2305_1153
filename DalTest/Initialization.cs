
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
            while (s_dalDependency!.Read(_id) != null);

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
       




        Task newTask = new Task();
        s_dalTask!.Create(newTask);
    }


    private static void createDependency()
    {
        Dependency[] deps =
        {
            new Dependency(1,),
            new Dependency(2,),
            new Dependency(3,),
            new Dependency(4,),
            new Dependency(5,),
            new Dependency(6,),
            new Dependency(7,),
            new Dependency(8,),
            new Dependency(9,),
            new Dependency(10,),
            new Dependency(11,),
            new Dependency(12,),
            new Dependency(13,),
            new Dependency(14,),
            new Dependency(15,),
            new Dependency(16,),
            new Dependency(17,),
            new Dependency(18,),
            new Dependency(19,),
            new Dependency(20,),
            new Dependency(21,),
            new Dependency(22,),
            new Dependency(23,),
            new Dependency(24,),
            new Dependency(25,),
            new Dependency(26,),
            new Dependency(27,),
            new Dependency(28,),
            new Dependency(29,),
            new Dependency(30,),
            new Dependency(31,),
            new Dependency(32,),
            new Dependency(33,),
            new Dependency(34,),
            new Dependency(35,),
            new Dependency(36,),
            new Dependency(37,),
            new Dependency(38,),
            new Dependency(39,),
            new Dependency(40,),
        };
        foreach (var dep in deps)
        {
            s_dalDependency!.Create(dep);
        }
    }

    
}
