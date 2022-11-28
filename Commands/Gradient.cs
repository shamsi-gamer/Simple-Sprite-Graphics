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
        public class Gradient : DrawCommand
        { 
            public const string Keyword = "grad";


            public WCoord Width;
            public HCoord Height;

            public Color  Color1;
            public Color  Color2;

            public int    Steps;



            public Gradient(XCoord x, YCoord y, WCoord width, HCoord height, Color c1, Color c2, int steps) : base(x, y)
            {
                Width   = new WCoord(width);
                Height  = new HCoord(height);

                Color1  = c1;
                Color2  = c2;

                Steps   = steps;
            }



            public override void Eval(Parser parser)
            {
                var scope = parser.CurrentScope;

                var x = X     .Abs(scope);
                var y = Y     .Abs(scope);
                var w = Width .Abs(scope);
                var h = Height.Abs(scope);

                for (var i = 0; i < Steps; i++)
                {
                    var r  = (byte)(Color1.R + (Color2.R - Color1.R) * (float)i / Steps);
                    var g  = (byte)(Color1.G + (Color2.G - Color1.G) * (float)i / Steps);
                    var b  = (byte)(Color1.B + (Color2.B - Color1.B) * (float)i / Steps);

                    var ws = w / Steps;

                    scope.Displays[0].FillRect(
                        x - ws/2 + i * ws,
                        y - h/2,
                        ws,
                        h,
                        new Color(r, g, b, 0xff));
                }
            }
        }



        // GRAD X Y W H col1 col2 steps

        public bool ParseGradient(Parser parser)
        {
            if (!parser.Match(Gradient.Keyword))
                return false;

            var x     = ParseXCoord(parser);
            var y     = ParseYCoord(parser);
            var w     = ParseWCoord(parser);
            var h     = ParseHCoord(parser);
            
            var col1  = ParseHexColor(parser.Move());
            var col2  = ParseHexColor(parser.Move());

            var steps = int.Parse(parser.Move());

            parser.AddCommand(new Gradient(x, y, w, h, col1, col2, steps));

            return false;
        }
    }
}
