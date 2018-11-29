﻿using Microsoft.Xna.Framework;
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
        private Vector2 screenMousePos;
        private Vector2 mouseDrawPos;
        private Texture2D bkg;
        private Vector2 bkgPos;
        private float rotationDiff = 0.02f;
        private float zoomDiff = 0.02f;
        private SpriteBatch spriteBatch;

        Dictionary<Type, Texture2D> tileTextures = new Dictionary<Type, Texture2D>();

        City city;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            city = new City(100, 100);

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

            // Set the Resolution
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = true;
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
            camera.Zoom = 1f;
            camera.Position = new Vector2(resolutionIndependence.VirtualWidth / 2, resolutionIndependence.VirtualHeight / 2);

            InitializeResolutionIndependence(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            mouseDrawPos.X = 700;
            mouseDrawPos.Y = 40f;
            //bkg = Content.Load<Texture2D>(@"background");
            bkgPos = new Vector2();

            // Load textures
            Texture2D baseTile = Content.Load<Texture2D>("sandtile");
            Texture2D roadTile = Content.Load<Texture2D>("sandtile");
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

            screenMousePos = resolutionIndependence.ScaleMouseToScreenCoordinates(inputHelper.MousePosition);

            if (inputHelper.ExitRequested)
                Exit();

            if (inputHelper.IsCurPress(Keys.Add))
            {
                if (inputHelper.IsCurPress(Keys.LeftShift))
                    camera.Rotation += rotationDiff;
                else
                    camera.Zoom += zoomDiff;
            }
            else if (inputHelper.IsCurPress(Keys.Subtract))
            {
                if (inputHelper.IsCurPress(Keys.LeftShift))
                    camera.Rotation -= rotationDiff;
                else
                    camera.Zoom -= zoomDiff;
            }
            else if (inputHelper.IsNewPress(Keys.R))
            {
                camera.Zoom = 1;
                camera.Rotation = 0;
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

            foreach (Tile tile in city.getTiles())
            {
                spriteBatch.Draw(tileTextures[tile.GetType()], new Rectangle(tile.X * 100, tile.Y * 100, 100, 100), Color.White);
            }

            spriteBatch.End();
            
            base.Draw(gameTime);
        }

        private void InitializeResolutionIndependence(int realScreenWidth, int realScreenHeight)
        {
            resolutionIndependence.VirtualWidth = 1366;
            resolutionIndependence.VirtualHeight = 768;
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