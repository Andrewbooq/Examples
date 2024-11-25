using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary
{
    class MyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {

    }
    public class Person
    {
        public string name = "Undefined";   // имя
        public int age = 0;                 // возраст

        public void Print()
        {
            Console.WriteLine($"Имя: {name}  Возраст: {age}");
        }

        public void UseDictionary()
        {
            var dict = new Dictionary<string, int>();

            var prodPrice = new Dictionary<string, double>()
            {
                ["bread"] = 23.3,
                ["apple"] = 45.2
            };
            Console.WriteLine($"bread price: {prodPrice["bread"]}");
            Console.WriteLine("Свойства");
            Console.WriteLine($"Count: {prodPrice.Count}");
        }

    }
}
