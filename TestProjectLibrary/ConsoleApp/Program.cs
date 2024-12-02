using System;
using ClassLibrary;
using System.Threading;
namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Measurement of access to element");
            MeasureDictionaries measurement = new MeasureDictionaries();

            measurement.TestDictionary();

        }
    }
}
