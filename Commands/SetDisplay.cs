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
        public class SetDisplay : Command
        {
            public const string Keyword = "DSP";


            public Display[]    Displays;



            public SetDisplay(Display[] displays)
            {
                Displays = new List<Display>(displays).ToArray();
            }



            public override void Eval(Parser parser) 
            {
                var scope = parser.CurrentScope;
            }
        }



        public bool ParseSetDisplay(Parser parser)
        {
            parser.Move(); // keyword


            var displays = new List<Display>();


            while (    parser.HasNext
                   && !parser.NextIsKeyword)
            {
                var name  = parser.Move();
                var panel = Get(name) as IMyTextPanel;

                if (panel != null)
                    displays.Add(new Display(panel));
            }



            if (displays.Count > 0)
            { 
                float minX = float.MaxValue, maxX = float.MinValue;
                float minY = float.MaxValue, maxY = float.MinValue;
                float minZ = float.MaxValue, maxZ = float.MinValue;

                foreach (var d in displays)
                {
                    minX = Math.Min(minX, d.Panel.Position.X);
                    minY = Math.Min(minY, d.Panel.Position.Y);
                    minZ = Math.Min(minZ, d.Panel.Position.Z);

                    maxX = Math.Max(maxX, d.Panel.Position.X);
                    maxY = Math.Max(maxY, d.Panel.Position.Y);
                    maxZ = Math.Max(maxZ, d.Panel.Position.Z);
                }

                var dx = Math.Abs(maxX - minX);
                var dy = Math.Abs(maxY - minY);
                var dz = Math.Abs(maxZ - minZ);


                //parser.CurrentScope.Area = new RectangleF(
                //    0,
                //    0,
                //    parser.Displays[0].ContentWidth,
                //    parser.Displays[0].ContentHeight);

                //panels = panels
                //    .OrderBy(b => b.Position.X)
                //    .OrderBy(b => b.Position.Y)
                //    .ToList();
            }
            else
            {

            }



            parser.CurrentScope.Displays = displays;

            return true;
        }
    }
}
