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
        public string[] ScanTokens(string str)
        {
            str = str.Replace("\t", " ");
            str = str.Replace("\\\"", "\uFFFC");


            var split = new List<string>();


            var quoteParts = str.Split('\"');

            for (var i = 0; i < quoteParts.Length; i += 2)
            {
                quoteParts[i] = quoteParts[i].Replace("\n", " "); // new lines are white space only outside of quotes

                var parts = quoteParts[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var part in parts)
                    split.Add(part);

                if (i < quoteParts.Length - 1)
                    split.Add(quoteParts[i+1].Replace("\uFFFC", "\""));
            }


            return split.ToArray();
        }



        public bool ParseSetDisplay(Parser parser)
        {
            parser.Move(); // DSP


            var panels = new List<IMyTextPanel>();
            GridTerminalSystem.GetBlocksOfType(panels);

            panels = panels
                .OrderBy(b => b.Position.X)
                .OrderBy(b => b.Position.Y)
                .ToList();


            var dspName  = parser.Move();
            var dspBlock = GetLcd(dspName);

            parser.CurrentScope.Displays.Add(new Display(dspBlock));


            parser.CurrentScope.Area = new RectangleF(
                0,
                0,
                parser.Displays[0].ContentWidth,
                parser.Displays[0].ContentHeight);


            return true;
        }



        public bool ParseSetArea(Parser parser)
        {
            parser.Move(); // DSP

            var x = (XCoord)ParseCoord(parser);
            var y = (YCoord)ParseCoord(parser);
            var w = (WCoord)ParseCoord(parser);
            var h = (HCoord)ParseCoord(parser);

            parser.Space = new RectangleF(
                x.GetAbsoluteValue(parser), 
                y.GetAbsoluteValue(parser), 
                w.GetAbsoluteValue(parser), 
                h.GetAbsoluteValue(parser));

            return true;
        }



        public Coord ParseCoord(Parser parse)
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



        //public bool ParseFillRect(Parser parse)
        //{
        //    parse.Move(); // FR

        //    var x = (XCoord)ParseCoord(parse);
        //    var y = (YCoord)ParseCoord(parse);
        //    var w = (WCoord)ParseCoord(parse);
        //    var h = (HCoord)ParseCoord(parse);

        //    FillRect(
        //        parse.Displays[0].Sprites, 
        //        x.GetAbsoluteValue(parse), 
        //        y.GetAbsoluteValue(parse), 
        //        w.GetAbsoluteValue(parse),
        //        h.GetAbsoluteValue(parse), 
        //        parse.Color);

        //    return true;
        //}



        //public bool ParseFillEllipse(Parser parse)
        //{
        //    parse.Move(); // FE

        //    var x  = (XCoord)ParseCoord(parse);
        //    var y  = (YCoord)ParseCoord(parse);
        //    var rx = (WCoord)ParseCoord(parse);
        //    var ry = (HCoord)ParseCoord(parse);

        //    FillEllipse(
        //        parse.Displays[0].Sprites, 
        //        x .GetAbsoluteValue(parse), 
        //        y .GetAbsoluteValue(parse), 
        //        rx.GetAbsoluteValue(parse),
        //        ry.GetAbsoluteValue(parse), 
        //        parse.Color);

        //    return true;
        //}


        //public bool ParseFillCircle(Parser parse)
        //{
        //    parse.Move(); // FE

        //    var x  = (XCoord)ParseCoord(parse);
        //    var y  = (YCoord)ParseCoord(parse);
        //    var ry = (HCoord)ParseCoord(parse);
        //    var rx = ry;

        //    FillEllipse(
        //        parse.Displays[0].Sprites, 
        //        x .GetAbsoluteValue(parse), 
        //        y .GetAbsoluteValue(parse), 
        //        rx.GetAbsoluteValue(parse),
        //        ry.GetAbsoluteValue(parse), 
        //        parse.Color);

        //    return true;
        //}



        //public bool ParseLineWidth(Parse parse)
        //{
        //    parse.Move(); // LW #

        //    var lw = float.Parse(parse.Move());

        //    parse.LineWidth = lw;

        //    return true;
        //}



        //public bool ParseDrawLine(Parse parse)
        //{
        //    parse.Move(); // DL

        //    var x1 = ParseCoordX(parse);
        //    var y1 = ParseCoordY(parse);
        //    var x2 = ParseCoordX(parse);
        //    var y2 = ParseCoordY(parse);

        //    DrawLine(parse.Displays[0].Sprites, x1, y1, x2, y2, parse.Color, parse.LineWidth);

        //    return true;
        //}



        public bool ParseDrawTexture(Parser parse)
        {
            parse.Move(); // DT

            var tex = parse.Move();

            var x = (XCoord)ParseCoord(parse);
            var y = (YCoord)ParseCoord(parse);
            var w = (WCoord)ParseCoord(parse);
            var h = (HCoord)ParseCoord(parse);

            DrawTexture(
                parse.Displays[0].Sprites, 
                tex,
                x.GetAbsoluteValue(parse), 
                y.GetAbsoluteValue(parse), 
                w.GetAbsoluteValue(parse),
                h.GetAbsoluteValue(parse), 
                parse.Color);

            var r = float.Parse(parse.Move());

            DrawTexture(parse.Displays[0].Sprites, tex, x, y, w, r, parse.Color, r);

            return true;
        }



        //public bool ParseDrawString(Parse parse)
        //{
        //    parse.Move(); // DS

        //    var str = parse.Move();

        //    var x     = ParseCoordX(parse);
        //    var y     = ParseCoordY(parse);
        //    var scale = float.Parse(parse.Move());
        //    var align = ParseStringAlign(parse.Move());

        //    DrawString(parse.Displays[0].Sprites, str, x, y, scale, parse.Color, align);

        //    return true;
        //}



        //public TextAlignment ParseStringAlign(string str)
        //{
        //    switch (str)
        //    {
        //        case "L": return TextAlignment.LEFT;
        //        case "C": return TextAlignment.CENTER;
        //        case "R": return TextAlignment.RIGHT;
        //        // default: // error
        //    }

        //    return TextAlignment.LEFT;
        //}
    }
}
