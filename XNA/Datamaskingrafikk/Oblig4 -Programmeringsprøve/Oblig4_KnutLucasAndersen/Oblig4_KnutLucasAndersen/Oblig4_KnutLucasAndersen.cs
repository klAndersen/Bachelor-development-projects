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

namespace Oblig4_KnutLucasAndersen {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Oblig4_KnutLucasAndersen : Microsoft.Xna.Framework.Game {
        private GraphicsDeviceManager graphics;
        private ContentManager content;
        private GraphicsDevice device;
        private Model sphere;
        private Model kube;
        private Texture2D bricks;
        private Texture2D kaal;
        private BasicEffect effect;

        //Liste med vertekser:
        private VertexPositionColor[] vertices;
        private VertexPositionColor[] kordinatSystem;

        //WVP-matrisene:
        private Matrix world;
        private Matrix projection;
        private Matrix view;

        //Kameraposisjon:
        private Vector3 cameraPosition = new Vector3(0.0f, 0.0f, 10.0f);
        private Vector3 cameraTarget = Vector3.Zero;
        private Vector3 cameraUpVector = new Vector3(0.0f, 1.0f, 0.0f);

        SpriteBatch spriteBatch;

        /// <summary>
        /// Konstruktør. Henter ut et graphics-objekt.
        /// </summary>
        public Oblig4_KnutLucasAndersen() {
            graphics = new GraphicsDeviceManager(this);
            content = new ContentManager(this.Services);
            content.RootDirectory = "Content";
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
            Window.Title = "Oblig4 - Knut Lucas Andersen";
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
            effect.VertexColorEnabled = true;
        }

        /// <summary>
        /// Vertekser for trekanten.
        /// </summary>
        private void InitVertices() {
            //kordinatsystem
            kordinatSystem = new VertexPositionColor[6];
            kordinatSystem[0].Position = new Vector3(-100.0f, 0.0f, 0.0f);
            kordinatSystem[1].Position = new Vector3(100.0f, 0.0f, 0.0f);
            kordinatSystem[2].Position = new Vector3(0.0f, 100.0f, 0.0f);
            kordinatSystem[3].Position = new Vector3(0.0f, -100.0f, 0.0f);
            kordinatSystem[4].Position = new Vector3(0.0f, 0.0f, 100.0f);
            kordinatSystem[5].Position = new Vector3(0.0f, 0.0f, -100.0f);

            //fargelegging kordinatsystem
            kordinatSystem[0].Color = Color.Blue;
            kordinatSystem[1].Color = Color.Blue;
            kordinatSystem[2].Color = Color.Red;
            kordinatSystem[3].Color = Color.Red;
            kordinatSystem[4].Color = Color.Yellow;
            kordinatSystem[5].Color = Color.Yellow;
            //vedlegg1
            Color c1 = Color.Orange;
            Color c2 = Color.Blue;
            Color c3 = Color.MediumPurple;
            Color c4 = Color.MistyRose;
            Color c5 = Color.Bisque;
            Color c6 = Color.Brown;

            Vector3 top = new Vector3(0f, 2f, 0f);
            Vector3 p1 = new Vector3(-2f, -2f, 2f);
            Vector3 p2 = new Vector3(-2f, -2f, -2f);
            Vector3 p3 = new Vector3(2f, -2f, 2f);
            Vector3 p4 = new Vector3(2f, -2f, -2f);

            vertices = new VertexPositionColor[18];

            //Gulv:
            vertices[0].Position = p1;
            vertices[0].Color = c1;
            vertices[1].Position = p2;
            vertices[1].Color = c1;
            vertices[2].Position = p3;
            vertices[2].Color = c1;

            vertices[3].Position = p2;
            vertices[3].Color = c2;
            vertices[4].Position = p3;
            vertices[4].Color = c2;
            vertices[5].Position = p4;
            vertices[5].Color = c2;

            //Front:
            vertices[6].Position = p1;
            vertices[6].Color = c3;
            vertices[7].Position = top;
            vertices[7].Color = c3;
            vertices[8].Position = p3;
            vertices[8].Color = c3;

            //Left side:
            vertices[9].Position = p2;
            vertices[9].Color = c4;
            vertices[10].Position = top;
            vertices[10].Color = c4;
            vertices[11].Position = p1;
            vertices[11].Color = c4;

            //Backside:
            vertices[12].Position = top;
            vertices[12].Color = c5;
            vertices[13].Position = p2;
            vertices[13].Color = c5;
            vertices[14].Position = p4;
            vertices[14].Color = c5;

            //Right side:
            vertices[15].Position = top;
            vertices[15].Color = c6;
            vertices[16].Position = p4;
            vertices[16].Color = c6;
            vertices[17].Position = p3;
            vertices[17].Color = c6;
        }


        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //laster inn kuben og sfæren
            kube = content.Load<Model>("kube");
            sphere = content.Load<Model>("sphere");
            //setter belysning
            (kube.Meshes[0].Effects[0] as BasicEffect).EnableDefaultLighting();
            (sphere.Meshes[0].Effects[0] as BasicEffect).EnableDefaultLighting();    
            //laster inn teksturer
            bricks = content.Load<Texture2D>("bricks");
            kaal = content.Load<Texture2D>("kaal");
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

        private float scaleSizeOfModel(Model model, int prosent) {
            //basert på lærebok s.100
            float strl = 0.0f;
            //loop gjennom meshene i modellen
            foreach(ModelMesh mesh in model.Meshes) {
                if (mesh.BoundingSphere.Radius > strl) {
                    strl = mesh.BoundingSphere.Radius;
                } //if
            } //foreach
            float verdi = (strl * prosent) / (100);
            //returner verdien 
            return verdi;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            RasterizerState rasterizerState1 = new RasterizerState();
            rasterizerState1.CullMode = CullMode.None;
            rasterizerState1.FillMode = FillMode.Solid;
            device.RasterizerState = rasterizerState1;
            effect.TextureEnabled = false;
            device.Clear(Color.PowderBlue);

            //Setter world=I:
            world = Matrix.Identity;
            // Setter world-matrisa på effect-objektet (verteks-shaderen):
            effect.World = world;

            //Starter tegning av kordinatsystem
            foreach (EffectPass pass in effect.CurrentTechnique.Passes) {
                pass.Apply();
                device.DrawUserPrimitives(PrimitiveType.LineList, kordinatSystem, 0, 3, VertexPositionColor.VertexDeclaration);
            }
            effect.World = world;
            //starter tegning av pyramide
            foreach (EffectPass pass in effect.CurrentTechnique.Passes) {
                pass.Apply();
                device.DrawUserPrimitives(PrimitiveType.TriangleStrip, vertices, 0, vertices.Length/2, VertexPositionColor.VertexDeclaration);
            }


            
            float strl = scaleSizeOfModel(kube, 1);
            bricks = content.Load<Texture2D>("bricks");
            effect.Texture = bricks;
            effect.TextureEnabled = true;
            Matrix matScale = Matrix.CreateScale(0.01f);
            
            kube.Draw(matScale, view, projection);

            base.Draw(gameTime);

        }
    }
}
