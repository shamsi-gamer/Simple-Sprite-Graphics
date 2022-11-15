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
        public class Command
        {
            public Command() { }

            public virtual void Eval(Parser parser) { }
        }



        public class DrawCommand : Command
        {
            public XCoord X;
            public YCoord Y;

            public DrawCommand(XCoord x, YCoord y)
            {
                X = new XCoord(x);
                Y = new YCoord(y);
            }
        }



        public class SetDisplayCommand : Command
        {
            public static string Keyword = "DSP";

            public Display[] Displays;

            public SetDisplayCommand(Display[] displays)
            {
                Displays = new List<Display>(displays).ToArray();
            }

            public override void Eval(Parser parser) 
            {
                var scope = parser.CurrentScope;
            }
        }



        public class DrawTextureCommand : DrawCommand
        { 
            public static string Keyword = "DT";

            public WCoord        Width;
            public HCoord        Height;
            public SpriteTexture Texture;

            public DrawTextureCommand(XCoord x, YCoord y, WCoord width, HCoord height, SpriteTexture tex) : base(x, y)
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



        //public class FillRectangle : Command
        //{ 
        //    public WCoord Width;
        //    public HCoord Height;

        //    public FillRectangle(XCoord x, YCoord y, WCoord width, HCoord height) : base(x, y)
        //    {
        //        Width  = new WCoord(width );
        //        Height = new HCoord(height);
        //    }

        //    public override void Eval(Parser parser) 
        //    {
        //        var x = X     .GetAbsoluteValue(parser);
        //        var y = Y     .GetAbsoluteValue(parser);
        //        var w = Width .GetAbsoluteValue(parser);
        //        var h = Height.GetAbsoluteValue(parser);

        //        FillRect(parser.Displays[0].Sprites, x, y, w, h, parser.Color);
        //    }
        //}



        //public class FillEllipse : Command
        //{ 
        //    public WCoord Width;
        //    public HCoord Height;

        //    public FillEllipse(XCoord x, YCoord y, WCoord width, HCoord height) : base(x, y)
        //    {
        //        Width  = new WCoord(width );
        //        Height = new HCoord(height);
        //    }

        //    public override void Eval(Parser parser) 
        //    {
        //        var x = X     .GetAbsoluteValue(parser);
        //        var y = Y     .GetAbsoluteValue(parser);
        //        var w = Width .GetAbsoluteValue(parser);
        //        var h = Height.GetAbsoluteValue(parser);

        //        FillRect(parser.Displays[0].Sprites, x, y, w, h, parser.Color);
        //    }
        //}
    }
}
