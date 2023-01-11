using Godot;
using System;
using System.Collections.Generic;
using SpaceStation.Framework;
using SpaceStation.Effects;
using SpaceStation.Util;

namespace SpaceStation
{
	public partial class AnimatedSplashScreen : Control
	{
		const string nextSceneResource = "res://Scenes/Game-UI/GrimaldiContraptions.tscn";
		const string clusterParticlesScenePath = "res://Scripts/Effects/ClusterParticles.tscn";
		const string linearParticlesScenePath = "res://Scripts/Effects/LinearFountainParticles.tscn";
		private AnimatedSprite2D animEffect;
		private RichTextLabel labelTitleEffects;
		private Timer splashTimer;
		private SceneUtilities sceneUtil;
		private List<ClusterParticles> clusterParticlesConglomerate = new List<ClusterParticles>();
		private List<LinearFountainParticles> fountainParticlesConglomerate = new List<LinearFountainParticles>();

		public delegate void ElegantExitDelegate();
		public event ElegantExitDelegate ElegantExit;

		/*
		private void GeneratePanelInfo()
		{
			PackedScene viewportScene = (PackedScene)ResourceLoader.Load(panelInfoResource);
			if (viewportScene != null)
			{
				helpBoard = (PanelInfo)viewportScene.Instantiate();
				helpBoard.Connect("ready",new Callable(this,nameof(_on_HelpBoard_Ready)));
				GD.Print($"Loading PanelInfo from PackedScene, PanelInfo = {helpBoard.Name}");
				helpBoard.Visible = false;
				AddChild(helpBoard);
			}
		}
		*/
		private void CreateClusterEffects()
		{
			PackedScene clusterParticlesScene = (PackedScene)ResourceLoader.Load(clusterParticlesScenePath);
			if (clusterParticlesScene != null)
			{
				ClusterParticles myCurrentParticlesObject = clusterParticlesScene.InstantiateOrNull<ClusterParticles>();
				if (myCurrentParticlesObject == null)
					return;
				clusterParticlesConglomerate.Add(myCurrentParticlesObject);
				clusterParticlesConglomerate.Add((ClusterParticles)myCurrentParticlesObject.Duplicate());
				clusterParticlesConglomerate.Add((ClusterParticles)myCurrentParticlesObject.Duplicate());
				clusterParticlesConglomerate.Add((ClusterParticles)myCurrentParticlesObject.Duplicate());
				clusterParticlesConglomerate.Add((ClusterParticles)myCurrentParticlesObject.Duplicate());
				myCurrentParticlesObject = clusterParticlesConglomerate[0];
				myCurrentParticlesObject.SetSourcePoints(2, true);
				//myCurrentParticlesObject.SetSourcePointsWithDistance(4, true, 250);
				myCurrentParticlesObject.SetManualPlacePosition("all", new Vector2(35, -32));
				//myCurrentParticlesObject.SetActiveSetsMaterialName("LIGHT_RAIN");
				myCurrentParticlesObject.SetVisible("all", true);
				//myCurrentParticlesObject.SetBasicAttributes(25, 1.8, 0.31, 0.04, 32.0, 55.0);
				myCurrentParticlesObject.Position = new Vector2(150, 180);
				myCurrentParticlesObject = clusterParticlesConglomerate[1];
				myCurrentParticlesObject.SetSourcePoints(2, true);
				//myCurrentParticlesObject.SetSourcePointsWithDistance(4, true, 250);
				myCurrentParticlesObject.SetManualPlacePosition("all", new Vector2(35, -32));
				//myCurrentParticlesObject.SetActiveSetsMaterialName("LIGHT_RAIN");
				myCurrentParticlesObject.SetVisible("all", true);
				//myCurrentParticlesObject.SetBasicAttributes(25, 1.8, 0.31, 0.04, 32.0, 55.0);
				myCurrentParticlesObject.Position = new Vector2(150, 180);

			}
		}

		private void CreateFountainParticles()
		{
			PackedScene fountainParticlesScene = (PackedScene)ResourceLoader.Load(linearParticlesScenePath); //1
			if (fountainParticlesScene != null)
			{
				LinearFountainParticles myCurrentParticlesObject = fountainParticlesScene.InstantiateOrNull<LinearFountainParticles>();
				Diagnostics.PrintNullValueMessage(myCurrentParticlesObject, "<instance>LinearFountainParticles");
				if (myCurrentParticlesObject == null)
				{
					GD.Print($"AnimatedSplashScreen, CreateFountainParticles() failed, particle instance creation failed");
					return;
				}
				fountainParticlesConglomerate.Add(myCurrentParticlesObject);
				myCurrentParticlesObject = fountainParticlesScene.InstantiateOrNull<LinearFountainParticles>(); //2
				fountainParticlesConglomerate.Add(myCurrentParticlesObject);
				myCurrentParticlesObject = fountainParticlesScene.InstantiateOrNull<LinearFountainParticles>(); //3
				fountainParticlesConglomerate.Add(myCurrentParticlesObject);
				myCurrentParticlesObject = fountainParticlesScene.InstantiateOrNull<LinearFountainParticles>(); //4
				fountainParticlesConglomerate.Add(myCurrentParticlesObject);
				myCurrentParticlesObject = fountainParticlesScene.InstantiateOrNull<LinearFountainParticles>(); //5
				fountainParticlesConglomerate.Add(myCurrentParticlesObject);
				GD.Print($"AnimatedSplashScreen, CreateFountainParticles(), particles list length = {fountainParticlesConglomerate.Count}");

				foreach (LinearFountainParticles particleObj in fountainParticlesConglomerate)
				{
					AddChild(particleObj);
					// IMPORTANT !!!!
					//Must add node instance here, else _Ready() isn't called and the node is then not initialised correctly
				}

				myCurrentParticlesObject = fountainParticlesConglomerate[0];
				myCurrentParticlesObject.SetSourcePoints(2, true);
				//myCurrentParticlesObject.SetSourcePointsWithDistance(4, true, 250);
				myCurrentParticlesObject.SetManualPlacePosition(1, new Vector2(35, -32));
				myCurrentParticlesObject.SetActiveSetsMaterialName("LIGHT_RAIN");
				myCurrentParticlesObject.SetVisible("all", true);
				myCurrentParticlesObject.SetBasicAttributes(25, 1.8f, 0.31f, 0.04f, 32.0f, 55.0f);
				myCurrentParticlesObject.Position = new Vector2(150, 180);
				myCurrentParticlesObject.Emit(true);

				myCurrentParticlesObject = fountainParticlesConglomerate[1];
				myCurrentParticlesObject.SetSourcePoints(4, true);
				myCurrentParticlesObject.SetActiveSetsMaterialName("LIGHT_RAIN");
				myCurrentParticlesObject.SetModulateColor("all", new Color(0.52f, 0.475f, 0.55f, 0.135f));
				myCurrentParticlesObject.SetVisible("all", true);
				myCurrentParticlesObject.SetBasicAttributes(55, 2.5f, 0.54f, 0.196f, 75.0f, 55.0f);
				myCurrentParticlesObject.Position = new Vector2(380, 110);
				myCurrentParticlesObject.Emit(true);

				myCurrentParticlesObject = fountainParticlesConglomerate[2];
				myCurrentParticlesObject.SetSourcePoints(4, true);
				myCurrentParticlesObject.SetLinearMovement(2, new Vector3(0, 48, 0), 24.5f, 3.5f, new Vector3(1, 1, 0));
				myCurrentParticlesObject.SetActiveSetsMaterialName("FEINT_SMOKE");
				myCurrentParticlesObject.SetModulateColor("all", new Color(0.62f, 0.61f, 0.55f, 0.115f));
				myCurrentParticlesObject.SetVisible("all", true);
				myCurrentParticlesObject.SetBasicAttributes(45, 2.9f, 0.375f, 0.265f, 32.0f, 55.0f);
				myCurrentParticlesObject.RotationDegrees = 175;
				myCurrentParticlesObject.Position = new Vector2(720, 160);
				myCurrentParticlesObject.Emit(true);

				myCurrentParticlesObject = fountainParticlesConglomerate[3];
				myCurrentParticlesObject.SetSourcePoints(4, true);
				//myCurrentParticlesObject.SetManualPlacePosition(1, Vector2(15, 16))
				//myCurrentParticlesObject.SetLinearMovement(3, Vector3(0, 48, 0), 17.5, 2.85, Vector3(-0.8, 1, 0))
				//SetLinearMovement(int Set, Vector3 gravity, float startVelocity, float linearAcceleration, Vector3 direction)
				myCurrentParticlesObject.SetActiveSetsMaterialName("KINDLE_FIRE");
				myCurrentParticlesObject.SetModulateColor("all", new Color(0.72f, 0.745f, 0.425f, 0.915f));
				myCurrentParticlesObject.SetVisible("all", true);
				myCurrentParticlesObject.SetBasicAttributes(62, 0.95f, 0.275f, 0.345f, 78.0f, 55.0f);
				myCurrentParticlesObject.RotationDegrees = 180;
				myCurrentParticlesObject.Position = new Vector2(720, 480);
				myCurrentParticlesObject.Emit(true);

				myCurrentParticlesObject = fountainParticlesConglomerate[4];
				myCurrentParticlesObject.SetSourcePoints(4, true);
				myCurrentParticlesObject.SetActiveSetsMaterialName("HEAVY_RAIN");
				myCurrentParticlesObject.SetModulateColor("set1", new Color(0.575f, 0.475f, 0.425f, 0.315f));
				myCurrentParticlesObject.SetModulateColor("set1", new Color(0.52f, 0.0f, 0.425f, 0.255f));
				myCurrentParticlesObject.SetVisible("all", true);
				myCurrentParticlesObject.SetBasicAttributes(75, 2.1f, 0.54f, 0.425f, 75.0f, 55.0f);
				myCurrentParticlesObject.Position = new Vector2(590, 320);
				myCurrentParticlesObject.Emit(true);
			}

		}

		private void _on_ElegantExit()
		{
			if (splashTimer != null)
			{
				splashTimer.Disconnect("timeout", new Callable(this, nameof(_on_SplashTimer_timeout)));
				splashTimer.Stop();
			}
			for (int ix = 0; ix < 4; ix++)
			{
				System.Threading.Thread.Sleep(100);
			}
			GD.Print($"_on_ElegantExit, switch scene to {nextSceneResource}\n");
			GetTree().Quit();
			//sceneUtil.ChangeSceneToFile(this, nextSceneResource);
		}

		public void _on_SplashTimer_timeout()
		{
			//processTimerCounter += 1;
		}

		private void CreateSplashBackgroundFX()
		{
			if (animEffect != null)
			{

			}
		}

		public override void _Ready()
		{
			GD.Print("AnimatedSplashScreen, _Ready() called");
			animEffect = GetNodeOrNull<AnimatedSprite2D>("sprite-Effects1");
			labelTitleEffects = GetNodeOrNull<RichTextLabel>("label-Title-Effects");
			splashTimer = GetNodeOrNull<Timer>("Timer-Splash");
			Diagnostics.PrintNullValueMessage(labelTitleEffects, "label-Title-Effects");
			if (labelTitleEffects != null)
			{
				labelTitleEffects.Text = "Space Station Grimaldi";
			}
			if (splashTimer != null)
			{
				splashTimer.Connect("timeout", new Callable(this, nameof(_on_SplashTimer_timeout)));
				splashTimer.Start();
			}
			ElegantExit += _on_ElegantExit;
			CreateFountainParticles();
		}

		//  // Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			if (Input.IsActionJustPressed("ui_cancel"))
			{ //The TAB key ?? displays menu
				if ((InputHandling.KeyBounceCheck("UI_CANCEL", 0.25f)) == false)
				{
					ElegantExit.Invoke();
				}
			}
		}
	}
}
