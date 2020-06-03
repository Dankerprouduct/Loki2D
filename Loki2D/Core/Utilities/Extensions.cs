using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
