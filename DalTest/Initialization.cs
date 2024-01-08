
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
        // Noam
        // אתה מוזמן לממש




        Task newTask = new Task(0);
        s_dalTask!.Create(newTask);
    }


    private static void createDependency()
    {


        // אתה מוזמן לממש



        Dependency newTask = new Dependency(0);
        s_dalDependency!.Create(newTask);
    }

    
}
