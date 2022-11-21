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
        public class DrawString : DrawCommand
        { 
            public const string Keyword = "str";


            public string String;
            public HCoord Scale;



            public DrawString(string str, XCoord x, YCoord y, HCoord scale) : base(x, y)
            {
                String = str;
                Scale  = scale;
            }



            public override void Eval(Parser parser) 
            {
                var scope = parser.CurrentScope;

                scope.Displays[0].DrawString(
                    String,
                    X     .Abs(scope), 
                    Y     .Abs(scope), 
                    Scale .Abs(scope),
                    scope.Color,
                    TextAlignment.LEFT); // TODO set alignment and font with commands
            }
        }



        // STR "string" X Y Scale

        public bool ParseDrawString(Parser parser)
        {
            if (!parser.Match(DrawString.Keyword))
                return false;

            var str   = parser.Move();

            var x     = ParseXCoord(parser);
            var y     = ParseYCoord(parser);

            var scale = ParseHCoord(parser);

            parser.AddCommand(new DrawString(str, x, y, scale));

            return false;
        }
    }
}
