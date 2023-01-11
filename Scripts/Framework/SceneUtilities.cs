using Godot;
using System;
using System.Threading;

namespace SpaceStation.Framework
{
	public partial class SceneUtilities : Node
	{
		public PackedScene newScene;

		public static void ThreadSleep(int milliSeconds)
		{
			if (milliSeconds >= 10)
			{
				System.Threading.Thread.Sleep(milliSeconds);
			}
		}

		public void CleanPreviousScenes(/*Node3D*/Node referenceScene, String callSource = "")
		{
			int numChildren = referenceScene.GetTree().Root.GetChildCount();
			GD.Print($"CleanPreviousScenes() - scenes count = {numChildren}, called from {callSource} \n");
			Node previousScene = referenceScene.GetTree().Root.GetChild(0);
			/*
			var props = Diagnostics.GetProperties(previousScene);
			if (props.Count > 0)
			{
				PrintObjectProperties("previousScene", introTimer);
			}
			*/
			referenceScene.GetTree().Root.RemoveChild(previousScene);
		}

		public static void DebugPrintScenesList(/*Node3D*/Node referenceScene, String identifier = "")
		{
			int numChildren = referenceScene.GetTree().Root.GetChildCount();
			if (numChildren > 0)
			{
				if (identifier.Length >= 2)
					GD.Print($"DebugPrintScenesList <{identifier}>");
				GD.Print($"DebugPrintScenesList - scenes count = {numChildren}\n");
				int idx = 0;
				foreach (object sceneX in referenceScene.GetTree().Root.GetChildren())
				{
					Diagnostics.PrintObjectProperties($"scene [{idx}] = ", sceneX);
					idx += 1;
				}
			}
		}

		public void ChangeSceneToFile(Node referenceScene, string scenePath)
		{
			newScene = (PackedScene)ResourceLoader.Load(scenePath);
			if (newScene != null)
				referenceScene.GetTree().Root.AddChild(newScene.Instantiate());
		}

		public static void ExitApplication(Node referenceScene)
		{
			referenceScene.GetTree().Quit();
		}

		public static void SetCameraPosition(Camera3D aCamera, Vector3 newPosition)
		{
			if (aCamera != null)
			{
				aCamera.Position = newPosition;
			}
		}

		public static Rect2 GetApplicationWindowExtent(Node3D referenceScene)
		{
			Rect2 res;
			res = referenceScene.GetTree().Root.GetVisibleRect();
			return res;
		}

		public static Rect2 GetApplicationWindowExtent(Node2D referenceScene)
		{
			Rect2 res;
			res = referenceScene.GetTree().Root.GetVisibleRect();
			return res;
		}

		public static Vector2 GetExtentOffsetsForCenter(Node3D referenceScene, Control graphicsControl)
		{
			Vector2 res = new Vector2(100.0f, 20.0f);
			if (graphicsControl != null)
			{
				Rect2 appExtent = GetApplicationWindowExtent(referenceScene);
				GD.Print($"GetExtentOffsetsForCenter(), appExtent = {appExtent}\n");
				if ( (appExtent.Size.x >= 100.0f) && (graphicsControl.GetRect().Position.x >= 100.0f) )
				{
					GD.Print($"GetExtentOffsetsForCenter(), appExtent = {appExtent}, graphicsControl size={graphicsControl.GetRect()}\n");
					res = new Vector2( (appExtent.Size.x / 2.0f) - (graphicsControl.GetRect().Position.x / 2.0f),
					(appExtent.Size.y / 2.0f) - (graphicsControl.GetRect().Position.y / 2.0f) );
				}
			}
			return res;
		}

		public static Vector2 GetExtentOffsetsForCenter(Node2D referenceScene, Control graphicsControl)
		{
			Vector2 res = new Vector2(100.0f, 20.0f);
			if (graphicsControl != null)
			{
				Rect2 appExtent = GetApplicationWindowExtent(referenceScene);
				GD.Print($"GetExtentOffsetsForCenter(), appExtent = {appExtent}\n");
				if ((appExtent.Size.x >= 100.0f) && (graphicsControl.GetRect().Position.x >= 100.0f))
				{
					GD.Print($"GetExtentOffsetsForCenter(), appExtent = {appExtent}, graphicsControl size={graphicsControl.GetRect().Position}\n");
					res = new Vector2((appExtent.Size.x / 2.0f) - (graphicsControl.GetRect().Position.x / 2.0f),
					(appExtent.Size.y / 2.0f) - (graphicsControl.GetRect().Position.y / 2.0f));
				}
			}
			return res;
		}

		public static void PlaceControlCentered(Node3D referenceScene, Control graphicsControl)
		{
			Vector2 placeCenterPos = GetExtentOffsetsForCenter(referenceScene, graphicsControl);
			PlaceControlTopLeft(referenceScene, graphicsControl, placeCenterPos);
		}

		public static void PlaceControlTopLeft(Node3D referenceScene, Control graphicsControl, Vector2 placePosition)
		{
			if (graphicsControl != null)
			{
				Rect2 appExtent = GetApplicationWindowExtent(referenceScene);
				if (appExtent.Size.x >= 100.0f)
				{
					//Vector2 controlExtent = graphicsControl.GetRect().Position;
					graphicsControl.SetPosition(placePosition);
				}
			}
		}

		public void ChangeScene(/*Spatial*/Node referenceScene, string scenePath)
		{
			newScene = (PackedScene)ResourceLoader.Load(scenePath);
			if (newScene != null)
				referenceScene.GetTree().Root.AddChild(newScene.Instantiate());
		}
	}
		/*
		public static void PlaceControlTopLeft(Node3D referenceScene, Control graphicsControl, Vector2 placePosition)
		{
			if ( (graphicsControl != null) && (placePosition != null) )
			{
				Rect2 appExtent = GetApplicationWindowExtent(referenceScene);
				if (appExtent.Size.x >= 100.0f)
				{
					Vector2 controlExtent = graphicsControl.Size;
					graphicsControl.Position = placePosition;
				}
			}
		}

		public static Color GenerateRandomColor(float min, float max, String palette = "")
		{
			Color res = Colors.Blue;
			if (String.IsNullOrEmpty(palette))
			{
				//Just plain vanilla random color generation, without any smartness
			}
			return res;
		}

		public static Color[] GenerateRandomColorsArray(int length, float minComp, float maxComp)
		{
			Color[] res = new Color[length];
			return res;
		}
		*/
}
