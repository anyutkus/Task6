using System.Collections.Concurrent;

namespace Task6._1
{
    public class Cache
    {
        private readonly int _capacity;
        private ConcurrentDictionary<string, (object, TimeSpan)> _elements = new();
        private bool IsCacheFull
        {
            get => _capacity - _elements.Count == 0;
        }
        private bool isThreadRunning = false;
        private Thread thread1;

        public Cache(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), "must be greater than 0");
            }
            _capacity = capacity;
            thread1 = new Thread(Scan);
        }

        public void Set(string key, object obj)
        {
            InputCheck(key, obj);

            if (IsCacheFull)
            {
                Remove(GetOldValues().First().key);
            }

            AddOrUpdate(key, obj);

            if (!isThreadRunning)
            {
                thread1.Start();
                isThreadRunning = true;
            }
        }

        private void AddOrUpdate(string key, object obj)
        {
            _elements.AddOrUpdate(key, (obj, DateTime.Now.TimeOfDay), (k, v) => (obj, DateTime.Now.TimeOfDay));
        }

        private static void InputCheck(string key)
        {
            if (key == "" || key == null)
            {
                throw new ArgumentException(nameof(key), "must be not empty and not null");
            }
        }

        private static void InputCheck(string key, object obj)
        {
            InputCheck(key);
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj), "must be not null");
            }
        }

        public object? Get(string key)
        {
            InputCheck(key);
            if (_elements.ContainsKey(key))
            {
                AddOrUpdate(key, _elements[key].Item1);
                return _elements[key].Item1;
            }
            else
            {
                return null;
            }
        }

        public bool Remove(string key)
        {
            return _elements.TryRemove(key, out _);
        }

        private void Scan()
        {
            while (_elements.Count > 0)
            {
                foreach (var (key, time) in GetOldValues())
                {
                    if ((DateTime.Now.TimeOfDay - time).Seconds > 10)
                    {
                        Remove(key);
                    }
                }
                Thread.Sleep(1000);
            }
        }

        public IEnumerable<(string key, TimeSpan time)> GetOldValues()
        {
            return _elements.OrderBy(x => x.Value.Item2).Select(x => (x.Key, x.Value.Item2));
        }

        public override string ToString()
        {
            string str = "";
            foreach (var element in _elements)
            {
                str += element.Key + " " + element.Value.ToString() + "\n";
            }
            return str;
        }
    }
}