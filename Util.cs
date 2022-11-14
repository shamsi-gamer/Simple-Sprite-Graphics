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
    partial class Program : MyGridProgram
    {
        public static double signedPow(double d, double p)
        {
            return Math.Sign(d) * Math.Pow(Math.Abs(d), p);
        }



        public static string printNoZero(double d, int dec)
        {
            return d.ToString((Math.Abs(d) < 1 ? "" : "0") + "." + new string('0', dec));
        }
        
        
        
        public static string printValue(double val, int dec, bool showZero, int pad)
        {
            if (showZero)
            {
                string format =
                      "0"
                    + (dec > 0 ? "." : "")
                    + new string('0', dec);

                return
                     val
                    .ToString(format)
                    .PadLeft(pad + dec + (dec > 0 ? 1 : 0));
            }
            else
            {
                return
                     printNoZero(val, dec)
                    .PadLeft(pad + dec + (dec > 0 ? 1 : 0));
            }
        }



        void             Get<T>(List<T> blocks)                          where T : class { GridTerminalSystem.GetBlocksOfType(blocks);            }
        //void             Get<T>(List<T> blocks, Func<T, bool> condition) where T : class { GridTerminalSystem.GetBlocksOfType(blocks, condition); }

        IMyTerminalBlock Get   (string s) { return GridTerminalSystem.GetBlockWithName(s); }
        IMyTextPanel     GetLcd(string s) { return Get(s) as IMyTextPanel; }


        string[] SplitWithQuotes(string str)
        {
            var split = new List<string>();


            var quoteParts = str.Split('\"');

            for (var i = 0; i < quoteParts.Length; i += 2)
            {
                quoteParts[i] = quoteParts[i].Replace("\n", " "); // new lines are white space only outside of quotes

                var parts = quoteParts[i].Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);

                foreach (var part in parts)
                    split.Add(part);

                if (i < quoteParts.Length-1)
                    split.Add(quoteParts[i+1]);
            }


            return split.ToArray();
        }



        Color ColorFromHex(string hex)
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
