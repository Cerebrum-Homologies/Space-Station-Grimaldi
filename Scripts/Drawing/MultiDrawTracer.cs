using Godot;
using System;
using System.Collections.Generic;

namespace SpaceStation.Drawing
{
	public partial class MultiDrawTracer : Node2D
	{
		private List<String> groupsList;
		//private Dictionary<String, DrawList> GroupDrawMap;

		void AddGroupImage(string group, string id, string resImage)
		{
			if (!String.IsNullOrEmpty(group) && !String.IsNullOrEmpty(id))
			{

			}
		}

		void SetLifetime(string id, int milliSeconds)
		{
			if (!String.IsNullOrEmpty(id))
			{

			}
		}

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			groupsList = new List<string>();

		}

		//  // Called every frame. 'delta' is the elapsed time since the previous frame.
		//  public override void _Process(float delta)
		//  {
		//      
		//  }
	}
}
