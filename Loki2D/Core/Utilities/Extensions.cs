using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.Utilities.Math;

namespace Loki2D.Core.Utilities
{
    public static class Extensions
    {
        /// <summary>
        /// Retrieves a random value from the list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T RandomValue<T>(this List<T> list)
        {
            var index = MathUtils.Random.Next(0, list.Count);
            return list[index];
        }

        /// <summary>
        /// Fills a list with random integer values
        /// </summary>
        /// <param name="list">the list to be modified</param>
        /// <param name="values">the amount of of values to be added</param>
        /// <param name="minValue">The min value to be added</param>
        /// <param name="maxValue">The max value to be added</param>
        /// <returns></returns>
        public static List<int> FillRandomInt(this List<int> list, int values, int minValue = 0, int maxValue = 100)
        {
            for (int i = 0; i < values; i++)
            {
                var newValue = MathUtils.Random.Next(minValue, maxValue);
                list.Add(newValue);
            }

            return list;
        }
    }
}
