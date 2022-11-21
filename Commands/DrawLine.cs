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
        public class DrawLine : DrawCommand
        { 
            public const string Keyword = "line";


            public XCoord X2;
            public YCoord Y2;



            public DrawLine(XCoord x1, YCoord y1, XCoord x2, YCoord y2) : base(x1, y1)
            {
                X2 = new XCoord(x2);
                Y2 = new YCoord(y2);
            }



            public override void Eval(Parser parser) 
            {
                var scope = parser.CurrentScope;

                scope.Displays[0].DrawLine(
                    X .Abs(scope),
                    Y .Abs(scope),
                    X2.Abs(scope),
                    Y2.Abs(scope),
                    scope.Color,
                    scope.LineWidth.Abs(scope));
            }
        }



        // LINE X1 Y1 X2 Y2

        public bool ParseDrawLine(Parser parser)
        {
            if (!parser.Match(DrawRectangle.Keyword))
                return false;

            var x1 = ParseXCoord(parser);
            var y1 = ParseYCoord(parser);
            var x2 = ParseWCoord(parser);
            var y2 = ParseHCoord(parser);

            parser.AddCommand(new DrawRectangle(x1, y1, x2, y2));

            return false;
        }
    }
}
