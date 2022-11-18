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
        //public bool ParseSetArea(Parser parser)
        //{
        //    parser.Move(); // DSP

        //    var x = (XCoord)ParseXCoord(parser);
        //    var y = (YCoord)ParseYCoord(parser);
        //    var w = (WCoord)ParseWCoord(parser);
        //    var h = (HCoord)ParseHCoord(parser);

        //    parser.Space = new RectangleF(
        //        x.GetAbsoluteValue(parser), 
        //        y.GetAbsoluteValue(parser), 
        //        w.GetAbsoluteValue(parser), 
        //        h.GetAbsoluteValue(parser));

        //    return true;
        //}



        //public bool ParseFillRect(Parser parse)
        //{
        //    parse.Move(); // FR

        //    var x = (XCoord)ParseXCoord(parse);
        //    var y = (YCoord)ParseYCoord(parse);
        //    var w = (WCoord)ParseWCoord(parse);
        //    var h = (HCoord)ParseHCoord(parse);

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

        //    var x  = (XCoord)ParseXCoord(parse);
        //    var y  = (YCoord)ParseYCoord(parse);
        //    var rx = (WCoord)ParseWCoord(parse);
        //    var ry = (HCoord)ParseHCoord(parse);

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
