using System;
using NeuroDB_DotNet_Driver;
namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            NeuroDBDriver driver = new NeuroDBDriver("127.0.0.1", 8839);
            ResultSet resultSet = driver.executeQuery("match (n) return n ");
            Console.WriteLine("Hello World!");
        }
    }
}
