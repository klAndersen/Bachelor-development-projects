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

/*
 * Jeg har valgt å bruke koden fra Øving 1 med trekant som grunnlag for 
 * oppgaven, men har gjort endringer som kreves for å fullføre oppgave 1a.
 */

namespace Oblig1B_KnutLucasAndersen {
    /// <summary>
    /// Et XNA klasse som tegne opp en enkel trekant.
    /// </summary>
    public class Oblig1B : Microsoft.Xna.Framework.Game {
        private GraphicsDeviceManager graphics;
        private ContentManager content;
        private GraphicsDevice device;

        private BasicEffect effect;

        //Liste med vertekser:
        private VertexPositionColor[] verteksSider;
        private VertexPositionColor[] verteksTopp;
        private VertexPositionColor[] verteksBunn;

        //WVP-matrisene:
        private Matrix world;
        private Matrix projection;
        private Matrix view;

        //Kameraposisjon:
        private Vector3 cameraPosition = new Vector3(0.0f, 0.0f, 5.0f);
        private Vector3 cameraTarget = Vector3.Zero;
        private Vector3 cameraUpVector = new Vector3(3.5f, 2.0f, 5.0f);

        SpriteBatch spriteBatch;

        /// <summary>
        /// Konstruktør. Henter ut et graphics-objekt.
        /// </summary>
        public Oblig1B() {
            graphics = new GraphicsDeviceManager(this);
            content = new ContentManager(this.Services);
            //Gjør at musepekeren er synlig over vinduet:
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Her legger man initialiseringskode.
        /// </summary>
        protected override void Initialize() {
            base.Initialize();
            InitDevice();
            InitCamera();
            InitVertices();
        }

        /// <summary>
        /// Diverse initilaliseringer.
        /// Henter ut device-objektet.
        /// </summary>
        private void InitDevice() {
            device = graphics.GraphicsDevice;

            //Setter størrelse på framebuffer:
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            Window.Title = "Oblig1b_KnutLucasAndersen";

            //Initialiserer Effect-objektet:
            effect = new BasicEffect(graphics.GraphicsDevice);
        }

        /// <summary>
        /// Stiller inn kameraet.
        /// </summary>
        private void InitCamera() {
            //Projeksjon:
            float aspectRatio = (float)graphics.GraphicsDevice.Viewport.Width / (float)graphics.GraphicsDevice.Viewport.Height;

            //Oppretter view-matrisa:
            Matrix.CreateLookAt(ref cameraPosition, ref cameraTarget, ref cameraUpVector, out view);

            //Oppretter projeksjonsmatrisa:
            Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 0.01f, 1000.0f, out projection);

            //Gir matrisene til shader:
            effect.Projection = projection;
            effect.View = view;
            //sett at verteksene skal fargelegges
            effect.VertexColorEnabled = true;
        }

        /// <summary>
        /// Vertekser for trekanten.
        /// </summary>
        private void InitVertices() {
            verteksSider = new VertexPositionColor[10];
            verteksTopp = new VertexPositionColor[4];
            verteksBunn = new VertexPositionColor[4];
            
            //tegning av sidene til kuben
            verteksSider[0].Position = new Vector3(-0.5f, -0.5f, 0.5f);
            verteksSider[1].Position = new Vector3(-0.5f, 0.5f, 0.5f);
            verteksSider[2].Position = new Vector3(0.5f, -0.5f, 0.5f);
            verteksSider[3].Position = new Vector3(0.5f, 0.5f, -0.5f);
           
            verteksSider[4].Position = new Vector3(1.0f, 0.0f, 0.0f);
            verteksSider[5].Position = new Vector3(1.0f,1.0f, -1.0f);
            verteksSider[6].Position = new Vector3(0.0f, 0.0f, 0.0f);

            verteksSider[7].Position = new Vector3(0.0f, 1.0f, -1.0f);
            verteksSider[8].Position = new Vector3(-0.5f, -0.5f, 0.5f);
            verteksSider[9].Position = new Vector3(-0.5f, 0.5f, 0.5f);
            //tegning av toppen til kuben
            verteksTopp[0].Position = new Vector3(-0.5f, 0.5f, 0.5f);
            verteksTopp[1].Position = new Vector3(0.0f, 1.0f, -1.0f);
            verteksTopp[2].Position = new Vector3(0.5f, 0.5f, -0.5f);
            verteksTopp[3].Position = new Vector3(1.0f, 1.0f, -1.0f);
            //tegning av bunnen til kuben
            verteksBunn[0].Position = new Vector3(-0.5f, -0.5f, 0.5f);
            verteksBunn[1].Position = new Vector3(0.0f, 0.0f, 0.0f);
            verteksBunn[2].Position = new Vector3(0.5f, -0.5f, 0.5f);
            verteksBunn[3].Position = new Vector3(1.0f, 0.0f, 0.0f);
            //maks verdi for antall som skal farges
            int max = 4;
            int antall = 0;
            //fargelegg topp og bunn av kuben
            fargeleggKube(antall, Color.Red, max, verteksTopp);
            fargeleggKube(antall, Color.Green, max, verteksBunn);
            //fargelegg kubens sider
            fargeleggKube(antall, Color.Blue, max, verteksSider);
            //øk max og antall 
            //(dette pga at det er et nytt punkt fra array som skal fargelegges)
            max += 3;
            antall += 3;
            fargeleggKube(3, Color.Pink, max, verteksSider);
            max += 3;
            antall += 3;
            fargeleggKube(6, Color.Yellow, max, verteksSider);

        }

        /// <summary>
        /// Fargelegger en del av kuben med gitte parameter
        /// </summary>
        /// <param name="teller">Startverdi i array for fargelegging</param>
        /// <param name="farge">Fargen som denne delen av kuben skal ha</param>
        /// <param name="max">Maks verdi for antall hjørner som skal farges</param>
        /// <param name="array">Array som inneholder verteksene som skal farges</param>
        private void fargeleggKube(int teller, Color farge, int max, VertexPositionColor[] array) {
            while (teller < max) {
                array[teller].Color = farge;
                teller++;
            } //while
        } //fargeleggKube

        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            RasterizerState rasterizerState1 = new RasterizerState();
            rasterizerState1.CullMode = CullMode.None;
            rasterizerState1.FillMode = FillMode.WireFrame;
            device.RasterizerState = rasterizerState1;
            device.Clear(Color.PowderBlue);

            //Setter world=I:
            world = Matrix.Identity;
            // Setter world-matrisa på effect-objektet (verteks-shaderen):
            effect.World = world;

            //Starter tegning - må bruke effect-objektet:
            foreach (EffectPass pass in effect.CurrentTechnique.Passes) {
                pass.Apply();
                // Angir primitivtype, aktuelle vertekser, en offsetverdi og antall
                device.DrawUserPrimitives(PrimitiveType.TriangleStrip, verteksSider, 0, 5, VertexPositionColor.VertexDeclaration);
                device.DrawUserPrimitives(PrimitiveType.TriangleStrip, verteksBunn, 0, 2, VertexPositionColor.VertexDeclaration);
                device.DrawUserPrimitives(PrimitiveType.TriangleStrip, verteksTopp, 0, 2, VertexPositionColor.VertexDeclaration);
            }

            base.Draw(gameTime);
        }
    } //Oblig1B
} //namespace