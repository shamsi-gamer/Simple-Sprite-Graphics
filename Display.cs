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
            public IMyTextSurfaceProvider Provider;
            public IMyTextSurface         Surface;

            public RectangleF             Viewport;
                                          
            public float                  Scale,
                                          
                                          ContentWidth,
                                          ContentHeight;

            public List<MySprite>         Sprites;



            public Display(IMyTextSurfaceProvider provider, int index = -1)
            {
                if (index < 0)
                    index = 0;

                Provider = provider;
                Surface  = Provider.GetSurface(index);

                Init();
            }



            public Display(IMyTextSurface surface)
            {
                Provider = null;
                Surface  = surface;

                Init();
            }



            void Init()
            {
                Surface.ContentType = ContentType.SCRIPT;

                Scale             = 1;
                                  
                Surface.Script    = "";

                Viewport          = new RectangleF((Surface.TextureSize - Surface.SurfaceSize) / 2, Surface.SurfaceSize);
                                  
                ContentWidth      = Viewport.Width;
                ContentHeight     = Viewport.Height;

                Sprites           = new List<MySprite>();
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



            public float UserScale
            {
                get
                {
                    if (Scale == 0)
                    {
                        return
                            Surface.SurfaceSize.X / ContentWidth < Surface.SurfaceSize.Y / ContentHeight
                            ? (Surface.SurfaceSize.X - 10) / ContentWidth
                            : (Surface.SurfaceSize.Y - 10) / ContentHeight;
                    }
                    else return Scale;
                }
            }



            public void Flush()
            {
                if (Sprites.Count > 0)
                    Draw(Sprites);

                Sprites = new List<MySprite>();
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
                     if (sprite.Type == SpriteType.TEXT   ) sprite.RotationOrScale *= UserScale;
                else if (sprite.Type == SpriteType.TEXTURE) sprite.Size            *= UserScale;

                sprite.Position *= UserScale;

                sprite.Position +=
                      Viewport.Position
                    + Viewport.Size / 2
                    - new Vector2(ContentWidth, ContentHeight) / 2 * UserScale;
                
                frame.Add(sprite);
            }
        }
    }
}
