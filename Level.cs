using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SonicDronsEngine
{
    struct TileStruct
    {
        public char designation;
        public int[][] pixelHeights;
        public float[] angle;
        public bool[] collidable;
        public Sprite sprite;
    }

    struct Tile
    {
        public TileStruct tileStruct;
        public Vector2 position;
    }

    struct Area
    {
        public int ChunkCount;
        public Chunk[] Chunks;
        public int X;
        public int Y;
    }

    struct Chunk
    {
        public Tile[,] Tiles;
        public int X;
        public int Y;
    }

    class Level
    {
        string Name;
        int AreaCount = 0;
        TileStruct[] TileStructs;
        public Area[] Areas;
        Texture2D TileSet;

        public Level(string levelName, ContentManager Content)
        {
            XmlDocument levelXml = new XmlDocument();
            levelXml.Load(Environment.CurrentDirectory.ToString() + "/Content/Levels/" + levelName + "/" + levelName + ".xml");
            TileSet = Content.Load<Texture2D>("Sprites/Levels/" + levelName + "/" + levelName);
            XmlElement level = levelXml.DocumentElement;
            foreach (XmlNode childNode in level.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Name":
                        Name = childNode.InnerText;
                        break;
                    case "TileSet":
                        TileStructs = new TileStruct[childNode.ChildNodes.Count];
                        {
                            int i = 0;
                            foreach (XmlNode tileStruct in childNode.ChildNodes)
                            {
                                TileStructs[i] = new TileStruct();
                                foreach (XmlNode attribute in tileStruct.ChildNodes)
                                {
                                    switch (attribute.Name)
                                    {
                                        case "designation":
                                            TileStructs[i].designation = char.Parse(attribute.InnerText);
                                            break;
                                        case "sides":
                                            TileStructs[i].pixelHeights = new int[4][];
                                            TileStructs[i].angle = new float[4];
                                            TileStructs[i].collidable = new bool[4];
                                            int k = 0;
                                            foreach (XmlNode side in attribute.ChildNodes)
                                            {
                                                foreach (XmlNode sideAttributes in side.ChildNodes)
                                                {
                                                    switch (sideAttributes.Name)
                                                    {
                                                        case "pixelHeights":
                                                            TileStructs[i].pixelHeights[k] = new int[sideAttributes.ChildNodes.Count];
                                                            {
                                                                int j = 0;
                                                                foreach (XmlNode pixelHeight in sideAttributes.ChildNodes)
                                                                {
                                                                    TileStructs[i].pixelHeights[k][j++] = int.Parse(pixelHeight.InnerText);
                                                                }
                                                            }
                                                            break;
                                                        case "angle":
                                                            TileStructs[i].angle[k] = float.Parse(sideAttributes.InnerText);
                                                            break;
                                                        case "collidable":
                                                            TileStructs[i].collidable[k] = sideAttributes.InnerText == "1" ? true : false;
                                                            break;
                                                    }
                                                }
                                                k++;
                                            }
                                            break;
                                        case "rectangle":
                                            Rectangle sourceSprite = new Rectangle();
                                            foreach (XmlNode arg in attribute.ChildNodes)
                                            {
                                                switch (arg.Name)
                                                {
                                                    case "X":
                                                        sourceSprite.X = int.Parse(arg.InnerText);
                                                        break;
                                                    case "Y":
                                                        sourceSprite.Y = int.Parse(arg.InnerText);
                                                        break;
                                                    case "Width":
                                                        sourceSprite.Width = int.Parse(arg.InnerText);
                                                        break;
                                                    case "Height":
                                                        sourceSprite.Height = int.Parse(arg.InnerText);
                                                        break;
                                                }
                                            }
                                            TileStructs[i].sprite = new Sprite(TileSet, sourceSprite, Vector2.Zero);
                                            break;
                                    }
                                }
                                i++;
                            }
                        }
                        break;
                    case "AreaCount":
                        AreaCount = int.Parse(childNode.InnerText);
                        break;
                    case "AreaProperties":
                        Areas = new Area[AreaCount];
                        {
                            int i = 0;
                            foreach (XmlNode area in childNode.ChildNodes)
                            {
                                Area tempArea = new Area();
                                foreach (XmlNode attribute in area.ChildNodes)
                                {
                                    switch (attribute.Name)
                                    {
                                        case "ChunkCount":
                                            tempArea.ChunkCount = int.Parse(attribute.InnerText);
                                            break;
                                        case "PlaceX":
                                            tempArea.X = int.Parse(attribute.InnerText);
                                            break;
                                        case "PlaceY":
                                            tempArea.Y = int.Parse(attribute.InnerText);
                                            break;
                                    }
                                }
                                Areas[i++] = tempArea;
                            }
                        }
                        break;
                    case "LevelStruct":
                        {
                            int i = 0;
                            foreach (XmlNode area in childNode.ChildNodes)
                            {
                                Areas[i].Chunks = new Chunk[Areas[i].ChunkCount];
                                int j = 0;
                                foreach (XmlNode chunk in area.ChildNodes)
                                {
                                    Areas[i].Chunks[j] = new Chunk();
                                    foreach (XmlNode attribute in chunk.ChildNodes)
                                    {
                                        switch (attribute.Name)
                                        {
                                            case "Chunk":
                                                Areas[i].Chunks[j].Tiles = new Tile[16, 16];
                                                int k = 0;
                                                foreach (XmlNode line in attribute.ChildNodes)
                                                {
                                                    string currentLine = line.InnerText;
                                                    string[] destinations = currentLine.Split('-');
                                                    int h = 0;
                                                    foreach (string t in destinations)
                                                    {
                                                        TileStruct foundedTile = new TileStruct();
                                                        foreach (TileStruct tile in TileStructs)
                                                        {
                                                            if (t == tile.designation.ToString())
                                                            {
                                                                foundedTile = tile;
                                                                break;
                                                            }
                                                        }
                                                        Areas[i].Chunks[j].Tiles[h, k] = new Tile { tileStruct = foundedTile, position = new Vector2(h, k) };
                                                        h++;
                                                    }
                                                    k++;
                                                }
                                                break;
                                            case "PlaceX":
                                                Areas[i].Chunks[j].X = int.Parse(attribute.InnerText);
                                                break;
                                            case "PlaceY":
                                                Areas[i].Chunks[j].Y = int.Parse(attribute.InnerText);
                                                break;
                                        }
                                    }
                                    j++;
                                }
                                i++;
                            }
                        }
                        break;
                }
            }
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Areas.Length; i++)
            {
                for (int j = 0; j < Areas[i].Chunks.Length; j++)
                {
                    for (int w = 0; w < Areas[i].Chunks[j].Tiles.GetLength(0); w++)
                    {
                        for (int h = 0; h < Areas[i].Chunks[j].Tiles.GetLength(1); h++)
                        {
                            if (Areas[i].Chunks[j].Tiles[w, h].tileStruct.designation != 0)
                                Areas[i].Chunks[j].Tiles[w, h].tileStruct.sprite.Draw(spriteBatch, new Vector2(((Areas[i].X - 1) * 64 * 16 + (Areas[i].Chunks[j].X - 1) * 16 * 16 + w * 16) * 2, ((Areas[i].Y - 1) * 64 * 16 + (Areas[i].Chunks[j].Y - 1) * 16 * 16 + h * 16) * 2), SpriteEffects.None);
                        }
                    }
                }
            }
        }
    }
}
