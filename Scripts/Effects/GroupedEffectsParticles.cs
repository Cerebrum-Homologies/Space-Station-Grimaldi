using Godot;
using System;
using System.Collections.Generic;
using SpaceStation.Enums;

namespace SpaceStation.Effects
{
	public partial class GroupedEffectsParticles : Node2D
	{
		private bool symmetricalPlacement;
		//private int numberOfSourcePoints;
		//private float degreesPerSecond = 0.0f;
		private Dictionary<String, String> mapGroupedParticles; 
		private Dictionary<String, ImageTexture> mapClusterParticleTextures;
		private Dictionary<String, bool> mapActiveSets;
		private Dictionary<String, String> mapParticleImageResources;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			mapGroupedParticles = new Dictionary<string, string>();
			mapClusterParticleTextures = new Dictionary<string, ImageTexture>();
			mapActiveSets = new Dictionary<string, bool>();
			mapParticleImageResources = new Dictionary<string, string>();
		}

		public void AddParticleGroupDefaultConfig(string group, int count, PlacementSelection placement)
		{
			if (String.IsNullOrEmpty(group))
				return;
			for (int idx = 0; idx < count; idx++)
			{
				//Generate unique ID for the particles node
				//mapGroupedParticles
			}
			//mapGroupedParticles.Add(group, strlist);
		}

		public void AddParticleGroup(string group, string[] idList, PlacementSelection placement, Vector2[] placeCoords)
		{
			if ((String.IsNullOrEmpty(group)) || (idList == null))
				return;
		}

		public void AddTextureResource(string resourceTag, string imageResource)
		{
			if (mapParticleImageResources == null)
				return;
			mapParticleImageResources.Add(resourceTag, imageResource);
		}

		//  // Called every frame. 'delta' is the elapsed time since the previous frame.
		//  public override void _Process(float delta)
		//  {
		//      
		//  }
	}
}
