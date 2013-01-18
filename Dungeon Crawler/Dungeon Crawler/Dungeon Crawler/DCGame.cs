using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections;

using Dungeon_Crawler.BLL;

namespace Dungeon_Crawler
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class DCGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont calibri;

        DCPlayer Player = null;
        ArrayList Actors = new ArrayList();
        int iCurrentIndex = 0;

        DateTime KeyboardDelay = DateTime.Now;

        public DCGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //graphics.IsFullScreen = true;
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

            calibri = Content.Load<SpriteFont>("PericlesSpriteFont");

            Texture2D PlayerAvatar = Content.Load<Texture2D>("Player");
            Player = new DCPlayer(new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), "Player", ref spriteBatch, PlayerAvatar);
            Actors.Add(Player);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Player = null;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            
            Player.Tick(gameTime);


            if (DateTime.Now.Subtract(KeyboardDelay).TotalSeconds > 1.5)
            {
                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Z))
                {
                    DCPlayer P = Actors[iCurrentIndex] as DCPlayer;
                    P.HumanControlled = false;

                    iCurrentIndex = (iCurrentIndex + 1 >= Actors.Count ? 0 : iCurrentIndex + 1);
                    P = Actors[iCurrentIndex] as DCPlayer;
                    P.HumanControlled = true;

                    KeyboardDelay = DateTime.Now;
                }
                else if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.X))
                {
                    Random R = new Random();
                    Vector2 Location = new Vector2(R.Next(0, GraphicsDevice.Viewport.Width - 64), R.Next(0, GraphicsDevice.Viewport.Height - 64));

                    Texture2D EnemyAvatar = Content.Load<Texture2D>("Enemy");
                    DCEnemy E = new DCEnemy(Location, new Vector2(0, 0), new Vector2(0, 0), "Actor_" + Actors.Count.ToString(), ref spriteBatch, EnemyAvatar);

                    Actors.Add(E);

                    KeyboardDelay = DateTime.Now;
                }
            }

            for (int i = 0; i < Actors.Count; i++)
            {
                DCPlayer P = Actors[i] as DCPlayer;
                P.Update(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Update Objects
            for (int i = 0; i < Actors.Count; i++)
            {
                DCPlayer P = Actors[i] as DCPlayer;
                P.Draw(ref spriteBatch);
            }

            //Game Drawing
            spriteBatch.Begin();

            spriteBatch.DrawString(calibri, Player.GetDebugInformation(), new Vector2(0, 0), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
