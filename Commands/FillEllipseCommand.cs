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
        public class FillEllipseCommand : DrawTextureCommand
        {
            public new const string Keyword = "felps";


            public FillEllipseCommand(XCoord x, YCoord y, WCoord w, HCoord h)
                : base(CircleTexture, x, y, w, h) { }
        }



        // FELPS X Y W H

        public bool ParseFillEllipse(Parser parser)
        {
            if (!parser.Match(FillEllipseCommand.Keyword))
                return false;

            var x = ParseXCoord(parser);
            var y = ParseYCoord(parser);
            var w = ParseWCoord(parser);
            var h = ParseHCoord(parser);

            parser.AddCommand(new FillEllipseCommand(x, y, w, h));

            return false;
        }
    }
}
