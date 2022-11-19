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
        public class Coord 
        { 
            public float Value; 
            public bool  Percent;


            public Coord(float val, bool percent = false)
            {
                Value   = val;
                Percent = percent;
            }


            public Coord(Coord coord)
            {
                Value   = coord.Value;
                Percent = coord.Percent;
            }


            public virtual float GetAbsoluteValue(Scope scope)
            { 
                return float.NaN; 
            }
        }



        public class WCoord : Coord
        {
            public WCoord(float val, bool percent = false) : base(val, percent) { }
            public WCoord(WCoord coord) : base(coord) { }

            public override float GetAbsoluteValue(Scope scope) 
            {
                return Value * (Percent ? scope.AbsoluteArea.Width/100 : 1);
            }
        }



        public class HCoord : Coord
        {
            public HCoord(float val, bool percent = false) : base(val, percent) { }
            public HCoord(HCoord coord) : base(coord) { }

            public override float GetAbsoluteValue(Scope scope) 
            {
                return Value * (Percent ? scope.AbsoluteArea.Height/100 : 1);
            }
        }



        public class XCoord : WCoord
        {
            public XCoord(float val, bool percent = false) : base(val, percent) { }
            public XCoord(XCoord coord) : base(coord) { }

            public override float GetAbsoluteValue(Scope scope) 
            {
                return base.GetAbsoluteValue(scope) + (Percent ? scope.AbsoluteArea.X : 0);
            }
        }



        public class YCoord : HCoord
        {
            public YCoord(float val, bool percent = false) : base(val, percent) { }
            public YCoord(YCoord coord) : base(coord) { }

            public override float GetAbsoluteValue(Scope scope) 
            {
                return base.GetAbsoluteValue(scope) + (Percent ? scope.AbsoluteArea.Y : 0);
            }
        }



        public class CRectangle
        {
            public XCoord X;
            public YCoord Y;
            public WCoord Width;
            public HCoord Height;


            public CRectangle()
            {
                X      = new XCoord(0, false);
                Y      = new YCoord(0, false);
                Width  = new WCoord(0, false);
                Height = new HCoord(0, false);
            }


            public CRectangle(XCoord x, YCoord y, WCoord width, HCoord height)
            {
                X      = new XCoord(x     );
                Y      = new YCoord(y     );
                Width  = new WCoord(width );
                Height = new HCoord(height);
            }


            public CRectangle(CRectangle rect)
            {
                X      = new XCoord(rect.X     );
                Y      = new YCoord(rect.Y     );
                Width  = new WCoord(rect.Width );
                Height = new HCoord(rect.Height);
            }



            public RectangleF GetAbsoluteValue(Scope scope)
            {
                return new RectangleF(
                    X     .GetAbsoluteValue(scope),
                    Y     .GetAbsoluteValue(scope),
                    Width .GetAbsoluteValue(scope),
                    Height.GetAbsoluteValue(scope));
            }
        }
    }
}
