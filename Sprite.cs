using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonicDronsEngine
{
    class Sprite
    {
        public Texture2D texture;
        public Rectangle splitSprite;
        public Vector2 origin;

        public Sprite(Texture2D _texture, Rectangle _splitSprite, Vector2 _origin)
        {
            texture = _texture;
            splitSprite = _splitSprite;
            origin = _origin;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffect)
        {
            spriteBatch.Draw(texture,
                                position,
                                splitSprite,
                                Color.White,
                                0,
                                origin,
                                2,
                                spriteEffect,
                                0);
        }
    }
}
