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
        public class SetRotationCommand : Command
        {
            public const string Keyword = "rot";


            public float        Rotation;


            public SetRotationCommand(float rot)
            {
                Rotation = rot;
            }



            public override void Eval(Parser parser) 
            {
                parser.CurrentScope.Rotation = Rotation/360f * Tau;
            }
        }



        // ROT deg

        public bool ParseSetRotation(Parser parser)
        {
            if (!parser.Match(SetColorCommand.Keyword)) 
                return false;


            var color = float.Parse(parser.Move());

            parser.AddCommand(new SetRotationCommand(color));


            return true;
        }
    }
}
