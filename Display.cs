using Sandbox.ModAPI.Ingame;
using System;
using System.Collections.Generic;
using VRage.Game.GUI.TextPanel;
using VRageMath;


namespace IngameScript
{
    partial class Program
    {
        public class Display
        {
            public IMyTextPanel           Panel;

            public IMyTextSurfaceProvider Provider;
            public IMyTextSurface         Surface;

            public RectangleF             Viewport;
            public Vector2                Offset; // this will be changed externally

            public float                  ContentWidth,
                                          ContentHeight;

            public List<MySprite>         Sprites;



            public RectangleF OffsetViewport 
            {
                get 
                { 
                    var viewport = Viewport;
                    viewport.Position -= Offset;
                    return viewport;
                } 
            }



            public Display(IMyTextSurfaceProvider provider, int index = -1)
            {
                if (index < 0)
                    index = 0;

                Panel    = null;

                Provider = provider;
                Surface  = Provider.GetSurface(index);

                Init();
            }



            public Display(IMyTextPanel panel)
            {
                Panel    = panel;

                Provider = null;
                Surface  = panel;

                Init();
            }



            void Init()
            {
                Surface.ContentType = ContentType.SCRIPT;
                Surface.Script      = "";
                                    
                Viewport            = new RectangleF((Surface.TextureSize - Surface.SurfaceSize) / 2, Surface.SurfaceSize);
                Offset              = new Vector2(0, 0);
                                    
                ContentWidth        = Viewport.Width;
                ContentHeight       = Viewport.Height;

                Sprites             = new List<MySprite>();
            }



            public float ContentScale
            {
                get
                {
                    return
                          Math.Min(Surface.TextureSize.X, Surface.TextureSize.Y) / 512
                        * Math.Min(Surface.SurfaceSize.X, Surface.SurfaceSize.Y)
                        / Math.Min(Surface.TextureSize.Y, Surface.TextureSize.Y);
                }
            }



            public void DrawString(string str, float x, float y, float scale, Color c, TextAlignment align = TextAlignment.LEFT, string font = "Monospace")
            {
                Sprites.Add(new MySprite()
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



            public void DrawTexture(string texture, float x, float y, float w, float h, Color c, float rotation = 0)
            {
                var pos  = new Vector2(x, y);
                var size = new Vector2(w, h);

                Sprites.Add(new MySprite()
                {
                    Type            = SpriteType.TEXTURE,
                    Data            = texture,
                    Position        = pos + size,
                    Size            = size,
                    Color           = c,
                    Alignment       = TextAlignment.CENTER,
                    RotationOrScale = rotation
                });
            }



            public void FillRect(float x, float y, float w, float h, Color c)
            {
                DrawTexture("SquareSimple", x, y, w, h, c);
            }



            public void FillEllipse(float x, float y, float rx, float ry, Color c)
            {
                DrawTexture(
                    CircleTexture.ID, 
                    x - rx, 
                    y - ry, 
                    rx*2, 
                    ry*2, 
                    c);
            }



            public void DrawLine(float x1, float y1, float x2, float y2, Color c, float w = 1)
            {
                var dp    = new Vector2(x2, y2) - new Vector2(x1, y1);
                var len   = dp.Length();
                var angle = (float)Math.Atan2(y1 - y2, x2 - x1);

                DrawTexture(
                    SquareTexture.ID,
                    x1 + dp.X/2 - len/2,
                    y1 + dp.Y/2 - w/2,
                    len,
                    w,
                    c,
                    -angle);
            }



            public void Draw(List<MySprite> sprites)
            {
                var frame = Surface.DrawFrame();
                Draw(ref frame, sprites);
                frame.Dispose();
            }



            public void Draw(ref MySpriteDrawFrame frame, List<MySprite> sprites = null)
            {
                foreach (var sprite in sprites)
                    Draw(ref frame, sprite);
            }



            public void Draw(ref MySpriteDrawFrame frame, MySprite sprite)
            {
                sprite.Position +=
                      Viewport.Position
                    - Offset
                    + Viewport.Size/2
                    - new Vector2(ContentWidth, ContentHeight) / 2;
                
                frame.Add(sprite);
            }



            public void FlushSprites()
            {
                if (Sprites.Count > 0)
                {
                    logPanel.WriteText("Sprites.Count = " + Sprites.Count + "\n", true);
                    Draw(Sprites);
                }

                Sprites.Clear();
            }
        }
    }
}
