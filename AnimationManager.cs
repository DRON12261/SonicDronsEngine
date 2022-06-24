using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonicDronsEngine
{
    class AnimationManager
    {
        public List<SpritePack> animations;
        public SpritePack currentAnimation { get; set; }
        public string[] animationNames;

        public AnimationManager()
        {
            animations = new List<SpritePack> { };
            animationNames = new string[] { };
        }

        public void Update(GameTime gameTime, float speed = 1)
        {
            currentAnimation.Update(gameTime, speed);
        }

        public void AddAnimation(string name, int frameWidth, int frameHeight, Texture2D spriteSheet, int originX = -1, int originY = -1, int startFrames = 0, int frameCount = 0)
        {
            string[] tempStringArray = new string[animationNames.Length];
            for (int i = 0; i < animationNames.Length; i++) tempStringArray[i] = animationNames[i];
            animationNames = new string[animationNames.Length + 1];
            for (int i = 0; i < tempStringArray.Length; i++) animationNames[i] = tempStringArray[i];
            animationNames[animationNames.Length - 1] = name;
            int gridWidth = spriteSheet.Width / frameWidth, gridHeight = spriteSheet.Height / frameHeight;
            if (frameCount == 0)
            {
                frameCount = gridWidth * gridHeight;
            }
            if (originX == -1) originX = frameWidth / 2;
            if (originY == -1) originY = frameHeight / 2;
            Sprite[] frames = new Sprite[frameCount];
            int count = 0;
            for (int i = 0; i < gridWidth; i++)
            {
                for (int j = 0; j < gridHeight; j++)
                {
                    frames[count++] = new Sprite(spriteSheet, new Rectangle(i * frameWidth, j * frameHeight, frameWidth, frameHeight), new Vector2(originX, originY));
                }
            }
            animations.Add(new SpritePack(name, frames, startFrames));
            SpritePackSorter Sorter = new SpritePackSorter();
            animations.Sort(Sorter);
            Array.Sort(animationNames);
        }

        public string GetAnimation() => currentAnimation.name;

        public void SetAnimation(string name) => currentAnimation = animations[Array.BinarySearch(animationNames, name)];

        public void PlayAnimation(SpriteBatch spriteBatch, Vector2 position, SpriteEffects dirrection, float speed)
        {
            currentAnimation.Draw(spriteBatch, position, dirrection);
        }
    }
}
