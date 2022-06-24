#region Connecting Libraries / Подключение Библиотек
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Collections.Generic;
#endregion

namespace SonicDronsEngine // Игровой движок "Sonic DronsEngine"
{
    public class SonicGame : Game
    {
        #region Variable Declaration / Объявление Переменных
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D SonicIdle, SonicBored1, SonicBored2, SonicLookUp, SonicLookDown, SonicWalk, SonicAirWalk, SonicJog,
                    SonicRun, SonicDash, SonicPeelout, SonicJump, SonicSpringTwirl, SonicSpringDiagonal, SonicSkid,
                    SonicSkidTurn, SonicSpindash, SonicDropDash, SonicPush, SonicHurt, SonicDie, SonicDrown,
                    SonicBalance1, SonicBalance2, SonicSpringCS, SonicStandCS, SonicVictory, SonicQuttaHere, SonicFan,
                    SonicHang, SonicHangMove, SonicLookDownEnd, SonicLookUpStart, SonicLookUpEnd,
                    VideoTexture,
                    TestMapTileSet;
        VideoManager videoManager;
        Video intro;
        Song TestZoneOST;
        Player Sonic;
        Level TestMap;
        public SpriteFont mainFont;
        bool introP = false, f11Click = false, f9Click = false;
        GameStates currentGameState;
        Camera2D playerCamera;
        ViewportAdapter viewportAdapter;
        DebugScreenStates debugScreenState;
        MouseState mouseState;
        #endregion

        #region Game Class Initialization / Инициализация класса Game
        public SonicGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.IsFullScreen = false;
            //graphics.SynchronizeWithVerticalRetrace = false;
            Content.RootDirectory = "Content";
        }
        #endregion

        #region Initialization / Инициализация
        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            videoManager = new VideoManager(graphics);
            currentGameState = GameStates.Intro;
            viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            playerCamera = new Camera2D(viewportAdapter);
            debugScreenState = DebugScreenStates.Min;
            base.Initialize();
        }
        #endregion

        #region Content Loading / Загрузка Контента
        protected override void LoadContent()
        {
            LoadSpriteSheets();     //Sprite Sheets Loading / Загрузка Спрайтовых Листов
            LoadPlayers();          //Players Loading / Загрузка Игроков
            LoadMusics();           //Music Loading / Загрузка Музыки
            LoadSounds();           //Sounds Loading / Загрузка Звуков
            LoadVideos();           //Videos Loading / Загрузка Видео
            LoadFonts();            //Fonts Loading / Загрузка Шрифтов
            LoadLevels();           //Levels Loading / Загрузка уровней
        }
        #region Loaders / Загрузчики
        void LoadSpriteSheets()
        {
            #region Sonic Sprite Sheets / Спрайтовые листы Соника
            SonicIdle = Content.Load<Texture2D>("Sprites/Player/Sonic/Idle");
            SonicBored1 = Content.Load<Texture2D>("Sprites/Player/Sonic/Bored1");
            SonicBored2 = Content.Load<Texture2D>("Sprites/Player/Sonic/Bored2");
            SonicLookUpStart = Content.Load<Texture2D>("Sprites/Player/Sonic/LookUpStart");
            SonicLookUp = Content.Load<Texture2D>("Sprites/Player/Sonic/LookUp");
            SonicLookUpEnd = Content.Load<Texture2D>("Sprites/Player/Sonic/LookUpEnd");
            SonicLookDown = Content.Load<Texture2D>("Sprites/Player/Sonic/LookDown");
            SonicLookDownEnd = Content.Load<Texture2D>("Sprites/Player/Sonic/LookDownEnd");
            SonicWalk = Content.Load<Texture2D>("Sprites/Player/Sonic/Walk");
            SonicAirWalk = Content.Load<Texture2D>("Sprites/Player/Sonic/AirWalk");
            SonicJog = Content.Load<Texture2D>("Sprites/Player/Sonic/Jog");
            SonicRun = Content.Load<Texture2D>("Sprites/Player/Sonic/Run");
            SonicDash = Content.Load<Texture2D>("Sprites/Player/Sonic/Dash");
            SonicPeelout = Content.Load<Texture2D>("Sprites/Player/Sonic/Peelout");
            SonicJump = Content.Load<Texture2D>("Sprites/Player/Sonic/Jump");
            SonicSpringTwirl = Content.Load<Texture2D>("Sprites/Player/Sonic/SpringTwirl");
            SonicSpringDiagonal = Content.Load<Texture2D>("Sprites/Player/Sonic/SpringDiagonal");
            SonicSkid = Content.Load<Texture2D>("Sprites/Player/Sonic/Skid");
            SonicSkidTurn = Content.Load<Texture2D>("Sprites/Player/Sonic/SkidTurn");
            SonicSpindash = Content.Load<Texture2D>("Sprites/Player/Sonic/Spindash");
            SonicDropDash = Content.Load<Texture2D>("Sprites/Player/Sonic/DropDash");
            SonicPush = Content.Load<Texture2D>("Sprites/Player/Sonic/Push");
            SonicHurt = Content.Load<Texture2D>("Sprites/Player/Sonic/Hurt");
            SonicDie = Content.Load<Texture2D>("Sprites/Player/Sonic/Die");
            SonicDrown = Content.Load<Texture2D>("Sprites/Player/Sonic/Drown");
            SonicBalance1 = Content.Load<Texture2D>("Sprites/Player/Sonic/Balance1");
            SonicBalance2 = Content.Load<Texture2D>("Sprites/Player/Sonic/Balance2");
            SonicSpringCS = Content.Load<Texture2D>("Sprites/Player/Sonic/SpringCS");
            SonicStandCS = Content.Load<Texture2D>("Sprites/Player/Sonic/StandCS");
            SonicVictory = Content.Load<Texture2D>("Sprites/Player/Sonic/Victory");
            SonicQuttaHere = Content.Load<Texture2D>("Sprites/Player/Sonic/QuttaHere");
            SonicFan = Content.Load<Texture2D>("Sprites/Player/Sonic/Fan");
            SonicHang = Content.Load<Texture2D>("Sprites/Player/Sonic/Hang");
            SonicHangMove = Content.Load<Texture2D>("Sprites/Player/Sonic/HangMove");
            #endregion
            #region Level Tilesets / Тайлсеты уровней
            //TestMapTileSet = Content.Load<Texture2D>("Sprites/Level/TestMap/chr000");
            #endregion
        }
        void LoadPlayers()
        {
            #region Sonic / Соник
            Sonic = new Player(0, 0 * 16 * 16 * 2 + 0 * 16 * 2, playerCamera);
            Sonic.playerAnimation.AddAnimation("Idle", 48, 48, SonicIdle, 24, 28);
            Sonic.playerAnimation.AddAnimation("Bored 1", 48, 48, SonicBored1, 24, 28);
            Sonic.playerAnimation.AddAnimation("Bored 2", 48, 48, SonicBored2, 24, 28);
            Sonic.playerAnimation.AddAnimation("Look Up Start", 48, 48, SonicLookUpStart, 24, 28);
            Sonic.playerAnimation.AddAnimation("Look Up", 48, 48, SonicLookUp, 24, 28);
            Sonic.playerAnimation.AddAnimation("Look Up End", 48, 48, SonicLookUpEnd, 24, 28);
            Sonic.playerAnimation.AddAnimation("Crouch", 48, 48, SonicLookDown, 24, 28);
            Sonic.playerAnimation.AddAnimation("Crouch End", 48, 48, SonicLookDownEnd, 24, 28, 5);
            Sonic.playerAnimation.AddAnimation("Walk", 48, 48, SonicWalk, 24, 28);
            Sonic.playerAnimation.AddAnimation("Air Walk", 48, 56, SonicAirWalk, 24, 28);
            Sonic.playerAnimation.AddAnimation("Jog", 48, 48, SonicJog, 24, 28);
            Sonic.playerAnimation.AddAnimation("Run", 48, 48, SonicRun, 24, 28);
            Sonic.playerAnimation.AddAnimation("Dash", 48, 48, SonicDash, 24, 28);
            Sonic.playerAnimation.AddAnimation("Peelout", 48, 48, SonicPeelout, 24, 28);
            Sonic.playerAnimation.AddAnimation("Jump", 48, 48, SonicJump, 24, 28);
            Sonic.playerAnimation.AddAnimation("Spring Twirl", 48, 56, SonicSpringTwirl, 24, 28);
            Sonic.playerAnimation.AddAnimation("Spring Diagonal", 48, 56, SonicSpringDiagonal, 24, 28);
            Sonic.playerAnimation.AddAnimation("Skid", 48, 48, SonicSkid, 24, 28);
            Sonic.playerAnimation.AddAnimation("Skid Turn", 48, 48, SonicSkidTurn, 24, 28);
            Sonic.playerAnimation.AddAnimation("Spindash", 48, 48, SonicSpindash, 24, 28);
            Sonic.playerAnimation.AddAnimation("Drop Dash", 48, 48, SonicDropDash, 24, 28);
            Sonic.playerAnimation.AddAnimation("Push", 48, 48, SonicPush, 24, 28);
            Sonic.playerAnimation.AddAnimation("Hurt", 48, 48, SonicHurt, 24, 28);
            Sonic.playerAnimation.AddAnimation("Die", 48, 56, SonicDie, 24, 28);
            Sonic.playerAnimation.AddAnimation("Drown", 48, 56, SonicDrown, 24, 28);
            Sonic.playerAnimation.AddAnimation("Balance1", 56, 48, SonicBalance1, 28, 28);
            Sonic.playerAnimation.AddAnimation("Balance2", 48, 48, SonicBalance2, 24, 28);
            Sonic.playerAnimation.AddAnimation("Spring CS", 48, 48, SonicSpringCS, 24, 28);
            Sonic.playerAnimation.AddAnimation("Stand CS", 48, 48, SonicStandCS, 24, 28);
            Sonic.playerAnimation.AddAnimation("Victory", 48, 64, SonicVictory, 24, 32);
            Sonic.playerAnimation.AddAnimation("Qutta Here", 48, 56, SonicQuttaHere, 24, 28);
            Sonic.playerAnimation.AddAnimation("Fan", 64, 48, SonicFan, 32, 24);
            Sonic.playerAnimation.AddAnimation("Hang", 48, 48, SonicHang, 24, 28);
            Sonic.playerAnimation.AddAnimation("Hang Move", 48, 56, SonicHangMove, 24, 28);
            Sonic.playerAnimation.SetAnimation("Idle");
            #endregion
        }
        void LoadMusics()
        {
            TestZoneOST = Content.Load<Song>("OST/TestZoneOST");
        }
        void LoadSounds()
        {

        }
        void LoadVideos()
        {
            #region Intro Movie / Вступительный Ролик
            intro = Content.Load<Video>("Video/Intro");
            #endregion
        }
        void LoadFonts()
        {
            #region Simple Out Font / Стандартный шрифт для вывода
            mainFont = Content.Load<SpriteFont>("Fonts/Main");
            #endregion
        }
        void LoadLevels()
        {
            TestMap = new Level("TestMap", Content);
            Sonic.IncludeLevel(TestMap);
        }
        #endregion
        #endregion

        #region Content Unloading / Выгрузка Контента
        protected override void UnloadContent()
        {
        }
        #endregion

        #region Game Logic Loop / Цикл Игровой Логики
        #region Game States Switcher / Переключение Игровых Состояний
        protected override void Update(GameTime gameTime)
        {
            #region FullscreenSwitcher
            if (Keyboard.GetState().IsKeyDown(Keys.F11) && !f11Click)
            {
                f11Click = true;
                if (graphics.IsFullScreen == true)
                {
                    graphics.IsFullScreen = false;
                    graphics.PreferredBackBufferWidth = 800;
                    graphics.PreferredBackBufferHeight = 600;
                    //viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                    graphics.ApplyChanges();
                }
                else
                {
                    graphics.IsFullScreen = true;
                    graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                    graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                    //viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                    graphics.ApplyChanges();
                }
            }
            if (Keyboard.GetState().IsKeyUp(Keys.F11) && f11Click)
            {
                f11Click = false;
            }
            #endregion
            #region DebugScreenSwitcher
            if (Keyboard.GetState().IsKeyDown(Keys.F9) && !f9Click)
            {
                f9Click = true;
                debugScreenState++;
                if ((int)debugScreenState >= 4)
                {
                    debugScreenState = 0;
                }
            }
            if (Keyboard.GetState().IsKeyUp(Keys.F9) && f9Click)
            {
                f9Click = false;
            }
            #endregion
            switch (currentGameState)
            {
                case GameStates.Intro:
                    UpdateIntro(gameTime);
                    break;
                case GameStates.MainMenu:
                    UpdateMainMenu(gameTime);
                    break;
                case GameStates.Game:
                    UpdateGame(gameTime);
                    break;
                case GameStates.Pause:
                    UpdatePause(gameTime);
                    break;
            }
            mouseState = Mouse.GetState();
            base.Update(gameTime);
        }
        #endregion

        #region Intro Game Logic Loop / Цикл Игровой Логики Интро
        public void UpdateIntro(GameTime gameTime)
        {
            if (videoManager.videoPlayer.State == MediaState.Stopped && !introP)
            {
                while (!introP)
                {
                    try
                    {
                        videoManager.videoPlayer.Play(intro);
                        introP = true;
                    }
                    catch
                    {
                        MediaPlayer.Stop();
                        videoManager.videoPlayer.Stop();
                    }
                }
            }
            if (videoManager.videoPlayer.State == MediaState.Stopped && introP)
            {
                currentGameState = GameStates.MainMenu;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) || GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed) { currentGameState = GameStates.MainMenu; videoManager.videoPlayer.Stop(); }
        }
        #endregion
        #region Main Menu Game Logic Loop / Цикл Игровой Логики Главного Меню
        public void UpdateMainMenu(GameTime gameTime)
        {
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.3f;
            MediaPlayer.Play(TestZoneOST);
            currentGameState = GameStates.Game;
        }
        #endregion
        #region Basic Game Logic Loop / Главный Цикл Игровой Логики
        public void UpdateGame(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Sonic.Update(gameTime);
        }
        #endregion
        #region Pause Menu Game Logic Loop / Цикл Игровой Логики Меню Паузы
        public void UpdatePause(GameTime gameTime)
        {

        }
        #endregion
        #endregion

        #region Rendering Loop / Цикл Рендеринга
        #region Game States Switcher / Переключение Игровых Состояний
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            var transformMatrix = Sonic.playerCamera.GetViewMatrix();
            switch (currentGameState)
            {
                case GameStates.Intro:
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null, RasterizerState.CullNone);
                    DrawIntro(gameTime);
                    spriteBatch.End();
                    break;
                case GameStates.MainMenu:
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null, RasterizerState.CullNone);
                    DrawMainMenu(gameTime);
                    spriteBatch.End();
                    break;
                case GameStates.Game:
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null, RasterizerState.CullNone, null, transformMatrix);
                    DrawGame(gameTime);
                    spriteBatch.End();
                    break;
                case GameStates.Pause:
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null, RasterizerState.CullNone);
                    DrawPause(gameTime);
                    spriteBatch.End();
                    break;
            }
            base.Draw(gameTime);
        }
        #endregion

        #region Intro Rendering Loop / Цикл Рендеринга Интро
        public void DrawIntro(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            videoManager.PlayVideoOnFullScreen(spriteBatch, intro);
            spriteBatch.DrawString(mainFont, "Intro", Vector2.Zero, Color.White);
        }
        #endregion
        #region Main Menu Rendering Loop / Цикл Рендеринга Главного Меню
        public void DrawMainMenu(GameTime gameTime)
        {
            spriteBatch.DrawString(mainFont, "Main Menu", Vector2.Zero, Color.White);
        }
        #endregion
        #region Basic Rendering Loop / Главный Цикл Рендеринга
        public void DrawGame(GameTime gameTime)
        {
            TestMap.Draw(spriteBatch);
            Sonic.Draw(spriteBatch);
            switch (debugScreenState)
            {
                case DebugScreenStates.Off:
                    break;
                case DebugScreenStates.Min:
                    spriteBatch.DrawPoint(Sonic.playerCamera.Position.X + mouseState.X, Sonic.playerCamera.Position.Y + mouseState.Y, Color.Red, 4);
                    spriteBatch.DrawString(mainFont, (Sonic.playerCamera.Position.X + mouseState.X) + ":" + (Sonic.playerCamera.Position.Y + mouseState.Y), new Vector2(Sonic.playerCamera.Position.X + mouseState.X + 2, Sonic.playerCamera.Position.Y + mouseState.Y), Color.White);
                    spriteBatch.DrawString(mainFont, "Game", Sonic.playerCamera.Position, Color.White);
                    spriteBatch.DrawString(mainFont, $"Camera Position: [{Math.Round(Sonic.playerCamera.Position.X, 2)}, {Math.Round(Sonic.playerCamera.Position.Y, 2)}]", new Vector2(Sonic.playerCamera.Position.X, Sonic.playerCamera.Position.Y + 20), Color.White);
                    break;
                case DebugScreenStates.Actual:
                    spriteBatch.DrawPoint(Sonic.playerCamera.Position.X + mouseState.X, Sonic.playerCamera.Position.Y + mouseState.Y, Color.Red, 4);
                    spriteBatch.DrawString(mainFont, (Sonic.playerCamera.Position.X + mouseState.X) + ":" + (Sonic.playerCamera.Position.Y + mouseState.Y), new Vector2(Sonic.playerCamera.Position.X + mouseState.X + 2, Sonic.playerCamera.Position.Y + mouseState.Y), Color.White);
                    spriteBatch.DrawString(mainFont, "Game", Sonic.playerCamera.Position, Color.White);
                    spriteBatch.DrawString(mainFont, $"Camera Position: [{Math.Round(Sonic.playerCamera.Position.X, 2)}, {Math.Round(Sonic.playerCamera.Position.Y, 2)}]", new Vector2(Sonic.playerCamera.Position.X, Sonic.playerCamera.Position.Y + 20), Color.White);
                    spriteBatch.DrawString(mainFont, $"Screen Resolution: [{graphics.PreferredBackBufferWidth}, {graphics.PreferredBackBufferHeight}]", Sonic.playerCamera.Position + new Vector2(0, 40), Color.White);
                    break;
                case DebugScreenStates.Full:
                    spriteBatch.DrawRectangle(new Rectangle(Sonic.tileXA, Sonic.tileYA, Sonic.tileW, Sonic.tileH), Color.Green, 5);
                    spriteBatch.DrawRectangle(new Rectangle(Sonic.tileXB, Sonic.tileYB, Sonic.tileW, Sonic.tileH), Color.GreenYellow, 5);
                    spriteBatch.DrawRectangle(new Rectangle(Sonic.tileXC, Sonic.tileYC, Sonic.tileW, Sonic.tileH), Color.Blue, 5);
                    spriteBatch.DrawRectangle(new Rectangle(Sonic.tileXD, Sonic.tileYD, Sonic.tileW, Sonic.tileH), Color.Yellow, 5);
                    spriteBatch.DrawRectangle(new Rectangle(Sonic.tileXE, Sonic.tileYE, Sonic.tileW, Sonic.tileH), Color.Red, 5);
                    spriteBatch.DrawRectangle(new Rectangle(Sonic.tileXF, Sonic.tileYF, Sonic.tileW, Sonic.tileH), Color.Pink, 5);
                    spriteBatch.DrawRectangle(Sonic.PlayerCollider.A, Color.Green, 1);
                    spriteBatch.DrawRectangle(Sonic.PlayerCollider.B, Color.GreenYellow, 1);
                    spriteBatch.DrawRectangle(Sonic.PlayerCollider.C, Color.Blue, 1);
                    spriteBatch.DrawRectangle(Sonic.PlayerCollider.D, Color.Yellow, 1);
                    spriteBatch.DrawRectangle(Sonic.PlayerCollider.E, Color.Red, 1);
                    spriteBatch.DrawRectangle(Sonic.PlayerCollider.F, Color.Pink, 1);
                    spriteBatch.DrawPoint(Sonic.xpos, Sonic.ypos, Color.White, 4);
                    spriteBatch.DrawPoint(Sonic.playerCamera.Position.X + mouseState.X, Sonic.playerCamera.Position.Y + mouseState.Y, Color.Red, 4);
                    spriteBatch.DrawString(mainFont, (Sonic.playerCamera.Position.X + mouseState.X) + ":" + (Sonic.playerCamera.Position.Y + mouseState.Y), new Vector2(Sonic.playerCamera.Position.X + mouseState.X + 2, Sonic.playerCamera.Position.Y + mouseState.Y), Color.White);
                    spriteBatch.DrawString(mainFont, "Game", Sonic.playerCamera.Position, Color.White);
                    spriteBatch.DrawString(mainFont, $"Camera Position: [{Math.Round(Sonic.playerCamera.Position.X, 2)}, {Math.Round(Sonic.playerCamera.Position.Y, 2)}]", new Vector2(Sonic.playerCamera.Position.X, Sonic.playerCamera.Position.Y + 20), Color.White);
                    spriteBatch.DrawString(mainFont, $"Screen Resolution: [{graphics.PreferredBackBufferWidth}, {graphics.PreferredBackBufferHeight}]", Sonic.playerCamera.Position + new Vector2(0, 40), Color.White);
                    spriteBatch.DrawString(mainFont, $"Player Position: [{Sonic.xpos}, {Sonic.ypos}]", Sonic.playerCamera.Position + new Vector2(0, 60), Color.White);
                    spriteBatch.DrawString(mainFont, $"Player Speed: [{Sonic.xsp}, {Sonic.ysp}]", Sonic.playerCamera.Position + new Vector2(0, 80), Color.White);
                    break;
            }
        }
        #endregion
        #region Pause Menu Rendering Loop / Цикл Рендеринга Меню Паузы
        public void DrawPause(GameTime gameTime)
        {
            spriteBatch.DrawString(mainFont, "Pause Menu", Vector2.Zero, Color.White);
        }
        #endregion
        #endregion
    }

    #region Additional Structures / Дополнительные структуры
    #region Game States Enumeration / Перечисление Игровых Состояний
    public enum GameStates
    {
        Intro,
        MainMenu,
        Game,
        Pause
    }
    #endregion
    #region Debug Screen States Enumeration / Перечисление Состояний Экрана Разаработчика
    enum DebugScreenStates
    {
        Off,
        Min,
        Actual,
        Full
    }
    #endregion
    #endregion
}