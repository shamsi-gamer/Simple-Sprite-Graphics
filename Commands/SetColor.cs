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
        public class SetColor : Command
        {
            public const string Keyword = "col";


            public Color        Color;


            public SetColor(Color color)
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
            if (!parser.Match(SetColor.Keyword)) 
                return false;


            var color = ParseHexColor(parser.Move());

            parser.AddCommand(new SetColor(color));


            return true;
        }



        static Color ParseHexColor(string hex)
        {
            if (hex.Length == 0)
                return new Color();

            if (hex[0] == '#')
                hex = hex.Substring(1);

            var col = new Color();

            const float gamma = 2;


            if (hex.Length == 6)
            {
                var r = Math.Pow((float)int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber) / 0xff, gamma);
                var g = Math.Pow((float)int.Parse(hex.Substring(2, 4), System.Globalization.NumberStyles.HexNumber) / 0xff, gamma);
                var b = Math.Pow((float)int.Parse(hex.Substring(4, 6), System.Globalization.NumberStyles.HexNumber) / 0xff, gamma);

                col.R = (byte)Math.Round(r * 0x7f);
                col.G = (byte)Math.Round(g * 0x7f);
                col.B = (byte)Math.Round(b * 0x7f);
                col.A = 0xff;
            }
            else if (hex.Length >= 3)
            {
                var r = Math.Pow((float)int.Parse(hex[0].ToString(), System.Globalization.NumberStyles.HexNumber) / 0x0f, gamma);
                var g = Math.Pow((float)int.Parse(hex[1].ToString(), System.Globalization.NumberStyles.HexNumber) / 0x0f, gamma);
                var b = Math.Pow((float)int.Parse(hex[2].ToString(), System.Globalization.NumberStyles.HexNumber) / 0x0f, gamma);

                col.R = (byte)Math.Round(r * 0x7f);
                col.G = (byte)Math.Round(g * 0x7f);
                col.B = (byte)Math.Round(b * 0x7f);
                col.A = 0xff;
            }
            else if (hex.Length == 2)
            {
                var v   = Math.Pow((float)int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber) / 0xff, gamma);
                var val = (byte)Math.Round(v * 0x7f);

                col.R = val;
                col.G = val;
                col.B = val;
                col.A = 0xff;
            }
            else if (hex.Length == 1)
            {
                var v   = Math.Pow((float)int.Parse(hex, System.Globalization.NumberStyles.HexNumber) / 0x0f, gamma);
                var val = (byte)Math.Round(v * 0x7f);

                col.R = val;
                col.G = val;
                col.B = val;
                col.A = 0xff;
            }


            return col;
        }
    }
}
