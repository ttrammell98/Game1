using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameWindowsStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D ball;
        Texture2D goal;
        Texture2D goalkeeper;
        Rectangle goalRect;
        Rectangle ballRect;
        Rectangle goalkeeperRect;
       
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 720;
            graphics.PreferredBackBufferHeight = 480;
            graphics.ApplyChanges();

            //goal rect settings (whole right side of screen)
            goalRect.Width = 100;
            goalRect.Height = graphics.PreferredBackBufferHeight;
            goalRect.X = graphics.PreferredBackBufferWidth - goalRect.Width;
            goalRect.Y = 0;

            //ball rect settings (left side middle of screen)
            ballRect.Width = 40;
            ballRect.Height = 40;
            ballRect.X = 0;
            ballRect.Y = 220;

            goalkeeperRect.Width = 90;
            goalkeeperRect.Height = 80;
            goalkeeperRect.X = graphics.PreferredBackBufferWidth - goalRect.Width - goalkeeperRect.Width;
            goalkeeperRect.Y = 198;

            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            goal = Content.Load<Texture2D>("soccer-goal-top-png");
            ball = Content.Load<Texture2D>("ball");
            goalkeeper = Content.Load<Texture2D>("goalie");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(goal, goalRect, Color.White);
            spriteBatch.Draw(ball, ballRect, Color.White);
            spriteBatch.Draw(goalkeeper, goalkeeperRect, Color.White);
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
