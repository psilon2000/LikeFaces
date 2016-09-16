using System;
using System.Collections.Generic;

namespace LikeApp
{
    public static class ShuffleList
    {
        private static readonly Random Rnd = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

    }
}