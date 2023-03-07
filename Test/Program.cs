using System;
using NeuroDB_DotNet_Driver;
namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            NeuroDBDriver driver = new NeuroDBDriver("124.223.0.109", 8839);
            ResultSet resultSet = driver.executeQuery("match (n) return n limit 2");
            Console.WriteLine("Hello World!");
        }
    }
}
