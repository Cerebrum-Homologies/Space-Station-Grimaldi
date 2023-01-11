using Godot;
using System;
using SpaceStation.Framework;

namespace SpaceStation
{
	public partial class GeoMorphs : Node3D
	{
		private CSGCombiner3D constructModelCombiner3D;
		private CSGBox3D constructModelBox3D;
		private CSGSphere3D constructModelSphere3D;
		
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			constructModelCombiner3D = GetNodeOrNull<CSGCombiner3D>("CSGCombiner3D");
			Diagnostics.PrintNullValueMessage(constructModelCombiner3D, "");
			if (constructModelCombiner3D != null)
			{
				constructModelBox3D = constructModelCombiner3D.GetNodeOrNull<CSGBox3D>("CSGCombiner3D");
				constructModelSphere3D = constructModelCombiner3D.GetNodeOrNull<CSGSphere3D>("CSGSphere3D");
			}
		}

		public StandardMaterial3D CreateStandardMaterial(bool genMetal, Godot.Collections.Array<Color> colorPalette)
		{
			StandardMaterial3D result = null;
			return result;
		}

		public ORMMaterial3D CreateOrmMaterial()
		{
			ORMMaterial3D result = null;
			return result;
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}
	}
}
