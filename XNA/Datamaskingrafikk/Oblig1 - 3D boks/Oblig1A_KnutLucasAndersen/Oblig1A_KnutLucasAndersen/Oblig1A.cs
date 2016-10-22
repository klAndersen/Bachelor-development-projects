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

namespace Oblig1A_KnutLucasAndersen {
    /// <summary>
    /// Et XNA klasse som tegne opp en enkel trekant.
    /// </summary>
    public class Oblig1A : Microsoft.Xna.Framework.Game {
        private GraphicsDeviceManager graphics;
        private ContentManager content;
        private GraphicsDevice device;

        private BasicEffect effect;

        //Liste med vertekser:
        private VertexPositionColor[] vertices;

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
        public Oblig1A() {
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

            Window.Title = "Oblig1a_KnutLucasAndersen";

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
            vertices = new VertexPositionColor[36];
            //firkant 1 - front
            vertices[0].Position = new Vector3(-0.5f, -0.5f, 0.5f);
            vertices[1].Position = new Vector3(-0.5f, 0.5f, 0.5f);
            vertices[2].Position = new Vector3(0.5f, 0.5f, -0.5f);
            vertices[3].Position = new Vector3(-0.5f, -0.5f, 0.5f);
            vertices[4].Position = new Vector3(0.5f, 0.5f, -0.5f);
            vertices[5].Position = new Vector3(0.5f, -0.5f, 0.5f);
            //firkant2 - høyre side
            vertices[6].Position = new Vector3(0.5f, -0.5f, 0.5f);
            vertices[7].Position = new Vector3(0.5f, 0.5f, -0.5f);
            vertices[8].Position = new Vector3(1.0f, 1.0f, -1.0f);
            vertices[9].Position = new Vector3(0.5f, -0.5f, 0.5f);
            vertices[10].Position = new Vector3(1.0f, 1.0f, -1.0f);
            vertices[11].Position = new Vector3(1.0f, 0.0f, 0.0f);
            //firkant 3 - topp
            vertices[12].Position = new Vector3(-0.5f, 0.5f, 0.5f);
            vertices[13].Position = new Vector3(0.0f, 1.0f, -1.0f);
            vertices[14].Position = new Vector3(1.0f, 1.0f, -1.0f);
            vertices[15].Position = new Vector3(-0.5f, 0.5f, 0.5f);
            vertices[16].Position = new Vector3(1.0f, 1.0f, -1.0f);
            vertices[17].Position = new Vector3(0.5f, 0.5f, -0.5f);
            //firkant 4 - bakside
            vertices[18].Position = new Vector3(0.0f, 0.0f, 0.0f);
            vertices[19].Position = new Vector3(0.0f, 1.0f, -1.0f);
            vertices[20].Position = new Vector3(1.0f, 1.0f, -1.0f);
            vertices[21].Position = new Vector3(0.0f, 0.0f, 0.0f);
            vertices[22].Position = new Vector3(1.0f, 1.0f, -1.0f);
            vertices[23].Position = new Vector3(1.0f, 0.0f, 0.0f);
            //firkant 5 - bunn
            vertices[24].Position = new Vector3(-0.5f, -0.5f, 0.5f);
            vertices[25].Position = new Vector3(0.0f, 0.0f, 0.0f);
            vertices[26].Position = new Vector3(1.0f, 0.0f, 0.0f);
            vertices[27].Position = new Vector3(-0.5f, -0.5f, 0.5f);
            vertices[28].Position = new Vector3(1.0f, 0.0f, 0.0f);
            vertices[29].Position = new Vector3(0.5f, -0.5f, 0.5f);
            //firkant 6 - venstre side
            vertices[30].Position = new Vector3(-0.5f, -0.5f, 0.5f);
            vertices[31].Position = new Vector3(-0.5f, 0.5f, 0.5f);
            vertices[32].Position = new Vector3(0.0f, 1.0f, -1.0f);
            vertices[33].Position = new Vector3(-0.5f, -0.5f, 0.5f);
            vertices[34].Position = new Vector3(0.0f, 1.0f, -1.0f);
            vertices[35].Position = new Vector3(0.0f, 0.0f, 0.0f);

            //startpunkt for fargelegging av elementer i array
            int start = 0;
            //start fargelegging av firkanten(e)
            start = fargeleggKube(start, Color.Purple);
            start = fargeleggKube(start, Color.Red);
            start = fargeleggKube(start, Color.Green);
            start = fargeleggKube(start, Color.Yellow);
            start = fargeleggKube(start, Color.Pink);
            start = fargeleggKube(start, Color.Black);
        }

        /// <summary>
        /// Fargelegger en del av kuben med gitte parameter
        /// </summary>
        /// <param name="teller">Startverdi i array for fargelegging</param>
        /// <param name="farge">Fargen som denne delen av kuben skal ha</param>
        /// <returns>int</returns>
        private int fargeleggKube(int teller, Color farge) {
            //max kunne vært en konstant, men basert på oppgavens
            //kontekst velger jeg å bare ha den internt i metoden
            int max = teller + 6;
            while (teller < max) {
                vertices[teller].Color = farge;
                teller++;
            } //while
            return teller;
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
                device.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, 12, VertexPositionColor.VertexDeclaration);
            }

            base.Draw(gameTime);
        }
    } //Oblig1A
} //namespace