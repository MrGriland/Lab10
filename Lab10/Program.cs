using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Lab10
{
    public class Good: IOrderedDictionary
    {
        private int cost;
        public int Cost
        {
            get
            {
                return cost;
            }
            set
            {
                cost = value;
            }
        }
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public bool IsFixedSize => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public ICollection Keys => throw new NotImplementedException();

        public ICollection Values => throw new NotImplementedException();

        public int Count => throw new NotImplementedException();

        public bool IsSynchronized => throw new NotImplementedException();

        public object SyncRoot => throw new NotImplementedException();

        public object this[object key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public object this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Good()
        {
            cost = 300;
            name = "default";
        }
        public Good(int cost, string name)
        {
            this.cost = cost;
            this.name = name;
        }

        public IDictionaryEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, object key, object value)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void Add(object key, object value)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(object key)
        {
            throw new NotImplementedException();
        }

        public void Remove(object key)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentBag<Good> goods = new ConcurrentBag<Good>();
            goods.Add(new Good());
            goods.Add(new Good(500, "bucket"));
            goods.Add(new Good(666, "Leather"));
            goods.Add(new Good(100, "pants"));
            Good good1;
            goods.TryTake(out good1);
            foreach (Good good in goods)
            {
                Console.WriteLine($"{good.Cost} {good.Name}");
            }
            Console.WriteLine("Введите стоимость: ");
            int x = Convert.ToInt32(Console.ReadLine());
            foreach (Good good in goods)
            {
                if (good.Cost == x)
                    Console.WriteLine($"{good.Cost} {good.Name}");
            }
            goods.Clear();

            Dictionary<int, string> words = new Dictionary<int, string>(3);
            words.Add(1, "Hello");
            words.Add(2, "Gym");
            words.Add(3, "Boss");
            for (int i = 1; i <= words.Count; i++)
            {
                Console.WriteLine(words[i]);
            }
            Console.Write($"Введите число элементов, которое вы хотите удалить с конца: ");
            int n = Int32.Parse(Console.ReadLine());
            int f = words.Count - n;
            for (int i = words.Count; i > f; i--)
            {
                Console.WriteLine($"{words[i]} удалён");
                words.Remove(i);
            }
            Console.WriteLine($"Оставшиеся элементы: ");
            for (int i = 1; i <= words.Count; i++)
            {
                Console.WriteLine(words[i]);
            }
            HashSet<string> wordsHash = new HashSet<string>(words.Count);
            for (int i = 1; i <= words.Count; i++)
            {
                wordsHash.Add(words[i]);
            }
            if (wordsHash.Contains("Gym")) Console.WriteLine($"wordsHash содержит элемент: \"Gym\"");


            ObservableCollection<Good> goods1 = new ObservableCollection<Good>()
            {
            new Good { Cost = 3000, Name = "Reaver"},
            new Good { Cost = 2225, Name = "Blink Dagger"},
            new Good { Cost = 4000, Name = "Lotus Orb"}
            };

            goods1.CollectionChanged += Goods_CollectionChanged;

            goods1.Add(new Good { Cost = 875, Name = "Quaterstaff" });
            goods1.RemoveAt(1);
            goods1[0] = new Good { Cost = 100, Name = "Sentry" };

            foreach (Good goodd in goods1)
            {
                Console.WriteLine(goodd.Name, goodd.Cost);
            }

            Console.Read();

        }
        private static void Goods_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add: // если добавление
                    Good newGood = e.NewItems[0] as Good;
                    Console.WriteLine($"Добавлен новый объект: {newGood.Name}");
                    break;
                case NotifyCollectionChangedAction.Remove: // если удаление
                    Good oldGood = e.OldItems[0] as Good;
                    Console.WriteLine($"Удален объект: {oldGood.Name}");
                    break;
                case NotifyCollectionChangedAction.Replace: // если замена
                    Good replacedGood = e.OldItems[0] as Good;
                    Good replacingGood = e.NewItems[0] as Good;
                    Console.WriteLine($"Объект {replacedGood.Name} заменен объектом {replacingGood.Name}");
                    break;
            }
        }
    }
}
