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
        public class FillRectangleCommand : DrawTextureCommand
        {
            public new const string Keyword = "frect";
            
            
            public FillRectangleCommand(XCoord x, YCoord y, WCoord w, HCoord h) 
                : base(SquareTexture, x, y, w, h) { }
        }



        // FRECT X Y W H

        public bool ParseFillRectangle(Parser parser)
        {
            if (!parser.Match(FillRectangleCommand.Keyword))
                return false;

            var x = ParseXCoord(parser);
            var y = ParseYCoord(parser);
            var w = ParseWCoord(parser);
            var h = ParseHCoord(parser);

            parser.AddCommand(new FillRectangleCommand(x, y, w, h));

            return false;
        }
    }
}
