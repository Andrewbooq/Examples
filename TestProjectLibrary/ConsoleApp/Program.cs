using System;
using ClassLibrary;
using System.Threading;
namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Test output");
            Person tom = new Person();
            //tom.Print();
            tom.UseDictionary();
            tom.GetHashCode();

            int milliseconds = 2000;
            Thread.Sleep(milliseconds);
        }
    }
}
