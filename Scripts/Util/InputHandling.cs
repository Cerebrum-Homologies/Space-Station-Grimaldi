using Godot;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace SpaceStation.Util
{
    public class MousePositionRecord
    {
        public int EventIndex { get; set;}
        public Vector2 Position { get; set;}
        public DateTime Recorded { get; set;}
        public MousePositionRecord(int index, Vector2 pos, DateTime markTime)
        {
            EventIndex = index;
            Position = pos;
            Recorded = markTime;
        }
    }

    public partial class InputHandling : Node2D
    {
        private static Dictionary<string, DateTime> keyBounceMap = new Dictionary<string, DateTime>();
        private static List<MousePositionRecord> mousePositionsTracker = new List<MousePositionRecord>();

        public static bool KeyBounceCheck(string key, float secondsIgnore)
        {
            bool res = true;
            if ((key != null) && (secondsIgnore >= 0.1))
            {
                if (keyBounceMap.ContainsKey(key))
                {
                    DateTime keyTimestamp = keyBounceMap[key];
                    var diff = DateTime.Now - keyTimestamp;
                    GD.Print($"KeyBounceCheck <key>={key}, milliseconds diff = {diff.Milliseconds}");
                    if (diff.Milliseconds >= (secondsIgnore * 1000.0f))
                    {
                        //if (diff.Milliseconds >= (/*2 * */secondsIgnore * 1000.0f))
                        //    keyBounceMap.Remove(key);
                        keyBounceMap[key] = DateTime.Now;
                        res = false;
                    }
                }
                else
                {
                    keyBounceMap[key] = DateTime.Now;
                    //keyBounceMap.Add(key, DateTime.Now);
                    res = false;
                }
            }
            GD.Print($"KeyBounceCheck <key>={key}, diff >= {secondsIgnore * 1000.0f} is [{res}]");
            return res;
        }

        public static bool AddMousePosition(int evtIndex, Vector2 evtPosition)
        {
            bool res = false;
            if (mousePositionsTracker == null)
                return res;
            bool updateMousePosition = false;
            if ((mousePositionsTracker.Count > 0))
            {
                foreach (MousePositionRecord mousePos in mousePositionsTracker)
                {
                    Vector2 diffVec = VectorUtils.Subtract(mousePos.Position, evtPosition);
                    double vecDistance = VectorUtils.Distance(diffVec);
                    GD.Print($"InputHandling - AddMousePosition(), difference={diffVec}, distance={vecDistance}");
                    if ((vecDistance >= 10.0f) || ((evtIndex - mousePos.EventIndex) >= 0))
                    {
                        updateMousePosition = true;
                        break;
                    }
                }
            }
            else
                updateMousePosition = true;
            if (updateMousePosition)
            {
                mousePositionsTracker.Add(new MousePositionRecord(evtIndex, evtPosition, DateTime.Now));
                res = true;
            }
            return res;
        }
    }

}