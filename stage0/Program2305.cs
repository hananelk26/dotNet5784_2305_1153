using System;
namespace targil0
{ 
    partial class Program
    {
        private static void Main(string[] args)
        {
            Welcome2305();
            Welcome1153();
        }

        static partial void Welcome1153();
        private static void Welcome2305()
        {
            Console.WriteLine("Enter your name: ");
            string userName = Console.ReadLine();
            Console.WriteLine(userName + ", welcome to my first console application");
        }
    }
}