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
        static void DrawString(List<MySprite> sprites, string str, float x, float y, float scale, Color c, TextAlignment align = TextAlignment.LEFT, string font = "Monospace")
        {
            sprites.Add(new MySprite()
            {
                Type            = SpriteType.TEXT,
                Data            = str,
                Position        = new Vector2(x, y),
                RotationOrScale = scale,
                Color           = c,
                Alignment       = align,
                FontId          = font
            });
        }



        static void DrawTexture(List<MySprite> sprites, string texture, Vector2 pos, Vector2 size, Color c, float rotation = 0)
        {
            sprites.Add(new MySprite()
            {
                Type            = SpriteType.TEXTURE,
                Data            = texture,
                Position        = pos + size/2,
                Size            = size,
                Color           = c,
                Alignment       = TextAlignment.CENTER,
                RotationOrScale = rotation
            });
        }



        static void DrawTexture(List<MySprite> sprites, string texture, float x, float y, float w, float h, Color c, float rotation = 0)
        {
            DrawTexture(sprites, texture, new Vector2(x, y), new Vector2(w, h), c, rotation);
        }
        
        

        static void FillRect(List<MySprite> sprites, float x, float y, float w, float h, Color c)
        {
            DrawTexture(sprites, "SquareSimple", x, y, w, h, c);
        }



        //static void FillEllipse(List<MySprite> sprites, Vector2 p, Vector2 r, Color color)
        //{
        //    DrawTexture(sprites, "Circle", p.X - r.X, p.Y - r.Y, r.X * 2, r.Y * 2, color);
        //}



        static void FillEllipse(List<MySprite> sprites, float x, float y, float rx, float ry, Color color)
        {
            DrawTexture(sprites, "Circle", x - rx, y - ry, rx * 2, ry * 2, color);
        }



        static void DrawLine(List<MySprite> sprites, Vector2 p1, Vector2 p2, Color col, float width = 1)
        {
            var dp    = p2 - p1;
            var len   = dp.Length();
            var angle = (float)Math.Atan2(p1.Y - p2.Y, p2.X - p1.X);

            DrawTexture(
                sprites,
                "SquareSimple",
                p1.X + dp.X/2 - len/2,
                p1.Y + dp.Y/2 - width/2,
                len,
                width,
                col,
                -angle);
        }
        
        

        static void DrawLine(List<MySprite> sprites, float x1, float y1, float x2, float y2, Color col, float width = 1)
        {
            DrawLine(
                sprites, 
                new Vector2(x1, y1), 
                new Vector2(x2, y2), 
                col, 
                width);
        }
    }
}
