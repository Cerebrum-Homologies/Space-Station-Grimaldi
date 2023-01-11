using System;
using System.Collections.Generic;
using SpaceStation.DataTypes.Base;

namespace SpaceStation.DataTypes.SystemModels
{
	public partial class EnergyComponent
	{
		private String Group { get; set; }
		private Dictionary<string, FloatRange> tagRecords;

		public EnergyComponent()
		{
			tagRecords = new Dictionary<string, FloatRange>();
		}

		public void AddTagRecord(string tagKey, FloatRange tagValue)
		{
			if (tagRecords != null)
				tagRecords.Add(tagKey, tagValue);
		}

		public FloatRange GetTagRecord(String tagKey)
		{
			FloatRange result = null;
			if (tagKey != null)
			{
				if (tagRecords.ContainsKey(tagKey))
					result = tagRecords[tagKey];
			}
			return result;
		}
	}
}
