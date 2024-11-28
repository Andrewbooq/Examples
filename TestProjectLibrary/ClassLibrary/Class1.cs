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
            _values = new LinkedList<KeyValuePair<TKey, UValue>>[15];
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

        public void UseStandardDictionary()
        {
            Console.WriteLine($"Dictionary, capacity={elementCount}");
            
            var dictionary = new Dictionary<Identifier, string>(elementCount);

            for (int i = 0; i < elementCount; ++i)
            {
                Identifier key = new Identifier(i);
                dictionary.Add(key, $"some string {i}");
            }

            string value1;
            Identifier key1 = new Identifier(2);
            Stopwatch stopWatch1 = Stopwatch.StartNew();
            value1 = dictionary[key1];
            Console.WriteLine($"Time to get element at the beginig {stopWatch1.Elapsed}");
            Console.WriteLine($"value={value1}");

            string value2;
            Identifier key2 = new Identifier(13);
            Stopwatch stopWatch2 = Stopwatch.StartNew();
            value2 = dictionary[key2];
            Console.WriteLine($"Time to get element in the middle {stopWatch2.Elapsed}");
            Console.WriteLine($"value={value2}");

            string value3;
            Identifier key3 = new Identifier(19);
            Stopwatch stopWatch3 = Stopwatch.StartNew();
            value3 = dictionary[key3];
            Console.WriteLine($"Time to get element at the end {stopWatch3.Elapsed}");
            Console.WriteLine($"value={value3}");
        }

        public void UseMyDictionary()
        {
            Console.WriteLine($"MyDictionary, capacity={elementCount}");

            var dictionary = new MyDictionary<Identifier, string>();

            for (int i = 0; i < elementCount; ++i)
            {
                Identifier key = new Identifier(i);
                dictionary.Add(key, $"some string {i}");
            }

            string value1;
            Identifier key1 = new Identifier(2);
            Stopwatch stopWatch1 = Stopwatch.StartNew();
            value1 = dictionary[key1];
            Console.WriteLine($"Time to get element at the beginig {stopWatch1.Elapsed}");
            Console.WriteLine($"value={value1}");

            string value2;
            Identifier key2 = new Identifier(13);
            Stopwatch stopWatch2 = Stopwatch.StartNew();
            value2 = dictionary[key2];
            Console.WriteLine($"Time to get element in the middle {stopWatch2.Elapsed}");
            Console.WriteLine($"value={value2}");

            string value3;
            Identifier key3 = new Identifier(19);
            Stopwatch stopWatch3 = Stopwatch.StartNew();
            value3 = dictionary[key3];
            Console.WriteLine($"Time to get element at the end {stopWatch3.Elapsed}");
            Console.WriteLine($"value={value3}");
        }
    }
}
