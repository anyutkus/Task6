using System;
using System.Collections.Concurrent;

namespace Task6._1
{
    public class Cache
    {
        private readonly int _capacity;
        private static Dictionary<string, (object, TimeSpan)> _elements = new();
        private bool IsCacheFull
        {
            get => _capacity - _elements.Count == 0;
        }
        private Timer _timer;

        public Cache(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), "must be greater than 0");
            }
            _capacity = capacity;
            _timer = new Timer(state => Scan(), null, 3000, 1000);
        }

        public void Set(string key, object obj)
        {
            InputCheck(key, obj);

            if (IsCacheFull)
            {
                Remove(GetOldValues().First().key);
            }
            lock(_elements)
            {
                if (_elements.ContainsKey(key))
                {
                    _elements[key] = (obj, DateTime.Now.TimeOfDay);
                }
                else
                {
                    _elements.Add(key, (obj, DateTime.Now.TimeOfDay));
                }
            }
        }

        private static void InputCheck(string key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentException("must be not empty and not null", nameof(key));
            }
        }

        private static void InputCheck(string key, object obj)
        {
            InputCheck(key);
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
        }

        public object? Get(string key)
        {
            InputCheck(key);
            if (_elements.ContainsKey(key))
            {
                lock(_elements)
                {
                    _elements[key] = (_elements[key].Item1, DateTime.Now.TimeOfDay);
                }
                return _elements[key].Item1;
            }
            else
            {
                return null;
            }
        }

        public bool Remove(string key)
        {
            lock(_elements)
            {
                return _elements.Remove(key, out _);
            }
        }

        private void Scan()
        {
            if (_elements.Count > 0)
            {
                lock(_elements)
                {
                    foreach (var (key, time) in GetOldValues())
                    {
                        if ((DateTime.Now.TimeOfDay - time).Seconds > 10)
                        {
                            Remove(key);
                        }
                    }
                }
            }
            else
            {
                _timer.Dispose();
            }
        }

        private IEnumerable<(string key, TimeSpan time)> GetOldValues()
        {
            return _elements.OrderBy(x => x.Value.Item2).Select(x => (x.Key, x.Value.Item2));
        }

        public override string ToString()
        {
            string str = "";
            lock(_elements)
            {
                foreach (var element in _elements)
                {
                    str += element.Key + " " + element.Value.ToString() + "\n";
                }
            }
            return str;
        }
    }
}