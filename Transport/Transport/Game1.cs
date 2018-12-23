using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

using Roboblob.XNA.WinRT.ResolutionIndependence;
using Roboblob.XNA.WinRT.Input;
using Roboblob.XNA.WinRT.Camera;

namespace Transport
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        private InputHelper inputHelper;
        // Resolution and camera modification
        private ResolutionIndependentRenderer resolutionIndependence;
        private Camera2D camera;
        private Vector2 worldMousePos;
        //private Texture2D bkg;
        //private Vector2 bkgPos;
        private float camRotationDiff = 0.02f;
        private float camZoomDiff = 0.04f;
        private float camMoveDiff = 10f;
        private float camMouseMoveDiff = 0.5f;
        private SpriteBatch spriteBatch;
        private const int TileSize = 100;
        private const int screenWidth = 1000;
        private const int screenHeight = 1000;


        Dictionary<Type, Texture2D> tileTextures = new Dictionary<Type, Texture2D>();

        City city;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            city = new City(10, 10);

            resolutionIndependence = new ResolutionIndependentRenderer(this);
            inputHelper = new InputHelper();
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
            //inputHandler = new InputHandler(city);
            this.IsMouseVisible = true;

            // Set the Resolution - Full Screen
            /*            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
                        graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
                        graphics.IsFullScreen = true;
            */
            // Set the Resolution
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

        }

        /// <summary>
        /// Loads all the tiles
        /// Creates a new city
        /// </summary>
        protected override void LoadContent()
        {
            // Setup camera / resolution
            camera = new Camera2D(resolutionIndependence);
            InitializeResolutionIndependence(screenWidth, screenHeight);
            camera.Zoom = 1f;
            camera.Position = new Vector2(resolutionIndependence.VirtualWidth / 2, resolutionIndependence.VirtualHeight / 2);

            //bkg = Content.Load<Texture2D>(@"background");
            //bkgPos = new Vector2();

            // Load textures
            Texture2D baseTile = Content.Load<Texture2D>("sandtile");
            Texture2D roadTile = Content.Load<Texture2D>("lavatile");
            Texture2D residentialTile = Content.Load<Texture2D>("sandtile");
            Texture2D workTile = Content.Load<Texture2D>("sandtile");
            Texture2D playTile = Content.Load<Texture2D>("sandtile");
            Texture2D shopTile = Content.Load<Texture2D>("sandtile");
            // Create a new SpriteBatch, which can be used to draw textures.
            this.tileTextures[typeof(Tile)] = baseTile;
            this.tileTextures[typeof(RoadTile)] = roadTile;
            this.tileTextures[typeof(ResidentialTile)] = residentialTile;
            this.tileTextures[typeof(WorkTile)] = workTile;
            this.tileTextures[typeof(PlayTile)] = playTile;
            this.tileTextures[typeof(ShopTile)] = shopTile;

            //city.enableGraphics(new SpriteBatch(GraphicsDevice), baseTile, roadTile, residentialTile, workTile, playTile, shopTile);
            spriteBatch = new SpriteBatch(GraphicsDevice);
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
            inputHelper.Update();

            worldMousePos = Vector2.Transform(inputHelper.MousePosition, Matrix.Invert(camera.GetViewTransformationMatrix()));

            if (inputHelper.ExitRequested)
                Exit();
 
            // Left control modifier?
            if (inputHelper.IsCurPress(Keys.LeftControl))
            {
                // Check arrow keys for camera rotation / zoom
                if (inputHelper.IsCurPress(Keys.Left))
                {
                    camera.Rotation += camRotationDiff;
                }
                if (inputHelper.IsCurPress(Keys.Right))
                {
                    camera.Rotation -= camRotationDiff;
                }
                if (inputHelper.IsCurPress(Keys.Up))
                {
                    camera.Zoom += camZoomDiff;
                }
                if (inputHelper.IsCurPress(Keys.Down))
                {
                    camera.Zoom -= camZoomDiff;
                }
            } else
            {
                // Check arrow keys for camera movement
                if (inputHelper.IsCurPress(Keys.Left))
                {
                    camera.Move(new Vector2(-camMoveDiff, 0));
                }
                if (inputHelper.IsCurPress(Keys.Right))
                {
                    camera.Move(new Vector2(camMoveDiff, 0));
                }
                if (inputHelper.IsCurPress(Keys.Up))
                {
                    camera.Move(new Vector2(0, -camMoveDiff));
                }
                if (inputHelper.IsCurPress(Keys.Down))
                { 
                    camera.Move(new Vector2(0, camMoveDiff));
                }

                // Mouse wheel
                if (inputHelper.MouseScrollWheelVelocity > 0)
                {
                    camera.Zoom += camZoomDiff;
                }
                else if (inputHelper.MouseScrollWheelVelocity < 0)
                {
                    camera.Zoom -= camZoomDiff;
                }
                // Mouse left click scroll
                else if (inputHelper.LeftHeld())
                {
                    camera.Move(-inputHelper.MouseVelocity * camMouseMoveDiff);
                }
                // Right click
                else if (inputHelper.IsOldPress(MouseButtons.RightButton))
                {
                    // Determine which tile was clicked
                    int xTile = Convert.ToInt32(Math.Floor(worldMousePos.X / TileSize));
                    int yTile = Convert.ToInt32(Math.Floor(worldMousePos.Y / TileSize));
                    city.changeTile(xTile, yTile, null);
                    //Tile tile = city.getTileAtPos(xTile, yTile);
                    //System.Diagnostics.Debug.WriteLine("x: " + Convert.ToInt32(Math.Floor(worldMousePos.X / TileSize)));

                    //MessageBox.Show("Mouse", "Mouse Position", MessageBoxButtons.OK);

                } else if (inputHelper.IsNewPress(Keys.R))
                {
                    camera.Zoom = 1;
                    camera.Rotation = 0;
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
            resolutionIndependence.BeginDraw();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, camera.GetViewTransformationMatrix());
            //spriteBatch.Draw(bkg, bkgPos, Color.White);
            foreach (Tile tile in city.getTiles()) {
                spriteBatch.Draw(tileTextures[tile.GetType()], new Rectangle(tile.X * TileSize, tile.Y * TileSize, TileSize, TileSize), Color.White);
            }

            spriteBatch.End();
            
            base.Draw(gameTime);
        }

        private void InitializeResolutionIndependence(int realScreenWidth, int realScreenHeight)
        {
            resolutionIndependence.VirtualHeight = 1000;
            resolutionIndependence.VirtualWidth = 1000;
            resolutionIndependence.ScreenWidth = realScreenWidth;
            resolutionIndependence.ScreenHeight = realScreenHeight;
            resolutionIndependence.Initialize();

            camera.RecalculateTransformationMatrices();
        }

        /*
        public void ScreenSizeChanged(Size size)
        {
            InitializeResolutionIndependence((int)size.Width, (int)size.Height);
        }
        */

    }
}
