using Godot;
using System;

namespace SpaceStation
{
    public partial class HUDShipRepair : Control
    {
        private PanelContainer panelContainer;
        private Panel panelMainTag;
        //[node name="panel-MainTag" type="Panel" parent="PanelContainer"]
        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            panelContainer = GetNodeOrNull<PanelContainer>("PanelContainer");
            panelMainTag = GetNodeOrNull<Panel>("panel-MainTag");
        }

        //  // Called every frame. 'delta' is the elapsed time since the previous frame.
        //  public override void _Process(double delta)
        //  {
        //      
        //  }
    }
}
