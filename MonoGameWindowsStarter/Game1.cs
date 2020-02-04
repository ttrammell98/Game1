using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MonoGameWindowsStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState keyboardState;
        Texture2D ball;
        Texture2D goal;
        Texture2D goalkeeper;
        Rectangle goalRect;
        Rectangle ballRect;
        Rectangle goalkeeperRect;
        int goalSpeed = 6;
        Random random = new Random();
        //Vector2 ballVelocity;
        Vector2 ballPosition = Vector2.Zero;
        SpriteFont font;
        int score = 0;

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

            //ballVelocity = new Vector2((float)random.Next(10), (float)random.NextDouble());
            //ballVelocity.Normalize();

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
            font = Content.Load<SpriteFont>("score");
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
            keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            if (keyboardState.IsKeyDown(Keys.L))
            {
                this.Reset();
            }

            //up and down buttons for gameplay
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                goalkeeperRect.Y += goalSpeed;
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                goalkeeperRect.Y -= goalSpeed;
            }


            ballRect.X += 5;
            

            //checking for wall collisions with ball
            if(ballPosition.Y < 0)
            {
                //ballVelocity.Y *= -1;
                float delta = 0 - ballPosition.Y;
                ballPosition.Y += 2 * delta;
            }
            if(ballPosition.Y > graphics.PreferredBackBufferHeight - ballRect.Height)
            {
                //ballVelocity.Y *= -1;
                float delta = graphics.PreferredBackBufferHeight - ballRect.Height - ballPosition.Y;
                ballPosition.Y += 2 * delta;
            }
            if(ballPosition.X > graphics.PreferredBackBufferWidth - ballRect.Width)
            {
                ballPosition.X = graphics.PreferredBackBufferWidth - ballRect.Width;
                ballPosition.Y = ballRect.Y;
            }
            //ballRect.X = (int)ballPosition.X;
            //ballRect.Y = (int)ballPosition.Y;
            //checking ball collision with goal keeper
            if (!(ballRect.X > goalkeeperRect.X + goalkeeperRect.Width || ballRect.X + ballRect.Width < goalkeeperRect.X || ballRect.Y > goalkeeperRect.Height || ballRect.Y + ballRect.Height < goalkeeperRect.Y)) //colliding
            {
                ballPosition.X = 0;
                ballPosition.Y = 215; 
                score += 1;
            }



            



            //logic for goalkeeper to stay onscreen
            if (goalkeeperRect.Y < 0)
            {
                goalkeeperRect.Y = 0;
            }
            if(goalkeeperRect.Y + goalkeeperRect.Height > GraphicsDevice.Viewport.Height)
            {
                goalkeeperRect.Y = GraphicsDevice.Viewport.Height - goalkeeperRect.Height;
            }

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
            spriteBatch.DrawString(font, "Score: " + score, new Vector2(0, 0), Color.Black);
            spriteBatch.End();


            base.Draw(gameTime);
        }

        private void Reset()
        {
            score = 0;
            this.Initialize();
        }
    }
}
