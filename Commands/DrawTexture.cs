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
        public class DrawTexture : DrawCommand
        { 
            public const string Keyword = "tex";


            public Texture Texture;

            public WCoord  Width;
            public HCoord  Height;



            public DrawTexture(Texture tex, XCoord x, YCoord y, WCoord width, HCoord height) : base(x, y)
            {
                Texture = tex;

                Width   = new WCoord(width);
                Height  = new HCoord(height);
            }



            public override void Eval(Parser parser) 
            {
                var scope = parser.CurrentScope;

                var x = X     .Abs(scope);
                var y = Y     .Abs(scope);
                var w = Width .Abs(scope);
                var h = Height.Abs(scope);

                scope.Displays[0].DrawTexture(
                    Texture.ID,
                    x - w/2, 
                    y - h/2, 
                    w,
                    h, 
                    scope.Color,
                    scope.Rotation);
            }
        }



        // TEX TextureName X Y W H

        public bool ParseDrawTexture(Parser parser)
        {
            if (!parser.Match(DrawTexture.Keyword))
                return false;

            var tag = parser.Move();
            var tex = Texture.From(tag);

            var x   = ParseXCoord(parser);
            var y   = ParseYCoord(parser);
            var w   = ParseWCoord(parser);
            var h   = ParseHCoord(parser);

            parser.AddCommand(new DrawTexture(tex, x, y, w, h));

            return false;
        }
    }
}
