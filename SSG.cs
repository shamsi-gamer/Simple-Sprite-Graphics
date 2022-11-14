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
        public struct Coord 
        { 
            public float Value; 
            public bool  Percent;

            public Coord(float val, bool percent)
            {
                Value   = val;
                Percent = percent;
            }
        }



        public class Parse
        {
            public string[]      Tokens;
            public int           Pos;

            public List<Display> Displays;

            public RectangleF    Space;

            public Color         Color;
            public float         LineWidth;


            public string Next { get { return Tokens[Pos]; } }



            public Parse(string[] tokens)
            {
                Tokens    = tokens;
                Pos       = 0;
                          
                Displays  = new List<Display>();

                Space     = new RectangleF();

                Color     = Color.White;
                LineWidth = 1;
            }



            public string Move()
            {
                if (Pos < Tokens.Length)
                    return Tokens[Pos++];

                else
                {
                    Pos++;
                    return "";
                }
            }



            public void Flush()
            {
                foreach (var display in Displays)
                    display.Flush();
            }
        }



        public bool ParseCSG(string csg)
        {
            var tokens = SplitWithQuotes(csg);
 
            var parse = new Parse(tokens);
            //parse.Displays.Add(display);


            var overflowProtect = 10;


            while (parse.Pos < parse.Tokens.Length
                && overflowProtect > 0)
            { 
                if (parse.Next[0] == '#')
                    parse.Color = ColorFromHex(parse.Move());
                
                else
                { 
                    switch (parse.Next.ToLower())
                    {
                        case "dsp": if (!ParseDisplays   (parse)) return false; break;
                        
                        case "fr":  if (!ParseFillRect   (parse)) return false; break;

                        case "fe":  if (!ParseFillEllipse(parse)) return false; break;
                        case "fc":  if (!ParseFillCircle (parse)) return false; break;

                        case "lw":  if (!ParseLineWidth  (parse)) return false; break;
                        case "dl":  if (!ParseDrawLine   (parse)) return false; break;

                        case "dt":  if (!ParseDrawTexture(parse)) return false; break;
                            
                        case "ds":  if (!ParseDrawString (parse)) return false; break;

                        default: overflowProtect--; break;
                    }
                }
            }


            parse.Flush(); // last dump


            return true;
        }



        public bool ParseDisplays(Parse parse)
        {
            parse.Move(); // DSP


            var dspName  = parse.Move();
            var dspBlock = GetLcd(dspName);

            parse.Displays.Add(new Display(dspBlock));


            parse.Space = new RectangleF(
                0,
                0,
                parse.Displays[0].ContentWidth,
                parse.Displays[0].ContentHeight);


            return true;
        }



        public Coord ParseCoord(Parse parse)
        {
            var str = parse.Move();

            var percent = false;

            if (   str.Length > 0
                && str[str.Length-1] == '%')
            {
                percent = true;
                str = str.Substring(0, str.Length-1);
            }


            var val = float.Parse(str);

            return new Coord(val, percent);
        }



        public float ParseCoordX(Parse parse)
        {
            var coord = ParseCoord(parse);

            if (coord.Percent)
                coord.Value = parse.Space.X + coord.Value/100 * parse.Space.Width;

            return coord.Value;
        }



        public float ParseCoordW(Parse parse)
        {
            var coord = ParseCoord(parse);

            if (coord.Percent)
                coord.Value = coord.Value/100 * parse.Space.Width;

            return coord.Value;
        }



        public float ParseCoordY(Parse parse)
        {
            var coord = ParseCoord(parse);

            if (coord.Percent)
                coord.Value = parse.Space.Y + coord.Value/100 * parse.Space.Height;

            return coord.Value;
        }



        public float ParseCoordH(Parse parse)
        {
            var coord = ParseCoord(parse);

            if (coord.Percent)
                coord.Value = coord.Value/100 * parse.Space.Height;

            return coord.Value;
        }



        public bool ParseFillRect(Parse parse)
        {
            parse.Move(); // FR

            var x = ParseCoordX(parse);
            var y = ParseCoordY(parse);
            var w = ParseCoordX(parse);
            var h = ParseCoordY(parse);

            FillRect(parse.Displays[0].Sprites, x, y, w, h, parse.Color);

            return true;
        }



        public bool ParseFillEllipse(Parse parse)
        {
            parse.Move(); // FE

            var x  = ParseCoordX(parse);
            var y  = ParseCoordY(parse);
            var rx = ParseCoordX(parse);
            var ry = ParseCoordY(parse);

            FillEllipse(parse.Displays[0].Sprites, x, y, rx, ry, parse.Color);

            return true;
        }


        public bool ParseFillCircle(Parse parse)
        {
            parse.Move(); // FE

            var x  = ParseCoordX(parse);
            var y  = ParseCoordY(parse);
            var r  = ParseCoordX(parse);

            var rx = r;
            var ry = r * parse.Displays[0].ContentHeight / parse.Displays[0].ContentWidth;

            FillEllipse(parse.Displays[0].Sprites, x, y, rx, ry, parse.Color);

            return true;
        }



        public bool ParseLineWidth(Parse parse)
        {
            parse.Move(); // LW #

            var lw = float.Parse(parse.Move());

            parse.LineWidth = lw;

            return true;
        }



        public bool ParseDrawLine(Parse parse)
        {
            parse.Move(); // DL

            var x1 = ParseCoordX(parse);
            var y1 = ParseCoordY(parse);
            var x2 = ParseCoordX(parse);
            var y2 = ParseCoordY(parse);

            DrawLine(parse.Displays[0].Sprites, x1, y1, x2, y2, parse.Color, parse.LineWidth);

            return true;
        }



        public bool ParseDrawTexture(Parse parse)
        {
            parse.Move(); // DT

            var tex = parse.Move();

            var x = ParseCoordX(parse);
            var y = ParseCoordY(parse);
            var w = ParseCoordX(parse);
            var h = ParseCoordY(parse);

            var r = float.Parse(parse.Move());

            DrawTexture(parse.Displays[0].Sprites, tex, x, y, w, r, parse.Color, r);

            return true;
        }



        public bool ParseDrawString(Parse parse)
        {
            parse.Move(); // DS

            var str = parse.Move();

            var x     = ParseCoordX(parse);
            var y     = ParseCoordY(parse);
            var scale = float.Parse(parse.Move());
            var align = ParseStringAlign(parse.Move());

            DrawString(parse.Displays[0].Sprites, str, x, y, scale, parse.Color, align);

            return true;
        }



        public TextAlignment ParseStringAlign(string str)
        {
            switch (str)
            {
                case "L": return TextAlignment.LEFT;
                case "C": return TextAlignment.CENTER;
                case "R": return TextAlignment.RIGHT;
                // default: // error
            }

            return TextAlignment.LEFT;
        }
    }
}
