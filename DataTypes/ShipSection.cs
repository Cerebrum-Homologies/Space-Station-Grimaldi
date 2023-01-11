using System;

namespace SpaceStation.DataTypes
{
    public partial class ShipSection<T>
    {
        private string sectionDescription { get; set; }
        T shipParent;

        public void SetParent(T aShip)
        {
            shipParent = aShip;
        }
    }
}