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
            public const string  Keyword = "dsp";


            public List<Display> Displays;

            public int           AxisX;
            public int           AxisY;

            public int           MaxX;
            public int           MaxY;



            public SetDisplay(List<Display> displays, int ax, int ay, int maxX, int maxY)
            {
                Displays = displays;

                AxisX    = ax;
                AxisY    = ay;

                MaxX     = maxX;
                MaxY     = maxY;
            }



            public override void Eval(Parser parser) 
            {
                var scope = parser.CurrentScope;


                //scope.Displays.ForEach(d => d.FlushSprites());


                scope.Displays = Displays;

                var dspBR = Displays.Find(d =>
                       d.Panel.Max[AxisX] == MaxX
                    && d.Panel.Max[AxisY] == MaxY);


                scope.Area = new CRectangle(
                    new XCoord(0),
                    new YCoord(0),
                    new WCoord(dspBR.Offset.X + dspBR.Surface.SurfaceSize.X),
                    new HCoord(dspBR.Offset.Y + dspBR.Surface.SurfaceSize.Y));


                scope.AbsoluteArea = new RectangleF(
                    scope.Area.X     .Value,
                    scope.Area.Y     .Value,
                    scope.Area.Width .Value,
                    scope.Area.Height.Value);
            }
        }



        // DSP PanelName
        // DSP ( PanelName1 PanelName2 ... )

        public bool ParseSetDisplay(Parser parser)
        {
            if (!parser.Match(SetDisplay.Keyword)) 
                return false;


            var displays = new List<Display>();


            if (parser.Match("("))
            {
                while (    parser.HasNext
                       && !parser.NextIsReserved)
                    ParseDisplay(displays, parser);

                parser.Match(")");
            }
            else
            {
                ParseDisplay(displays, parser);
            }


            var ax  = -1;
            var ay  = -1;

            var min = new Vector3I(int.MaxValue, int.MaxValue, int.MaxValue);
            var max = new Vector3I(int.MinValue, int.MinValue, int.MinValue);


            if (displays.Count > 0)
            { 
                foreach (var dsp in displays)
                {
                    for (int i = 0; i < 3; i++)
                    { 
                        min[i] = Math.Min(min[i], dsp.Panel.Min[i]);
                        max[i] = Math.Max(max[i], dsp.Panel.Max[i]);
                    }
                }


                var dx = Math.Abs(max.X - min.X) + 1;
                var dy = Math.Abs(max.Y - min.Y) + 1;
                var dz = Math.Abs(max.Z - min.Z) + 1;


                foreach (var dsp in displays)
                { 
                         if (dx >= dy && dy >= dz) { ax = 0; ay = 1; }
                    else if (dx >= dz && dz >= dy) { ax = 0; ay = 2; }
                    else if (dy >= dx && dx >= dz) { ax = 1; ay = 0; }
                    else if (dy >= dz && dz >= dx) { ax = 1; ay = 2; }
                    else if (dz >= dx && dx >= dy) { ax = 2; ay = 0; }
                    else if (dz >= dy && dy >= dx) { ax = 2; ay = 1; }
                }


                displays = displays
                    .OrderBy(d => d.Panel.Position[ax])
                    .OrderBy(d => d.Panel.Position[ay])
                    .ToList();

                foreach (var dsp in displays)
                { 
                    var pos = dsp.Panel.Position;

                    dsp.Offset[ax] = (dsp.Panel.Min[ax] - min[ax]) * dsp.Surface.SurfaceSize.X; // TODO judging by its own size for now 
                    dsp.Offset[ay] = (dsp.Panel.Min[ay] - min[ay]) * dsp.Surface.SurfaceSize.Y;

                    logPanel.WriteText("dsp.Offset[" + ax + "] = " + dsp.Offset[ax] + "\n", true);
                    logPanel.WriteText("dsp.Offset[" + ay + "] = " + dsp.Offset[ay] + "\n", true);
                }
            }


            parser.AddCommand(new SetDisplay(displays, ax, ay, max[ax], max[ay]));


            return true;
        }



        public void ParseDisplay(List<Display> displays, Parser parser)
        {
            var name = parser.Move();
            var panel = Get(name) as IMyTextPanel;

            if (panel != null)
            {
                var dsp = new Display(panel);

                if (!parser.Displays.Contains(dsp))
                    parser.Displays.Add(dsp);

                displays.Add(dsp);
            }
        }
    }
}
