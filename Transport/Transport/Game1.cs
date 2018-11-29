using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Transport
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        InputHandler inputHandler;
        City city;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            city = new City(100, 100);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            inputHandler = new InputHandler(city);
            this.IsMouseVisible = true;

            // Set the Resolution
            //graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            //graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 1000;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

        }

        /// <summary>
        /// Loads all the tiles
        /// Creates a new city
        /// </summary>
        protected override void LoadContent()
        {
            Texture2D baseTile = Content.Load<Texture2D>("sandtile");
            Texture2D roadTile = Content.Load<Texture2D>("sandtile");
            Texture2D residentialTile = Content.Load<Texture2D>("sandtile");
            Texture2D workTile = Content.Load<Texture2D>("sandtile");
            Texture2D playTile = Content.Load<Texture2D>("sandtile");
            Texture2D shopTile = Content.Load<Texture2D>("sandtile");
            city.enableGraphics(new SpriteBatch(GraphicsDevice), baseTile, roadTile, residentialTile, workTile, playTile, shopTile);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            Boolean finish = inputHandler.handleInput(Mouse.GetState(), Keyboard.GetState());
            if (finish)
                Exit();

            base.Update(gameTime);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            city.DrawTiles(gameTime);
            
            base.Draw(gameTime);
        }
    }
}
