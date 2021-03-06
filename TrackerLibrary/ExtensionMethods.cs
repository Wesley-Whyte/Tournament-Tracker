using System;
using System.Collections.Generic;
using System.Text;

namespace TrackerLibrary
{
    public static class ExtensionMethods
    {
        private static Random rng = new Random();
        /// <summary>
        /// Randomizes elemens T of a generic Ilist list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this IList<T> list)
        {
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
    }
}
