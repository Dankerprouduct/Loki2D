using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.Utilities.MathHelper;
using SharpDX;
using Color = Microsoft.Xna.Framework.Color;
using Vector3 = Microsoft.Xna.Framework.Vector3;

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
            var r = (float)random.Next(0, 255);
            var g = (float)random.Next(0, 255);
            var b = (float)random.Next(0, 255);

            return new Color(new Vector3(r/255,g/255,b/255));
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
        /// Retrieves a random value from the list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T RandomValue<T>(this IList<T> collection)
        {
            var index = MathUtils.Random.Next(0, collection.Count);
            return collection[index];
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
