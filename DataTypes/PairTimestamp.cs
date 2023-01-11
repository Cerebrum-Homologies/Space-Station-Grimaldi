using System;

namespace SpaceStation.DataTypes
{
    public partial class PairTimestamp<T>
    {
        DateTime time;
        T pairX;

        public PairTimestamp(DateTime aTime, T attachedPair)
        {
            time = aTime;
            pairX = attachedPair;
        }

        public PairTimestamp(T attachedPair)
        {
            time = DateTime.Now;
            pairX = attachedPair;
        }
    }
}
