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

namespace Oblig2_Oppg1_KnutLucasAndersen {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Oblig2_Oppg1 : Microsoft.Xna.Framework.Game {

        private GraphicsDeviceManager graphics;
        private ContentManager content;
        private GraphicsDevice device;      //Representerer tegneflata.

        private BasicEffect effect;
        private VertexPositionColor[] verticesSphere;
        private VertexPositionColor[] verteksKordinater;

        //WVP-matrisene:
        private Matrix world;
        private Matrix projection;
        private Matrix view;

        //Kameraposisjon:
        private Vector3 cameraPosition = new Vector3(3.0f, 2.0f, 5.0f);
        //private Vector3 cameraPosition = new Vector3(0.0f, 0.0f, 5.0f);
        private Vector3 cameraTarget = Vector3.Zero;
        private Vector3 cameraUpVector = Vector3.Up;

        SpriteBatch spriteBatch;

        /// <summary>
        /// Konstruktør. Henter ut et graphics-objekt.
        /// </summary>
        public Oblig2_Oppg1() {
            graphics = new GraphicsDeviceManager(this);
            content = new ContentManager(this.Services);
            //Gjør at musepekeren er synlig over vinduet:
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Her legger man initialiseringskode.
        /// </summary>
        protected override void Initialize()
        {
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

            Window.Title = "Oblig2_Oppg1_KnutLucasAndersen";

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
            //kordinatsystem
            verteksKordinater = new VertexPositionColor[6];
            /*
                - x(T, P) = sinT * cosP
                - y(T, P) = cosT * cosP
                - z(T, P) = sinP
                - der 0 <= P <= 3,14 og 0 <= T <= 2*3,14. (T = theta, P=phi)
                - vinkelen P mellom -80 og +80 grader med 20 graders avstand
                - 9 punkter ”nedover” (de røde)
                - For hver verdi av P varierer vi T mellom –180 og +180 grader 
                  med step-verdi lik 20 grader. Dette gir 19 punkter, i form av 
                  x, y og z-verdier, for hver sirkel (skive)
                - mindre step-verdi = glattere kule
                - I den ytre løkka varieres fi (P) fra for eksempel +80 grader til -80 grader med en step-verdi på 20 grader.
                - For hver verdi av P varieres teta fra -180 til 180 grader (altså en hel sirkel) i en indre løkke. 
                - For hver runde i den indre løkka beregnes to vertekser ved å regne ut x, y og z-verdiene gitt 
                  P og T - en for P = n og en for P = n+20 (grader)
                - I neste runde av indre løkke genereres punkt 3 og 4 osv.
             */

            float c = (float)Math.PI / 180.0f; //Opererer med radianer.
            float phir = 0.0f;
            float phir20 = 0.0f;
            float thetar = 0.0f;
            float x = 0.0f, y = 0.0f, z = 0.0f;
            int i = 0;
            //Finn antall vertekser:
            verticesSphere = new VertexPositionColor[342];
            //Varierer fi:
            for (float phi = -80.0f; phi < 80.0f; phi += 20) {
                phir = c * phi; //phi radianer
                phir20 = c * (phi + 20); //(phi+20) radianer
                //Varierer teta:
                for (float theta = -180.0f; theta < 180.0f; theta += 20) {
                    thetar = c * theta;
                    //Her skal x, y og z beregnes for pkt.1-3-5-7...:
                    //basert på formel forklart på s.1 i pdf
                    x = (float)Math.Sin(thetar) * (float)Math.Cos(phir);
                    y = (float)Math.Cos(thetar) * (float)Math.Cos(phir);
                    z = (float)Math.Sin(phir);
                    verticesSphere[i].Position = new Vector3(x, y, z);
                    verticesSphere[i].Color = Color.Gray;
                    i++;
                    //Her skal x, y og z beregnes for pkt.2-4-6-8
                    //kode her...
                    x = (float)Math.Sin(thetar) * (float)Math.Cos(phir20);
                    y = (float)Math.Cos(thetar) * (float)Math.Cos(phir20);
                    z = (float)Math.Sin(phir20);
                    verticesSphere[i].Position = new Vector3(x, y, z);
                    verticesSphere[i].Color = Color.Gray;
                    i++;
                } //indre for
            } //ytre for

            //kordinatsystem; X
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

        } //InitVertices

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

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

            //Starter tegning av kordinatsystem
            foreach (EffectPass pass in effect.CurrentTechnique.Passes) {
                pass.Apply();
                // Angir primitivtype, aktuelle vertekser, en offsetverdi og antall linjer
                device.DrawUserPrimitives(PrimitiveType.LineList, verteksKordinater, 0, 3, VertexPositionColor.VertexDeclaration);
            }

            //Starter tegning - må bruke effect-objektet:
            foreach (EffectPass pass in effect.CurrentTechnique.Passes) {
                pass.Apply();
                // Angir primitivtype, aktuelle vertekser, en offsetverdi og antall
                device.DrawUserPrimitives(PrimitiveType.TriangleStrip, verticesSphere, 0, verticesSphere.Length - 2, VertexPositionColor.VertexDeclaration);
            }

            base.Draw(gameTime);

        }
    }
}
