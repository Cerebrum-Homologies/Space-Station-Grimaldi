using Godot;
using System;
using System.Collections.Generic;
using SpaceStation.Enums;
using SpaceStation.Util;

namespace SpaceStation.Effects
{
	public partial class LinearFountainParticles : Node2D
	{
		private GPUParticles2D particleSet1;
		private GPUParticles2D particleSet2;
		private GPUParticles2D particleSet3;
		private GPUParticles2D particleSet4;
		private bool symmetricalPlacement;
		private int numberOfSourcePoints;
		private float degreesPerSecond = 0.0f;
		//private Dictionary<String, String> mapGroupedParticles(); 
		private Dictionary<String, ImageTexture> mapClusterParticleTextures;
		private Dictionary<String, bool> mapActiveSets;
		private Dictionary<String, String> mapParticleImageResources;
		private String objectName;
		private bool isInternalResourceTexture = true;
		//private StringMagic strMagicApparatus;

		private void LoadPresetResources()
		{
			mapParticleImageResources.Add("light-rain1", "res://Assets/Images/FX/64x64/rain_particles_light1.png");
			mapParticleImageResources.Add("light-rain2", "res://Assets/Images/FX/64x64/rain_particles_light2.png");
			mapParticleImageResources.Add("heavy-rain1", "res://Assets/Images/FX/64x64/rain_particles_heavy1.png");
			mapParticleImageResources.Add("heavy-rain2", "res://Assets/Images/FX/64x64/rain_particles_heavy2.png");
			mapParticleImageResources.Add("feint-smoke1", "res://Assets/Images/FX/64x64/smoke_particles_feint1.png");
			mapParticleImageResources.Add("feint-smoke2", "res://Assets/Images/FX/64x64/smoke_particles_feint2.png");
			mapParticleImageResources.Add("feint-smoke3", "res://Assets/Images/FX/64x64/smoke_particles_feint3.png");
			mapParticleImageResources.Add("medium-smoke1", "res://Assets/Images/FX/64x64/smoke_particles_medium1.png");
			mapParticleImageResources.Add("medium-smoke2", "res://Assets/Images/FX/64x64/smoke_particles_medium2.png");
			mapParticleImageResources.Add("kindle-fire1", "res://Assets/Images/FX/64x64/fire_particles_lick1.png");
			mapParticleImageResources.Add("kindle-fire2", "res://Assets/Images/FX/64x64/fire_particles_lick2.png");
			mapParticleImageResources.Add("kindle-fire3", "res://Assets/Images/FX/64x64/fire_particles_lick3.png");
			mapParticleImageResources.Add("kindle-fire4", "res://Assets/Images/FX/64x64/fire_particles_lick4.png");
		}

		public LinearFountainParticles()
		{
			//strMagicApparatus = new StringMagic();
			objectName = StringMagic.RandomName(8, 16);
			Name = objectName;
			mapClusterParticleTextures = new Dictionary<string, ImageTexture>();
			mapActiveSets = new Dictionary<string, bool>();
			mapParticleImageResources = new Dictionary<string, string>();
			LoadPresetResources();
		}

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			GD.Print($"{objectName} :: _Ready() called");
			particleSet1 = GetNodeOrNull<GPUParticles2D>("GPUParticles2D-1");
			particleSet2 = GetNodeOrNull<GPUParticles2D>("GPUParticles2D-2");
			particleSet3 = GetNodeOrNull<GPUParticles2D>("GPUParticles2D-3");
			particleSet4 = GetNodeOrNull<GPUParticles2D>("GPUParticles2D-4");
			/* Trial code for creating unique ProcessMaterial of <ParticleProcessMaterial> for each particles node */
			particleSet1.ProcessMaterial = new ParticleProcessMaterial();
			particleSet2.ProcessMaterial = new ParticleProcessMaterial();
			particleSet3.ProcessMaterial = new ParticleProcessMaterial();
			particleSet4.ProcessMaterial = new ParticleProcessMaterial();
			/* Trial code for creating unique ProcessMaterial of <ParticleProcessMaterial> for each particles node */
		}

		public void DebugParticleTexture(string filter)
		{
			if (String.IsNullOrEmpty(filter))
			{
				GD.Print($"{objectName} :: DebugParticleTexture, set1 texture={particleSet1.Texture}");
				GD.Print($"{objectName} :: DebugParticleTexture, set2 texture={particleSet2.Texture}");
				GD.Print($"{objectName} :: DebugParticleTexture, set3 texture={particleSet3.Texture}");
				GD.Print($"{objectName} :: DebugParticleTexture, set4 texture={particleSet4.Texture}");
			}
		}

		public void SetSourcePoints(int sourceCount, bool symmetrical)
		{
			numberOfSourcePoints = sourceCount;
			symmetricalPlacement = symmetrical;
			GD.Print($"{objectName} :: SetSourcePoints sourceCount={sourceCount}, symmetrical={symmetrical}");
			for (int idx = 1; idx <= sourceCount; idx++)
			{
				string strKey = $"set{idx}";
				GD.Print($"{objectName} :: SetSourcePoints index={idx}, key={strKey}");
				if (mapActiveSets.ContainsKey(strKey))
					mapActiveSets.Remove(strKey);
				mapActiveSets.Add(strKey, true);
			}
		}

		private void ChangeSetActive(int Set, bool active)
		{
			string strKey = $"set{Set}";
			if (mapActiveSets.ContainsKey(strKey))
			{
				if (mapActiveSets[strKey] != active)
					mapActiveSets[strKey] = active;
			}
			else
			{
				mapActiveSets.Add(strKey, active);
			}
		}

		public void SetManualPlacePosition(int Set, Vector2 placePosition)
		{
			if ((Set >= 1) && (Set <= numberOfSourcePoints))
			{
				string strKey = $"set{Set}";
				GPUParticles2D particleSetCurrent = GetParticleSet(strKey);
				GD.Print($"{objectName} :: SetManualPlacePosition, strKey={strKey}, particleSetCurrent={particleSetCurrent}");
				if (particleSetCurrent != null)
				{
					particleSetCurrent.Position = placePosition;
				}
			}
		}

		public void SetLinearMovement(int Set, Vector3 gravity, float startVelocity, float linearAcceleration, Vector3 direction)
		{
			if ((Set >= 1) && (Set <= numberOfSourcePoints))
			{
				string strKey = $"set{Set}";
				GPUParticles2D particleSetCurrent = GetParticleSet(strKey);
				if (particleSetCurrent != null)
				{
					GD.Print($"{objectName} :: SetLinearMovement, current set={particleSetCurrent}, velocity={startVelocity}, accel={linearAcceleration}");
					ParticleProcessMaterial procMaterial = (ParticleProcessMaterial)particleSetCurrent.ProcessMaterial;
					GD.Print($"{objectName} :: SetLinearMovement, strKey={strKey}, procMaterial={procMaterial}");
					if (procMaterial != null)
					{
						procMaterial.Gravity = gravity;
						procMaterial.InitialVelocityMin = startVelocity;
						procMaterial.LinearAccelMin = linearAcceleration;
						procMaterial.Direction = direction;
					}
				}
			}
		}

		public void SetGroupingRotation(float degreesSec)
		{
			degreesPerSecond = degreesSec;
		}

		public bool IsActiveSet(string sourceTag)
		{
			if (mapActiveSets == null)
				return false;
			bool res = mapActiveSets.ContainsKey(sourceTag) && mapActiveSets[sourceTag] == true;
			return res;
		}

		public void Enable(bool enable)
		{
			//If enabled then add active/selected particles to the scene
			if (enable)
			{
				if (IsActiveSet("set1"))
					AddChild(particleSet1);
				if (IsActiveSet("set2"))
					AddChild(particleSet2);
				if (IsActiveSet("set3"))
					AddChild(particleSet3);
				if (IsActiveSet("set4"))
					AddChild(particleSet4);
			}
		}

		public void Emit(bool emit)
		{
			for (int idx = 1; idx <= numberOfSourcePoints; idx++)
			{
				ChangeSetActive(1, emit);
			}
			GD.Print($"Emit emit={emit}, active-1={IsActiveSet("set1")}");
			if (IsActiveSet("set1"))
				particleSet1.Emitting = emit;
			if (IsActiveSet("set2"))
				particleSet2.Emitting = emit;
			if (IsActiveSet("set3"))
				particleSet3.Emitting = emit;
			if (IsActiveSet("set4"))
				particleSet4.Emitting = emit;
		}

		public String GetImageResource(string resourceTag)
		{
			string res = "";
			if (mapParticleImageResources != null)
			{
				if (mapParticleImageResources.ContainsKey(resourceTag))
					res = mapParticleImageResources[resourceTag];
			}
			return res;
		}

		public /*ImageTexture*/Texture2D CreateTexture(string resourcePath, bool fill, Color fillColor, Vector2 imgSize)
		{
			/*
			ImageTexture res = new ImageTexture();
			if ((imgSize != null) && (imgSize.x >= 16) && (imgSize.y >= 16))
				res.SetSizeOverride(new Vector2i((int)imgSize.x, (int)imgSize.y));

			Image myImage = null;
			*/
			Texture2D resTexture = null;
			GD.Print($"{objectName} :: CreateTexture(), resourcePath={resourcePath}");
			/* if resourcePath is a project resource then use the following code */
			if (isInternalResourceTexture)
			{
				try
				{
					resTexture = ResourceLoader.Load<Texture2D>(resourcePath);
					//res.SetImage();
					return resTexture;
				}
				catch (InvalidCastException ex)
				{
					GD.Print($"{objectName} :: CreateTexture() Error, resourcePath={resourcePath}, exception={ex.Message}");
				}
			}
			else
			{
				/* if resourcePath is external, i.e not in the application resource pack, use the following code */
				Image myImage = Image.LoadFromFile(resourcePath);
				if (fill)
				{
					//myImage = Image.Create((int)imgSize.x, (int)imgSize.y, false, Image.Format.Rgba8);
					myImage.Fill(fillColor);
				}
			}
			//GD.Print($"CreateTexture resourcePath={resourcePath}, width={myImage.GetWidth()}");
			//res = ImageTexture.CreateFromImage(myImage);
			return resTexture;
		}

		public void SetVisible(string tag, bool visible)
		{
			if (String.IsNullOrEmpty(tag) == false)
			{
				switch (tag)
				{
					case "all":
						if (IsActiveSet("set1"))
							particleSet1.Visible = visible;
						if (IsActiveSet("set2"))
							particleSet2.Visible = visible;
						if (IsActiveSet("set3"))
							particleSet3.Visible = visible;
						if (IsActiveSet("set4"))
							particleSet4.Visible = visible;
						break;
					default:
						break;
				}
			}
		}

		public void SetActiveSetsMaterialName(string materialName)
		{
			PredefParticleMaterial materialChoice = PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_BLOCK;
			switch (materialName)
			{
				case "BLOCK":
					materialChoice = PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_BLOCK;
					break;
				case "CIRCLE":
					materialChoice = PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_CIRCLE;
					break;
				case "LIGHT_RAIN":
					materialChoice = PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_LIGHT_RAIN;
					break;
				case "HEAVY_RAIN":
					materialChoice = PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_HEAVY_RAIN;
					break;
				case "FEINT_SMOKE":
					materialChoice = PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_FEINT_SMOKE;
					break;
				case "MEDIUM_SMOKE":
					materialChoice = PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_MEDIUM_SMOKE;
					break;
				case "THICK_SMOKE":
					materialChoice = PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_THICK_SMOKE;
					break;
				case "KINDLE_FIRE":
					materialChoice = PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_KINDLE_FIRE;
					break;
			}
			SetActiveSetsMaterial(materialChoice);
		}

		public void SetActiveSetsMaterial(PredefParticleMaterial defMaterial)
		{
			Color defaultFill = new Color(0.0f, 0.0f, 0.0f);
			Vector2 noDefinedSize = new Vector2(0, 0);
			string particleTextureRes1 = "";
			string particleTextureRes2 = "";
			string particleTextureRes3 = "";
			string particleTextureRes4 = "";
			switch (defMaterial)
			{
				case PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_BLOCK:
					//Set the Material to null to use the default block shape
					break;
				case PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_CIRCLE:
					//Draw circle on canvas or another node and copy the texture across
					break;
				case PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_LIGHT_RAIN:
					particleTextureRes1 = GetImageResource("light-rain1");
					particleTextureRes2 = GetImageResource("light-rain2");
					GD.Print($"SetActiveSetsMaterial (LIGHT_RAIN), res1={particleTextureRes1}, res2={particleTextureRes2}");
					/*ImageTexture*/
					Texture2D rainLight1Texture = CreateTexture(particleTextureRes1, false, defaultFill, noDefinedSize);
					/*ImageTexture*/
					Texture2D rainLight2Texture = CreateTexture(particleTextureRes2, false, defaultFill, noDefinedSize);
					GD.Print($"{objectName} :: SetActiveSetsMaterial (LIGHT_RAIN), texture1={rainLight1Texture}, texture2={rainLight2Texture}");
					if (IsActiveSet("set1"))
						particleSet1.Texture = rainLight1Texture;
					//GD.Print($"SetActiveSetsMaterial (LIGHT_RAIN), set1 texture={particleSet1.Texture}");
					if (IsActiveSet("set2"))
						particleSet2.Texture = rainLight2Texture;
					if (IsActiveSet("set3"))
						particleSet3.Texture = rainLight1Texture;
					if (IsActiveSet("set4"))
						particleSet4.Texture = rainLight2Texture;
					break;
				case PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_HEAVY_RAIN:
					particleTextureRes1 = GetImageResource("heavy-rain1");
					particleTextureRes2 = GetImageResource("heavy-rain2");
					GD.Print($"{objectName} :: SetActiveSetsMaterial (HEAVY_RAIN), res1={particleTextureRes1}, res2={particleTextureRes2}");
					Texture2D rainHeavy1Texture = CreateTexture(particleTextureRes1, false, defaultFill, noDefinedSize);
					Texture2D rainHeavy2Texture = CreateTexture(particleTextureRes2, false, defaultFill, noDefinedSize);
					if (IsActiveSet("set1"))
						particleSet1.Texture = rainHeavy1Texture;
					if (IsActiveSet("set2"))
						particleSet2.Texture = rainHeavy2Texture;
					if (IsActiveSet("set3"))
						particleSet3.Texture = rainHeavy1Texture;
					if (IsActiveSet("set4"))
						particleSet4.Texture = rainHeavy2Texture;
					break;
				case PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_FEINT_SMOKE:
					particleTextureRes1 = GetImageResource("feint-smoke1");
					particleTextureRes2 = GetImageResource("feint-smoke2");
					particleTextureRes3 = GetImageResource("feint-smoke3");
					GD.Print($"{objectName} :: SetActiveSetsMaterial (FEINT_SMOKE), res1={particleTextureRes1}, res2={particleTextureRes2}, res3={particleTextureRes3}");
					Texture2D smokeFeint1Texture = CreateTexture(particleTextureRes1, false, defaultFill, noDefinedSize);
					Texture2D smokeFeint2Texture = CreateTexture(particleTextureRes2, false, defaultFill, noDefinedSize);
					if (IsActiveSet("set1"))
						particleSet1.Texture = smokeFeint1Texture;
					if (IsActiveSet("set2"))
						particleSet2.Texture = smokeFeint2Texture;
					if (IsActiveSet("set3"))
						particleSet3.Texture = smokeFeint1Texture;
					if (IsActiveSet("set4"))
						particleSet4.Texture = smokeFeint2Texture;
					break;
				case PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_MEDIUM_SMOKE:
					particleTextureRes1 = GetImageResource("medium-smoke1");
					particleTextureRes2 = GetImageResource("medium-smoke2");
					Texture2D smokeMedium1Texture = CreateTexture(particleTextureRes1, false, defaultFill, noDefinedSize);
					Texture2D smokeMedium2Texture = CreateTexture(particleTextureRes2, false, defaultFill, noDefinedSize);
					if (IsActiveSet("set1"))
						particleSet1.Texture = smokeMedium1Texture;
					if (IsActiveSet("set2"))
						particleSet2.Texture = smokeMedium2Texture;
					if (IsActiveSet("set3"))
						particleSet3.Texture = smokeMedium1Texture;
					if (IsActiveSet("set4"))
						particleSet4.Texture = smokeMedium2Texture;
					break;
				case PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_KINDLE_FIRE:
					particleTextureRes1 = GetImageResource("kindle-fire1");
					particleTextureRes2 = GetImageResource("kindle-fire2");
					particleTextureRes3 = GetImageResource("kindle-fire3");
					particleTextureRes4 = GetImageResource("kindle-fire4");
					Texture2D fireKindle1Texture = CreateTexture(particleTextureRes1, false, defaultFill, noDefinedSize);
					Texture2D fireKindle2Texture = CreateTexture(particleTextureRes2, false, defaultFill, noDefinedSize);
					Texture2D fireKindle3Texture = CreateTexture(particleTextureRes3, false, defaultFill, noDefinedSize);
					Texture2D fireKindle4Texture = CreateTexture(particleTextureRes4, false, defaultFill, noDefinedSize);
					if (IsActiveSet("set1"))
						particleSet1.Texture = fireKindle1Texture;
					if (IsActiveSet("set2"))
						particleSet2.Texture = fireKindle2Texture;
					if (IsActiveSet("set3"))
						particleSet3.Texture = fireKindle3Texture;
					if (IsActiveSet("set4"))
						particleSet4.Texture = fireKindle4Texture;
					break;
				default:
					break;
			}
		}

		public void SetDefaultPredefinedMaterial(PredefParticleMaterial defMaterial)
		{
			switch (defMaterial)
			{
				case PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_BLOCK:
					SetActiveSetsMaterial(PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_BLOCK);
					break;
				case PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_CIRCLE:
					SetActiveSetsMaterial(PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_CIRCLE);
					break;
				case PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_LIGHT_RAIN:
					SetActiveSetsMaterial(PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_LIGHT_RAIN);
					break;
				case PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_HEAVY_RAIN:
					SetActiveSetsMaterial(PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_HEAVY_RAIN);
					break;
				case PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_FEINT_SMOKE:
					SetActiveSetsMaterial(PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_FEINT_SMOKE);
					break;
				case PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_MEDIUM_SMOKE:
					SetActiveSetsMaterial(PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_MEDIUM_SMOKE);
					break;
				case PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_THICK_SMOKE:
					SetActiveSetsMaterial(PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_THICK_SMOKE);
					break;
				case PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_KINDLE_FIRE:
					SetActiveSetsMaterial(PredefParticleMaterial.PREDEF_MATERIAL_DEFAULT_KINDLE_FIRE);
					break;
				default:
					break;
			}
		}

		public GPUParticles2D GetParticleSet(string setKey)
		{
			GPUParticles2D res = null;
			switch (setKey)
			{
				case "set1":
					res = particleSet1;
					break;
				case "set2":
					res = particleSet2;
					break;
				case "set3":
					res = particleSet3;
					break;
				case "set4":
					res = particleSet4;
					break;
				default:
					break;
			}
			return res;
		}

		public void SetModulateColor(string setTag, Color modColor)
		{
			GD.Print($"{objectName} :: SetModulateColor, setTag={setTag}, modColor={modColor}, source points={numberOfSourcePoints}");
			if (String.IsNullOrEmpty(setTag) || (setTag == "all"))
			{
				for (int idx = 1; idx <= numberOfSourcePoints; idx++)
				{
					string strKey = $"set{idx}";
					GPUParticles2D particleSetCurrent = GetParticleSet(strKey);
					if (particleSetCurrent != null)
					{
						GD.Print($"{objectName} :: SetModulateColor, current set={particleSetCurrent}, color={modColor}");
						ParticleProcessMaterial procMaterial = (ParticleProcessMaterial)particleSetCurrent.ProcessMaterial;
						GD.Print($"{objectName} :: SetModulateColor, procMaterial={procMaterial}");
						procMaterial.Color = modColor;
					}
				}
			}
			else
			{
				GPUParticles2D particleSetCurrent = GetParticleSet(setTag);
				if (particleSetCurrent != null)
				{
					GD.Print($"{objectName} :: SetModulateColor, current set={particleSetCurrent}, color={modColor}");
					ParticleProcessMaterial procMaterial = (ParticleProcessMaterial)particleSetCurrent.ProcessMaterial;
					GD.Print($"{objectName} :: SetModulateColor, procMaterial={procMaterial}");
					procMaterial.Color = modColor;
				}
			}
		}

		public void SetBasicAttributes(int amount, float lifetime, float lifeRandomness, float explosiveness, float angularVelocity, float Angle)
		{
			for (int idx = 1; idx <= numberOfSourcePoints; idx++)
			{
				string strKey = $"set{idx}";
				GPUParticles2D particleSetCurrent = GetParticleSet(strKey);
				GD.Print($"{objectName} :: SetBasicAttributes, strKey={strKey}, particleSetCurrent={particleSetCurrent}");
				if (particleSetCurrent != null)
				{
					particleSetCurrent.Amount = amount;
					if (lifetime >= 0.1)
						particleSetCurrent.Lifetime = lifetime;
					if (explosiveness > 0.05)
						particleSetCurrent.Explosiveness = explosiveness;
					ParticleProcessMaterial procMaterial = (ParticleProcessMaterial)particleSetCurrent.ProcessMaterial;
					procMaterial.AngularVelocityMin = angularVelocity;
					procMaterial.AngleMin = Angle;
				}
			}
		}

	}
}
