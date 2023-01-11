using Godot;
using System;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace SpaceStation.Util
{
    public class VectorUtils
    {
        public static Vector2 Subtract(Vector2 first, Vector2 second)
        {
            Vector2 res = Vector2.Zero;
            res = new Vector2(first.x - second.x, first.y - second.y);
            return res;
        }

        public static double Distance(Vector2 thisVector) {
            float res = 0.0f;
            res = Mathf.Sqrt((thisVector.x * thisVector.x) + (thisVector.y * thisVector.y));
            return res;
        }

        public static double Distance(Vector2 thisVector, Vector2 anotherVector) {
            float res = 0.0f;
            float xD = thisVector.x  - anotherVector.x;
            float yD = thisVector.y  - anotherVector.y;
            //float xD = thisVector.x  - anotherVector.x;
            res = Mathf.Sqrt((xD * xD) + (yD * yD));
            return res;
        }

        public static double Distance(Vector3 thisVector) {
            float res = 0.0f;
            res = Mathf.Sqrt((thisVector.x * thisVector.x) + (thisVector.y * thisVector.y) + (thisVector.z * thisVector.z));
            return res;
        }
    }
}