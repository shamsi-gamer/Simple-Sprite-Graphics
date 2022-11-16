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
            public const string  Keyword = "DT";


            public WCoord        Width;
            public HCoord        Height;

            public SpriteTexture Texture;



            public DrawTexture(XCoord x, YCoord y, WCoord width, HCoord height, SpriteTexture tex) : base(x, y)
            {
                Width   = new WCoord(width);
                Height  = new HCoord(height);
                
                Texture = tex;
            }



            public override void Eval(Parser parser) 
            {
                var scope = parser.CurrentScope;

                //DrawTexture(
                //    scope.Displays[0].Sprites, 
                //    Texture.Name, 
                //    X     .GetAbsoluteValue(parser), 
                //    Y     .GetAbsoluteValue(parser), 
                //    Width .GetAbsoluteValue(parser),
                //    Height.GetAbsoluteValue(parser), 
                //    scope.Color);
            }
        }



        public static bool ParseDrawTexture(Parser parser)
        {
            parser.Move(); // keyword

            //var tex = parse.Move();

            //var x = (XCoord)ParseCoord(parse);
            //var y = (YCoord)ParseCoord(parse);
            //var w = (WCoord)ParseCoord(parse);
            //var h = (HCoord)ParseCoord(parse);

            //DrawTexture(
            //    parse.Displays[0].Sprites, 
            //    tex,
            //    x.GetAbsoluteValue(parse), 
            //    y.GetAbsoluteValue(parse), 
            //    w.GetAbsoluteValue(parse),
            //    h.GetAbsoluteValue(parse), 
            //    parse.Color);

            //var r = float.Parse(parse.Move());

            //DrawTexture(parse.Displays[0].Sprites, tex, x, y, w, r, parse.Color, r);

            return true;
        }
    }
}
