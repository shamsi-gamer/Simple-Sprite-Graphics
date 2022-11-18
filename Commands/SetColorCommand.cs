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
        public class SetColorCommand : Command
        {
            public const string Keyword = "col";


            public Color        Color;


            public SetColorCommand(Color color)
            {
                Color = color;
            }



            public override void Eval(Parser parser) 
            {
                parser.CurrentScope.Color = Color;
            }
        }



        // COL #rgb

        public bool ParseSetColor(Parser parser)
        {
            if (!parser.Match(SetColorCommand.Keyword)) 
                return false;


            var color = ParseHexColor(parser.Move());

            parser.AddCommand(new SetColorCommand(color));


            return true;
        }



        public void ParseDisplay(List<Display> displays, Parser parser)
        {
            var name  = parser.Move();
            var panel = Get(name) as IMyTextPanel;

            if (panel != null)
                displays.Add(new Display(panel));
        }



        static Color ParseHexColor(string hex)
        {
            if (hex.Length == 0)
                return new Color();

            if (hex[0] == '#')
                hex = hex.Substring(1);

            var col = new Color();


            if (hex.Length == 8)
            {
                col.R = (byte)int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                col.G = (byte)int.Parse(hex.Substring(2, 4), System.Globalization.NumberStyles.HexNumber);
                col.B = (byte)int.Parse(hex.Substring(4, 6), System.Globalization.NumberStyles.HexNumber);
                col.A = (byte)int.Parse(hex.Substring(6, 8), System.Globalization.NumberStyles.HexNumber);
            }
            else if (hex.Length >= 6)
            {
                col.R = (byte)int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                col.G = (byte)int.Parse(hex.Substring(2, 4), System.Globalization.NumberStyles.HexNumber);
                col.B = (byte)int.Parse(hex.Substring(4, 6), System.Globalization.NumberStyles.HexNumber);
            }
            else if (hex.Length >= 4)
            {
                col.R = (byte)(int.Parse(hex[0].ToString(), System.Globalization.NumberStyles.HexNumber) * 0x11);
                col.G = (byte)(int.Parse(hex[1].ToString(), System.Globalization.NumberStyles.HexNumber) * 0x11);
                col.B = (byte)(int.Parse(hex[2].ToString(), System.Globalization.NumberStyles.HexNumber) * 0x11);
                col.A = (byte)(int.Parse(hex[3].ToString(), System.Globalization.NumberStyles.HexNumber) * 0x11);
            }
            else if (hex.Length == 3)
            {
                col.R = (byte)(int.Parse(hex[0].ToString(), System.Globalization.NumberStyles.HexNumber) * 0x11);
                col.G = (byte)(int.Parse(hex[1].ToString(), System.Globalization.NumberStyles.HexNumber) * 0x11);
                col.B = (byte)(int.Parse(hex[2].ToString(), System.Globalization.NumberStyles.HexNumber) * 0x11);
                col.A = 0xff;
            }
            else if (hex.Length == 2)
            {
                var v = (byte)int.Parse(hex, System.Globalization.NumberStyles.HexNumber);

                col.R = v;
                col.G = v;
                col.B = v;
                col.A = 0xff;
            }
            else if (hex.Length == 1)
            {
                var v = (byte)(int.Parse(hex, System.Globalization.NumberStyles.HexNumber) * 0x11);

                col.R = v;
                col.G = v;
                col.B = v;
                col.A = 0xff;
            }


            return col;
        }
    }
}