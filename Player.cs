using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace SonicDronsEngine
{
    struct CharacterCollider
    {
        public string name;
        public Rectangle A;
        public Rectangle B;
        public Rectangle C;
        public Rectangle D;
        public Rectangle E;
        public Rectangle F;
        public bool cA;
        public bool cB;
        public bool cC;
        public bool cD;
        public bool cE;
        public bool cF;
    }

    class Player
    {
        Level actualLevel;
        public float xpos, ypos, xsp = 0, ysp = 0, gsp = 0, slope = 0, ang = 0, cameraX = 0, cameraY = 0;
        float acc = 0.046875F,
                dec = 0.5F,
                frc = 0.046875F,
                air = 0.09375F,
                grv = 0.21875F,
                top = 6,
                jmp = 6.5F,
                slp = 0.125F,
                slprollup = 0.078125F,
                slprolldown = 0.3125F,
                fall = 2.5F;
        public SpriteEffects dirrection = SpriteEffects.None;
        public AnimationManager playerAnimation;
        public Camera2D playerCamera;
        public bool brake = false, onGround = false;
        public List<CharacterCollider> PlayerColliders;
        public int tileX = 0, tileY = 0, tileW = 16 * 2, tileH = 16 * 2;
        public CharacterCollider PlayerCollider = new CharacterCollider();
        public int areaX = 0, areaY = 0, chunkX = 0, chunkY = 0, tileXA = 0, tileYA = 0, tileXB = 0, tileYB = 0, tileXC = 0, tileYC = 0, tileXD = 0, tileYD = 0, tileXE = 0, tileYE = 0, tileXF = 0, tileYF = 0;

        public Player(float _xpos, float _ypos, Camera2D _playerCamera)
        {
            xpos = _xpos;
            ypos = _ypos;
            playerCamera = _playerCamera;
            playerAnimation = new AnimationManager();
            PlayerColliders = new List<CharacterCollider> { };
            PlayerColliders.Add(new CharacterCollider()
            {
                name = "Default",
                A = new Rectangle(),
                B = new Rectangle(),
                C = new Rectangle(),
                D = new Rectangle(),
                E = new Rectangle(),
                F = new Rectangle()
            });

        }

        public void IncludeLevel(Level currentLevel)
        {
            actualLevel = currentLevel;
        }

        public void CalculateCollide()
        {
            if (!onGround)
            {
                PlayerCollider.A = new Rectangle((int)xpos - 9 * 2, (int)ypos, 1 * 2, 20 * 2);
                //PlayerCollider.B = new Rectangle((int) xpos + 9 * 2, (int) ypos, -1 * 2, 20 * 2);
                PlayerCollider.B = new Rectangle((int)xpos + 8 * 2, (int)ypos, 1 * 2, 20 * 2);
                PlayerCollider.C = new Rectangle((int)xpos - 9 * 2, (int)ypos - 13 * 2, 1 * 2, 13 * 2);
                //PlayerCollider.D = new Rectangle((int) xpos + 9 * 2, (int) ypos - 20 * 2, -1 * 2, 20 * 2);
                PlayerCollider.D = new Rectangle((int)xpos + 8 * 2, (int)ypos - 13 * 2, 1 * 2, 13 * 2);
                PlayerCollider.E = new Rectangle((int)xpos - 10 * 2, (int)ypos - 10 * 2, 10 * 2, 20 * 2);
                PlayerCollider.F = new Rectangle((int)xpos, (int)ypos - 10 * 2, 10 * 2, 20 * 2);
                PlayerCollider.cA = false;
                PlayerCollider.cB = false;
                PlayerCollider.cC = false;
                PlayerCollider.cD = false;
                PlayerCollider.cE = false;
                PlayerCollider.cF = false;
            }
            else
            {
                PlayerCollider.A = new Rectangle((int)xpos - 9 * 2, (int)ypos, 1 * 2, 20 * 2);
                //PlayerCollider.B = new Rectangle((int) xpos + 9 * 2, (int) ypos, -1 * 2, 20 * 2);
                PlayerCollider.B = new Rectangle((int)xpos + 8 * 2, (int)ypos, 1 * 2, 20 * 2);
                PlayerCollider.C = new Rectangle((int)xpos - 9 * 2, (int)ypos - 20 * 2, 1 * 2, 20 * 2);
                //PlayerCollider.D = new Rectangle((int) xpos + 9 * 2, (int) ypos - 20 * 2, -1 * 2, 20 * 2);
                PlayerCollider.D = new Rectangle((int)xpos + 8 * 2, (int)ypos - 20 * 2, 1 * 2, 20 * 2);
                PlayerCollider.E = new Rectangle((int)xpos - 10 * 2, (int)ypos - 10 * 2, 10 * 2, 20 * 2);
                PlayerCollider.F = new Rectangle((int)xpos, (int)ypos - 10 * 2, 10 * 2, 20 * 2);
                PlayerCollider.cA = false;
                PlayerCollider.cB = false;
                PlayerCollider.cC = false;
                PlayerCollider.cD = false;
                PlayerCollider.cE = false;
                PlayerCollider.cF = false;
            }
            onGround = false;



            foreach (Area area in actualLevel.Areas)
            {
                areaX = (area.X - 1) * 64 * 16 * 2; areaY = (area.Y - 1) * 64 * 16 * 2;
                if (PlayerCollider.A.Intersects(new Rectangle(areaX, areaY, 64 * 16 * 2, 64 * 16 * 2)))
                {
                    foreach (Chunk chunk in area.Chunks)
                    {
                        chunkX = areaX + ((chunk.X - 1) * 16 * 16) * 2; chunkY = areaY + ((chunk.Y - 1) * 16 * 16) * 2;
                        if (PlayerCollider.A.Intersects(new Rectangle(chunkX, chunkY, 16 * 16 * 2, 16 * 16 * 2)))
                        {
                            foreach (Tile tile in chunk.Tiles)
                            {
                                tileX = chunkX + (((int)tile.position.X) * 16) * 2; tileY = chunkY + (((int)tile.position.Y) * 16) * 2;
                                if (PlayerCollider.A.Intersects(new Rectangle(tileX, tileY, 16 * 2, 16 * 2)) && tile.tileStruct.designation != 0)
                                {
                                    tileXA = tileX; tileYA = tileY;
                                    PlayerCollider.cA = true;
                                    break;
                                }
                            }
                        }
                        if (PlayerCollider.cA) break;
                    }
                }
                if (PlayerCollider.B.Intersects(new Rectangle(areaX, areaY, 64 * 16 * 2, 64 * 16 * 2)))
                {
                    foreach (Chunk chunk in area.Chunks)
                    {
                        chunkX = areaX + ((chunk.X - 1) * 16 * 16) * 2; chunkY = areaY + ((chunk.Y - 1) * 16 * 16) * 2;
                        if (PlayerCollider.B.Intersects(new Rectangle(chunkX, chunkY, 16 * 16 * 2, 16 * 16 * 2)))
                        {
                            foreach (Tile tile in chunk.Tiles)
                            {
                                tileX = chunkX + (((int)tile.position.X) * 16) * 2; tileY = chunkY + (((int)tile.position.Y) * 16) * 2;
                                if (PlayerCollider.B.Intersects(new Rectangle(tileX, tileY, 16 * 2, 16 * 2)) && tile.tileStruct.designation != 0)
                                {
                                    tileXB = tileX; tileYB = tileY;
                                    PlayerCollider.cB = true;
                                    break;
                                }
                            }
                        }
                        if (PlayerCollider.cB) break;
                    }
                }
                if (PlayerCollider.C.Intersects(new Rectangle(areaX, areaY, 64 * 16 * 2, 64 * 16 * 2)))
                {
                    foreach (Chunk chunk in area.Chunks)
                    {
                        chunkX = areaX + ((chunk.X - 1) * 16 * 16) * 2; chunkY = areaY + ((chunk.Y - 1) * 16 * 16) * 2;
                        if (PlayerCollider.C.Intersects(new Rectangle(chunkX, chunkY, 16 * 16 * 2, 16 * 16 * 2)))
                        {
                            foreach (Tile tile in chunk.Tiles)
                            {
                                tileX = chunkX + (((int)tile.position.X) * 16) * 2; tileY = chunkY + (((int)tile.position.Y) * 16) * 2;
                                if (PlayerCollider.C.Intersects(new Rectangle(tileX, tileY, 16 * 2, 16 * 2)) && tile.tileStruct.designation != 0)
                                {
                                    tileXC = tileX; tileYC = tileY;
                                    PlayerCollider.cC = true;
                                    break;
                                }
                            }
                        }
                        if (PlayerCollider.cC) break;
                    }
                }
                if (PlayerCollider.D.Intersects(new Rectangle(areaX, areaY, 64 * 16 * 2, 64 * 16 * 2)))
                {
                    foreach (Chunk chunk in area.Chunks)
                    {
                        chunkX = areaX + ((chunk.X - 1) * 16 * 16) * 2; chunkY = areaY + ((chunk.Y - 1) * 16 * 16) * 2;
                        if (PlayerCollider.D.Intersects(new Rectangle(chunkX, chunkY, 16 * 16 * 2, 16 * 16 * 2)))
                        {
                            foreach (Tile tile in chunk.Tiles)
                            {
                                tileX = chunkX + (((int)tile.position.X) * 16) * 2; tileY = chunkY + (((int)tile.position.Y) * 16) * 2;
                                if (PlayerCollider.D.Intersects(new Rectangle(tileX, tileY, 16 * 2, 16 * 2)) && tile.tileStruct.designation != 0)
                                {
                                    tileXD = tileX; tileYD = tileY;
                                    PlayerCollider.cD = true;
                                    break;
                                }
                            }
                        }
                        if (PlayerCollider.cD) break;
                    }
                }
                if (PlayerCollider.E.Intersects(new Rectangle(areaX, areaY, 64 * 16 * 2, 64 * 16 * 2)))
                {
                    foreach (Chunk chunk in area.Chunks)
                    {
                        chunkX = areaX + ((chunk.X - 1) * 16 * 16) * 2; chunkY = areaY + ((chunk.Y - 1) * 16 * 16) * 2;
                        if (PlayerCollider.E.Intersects(new Rectangle(chunkX, chunkY, 16 * 16 * 2, 16 * 16 * 2)))
                        {
                            foreach (Tile tile in chunk.Tiles)
                            {
                                tileX = chunkX + (((int)tile.position.X) * 16) * 2; tileY = chunkY + (((int)tile.position.Y) * 16) * 2;
                                if (PlayerCollider.E.Intersects(new Rectangle(tileX, tileY, 16 * 2, 16 * 2)) && tile.tileStruct.designation != 0)
                                {
                                    tileXE = tileX; tileYE = tileY;
                                    PlayerCollider.cE = true;
                                    break;
                                }
                            }
                        }
                        if (PlayerCollider.cE) break;
                    }
                }
                if (PlayerCollider.F.Intersects(new Rectangle(areaX, areaY, 64 * 16 * 2, 64 * 16 * 2)))
                {
                    foreach (Chunk chunk in area.Chunks)
                    {
                        chunkX = areaX + ((chunk.X - 1) * 16 * 16) * 2; chunkY = areaY + ((chunk.Y - 1) * 16 * 16) * 2;
                        if (PlayerCollider.F.Intersects(new Rectangle(chunkX, chunkY, 16 * 16 * 2, 16 * 16 * 2)))
                        {
                            foreach (Tile tile in chunk.Tiles)
                            {
                                tileX = chunkX + (((int)tile.position.X) * 16) * 2; tileY = chunkY + (((int)tile.position.Y) * 16) * 2;
                                if (PlayerCollider.F.Intersects(new Rectangle(tileX, tileY, 16 * 2, 16 * 2)) && tile.tileStruct.designation != 0)
                                {
                                    tileXF = tileX; tileYF = tileY;
                                    PlayerCollider.cF = true;
                                    break;
                                }
                            }
                        }
                        if (PlayerCollider.cF) break;
                    }
                }
            }
            /*foreach (Area area in actualLevel.Areas)
            {
                if (xpos >= (area.X - 1) * 64 * 16 * 2 && xpos <= (area.X) * 64 * 16 * 2 && ypos + 40 >= (area.Y - 1) * 64 * 16 * 2 && ypos + 40 <= (area.Y) * 64 * 16 * 2)
                {
                    areaX = (area.X - 1) * 64 * 16 * 2; areaY = (area.Y - 1) * 64 * 16 * 2;
                    foreach (Chunk chunk in area.Chunks)
                    {
                        if (xpos >= areaX + ((chunk.X - 1) * 16 * 16) * 2 && xpos <= areaX + ((chunk.X) * 16 * 16) * 2 && ypos + 40 >= areaY + ((chunk.Y - 1) * 16 * 16) * 2 && ypos + 40 <= areaY + ((chunk.Y) * 16 * 16) * 2)
                        {

                            chunkX = areaX + ((chunk.X - 1) * 16 * 16) * 2; chunkY = areaY + ((chunk.Y - 1) * 16 * 16) * 2;
                            foreach (Tile tile in chunk.Tiles)
                            {
                                if (xpos >= chunkX + (((int)tile.position.X) * 16) * 2 && xpos <= chunkX + (((int)tile.position.X + 1) * 16) * 2 && ypos + 40 >= chunkY + (((int)tile.position.Y) * 16) * 2 && ypos + 40 <= chunkY + (((int)tile.position.Y + 1) * 16) * 2 && tile.tileStruct.designation != 0)
                                {
                                    tileX = chunkX + (((int)tile.position.X) * 16) * 2;
                                    tileW = chunkX + (((int)tile.position.X + 1) * 16) * 2;
                                    tileY = chunkY + (((int)tile.position.Y) * 16) * 2;
                                    tileH = chunkY + (((int)tile.position.Y + 1) * 16) * 2;
                                    ysp = 0;
                                    ypos = tileY - 40;
                                    onGround = true;
                                    break;
                                }
                            }
                        }
                        if (onGround) break;
                    }
                }
                if (onGround) break;
            }*/
            if (PlayerCollider.cE)
            {
                xsp = 0;
                xpos = tileXE + 52;
            }
            else if (PlayerCollider.cF)
            {
                xsp = 0;
                xpos = tileXF - 19 - acc;
            }
            if (PlayerCollider.cA || PlayerCollider.cB)
            {
                ysp = 0;
                if (PlayerCollider.cA && PlayerCollider.cB)
                {
                    if (tileYA > tileYB)
                        ypos = tileYA - 40 + 1;
                    else
                        ypos = tileYB - 40 + 1;
                }
                else if (PlayerCollider.cA)
                    ypos = tileYA - 40 + 1;
                else
                    ypos = tileYB - 40 + 1;
                onGround = true;
            }
            /*
            foreach (Area area in actualLevel.Areas)
            {
                if (PlayerCollider.C.Intersects(new Rectangle(areaX, areaY, 64 * 16 * 2, 64 * 16 * 2)))
                {
                    foreach (Chunk chunk in area.Chunks)
                    {
                        chunkX = areaX + ((chunk.X - 1) * 16 * 16) * 2; chunkY = areaY + ((chunk.Y - 1) * 16 * 16) * 2;
                        if (PlayerCollider.C.Intersects(new Rectangle(chunkX, chunkY, 16 * 16 * 2, 16 * 16 * 2)))
                        {
                            foreach (Tile tile in chunk.Tiles)
                            {
                                tileX = chunkX + (((int)tile.position.X) * 16) * 2; tileY = chunkY + (((int)tile.position.Y) * 16) * 2;
                                if (PlayerCollider.C.Intersects(new Rectangle(tileX, tileY, 16 * 2, 16 * 2)) && tile.tileStruct.designation != 0)
                                {
                                    tileXC = tileX; tileYC = tileY;
                                    PlayerCollider.cC = true;
                                    break;
                                }
                            }
                        }
                        if (PlayerCollider.cC) break;
                    }
                }
                if (PlayerCollider.D.Intersects(new Rectangle(areaX, areaY, 64 * 16 * 2, 64 * 16 * 2)))
                {
                    foreach (Chunk chunk in area.Chunks)
                    {
                        chunkX = areaX + ((chunk.X - 1) * 16 * 16) * 2; chunkY = areaY + ((chunk.Y - 1) * 16 * 16) * 2;
                        if (PlayerCollider.D.Intersects(new Rectangle(chunkX, chunkY, 16 * 16 * 2, 16 * 16 * 2)))
                        {
                            foreach (Tile tile in chunk.Tiles)
                            {
                                tileX = chunkX + (((int)tile.position.X) * 16) * 2; tileY = chunkY + (((int)tile.position.Y) * 16) * 2;
                                if (PlayerCollider.D.Intersects(new Rectangle(tileX, tileY, 16 * 2, 16 * 2)) && tile.tileStruct.designation != 0)
                                {
                                    tileXD = tileX; tileYD = tileY;
                                    PlayerCollider.cD = true;
                                    break;
                                }
                            }
                        }
                        if (PlayerCollider.cD) break;
                    }
                }
            }*/
            if (PlayerCollider.cC || PlayerCollider.cD)
            {
                ysp = 0;
                if (PlayerCollider.cC)
                    ypos = tileYC + 80 - 1;
                else
                    ypos = tileYD + 80 - 1;
            }
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.R))
            {
                xpos = 0; ypos = 0;
            }
            GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);
            ysp += grv;
            if (ysp > 16) ysp = 16;
            if (keyboardState.IsKeyDown(Keys.Right) || gamepadState.IsButtonDown(Buttons.LeftThumbstickRight))
            {
                //if (xsp > -2 && xsp < 0) xsp = -xsp;
                if (xsp < 0) { xsp += dec / 2; if (xsp < -2) brake = true; } else xsp += acc;
            }
            else if (keyboardState.IsKeyDown(Keys.Left) || gamepadState.IsButtonDown(Buttons.LeftThumbstickLeft))
            {
                //if (xsp < 2 && xsp > 0) xsp = -xsp;
                if (xsp > 0) { xsp -= dec / 2; if (xsp > 2) brake = true; } else xsp += -acc;
            }
            else
            {
                if (xsp > 0.51) xsp -= frc;
                if (xsp < -0.51) xsp += frc;
                if (xsp > 0 && xsp < 1) xsp = 0;
                if (xsp < 0 && xsp > -1) xsp = 0;
            }

            if (xsp > 12) xsp = 12; if (xsp < -12) xsp = -12;

            if ((keyboardState.IsKeyDown(Keys.Space) || gamepadState.IsButtonDown(Buttons.A)) && onGround == true)
            {
                onGround = false;
                ysp = -7.5F;
            }
            else CalculateCollide();
            ypos += ysp;
            xpos += xsp;
            if (xsp < 0) dirrection = SpriteEffects.FlipHorizontally;
            if (xsp > 0) dirrection = SpriteEffects.None;
            if (xsp == 0) playerAnimation.SetAnimation("Idle");
            if (keyboardState.IsKeyDown(Keys.Up) || gamepadState.IsButtonDown(Buttons.LeftThumbstickUp)) playerAnimation.SetAnimation("Look Up");
            else if (keyboardState.IsKeyDown(Keys.Down) || gamepadState.IsButtonDown(Buttons.LeftThumbstickDown)) playerAnimation.SetAnimation("Crouch");
            if ((xsp > acc && xsp <= 4) || (xsp < -acc && xsp >= -4)) playerAnimation.SetAnimation("Walk");
            if ((xsp > 4 && xsp <= 7) || (xsp < -4 && xsp >= -7)) playerAnimation.SetAnimation("Jog");
            if ((xsp > 7 && xsp <= 9) || (xsp < -7 && xsp >= -9)) playerAnimation.SetAnimation("Run");
            if ((PlayerCollider.cE || PlayerCollider.cF) && onGround) playerAnimation.SetAnimation("Push");
            if (xsp > 9 || xsp < -9) playerAnimation.SetAnimation("Dash");
            if (brake) playerAnimation.SetAnimation("Skid");
            if (((PlayerCollider.cA && !PlayerCollider.cB && dirrection == SpriteEffects.None) || (!PlayerCollider.cA && PlayerCollider.cB && dirrection == SpriteEffects.FlipHorizontally)) && xsp == 0) playerAnimation.SetAnimation("Balance1");
            if (((!PlayerCollider.cA && PlayerCollider.cB && dirrection == SpriteEffects.None) || (PlayerCollider.cA && !PlayerCollider.cB && dirrection == SpriteEffects.FlipHorizontally)) && xsp == 0) playerAnimation.SetAnimation("Balance2");
            if (!onGround) playerAnimation.SetAnimation("Jump");
            float frameSpeed = 0;
            if (xsp != 0) frameSpeed = xsp; else frameSpeed = 1;
            if (playerAnimation.GetAnimation() == "Jump") frameSpeed = 3;
            if (playerAnimation.GetAnimation() == "Look Up" || playerAnimation.GetAnimation() == "Crouch") frameSpeed = 1;
            playerAnimation.Update(gameTime, frameSpeed);
            if (xsp > 4 || xsp < -4)
            {
                cameraX = GraphicsDeviceManager.DefaultBackBufferWidth * 0.8f / 2 * (dirrection == SpriteEffects.None ? 1 : -1) * ((Math.Abs(xsp) - 2) / 4);
            }
            else
            {
                cameraX = 0;
            }
            if (Math.Abs(cameraX) > GraphicsDeviceManager.DefaultBackBufferWidth * 0.8f / 2)
            {
                cameraX = GraphicsDeviceManager.DefaultBackBufferWidth * 0.8f / 2 * (dirrection == SpriteEffects.None ? 1 : -1);
            }
            playerCamera.LookAt(new Vector2(xpos + cameraX, ypos + cameraY));
            brake = false;
            //ypos += fall;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playerAnimation.PlayAnimation(spriteBatch, new Vector2(xpos, ypos), dirrection, 1);
        }
    }
}