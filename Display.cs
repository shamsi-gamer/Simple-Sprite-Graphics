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
            public Vector2                Offset; // this will be changed externally

            public float                  ContentWidth,
                                          ContentHeight;



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
                Surface.Script      = "";
                                    
                Viewport            = new RectangleF((Surface.TextureSize - Surface.SurfaceSize) / 2, Surface.SurfaceSize);
                Offset              = new Vector2(0, 0);
                                    
                ContentWidth        = Viewport.Width;
                ContentHeight       = Viewport.Height;
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
                    + Viewport.Size / 2
                    - new Vector2(ContentWidth, ContentHeight) / 2;
                
                frame.Add(sprite);
            }
        }
    }
}
