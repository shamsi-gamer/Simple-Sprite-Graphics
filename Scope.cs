using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;



namespace IngameScript
{
    partial class Program
    {
        public class Scope
        {
            public List<Display> Displays;
                                    
            public CRectangle    Area;
            public RectangleF    AbsoluteArea;
                                    
            public Color         Color;
            public float         Rotation;

            public HCoord        LineWidth;
            public HCoord        FontSize;



            public Scope()
            {
                Displays     = new List<Display>();

                Area         = new CRectangle();
                AbsoluteArea = new RectangleF();

                Color        = Color.White;
                Rotation     = 0;
                
                LineWidth    = new HCoord(1, false);
                FontSize     = new HCoord(1, false);
            }



            public Scope(Scope scope)
            {
                Displays     = new List<Display>(scope.Displays);

                Area         = new CRectangle(scope.Area);
                AbsoluteArea = scope.AbsoluteArea;

                Color        = scope.Color;
                Rotation     = scope.Rotation;

                LineWidth    = new HCoord(scope.LineWidth);
                FontSize     = new HCoord(scope.FontSize);
            }
        }
    }
}
