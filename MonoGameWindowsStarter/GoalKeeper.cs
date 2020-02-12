﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MonoGameWindowsStarter
{
    /// <summary>
    /// An enum representing the states the player can be in
    /// </summary>
    enum State
    {
        South = 0,
        East = 1,
        West = 2,
        North = 3,
        Idle = 4,
    }

    /// <summary>
    /// A class representing a player
    /// </summary>
    public class GoalKeeper
    {
        /// <summary>
        /// How quickly the animation should advance frames (1/8 second as milliseconds)
        /// </summary>
        const int ANIMATION_FRAME_RATE = 124;

        /// <summary>
        /// How quickly the player should move
        /// </summary>
        const float PLAYER_SPEED = 180;

        /// <summary>
        /// The width of the animation frames
        /// </summary>
        public const int FRAME_WIDTH = 49;

        /// <summary>
        /// The hieght of the animation frames
        /// </summary>
        public const int FRAME_HEIGHT = 64;


        // Private variables
        Game1 game;
        Texture2D texture;
        State state;
        TimeSpan timer;
        int frame;
        public Vector2 position;
    

        /// <summary>
        /// Creates a new player object
        /// </summary>
        /// <param name="game"></param>
        public GoalKeeper(Game1 game)
        {
            this.game = game;
            timer = new TimeSpan(0);
            this.position = new Vector2(571, 200);
            state = State.Idle;
        }

        /// <summary>
        /// gets the width of the goalkeeper's frame
        /// </summary>
        /// <returns></returns>
        public int getWidth()
        {
            return FRAME_WIDTH;
        }

        /// <summary>
        /// gets the height of the goalkeeper's frame
        /// </summary>
        /// <returns></returns>
        public int getHeight()
        {
            return FRAME_HEIGHT;
        }
        /// <summary>
        /// Loads the sprite's content
        /// </summary>
        public void LoadContent()
        {
            texture = game.Content.Load<Texture2D>("spritesheet");
        }

        /// <summary>
        /// Update the sprite, moving and animating it
        /// </summary>
        /// <param name="gameTime">The GameTime object</param>
        public void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Update the player state based on input
            if (keyboard.IsKeyDown(Keys.Up))
            {
                state = State.North;
                position.Y -= delta * PLAYER_SPEED;
            }
            else if (keyboard.IsKeyDown(Keys.Down))
            {
                state = State.South;
                position.Y += delta * PLAYER_SPEED;
            }
            else state = State.Idle;

            // Update the player animation timer when the player is moving
            if (state != State.Idle) timer += gameTime.ElapsedGameTime;

            // Determine the frame should increase.  Using a while 
            // loop will accomodate the possiblity the animation should 
            // advance more than one frame.
            while (timer.TotalMilliseconds > ANIMATION_FRAME_RATE)
            {
                // increase by one frame
                frame++;
                // reduce the timer by one frame duration
                timer -= new TimeSpan(0, 0, 0, 0, ANIMATION_FRAME_RATE);
            }

            // Keep the frame within bounds (there are four frames)
            frame %= 4;

            //logic to keep goalkeeper on screen
            if(position.Y < 0)
            {
                position.Y = 0;
            }
            if(position.Y + FRAME_HEIGHT > 480)
            {
                position.Y = 480 - FRAME_HEIGHT;
            }
        }

        /// <summary>
        /// Renders the sprite on-screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // determine the source rectagle of the sprite's current frame
            var source = new Rectangle(
                frame * FRAME_WIDTH, // X value 
                (int)state % 4 * FRAME_HEIGHT, // Y value
                FRAME_WIDTH, // Width 
                FRAME_HEIGHT // Height
                );

            // render the sprite
            spriteBatch.Draw(texture, position, source, Color.White);
        }

    }
}
