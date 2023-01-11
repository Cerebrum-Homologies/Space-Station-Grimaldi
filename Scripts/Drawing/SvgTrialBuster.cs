using System;
using System.Collections.Generic;
using Godot;
using Svg;
using System.Drawing;
using System.IO;

namespace SpaceStation.Drawing
{
	public partial class SvgTrialBuster
	{
		//private SvgDocument FSvgDoc;

		public SvgTrialBuster()
		{
		}

		public void PullFromContent()
		{

		}

		public void WriteToFile(String filePath, String[] content)
		{
			System.IO.File.WriteAllLines(filePath, content);
		}

		public void ReplaceEntitiesTest(string filePath, string saveFile)
		{
			GD.Print($"ReplaceEntitiesTest(), base path={AppDomain.CurrentDomain.BaseDirectory}");
			var combinedPath = filePath.Replace("res://", AppDomain.CurrentDomain.BaseDirectory + "/");
			GD.Print($"ReplaceEntitiesTest(), combinedPath={combinedPath}");
			//string filePath = Path3D.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\sample.svg");
			try
			{
                /*
				var sampleDoc = SvgDocument.Open<SvgDocument>(combinedPath, new Dictionary<string, string>
				{
					{"entity1", "fill:red" },
					{"entity2", "fill:yellow" }
				});
				*/
                var sampleDoc = SvgDocument.Open<SvgDocument>(combinedPath);
                //sampleDoc.Descendants(); iterate on children
                GD.Print($"ReplaceEntitiesTest(), Opened SVG");

				sampleDoc.Draw().Save(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, saveFile));
				GD.Print($"ReplaceEntitiesTest(), Saved edited SVG");
			} catch (Exception e)
			{
				GD.Print($"ReplaceEntitiesTest(), exception={e.Message}");
			}
		}

		SvgDocument CreateSvgCircleDemo()
		{
			var FSvgDoc = new SvgDocument
			{
				Width = 500,
				Height = 500
			};

			FSvgDoc.ViewBox = new SvgViewBox(-250, -250, 500, 500);

			var group = new SvgGroup();
			FSvgDoc.Children.Add(group);

			group.Children.Add(new SvgCircle
			{
				Radius = 100,
				Fill = new SvgColourServer(System.Drawing.Color.Red),
				Stroke = new SvgColourServer(System.Drawing.Color.Black),
				StrokeWidth = 2
			});
			return FSvgDoc;
		}

		public SvgDocument CreateSvgSimpleShapesDemo()
		{
			var FSvgDoc = new SvgDocument
			{
				Width = 800,
				Height = 800
			};

			FSvgDoc.ViewBox = new SvgViewBox(-400, -400, 800, 800);

			var group = new SvgGroup();
			FSvgDoc.Children.Add(group);

			group.Children.Add(new SvgCircle
			{
				CenterX = 180,
				CenterY = 150,
				Radius = 60,
				Fill = new SvgColourServer(System.Drawing.Color.Azure),
				Stroke = new SvgColourServer(System.Drawing.Color.SandyBrown),
				StrokeWidth = 6
			});
			group.Children.Add(new SvgPolygon
			{
				Points = new SvgPointCollection {
				new SvgUnit(16f), new SvgUnit(14f),
				new SvgUnit(122f), new SvgUnit(12f),
				new SvgUnit(126f), new SvgUnit(88f),
				new SvgUnit(16f), new SvgUnit(88f) },
				Fill = new SvgColourServer(System.Drawing.Color.Chocolate),
				Stroke = new SvgColourServer(System.Drawing.Color.DarkGoldenrod),
				StrokeWidth = 15
			});

			return FSvgDoc;
		}

		public void GenerateSVG(string saveFile)
		{
			var FSvgDoc = CreateSvgCircleDemo();
			var FSvgDoc2 = CreateSvgSimpleShapesDemo();

			var combinedPath = saveFile.Replace("res://", AppDomain.CurrentDomain.BaseDirectory + "/");
			var combinedPath2 = combinedPath + ".extra.svg";
			GD.Print($"GenerateSVG(), combinedPath={combinedPath}");
			/*
			var stream = new MemoryStream();
			FSvgDoc.Write(stream);
			stream.Position = 0;
			StreamReader sr = new StreamReader(stream);
			stream.Close();           
			string[] content = { sr.ReadToEnd() };
			WriteToFile(combinedPath, content);      
			*/
			var fstream = new FileStream(combinedPath, FileMode.Create/*, FileAccess.Write*/); //"testing_create_svg1.svg"
			FSvgDoc.Write(fstream);			
			fstream.Close();

			var fstream2 = new FileStream(combinedPath2, FileMode.Create/*, FileAccess.Write*/); //"testing_create_svg1.svg"
			FSvgDoc2.Write(fstream2);
			fstream2.Close();
		}
	}
}
