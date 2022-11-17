﻿using Sandbox.Game.EntityComponents;
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
            public const string  Keyword = "tex";


            public SpriteTexture Texture;

            public WCoord        Width;
            public HCoord        Height;



            public DrawTexture(SpriteTexture tex, XCoord x, YCoord y, WCoord width, HCoord height) : base(x, y)
            {
                Texture = tex;

                Width   = new WCoord(width);
                Height  = new HCoord(height);
            }



            public override void Eval(Parser parser) 
            {
                var scope = parser.CurrentScope;

                scope.Displays[0].DrawTexture(
                    scope.Displays[0].Sprites, 
                    Texture.ID, 
                    X     .GetAbsoluteValue(scope), 
                    Y     .GetAbsoluteValue(scope), 
                    Width .GetAbsoluteValue(scope),
                    Height.GetAbsoluteValue(scope), 
                    scope.Color);

                logPanel.WriteText("DrawTexture.Eval()\n", true);
            }
        }



        // TEX TextureName X Y W H

        public bool ParseDrawTexture(Parser parser)
        {
            if (!parser.Match(DrawTexture.Keyword))
                return false;

            var tag = parser.Move();
            var tex = SpriteTexture.From(tag);

            var x   = (XCoord)ParseCoord(parser);
            var y   = (YCoord)ParseCoord(parser);
            var w   = (WCoord)ParseCoord(parser);
            var h   = (HCoord)ParseCoord(parser);


            parser.AddCommand(new DrawTexture(tex, x, y, w, h));


            return false;
        }
    }
}
