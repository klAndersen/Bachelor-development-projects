using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

//kobling til bibliotek for programmet
using BilDemoBibliotek;

namespace Prosjekt1_KnutLucasAndersen {
    /// <summary>
    /// Starter avspilling av bildemoen
    /// </summary>
    public class StartDemo : Microsoft.Xna.Framework.Game {
        #region VARIABLER
        //pre-deklarerte objekter
        private GraphicsDeviceManager graphics;
        private GraphicsDevice device;
        private SpriteBatch spriteBatch;
        //objekter fra biblioteksklassen
        private QuaternionKamera kamera;
        private ModellBehandling modellering;
        private SpriteBehandling spriteBehandling;
        #endregion

        public StartDemo() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //opprett kobling til kamera klassen
            kamera = new QuaternionKamera(this);
            Components.Add(kamera);
            //opprett kobling til modell klassen
            modellering = new ModellBehandling(this);
            Components.Add(modellering);
            //opprett kobling til spritebehandling klassen
            spriteBehandling = new SpriteBehandling(this);
            Components.Add(spriteBehandling);
        } //konstruktør

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            //oversend verdier til Kamera-klassen
            kamera.ViewAngle = MathHelper.PiOver4;
            kamera.AspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;
            kamera.NearPlane = 0.5f;
            kamera.FarPlane = 100.0f;
            base.Initialize();
            //Setter størrelse på framebuffer:
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            //synliggjør musa
            this.IsMouseVisible = true;
        } //Initialize

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            device = graphics.GraphicsDevice;
        } //LoadContent

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        } //UnloadContent

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            base.Update(gameTime);
        } //Update

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            //tøm/rens skjermen
            device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1, 0);
            base.Draw(gameTime);            
        } //Draw
    } //StartDemo
} //namespace