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
        public class FillEllipse : DrawTexture
        {
            public new const string Keyword = "felps";



            public FillEllipse(XCoord x, YCoord y, WCoord w, HCoord h)
                : base(CircleTexture, x, y, w, h) { }



            public override void Eval(Parser parser) 
            {
                var scope = parser.CurrentScope;

                //logPanel.WriteText("scope.Color = " + scope.Color + "\n", true);

                var x = X     .Abs(scope);
                var y = Y     .Abs(scope);
                var w = Width .Abs(scope);
                var h = Height.Abs(scope);

                scope.Displays[0].DrawTexture(
                    Texture.ID, 
                    x - w, 
                    y - h, 
                    w,
                    h, 
                    scope.Color,
                    scope.Rotation);

                //logPanel.WriteText("scope.Displays[0].Sprites.Count = " + scope.Displays[0].Sprites.Count + "\n", true);
            }
        }



        // FELPS X Y W H

        public bool ParseFillEllipse(Parser parser)
        {
            if (!parser.Match(FillEllipse.Keyword))
                return false;

            var x = ParseXCoord(parser);
            var y = ParseYCoord(parser);
            var w = ParseWCoord(parser);
            var h = ParseHCoord(parser);

            parser.AddCommand(new FillEllipse(x, y, w, h));

            return false;
        }
    }
}
