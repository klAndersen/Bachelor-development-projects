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
        private VertexPositionColor[] verteksKordinater;

        //WVP-matrisene:
        private Matrix world;
        private Matrix projection;
        private Matrix view;

        //Kameraposisjon:
        //ser feil i øverste høyre hjørne bak
        //2.0f eller 3.0f i Y?
        private Vector3 cameraPosition = new Vector3(3.0f, 2.0f, 5.0f); 
        //ser "riktig" ut fra denne vinkelen
        //private Vector3 cameraPosition = new Vector3(0.0f, 0.0f, 5.0f); 
        private Vector3 cameraTarget = Vector3.Zero;
        private Vector3 cameraUpVector = Vector3.Up;

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
            verteksKordinater = new VertexPositionColor[6];
            //firkant 1 - front
            vertices[0].Position = new Vector3(-0.5f, -0.5f, 0.5f); //nederst til venstre - front
            vertices[1].Position = new Vector3(-0.5f, 0.5f, 0.5f); //øverst til venstre - front
            vertices[2].Position = new Vector3(0.5f, -0.5f, 0.5f); //nederst til høyre - front
            vertices[3].Position = vertices[2].Position; //nederst til høyre - front
            vertices[4].Position = vertices[1].Position; //øverst til venstre - front
            vertices[5].Position = new Vector3(0.5f, 0.5f, 0.5f); //øverst til høyre - front
            //firkant 2 - høyre side
            vertices[6].Position = vertices[2].Position; //nederst til høyre - front
            vertices[7].Position = vertices[5].Position; //øverst til høyre - front
            vertices[8].Position = new Vector3(0.5f, -0.5f, -0.5f); //nederst til høyre - bak
            vertices[9].Position = vertices[8].Position; //nederst til høyre - bak
            vertices[10].Position = vertices[5].Position; //øverst til høyre - front
            vertices[11].Position = new Vector3(0.5f, 0.5f, -0.5f); //øverst til høyre - bak
            //firkant 3 - bakside
            vertices[12].Position = vertices[8].Position; //nederst til høyre - bak
            vertices[13].Position = vertices[11].Position; //øverst til høyre - bak
            vertices[14].Position = new Vector3(-0.5f, -0.5f, -0.5f); //nederst til venstre - bak
            vertices[15].Position = vertices[14].Position; //nederst til venstre - bak
            vertices[16].Position = vertices[11].Position; //øverst til høyre - bak
            vertices[17].Position = new Vector3(-0.5f, 0.5f, -0.5f); //øverst til venstre - bak
            //firkant 4 - venstre side
            vertices[18].Position = vertices[14].Position; //nederst til venstre - bak
            vertices[19].Position = vertices[17].Position; //øverst til venstre - bak
            vertices[20].Position = vertices[0].Position; //nederst til venstre - front
            vertices[21].Position = vertices[0].Position; //nederst til venstre - front
            vertices[22].Position = vertices[17].Position; //øverst til venstre - bak
            vertices[23].Position = vertices[1].Position; //øverst til venstre - front
            //firkant 5 - topp
            vertices[24].Position = vertices[1].Position; //øverst til venstre - front
            vertices[25].Position = vertices[17].Position; //øverst til venstre - bak
            vertices[26].Position = vertices[5].Position; //øverst til høyre - front
            vertices[27].Position = vertices[5].Position; //øverst til høyre - front
            vertices[28].Position = vertices[17].Position; //øverst til venstre - bak
            vertices[29].Position = vertices[11].Position; //øverst til høyre - bak
            //firkant 6 - bunn
            vertices[30].Position = vertices[0].Position; //nederst til venstre - front
            vertices[31].Position = vertices[14].Position; //nederst til venstre - bak
            vertices[32].Position = vertices[2].Position; //nederst til høyre - front
            vertices[33].Position = vertices[2].Position; //nederst til høyre - front
            vertices[34].Position = vertices[14].Position; //nederst til venstre - bak
            vertices[35].Position = vertices[8].Position; //nederst til høyre - bak

            //startpunkt for fargelegging av elementer i array*/
            int start = 0;
            //start fargelegging av firkanten(e)
            start = fargeleggKube(start, Color.Purple); //front
            start = fargeleggKube(start, Color.Red); //høyre
            start = fargeleggKube(start, Color.Green); //bak
            start = fargeleggKube(start, Color.Yellow); //venstre
            start = fargeleggKube(start, Color.Pink); //topp
            start = fargeleggKube(start, Color.Black); //bunn

            //kommentert ut for bedre synlighet
            /*/kordinatsystem; X
            verteksKordinater[0].Position = new Vector3(-100.0f, 0.0f, 0.0f);
            verteksKordinater[1].Position = new Vector3(100.0f, 0.0f, 0.0f);
            //kordinatsystem; Y
            verteksKordinater[2].Position = new Vector3(0.0f, 100.0f, 0.0f);
            verteksKordinater[3].Position = new Vector3(0.0f, -100.0f, 0.0f);
            //kordinatsystem; Z
            verteksKordinater[4].Position = new Vector3(0.0f, 0.0f, 100.0f);
            verteksKordinater[5].Position = new Vector3(0.0f, 0.0f, -100.0f);
            //fargelegging av kordinatsystem
            verteksKordinater[0].Color = Color.DeepPink;
            verteksKordinater[1].Color = Color.DeepPink;
            verteksKordinater[2].Color = Color.FloralWhite;
            verteksKordinater[3].Color = Color.FloralWhite;
            verteksKordinater[4].Color = Color.DarkBlue;
            verteksKordinater[5].Color = Color.DarkBlue;//*/
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
            //rasterizerState1.FillMode = FillMode.Solid;
            device.RasterizerState = rasterizerState1;
            device.Clear(Color.PowderBlue);

            //Setter world=I:
            world = Matrix.Identity;
            // Setter world-matrisa på effect-objektet (verteks-shaderen):
            effect.World = world;

            //Starter tegning av kordinatsystem
            foreach (EffectPass pass in effect.CurrentTechnique.Passes) {
                pass.Apply();
                // Angir primitivtype, aktuelle vertekser, en offsetverdi og antall linjer
                device.DrawUserPrimitives(PrimitiveType.LineList, verteksKordinater, 0, 3, VertexPositionColor.VertexDeclaration);
            }

            effect.World = world;
            //Starter tegning av firkantene (kube)
            foreach (EffectPass pass in effect.CurrentTechnique.Passes) {
                pass.Apply();
                // Angir primitivtype, aktuelle vertekser, en offsetverdi og antall trekanter
                device.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, 12, VertexPositionColor.VertexDeclaration);
            }

            base.Draw(gameTime);
        }
    } //Oblig1A
} //namespace