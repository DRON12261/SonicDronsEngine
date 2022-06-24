using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonicDronsEngine
{
    public class VideoManager
    {
        public VideoPlayer videoPlayer;
        GraphicsDeviceManager graphics;
        Texture2D videoTexture;

        public VideoManager(GraphicsDeviceManager _graphics)
        {
            videoPlayer = new VideoPlayer();
            graphics = _graphics;
        }

        public void PlayVideoOnFullScreen(SpriteBatch spriteBatch, Video video)
        {
            if (videoPlayer.State != MediaState.Stopped)
                videoTexture = videoPlayer.GetTexture();
            Rectangle screen = new Rectangle(0,
                0,
                graphics.PreferredBackBufferWidth,
                graphics.PreferredBackBufferHeight);
            if (videoTexture != null)
            {
                float scaleFactor = 1,
                    scaleFactorW = (float)graphics.PreferredBackBufferWidth / 1280,
                    scaleFactorH = (float)graphics.PreferredBackBufferHeight / 720;
                if (1280 * scaleFactorH > graphics.PreferredBackBufferWidth)
                    scaleFactor = scaleFactorW;
                if (720 * scaleFactorW > graphics.PreferredBackBufferHeight)
                    scaleFactor = scaleFactorH;
                Vector2 videoPosition = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
                Vector2 videoOrigin = new Vector2(1280 / 2, 720 / 2);
                spriteBatch.Draw(videoTexture, videoPosition, new Rectangle(0, 0, 1280, 720), Color.White, 0, videoOrigin, scaleFactor, SpriteEffects.None, 0);
            }
        }
    }
}
