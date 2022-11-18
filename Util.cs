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



        void             Get<T>(List<T> blocks) where T : class { GridTerminalSystem.GetBlocksOfType(blocks);            }
        //void             Get<T>(List<T> blocks, Func<T, bool> condition) where T : class { GridTerminalSystem.GetBlocksOfType(blocks, condition); }

        IMyTerminalBlock Get   (string s) { return GridTerminalSystem.GetBlockWithName(s); }
        //IMyTextPanel     GetLcd(string s) { return Get(s) as IMyTextPanel; }
    }
}
