using System;
using Microsoft.Xna.Framework;

namespace Loki2D.Core.Utilities.MathHelper
{
    public static class MathUtils
    {
        /// <summary>
        /// RNG Generator
        /// </summary>
        public static Random Random = new Random();

        public static T Clamp<T>(T val, T min, T max) where T: IComparable<T>
        {
            if (val.CompareTo(min) < 0)
            {
                return min; 
            }
            else if (val.CompareTo(max) > 0)
            {
                return max;
            }
            else
            {
                return val;
            }
        }

        public static float Normalize(float val, float min, float max)
        {
            var result = val;
            result = (val - min) / (max - min);
            return result;
        }

        public static float Lerp(float from, float to, float step)
        {
            return from + step * (to - from);
        }

        public static Vector2 Slerp(Vector2 from, Vector2 to, float step)
        {
            if (step == 0) return from;
            if (from == to || step == 1) return to;

            var theta = System.Math.Acos(Vector2.Dot(from, to));
            if (theta == 0) return to;

            var sinTheta = System.Math.Sin(theta);
            return (float)(System.Math.Sin((1 - step) * theta) / sinTheta) * from + (float)(System.Math.Sin(step * theta) / sinTheta) * to;
        }

        public static float WrapAngle(float from, float to, float step)
        {
            if (step == 0)
            {
                return from;
            }

            if (from == to || step == 1) return to;

            var fromVector = new Vector2(
                (float)System.Math.Cos(from), 
                (float)System.Math.Sin(from));

            var toVector = new Vector2(
                (float)System.Math.Cos(to),
                (float)System.Math.Sin(to));

            var slerp = Slerp(fromVector, toVector, step);

            return (float) System.Math.Atan2(slerp.Y, slerp.X);
        }

        public static Vector2 RotationToVector2(float rotation)
        {
            return new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
        }

        public static float RotationFromVector2(Vector2 v1, Vector2 v2)
        {
            Vector2 direction = v1 - v2;
            direction.Normalize();

            float angle = (float)Math.Atan2(direction.Y, direction.X);
            return angle;
        }
    }
}
