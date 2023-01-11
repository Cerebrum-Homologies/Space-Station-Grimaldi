using Godot;
using System;
using System.Collections.Generic;
using SpaceStation.Util;

namespace SpaceStation.Effects
{
	public partial class ClusterParticles : Node2D
	{
		private CharacterBody2D kineCluster1;
		private CollisionShape2D shapecluster1;
		private GPUParticles2D particlesCluster1;
		private CharacterBody2D kineCluster2;
		private CollisionShape2D shapecluster2;
		private GPUParticles2D particlesCluster2;
		private CharacterBody2D kineCluster3;
		private CollisionShape2D shapecluster3;
		private GPUParticles2D particlesCluster3;
		private Node2D influenceZone;
		private Area2D influenceZoneArea;
		private bool outlineInfluenceZone = false;
		private Dictionary<String, bool> debugBoundBoxes;
		private bool symmetricalPlacement;
		private int numberOfSourcePoints;
		private Dictionary<String, ImageTexture> mapClusterParticleTextures;
		private String objectName;

		public ClusterParticles()
		{
			objectName = StringMagic.RandomName(8, 16);
			Name = objectName;
			debugBoundBoxes = new Dictionary<string, bool>();
			mapClusterParticleTextures = new Dictionary<string, ImageTexture>();
		}

		public void AddClusterTexture(string tag, ImageTexture myTexture)
		{
			if (mapClusterParticleTextures != null)
				mapClusterParticleTextures.Add(tag, myTexture);
		}

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{			
			kineCluster1 = GetNodeOrNull<CharacterBody2D>("Kinematic-Cluster1");
			if (kineCluster1 != null)
			{
				shapecluster1 = kineCluster1.GetNodeOrNull<CollisionShape2D>("CollisionShape3D-Cluster1");
				particlesCluster1 = kineCluster1.GetNodeOrNull<GPUParticles2D>("GPUParticles3D-Cluster1");
			}
			kineCluster2 = GetNodeOrNull<CharacterBody2D>("Kinematic-Cluster2");
			if (kineCluster2 != null)
			{
				shapecluster2 = kineCluster2.GetNodeOrNull<CollisionShape2D>("CollisionShape3D-Cluster2");
				particlesCluster2 = kineCluster2.GetNodeOrNull<GPUParticles2D>("GPUParticles3D-Cluster2");
			}
			kineCluster3 = GetNodeOrNull<CharacterBody2D>("Kinematic-Cluster3");
			if (kineCluster3 != null)
			{
				shapecluster3 = kineCluster2.GetNodeOrNull<CollisionShape2D>("CollisionShape3D-Cluster3");
				particlesCluster3 = kineCluster2.GetNodeOrNull<GPUParticles2D>("GPUParticles3D-Cluster3");
			}
			influenceZone = GetNodeOrNull<Area2D>("InfluenceZone");
			if (influenceZone != null)
			{
				influenceZoneArea = kineCluster2.GetNodeOrNull<Area2D>("Area3D-InfluenceZone");
			}
		}

		public void SetSourcePoints(int sourceCount, bool symmetrical)
		{
			numberOfSourcePoints = sourceCount;
			symmetricalPlacement = symmetrical;
			GD.Print($"SetSourcePoints sourceCount={sourceCount}, symmetrical={symmetrical}");
			for (int idx = 1; idx < numberOfSourcePoints; idx++)
			{

			}
		}

		public override void _Input(InputEvent @event)
		{
			base._Input(@event);
			if (@event.IsPressed())
			{

			}
		}

		public void SetVisible(string tag, bool visible)
		{
			if (String.IsNullOrEmpty(tag) == false)
			{
				switch (tag)
				{
					case "all":
						kineCluster1.Visible = visible;
						kineCluster2.Visible = visible;
						kineCluster3.Visible = visible;
						break;
					case "cluster1":
						kineCluster1.Visible = visible;
						break;
					case "cluster2":
						kineCluster2.Visible = visible;
						break;
					case "cluster3":
						kineCluster3.Visible = visible;
						break;
					default:
						break;
				}
			}
		}

		public CharacterBody2D GetParticleSet(string tag) {
			CharacterBody2D res = null;
			switch (tag)
				{
					case "all":
						res = kineCluster1;
						break;
					case "cluster1":
						res = kineCluster1;
						break;
					case "cluster2":
						res = kineCluster2;
						break;
					case "cluster3":
						res = kineCluster3;
						break;
					default:
						break;
				}
			return res;
		}

		public void SetManualPlacePosition(string tag, Vector2 placePosition)
		{
			//if ((Set >= 1) && (Set <= numberOfSourcePoints))
			//{
				string strKey = $"set{Set}";
				CharacterBody2D particleSetCurrent = GetParticleSet(tag);
				//GD.Print($"SetManualPlacePosition, strKey={strKey}, particleSetCurrent={particleSetCurrent}");
				if (particleSetCurrent != null)
				{
					particleSetCurrent.Position = placePosition;
				}
			//}
		}

		public void DrawInfluenceOutline()
		{

		}

		public override void _Draw()
		{
			base._Draw();
			//Draw the Influence (box) area if outline toggle is on
			if (outlineInfluenceZone)
			{
				DrawInfluenceOutline();
			}
		}

		public void PlaceClusters(Vector2 cluster1, Vector2 cluster2, Vector2 influence)
		{
			kineCluster1.Position = cluster1;
			kineCluster2.Position = cluster2;
			influenceZone.Position = influence;
		}

	}
}
