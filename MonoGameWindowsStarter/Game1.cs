/*
 * Tyler Trammell - Game Project 2
 * 
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Audio;

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
        //Texture2D goalkeeper;
        Rectangle goalRect;
        Rectangle ballRect;
        Random random = new Random();
        Vector2 ballPosition = Vector2.Zero;
        SpriteFont font;
        int score = 0;
        SoundEffect blockSFX;
        SoundEffect booSFX;
        GoalKeeper goalkeeper;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            goalkeeper = new GoalKeeper(this);
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
            ballRect.Y = RandomizeY(ballRect);

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
            goalkeeper.LoadContent();
            font = Content.Load<SpriteFont>("score");
            blockSFX = Content.Load<SoundEffect>("Powerup2");
            booSFX = Content.Load<SoundEffect>("booing");
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
            if (keyboardState.IsKeyDown(Keys.Tab))
            {
                this.Reset();
            }

            goalkeeper.Update(gameTime);

            ballRect.X += random.Next(3, 11); //randomizing speed

            

            if(ballRect.X > graphics.PreferredBackBufferWidth - ballRect.Width)
            {
                ballRect.X = graphics.PreferredBackBufferWidth - ballRect.Width;
                ballPosition.Y = ballRect.Y;
                booSFX.Play();
                score -= 1;
                System.Threading.Thread.Sleep(1250); //wait 1.25 seconds before sending ball back
                SendBallBack();
            }

            if((ballRect.X < goalkeeper.position.X + goalkeeper.getWidth()) && (goalkeeper.position.X < (ballRect.X + ballRect.Width)) && (ballRect.Y < goalkeeper.position.Y + goalkeeper.getHeight()) && (goalkeeper.position.Y < ballRect.Y + ballRect.Height))
            {
                ballRect.X = 0;
                ballRect.Y = RandomizeY(ballRect);
                blockSFX.Play();
                score += 1;
            }
            //logic for goalkeeper to stay onscreen
            //if (goalkeeperRect.Y < 0)
            //{
            //    goalkeeperRect.Y = 0;
            //}
            //if(goalkeeperRect.Y + goalkeeperRect.Height > GraphicsDevice.Viewport.Height)
            //{
            //    goalkeeperRect.Y = GraphicsDevice.Viewport.Height - goalkeeperRect.Height;
            //}
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
            goalkeeper.Draw(spriteBatch);
            spriteBatch.DrawString(font, "Score: " + score, new Vector2(0, 0), Color.Black);
            spriteBatch.End();


            base.Draw(gameTime);
        }

        /// <summary>
        /// Resets the game / score goes back to 0. Used when tab is clicked
        /// </summary>
        private void Reset()
        {
            score = 0;
            this.Initialize();
        }

        /// <summary>
        /// Resetting the game but keeping the score
        /// </summary>
        private void SendBallBack()
        {
            ballRect.Width = 40;
            ballRect.Height = 40;
            ballRect.X = 0;
            ballRect.Y = RandomizeY(ballRect);

            goalkeeper.position.Y = 200;
        }
        /// <summary>
        /// Randomizing the Y value 
        /// </summary>
        /// <param name="rect">rectangle to randomize the y</param>
        /// <returns></returns>
        private int RandomizeY(Rectangle rect)
        {
            rect.Y = random.Next(0, graphics.PreferredBackBufferHeight - ballRect.Height);
            return rect.Y;
        }
    }
}
