using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Loki2D.Core.Utilities
{
    public static class RandomHelper
    {
        /// <summary>
        /// Generates a random color
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        public static Color RandomColor(this Random random)
        {
            var r = random.Next(0, 255);
            var g = random.Next(0, 255);
            var b = random.Next(0, 255);

            return new Color(new Vector3(r,g,b));
        }

        /// <summary>
        /// Retrieves a random value from the list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T RandomValue<T>(this Random random, List<T> list)
        {
            var index = random.Next(0, list.Count);
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
        public static List<int> FillRandomInt(this Random random, List<int> list, int values, int minValue = 0, int maxValue = 100)
        {
            for (int i = 0; i < values; i++)
            {
                var newValue = random.Next(minValue, maxValue);
                list.Add(newValue);
            }

            return list;
        }
    }
}
