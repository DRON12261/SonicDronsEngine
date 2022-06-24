using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonicDronsEngine
{
    class SpritePack
    {
        public string name;
        public Sprite[] sprites;
        public Sprite currentFrame;
        public int startFrames;
        public float frameTime;
        public int frameNumber = 0;

        public SpritePack(string _name, Sprite[] _sprites, int _startFrames = 0)
        {
            name = _name;
            sprites = _sprites;
            startFrames = _startFrames;
            currentFrame = sprites[frameNumber];
        }

        public void Update(GameTime gameTime, float speed = 1)
        {
            PlayAnimation(gameTime, speed);
        }

        public void PlayAnimation(GameTime gameTime, float speed)
        {
            frameTime += gameTime.ElapsedGameTime.Milliseconds * Math.Abs(speed);
            if (frameTime > 100)
            {
                frameNumber++;
                if (frameNumber >= sprites.Length)
                {
                    frameNumber = startFrames != 0 ? startFrames - 1 : 0;
                }
                currentFrame = sprites[frameNumber];
                frameTime = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffect)
        {
            currentFrame.Draw(spriteBatch, position, spriteEffect);
        }
    }

    class SpritePackSorter : IComparer<SpritePack>
    {
        public int Compare(SpritePack _spritePack1, SpritePack _spritePack2)
        {
            for (int i = 0; i < (_spritePack1.name.Length > _spritePack2.name.Length ? _spritePack2.name.Length : _spritePack1.name.Length); i++)
            {
                if (_spritePack1.name.ToCharArray()[i] < _spritePack2.name.ToCharArray()[i]) return -1;
                if (_spritePack1.name.ToCharArray()[i] > _spritePack2.name.ToCharArray()[i]) return 1;
            }
            return 0;
        }
    }
}
