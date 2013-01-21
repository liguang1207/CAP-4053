using System;
using System.Linq;
using System.Collections;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;

using The_Dungeon.BLL;

namespace The_Dungeon
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class TDGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<Actor> WorldActors = new List<Actor>();

        //Some Content
        SpriteFont DebugFont;

        Boolean bDebug = true;

        public TDGame()
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
            //Player
            Texture2D PlayerTexture = Content.Load<Texture2D>("Player");
            ControllableActor Player = new ControllableActor(PlayerTexture, new Rectangle(0, 0, PlayerTexture.Width, PlayerTexture.Height), Color.SlateGray);
            Player.Position = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
            Player.ToggleDebug(bDebug);
            WorldActors.Add(Player);
            

            //Walls
            Texture2D WallTexture = Content.Load<Texture2D>("Wall");
            BlockingActor Wall = new BlockingActor(WallTexture, new Rectangle(0, 0, WallTexture.Width, WallTexture.Height), Color.SlateGray);
            Wall.Position = new Vector2(40, 40);
            Wall.ToggleDebug(bDebug);
            WorldActors.Add(Wall);

            Wall = new BlockingActor(WallTexture, new Rectangle(0, 0, WallTexture.Width, WallTexture.Height), Color.SlateGray);
            Wall.Position = new Vector2(70, 90);
            Wall.Rotation = 7.5f;
            Wall.ToggleDebug(bDebug);
            WorldActors.Add(Wall);

            //Enemies
            Texture2D EnemyTexture = Content.Load<Texture2D>("Enemy");
            ControllableActor Enemy = new ControllableActor(EnemyTexture, new Rectangle(0, 0, EnemyTexture.Width, EnemyTexture.Height), Color.SlateGray);
            Enemy.Position = new Vector2(100, 100);
            Enemy.ToggleDebug(bDebug);
            Enemy.SetController(null);
            WorldActors.Add(Enemy);

            Enemy = new ControllableActor(EnemyTexture, new Rectangle(0, 0, EnemyTexture.Width, EnemyTexture.Height), Color.SlateGray);
            Enemy.Position = new Vector2(100, 200);
            Enemy.ToggleDebug(bDebug);
            Enemy.SetController(null);
            WorldActors.Add(Enemy);


            //Debug Font
            DebugFont = Content.Load<SpriteFont>("Debug Font");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            WorldActors.Clear();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            foreach (Actor A in WorldActors)
            {
                if (A != null)
                {
                    A.Update(gameTime);
                }
            }

            // Check Collisions
            foreach (Actor A in WorldActors)
            {
                foreach (Actor B in WorldActors)
                {
                    if (A != null && B != null && A.Collision == Actor.CollisionType.Solid && B.Collision == Actor.CollisionType.Solid && A != B)
                    {
                        A.CheckCollision(B.CollisionRectangle);
                    }
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SlateGray);
            
            // TODO: Add your drawing code here
            foreach (Actor A in WorldActors)
            {
                if (A != null)
                {
                    A.Draw(ref spriteBatch);
                }
            }

            base.Draw(gameTime);
        }
    }
}