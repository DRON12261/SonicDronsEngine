using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonicDronsEngine
{
    class TileSet
    {
        List<SpritePack> TileSetSpriteSheets;
        string[] TileSetNames;
        List<List<Sprite>> Tiles;

        public TileSet()
        {
            TileSetSpriteSheets = new List<SpritePack> { };
            TileSetNames = new string[] { };
            Tiles = new List<List<Sprite>> { };
        }

        public void AddTiles(Texture2D tileSet, string name)
        {
            string[] tempStringArray = new string[TileSetNames.Length];
            for (int i = 0; i < TileSetNames.Length; i++) tempStringArray[i] = TileSetNames[i];
            TileSetNames = new string[TileSetNames.Length + 1];
            for (int i = 0; i < tempStringArray.Length; i++) TileSetNames[i] = tempStringArray[i];
            TileSetNames[TileSetNames.Length - 1] = name;
            TileSetSpriteSheets.Add(new SpritePack(name, new Sprite[] { new Sprite(tileSet, new Rectangle(0, 0, tileSet.Width, tileSet.Height), Vector2.Zero) }));
            SpritePackSorter Sorter = new SpritePackSorter();
            TileSetSpriteSheets.Sort(Sorter);
            Array.Sort(TileSetNames);
        }

        public void BreakToTiles()
        {
            foreach (SpritePack tileSet in TileSetSpriteSheets)
            {
                List<Sprite> tiles = new List<Sprite> { };
                int frameWidth = tileSet.sprites[0].texture.Width, frameHeight = tileSet.sprites[0].texture.Height;
                int gridWidth = frameWidth / 16, gridHeight = frameHeight / 16;
                for (int i = 0; i < gridWidth; i++)
                {
                    for (int j = 0; j < gridHeight; j++)
                    {
                        tiles.Add(new Sprite(tileSet.sprites[0].texture, new Rectangle(i * frameWidth, j * frameHeight, frameWidth, frameHeight), new Vector2(frameWidth / 2, frameHeight / 2)));
                    }
                }
                Tiles.Add(tiles);
            }
        }
    }
}
