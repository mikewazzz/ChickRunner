using System;
using System.Collections.Generic;

namespace Amsterdam.Managers.Extentions
{
    public static class ShuffleListExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        
        public static T RemoveRandomElement<T>(this IList<T> list)
        {
            if (list.Count == 0)
            {
                throw new IndexOutOfRangeException("Cannot remove a random element from an empty list");
            }

            int index = UnityEngine.Random.Range(0, list.Count);
            T item = list[index];
            list.RemoveAt(index);
            return item;
        }

        public static IList<T> CollectAllowedElements<T>(this IList<T> list, IList<T> excludedElements)
        {
            if (list.Count == 0)
            {
                throw new IndexOutOfRangeException("Cannot collect from an empty list");
            }

            List<T> allowedElements = new List<T>();
            foreach (T element in list)
            {
                if (!excludedElements.Contains(element))
                {
                    allowedElements.Add(element);
                }
            }

            return allowedElements;
        }

        public static T RandomElement<T>(this IList<T> list)
        {
            if (list.Count == 0)
            {
                throw new IndexOutOfRangeException("Cannot get a random element from an empty list");
            }

            Random random = new Random();
            int randomIndex = random.Next(list.Count);
            return list[randomIndex];
        }

        public static T RandomElement<T>(this IList<T> list, IList<T> excludedElements)
        {
            if (list.Count == 0)
            {
                throw new IndexOutOfRangeException("Cannot get a random element from an empty list");
            }

            IList<T> allowedElements = CollectAllowedElements(list, excludedElements);
            return RandomElement(allowedElements);
        }

        public static T RandomElement<T>(this IList<T> list, T excludeElement)
        {
            if (list.Count == 0)
            {
                throw new IndexOutOfRangeException("Cannot get a random element from an empty list");
            }

            IList<T> allowedElements = CollectAllowedElements(list, new[] {excludeElement});
            return RandomElement(allowedElements);
        }
    }
}