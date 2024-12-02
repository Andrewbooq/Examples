using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TS.Core.Objects;

namespace ClassLibrary
{
    public class MyDictionary<TKey, UValue> : IEnumerable<KeyValuePair<TKey, UValue>> where TKey : IdentifiedObject
    {
        private LinkedList<KeyValuePair<TKey, UValue>>[] _values;
        private int capacity;
        public MyDictionary()
        {
            _values = new LinkedList<KeyValuePair<TKey, UValue>>[20];
        }
        public MyDictionary(int size)
        {
            _values = new LinkedList<KeyValuePair<TKey, UValue>>[size];
        }
        public int Count => _values.Length;

        public void Add(TKey key, UValue val)
        {
            var hash = GetHashValue(key);
            if (_values[hash] == null)
            {
                _values[hash] = new LinkedList<KeyValuePair<TKey, UValue>>();
            }
            var keyPresent = _values[hash].Any(p => p.Key.Equals(key));
            if (keyPresent)
            {
                throw new Exception("Duplicate key has been found");
            }
            var newValue = new KeyValuePair<TKey, UValue>(key, val);
            _values[hash].AddLast(newValue);
            capacity++;
            if (Count <= capacity)
            {
                //ResizeCollection();
            }
        }

        private void ResizeCollection()
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(TKey key)
        {
            var hash = GetHashValue(key);
            return _values[hash] == null ? false : _values[hash].Any(p => p.Key.Equals(key));
        }

        public UValue GetValue(TKey key)
        {
            var hash = GetHashValue(key);
            return _values[hash] == null ? default(UValue) :
                _values[hash].First(m => m.Key.Equals(key)).Value;
        }

        public IEnumerator<KeyValuePair<TKey, UValue>> GetEnumerator()
        {
            return (from collections in _values
                    where collections != null
                    from item in collections
                    select item).GetEnumerator();
        }

        private int GetHashValue(TKey key)
        {
            return (Math.Abs(key.GetHashCode())) % _values.Length;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public UValue this[TKey key]
        {
            get
            {
                int h = GetHashValue(key);
                if (_values[h] == null) throw new KeyNotFoundException("Keys not found");
                return _values[h].FirstOrDefault(p => p.Key.Equals(key)).Value;
            }
            set
            {
                int h = GetHashValue(key);
                _values[h] = new LinkedList<KeyValuePair<TKey, UValue>>();
                _values[h].AddLast(new KeyValuePair<TKey, UValue>
                                                    (key, value));
            }
        }
    }

    public class MeasureDictionaries
    {
        private const int elementCount = 20;

        private void OutputResults(TimeSpan[] tests1, TimeSpan[] tests2)
        {
            Console.WriteLine("");
            Console.WriteLine("Standard Dictionary, ms | ms My Dictionary");

            TimeSpan average1 = new TimeSpan(0);
            TimeSpan average2 = new TimeSpan(0);
            for (int i = 0; i < elementCount; ++i)
            {
                Console.WriteLine("               {0} | {1}", tests1[i].ToString("\\.fffffff"), tests2[i].ToString("\\.fffffff"));
                average1 += tests1[i];
                average2 += tests2[i];
            }
            Console.WriteLine($"          AVG: {(average1/elementCount).ToString("\\.fffffff")} | {(average2/elementCount).ToString("\\.fffffff")} :AVG");
        }

        public void TestDictionary()
        {
            Console.WriteLine($"Dictionary, capacity={elementCount}");
            
            // Standard Dictionary
            var dictionary1 = new Dictionary<Identifier, string>(elementCount);

            for (int i = 0; i < elementCount; ++i)
            {
                Identifier key = new Identifier(i);
                dictionary1.Add(key, $"some string {i}");
            }

            TimeSpan[] tests1 = new TimeSpan[elementCount];
            string[] strings1 = new string[elementCount];

            for (int i = 0; i < elementCount; ++i)
            {
                Identifier key = new Identifier(i);
                Stopwatch stopWatch = Stopwatch.StartNew();
                strings1[i] = dictionary1[key];
                tests1[i] = stopWatch.Elapsed;
            }

            // My implementation of Dictionary
            var dictionary2 = new MyDictionary<Identifier, string>(elementCount);

            for (int i = 0; i < elementCount; ++i)
            {
                Identifier key = new Identifier(i);
                dictionary2.Add(key, $"some string {i}");
            }

            TimeSpan[] tests2 = new TimeSpan[elementCount];
            string[] strings2 = new string[elementCount];

            for (int i = 0; i < elementCount; ++i)
            {
                Identifier key = new Identifier(i);
                Stopwatch stopWatch = Stopwatch.StartNew();
                strings2[i] = dictionary2[key];
                tests2[i] = stopWatch.Elapsed;
            }

            OutputResults(tests1, tests2);
        }
    }
}
