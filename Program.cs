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
    // SSG - Simple Sprite Graphics
    


    // only group panels
    
    // VAR define variables that can be referenced and updated from the outside through arguments

    // AREA x y w h
    // COMP ...
    
    // DSP Display1 // sets drawing display
    // DSP Display1 "Display 2"
       
    // DSP WIDE Display1 / Display2 "Display 3"
    // DSP Display1 "Display 2" / Display3 "Display 4"
    
    // add ability to draw on rotated displays

    // grobal transforms


    // X 1 x y rot
    // X "SolidRect" // texture
       
    // R 0 0 100% 100%
    // #4gb62aff R 0 0 100% 100%
       
    // #4gb62aff // sets the drawing color
    // FONT 5 "Monospace" // sets the drawing font
       
    // T "text" 50% 50% L/C/R T/M/B
    // T $width 50% (50%-20) CM


    // tables
    // lists
    // grids
    // w/ line height etc

    // SYM X|Y/0 origin
    // FLIP X|Y/0 origin

    // FOR 10   LERP x 0 100   LERP o 0 1   RND y 30 40  (OP o  FR x y 20 20)

    // AO animate opacity
    // AO LIN 1
    // AO CYC 0.2
    // AV visibility
    // AX
    // AY
    // AP position
    // AW
    // AH
    // AS size
    // AR rotation
    // AG group

    // animation somehow
    // show/hide, move, opacity

    // groups

    // generative effects
    // filters?

    // reusable macros

    // transforms


    partial class Program : MyGridProgram
    {
        public static IMyTextPanel logPanel;

        public int    codeLength;
        public int    codeHash;

        public Parser parser;


        public Program()
        {
            logPanel = Get("Log Panel") as IMyTextPanel;
            logPanel.ContentType = ContentType.TEXT_AND_IMAGE;

            Runtime.UpdateFrequency = 
                  UpdateFrequency.Update10
                | UpdateFrequency.Update100;

            ParseCode();
        }
    }
}
