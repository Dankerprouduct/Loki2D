using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.Utilities.MathHelper;
using Microsoft.Xna.Framework;

namespace Loki2D.Core.Utilities
{
    public static class Extensions
    {
        

        /// <summary>
        /// Returns the half vector
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector2 Half(this Vector2 vector)
        {
            var x = vector.X / 2;
            var y = vector.Y / 2;
            return new Vector2(x,y);
        }

        /// <summary>
        /// Returns the half point
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Point Half(this Point point)
        {
            return (point.ToVector2()).ToPoint();
        }

        
    }
}
