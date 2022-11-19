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
        const float Tau = (float)(Math.PI * 2);



        static double nozero(double d) { return d == 0 ? double.Epsilon : d; }

        static double sqr(double d) { return d * d; }



        static double limitAngle(double a)
        {
            while (a >= Tau) a -= Tau;
            while (a <  0  ) a += Tau;
            
            return a;
        }



        static double limitAngleDiff(double a)
        {
            while (a >= Tau/2) a -= Tau;
            while (a < -Tau/2) a += Tau;
            
            return a;
        }



        static double angleDiff(double a1, double a2)
        {
            double result = a2 - a1;

                 if (a2 <= Tau / 4   && a1 >  Tau * 3/4) result += Tau;
            else if (a2 >  Tau * 3/4 && a1 <= Tau / 4)   result -= Tau;

            return limitAngleDiff(result);
        }
    }
}
