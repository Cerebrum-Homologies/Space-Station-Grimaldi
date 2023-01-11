using System;

namespace SpaceStation.DataTypes.Base
{
	public partial class FloatRange
	{
		private float value1, value2;

		public FloatRange(float _value1, float _value2)
		{
			value1 = _value1;
			value2 = _value2;
		}

		public bool IsInRange(float value)
		{
			bool res = false;
			return res;
		}
	}
}
