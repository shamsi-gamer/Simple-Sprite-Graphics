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
        public class Object
        {
            public float X;
            public float Y;

            public Object(float x, float y)
            {
                X = x;
                Y = y;
            }

            public virtual void Draw() { }
        }



        public class FilledRectangle : Object
        { 
            public float Width;
            public float Height;
          
            public FilledRectangle(float x, float y, float width, float height) : base(x, y)
            {
                Width  = width;
                Height = height;
            }

            public override void Draw() { }
        }
    }
}
