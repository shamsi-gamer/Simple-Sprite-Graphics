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
        public class DrawRectangle : DrawCommand
        { 
            public const string Keyword = "drect";


            public WCoord Width;
            public HCoord Height;



            public DrawRectangle(XCoord x, YCoord y, WCoord width, HCoord height) : base(x, y)
            {
                Width  = new WCoord(width);
                Height = new HCoord(height);
            }



            public override void Eval(Parser parser) 
            {
                var scope = parser.CurrentScope;

                var x    = X     .Abs(scope);
                var y    = Y     .Abs(scope);
                var w    = Width .Abs(scope);
                var h    = Height.Abs(scope);

                var lw   = scope.LineWidth.Abs(scope);
                var lw_2 = scope.LineWidth.Abs(scope) / 2;

                scope.Displays[0].DrawLine(x - w/2 - lw_2, y,        x + w/2 + lw_2, y,     scope.Color, lw);
                scope.Displays[0].DrawLine(x - w/2 - lw_2, y + h,    x + w/2 + lw_2, y + h, scope.Color, lw);
                scope.Displays[0].DrawLine(x - w/2,        y - lw_2, x - w/2,        y + h, scope.Color, lw);
                scope.Displays[0].DrawLine(x + w/2,        y - lw_2, x + w/2,        y + h, scope.Color, lw);
            }
        }



        // DRECT TextureName X Y W H

        public bool ParseDrawRectangle(Parser parser)
        {
            if (!parser.Match(DrawRectangle.Keyword))
                return false;

            var x = ParseXCoord(parser);
            var y = ParseYCoord(parser);
            var w = ParseWCoord(parser);
            var h = ParseHCoord(parser);

            parser.AddCommand(new DrawRectangle(x, y, w, h));

            return false;
        }
    }
}
