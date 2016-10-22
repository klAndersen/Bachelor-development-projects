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

namespace Oblig6_Oppg1_KnutLucasAndersen {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Oppgave1 : Microsoft.Xna.Framework.Game {

        public struct VertexPositionColorNormal {
            public Vector3 Position;
            public Color Color;
            public Vector3 Normal;

            public readonly static VertexDeclaration VertexDeclaration = new VertexDeclaration
            (
                new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
                new VertexElement(sizeof(float) * 3, VertexElementFormat.Color, VertexElementUsage.Color, 0),
                new VertexElement(sizeof(float) * 3 + 4, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0)
            );
        }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GraphicsDevice device;

        Effect effect;
        BasicEffect bEffect;
        VertexPositionColorNormal[] vertices;
        Matrix viewMatrix;
        Matrix projectionMatrix;
        int[] indices;

        private float angle = 0f;
        private int terrainWidth = 4;
        private int terrainHeight = 3;
        private float[,] heightData;

        public Oppgave1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        protected override void Initialize() {
            graphics.PreferredBackBufferWidth = 500;
            graphics.PreferredBackBufferHeight = 500;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Oblig6 Oppg1 - Knut Lucas Andersen";

            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            device = graphics.GraphicsDevice;
            bEffect = new BasicEffect(device);
            effect = Content.Load<Effect>("effects");
            SetUpCamera();
            Texture2D heightMap = Content.Load<Texture2D>("heightmap");
            LoadHeightData(heightMap);
            SetUpVertices();
            SetUpIndices();
            CalculateNormals();
        }

        protected override void UnloadContent() {
        }

        private void SetUpVertices() {
            float minHeight = float.MaxValue;
            float maxHeight = float.MinValue;
            for (int x = 0; x < terrainWidth; x++) {
                for (int y = 0; y < terrainHeight; y++) {
                    if (heightData[x, y] < minHeight)
                        minHeight = heightData[x, y];
                    if (heightData[x, y] > maxHeight)
                        maxHeight = heightData[x, y];
                }
            }

            vertices = new VertexPositionColorNormal[terrainWidth * terrainHeight];
            for (int x = 0; x < terrainWidth; x++) {
                for (int y = 0; y < terrainHeight; y++) {
                    vertices[x + y * terrainWidth].Position = new Vector3(x, heightData[x, y], -y);

                    if (heightData[x, y] < minHeight + (maxHeight - minHeight) / 4)
                        vertices[x + y * terrainWidth].Color = Color.Blue;
                    else if (heightData[x, y] < minHeight + (maxHeight - minHeight) * 2 / 4)
                        vertices[x + y * terrainWidth].Color = Color.Green;
                    else if (heightData[x, y] < minHeight + (maxHeight - minHeight) * 3 / 4)
                        vertices[x + y * terrainWidth].Color = Color.Brown;
                    else
                        vertices[x + y * terrainWidth].Color = Color.White;
                }
            }
        }

        private void SetUpIndices() {
            indices = new int[(terrainWidth - 1) * (terrainHeight - 1) * 6];
            int counter = 0;
            for (int y = 0; y < terrainHeight - 1; y++) {
                for (int x = 0; x < terrainWidth - 1; x++) {
                    int lowerLeft = x + y * terrainWidth;
                    int lowerRight = (x + 1) + y * terrainWidth;
                    int topLeft = x + (y + 1) * terrainWidth;
                    int topRight = (x + 1) + (y + 1) * terrainWidth;

                    indices[counter++] = topLeft;
                    indices[counter++] = lowerRight;
                    indices[counter++] = lowerLeft;

                    indices[counter++] = topLeft;
                    indices[counter++] = topRight;
                    indices[counter++] = lowerRight;
                }
            }
        }

        private void CalculateNormals() {
            for (int i = 0; i < vertices.Length; i++)
                vertices[i].Normal = new Vector3(0, 0, 0);

            for (int i = 0; i < indices.Length / 3; i++) {
                int index1 = indices[i * 3];
                int index2 = indices[i * 3 + 1];
                int index3 = indices[i * 3 + 2];

                Vector3 side1 = vertices[index1].Position - vertices[index3].Position;
                Vector3 side2 = vertices[index1].Position - vertices[index2].Position;
                Vector3 normal = Vector3.Cross(side1, side2);

                vertices[index1].Normal += normal;
                vertices[index2].Normal += normal;
                vertices[index3].Normal += normal;
            }

            for (int i = 0; i < vertices.Length; i++)
                vertices[i].Normal.Normalize();
        }

        private void LoadHeightData(Texture2D heightMap) {
            terrainWidth = heightMap.Width;
            terrainHeight = heightMap.Height;

            Color[] heightMapColors = new Color[terrainWidth * terrainHeight];
            heightMap.GetData(heightMapColors);

            heightData = new float[terrainWidth, terrainHeight];
            for (int x = 0; x < terrainWidth; x++)
                for (int y = 0; y < terrainHeight; y++)
                    heightData[x, y] = heightMapColors[x + y * terrainWidth].R / 5.0f;
        }

        private void SetUpCamera() {
            viewMatrix = Matrix.CreateLookAt(new Vector3(60, 80, -80), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, device.Viewport.AspectRatio, 1.0f, 300.0f);
            //Gir matrisene til shader:
            bEffect.Projection = projectionMatrix;
            bEffect.View = viewMatrix;
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            angle += 0.005f;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            device.Clear(Color.Beige);

            RasterizerState rs = new RasterizerState();
            rs.CullMode = CullMode.None;
            rs.FillMode = FillMode.Solid;
            //rs.FillMode = FillMode.WireFrame;
            device.RasterizerState = rs;
            //opprett worldmatrisen
            Matrix worldMatrix = Matrix.CreateTranslation(-terrainWidth / 2.0f, 0, terrainHeight / 2.0f);

            //endre fra true/false ettersom om basiceffect eller effect.fx skal brukes
            bool brukBasicEffect = true;

            if (brukBasicEffect) {
                //sett til true/false basert på VertexColorEnabled
                bool visFarge = true;
                //setter fargen 
                Color diffusFarge = Color.Black;
                tegnTerrengMedBasicEffect(worldMatrix, visFarge, diffusFarge);
            } else {
                tegnTerrengMedEffect(worldMatrix);
            } //if (brukBasicEffect)

            base.Draw(gameTime);
        } //Draw

        /// <summary>
        /// Tegner et terreng basert på heightmap.bmp 
        /// Effekter og lys er basert data fra filen effect.fx
        /// </summary>
        /// <param name="worldMatrix">Matrix - World matrisen</param>
        private void tegnTerrengMedEffect(Matrix worldMatrix) {
            //henter ut og setter verdier fra effect.fx
            effect.CurrentTechnique = effect.Techniques["Colored"];
            effect.Parameters["xView"].SetValue(viewMatrix);
            effect.Parameters["xProjection"].SetValue(projectionMatrix);
            effect.Parameters["xWorld"].SetValue(worldMatrix);
            Vector3 lightDirection = new Vector3(1.0f, -1.0f, -1.0f);
            lightDirection.Normalize();
            effect.Parameters["xLightDirection"].SetValue(lightDirection);
            effect.Parameters["xAmbient"].SetValue(0.1f);
            effect.Parameters["xEnableLighting"].SetValue(true);

            foreach (EffectPass pass in effect.CurrentTechnique.Passes) {
                pass.Apply();
                //tegn terreng
                device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertices, 0, vertices.Length, indices, 0, indices.Length / 3, VertexPositionColorNormal.VertexDeclaration);
            } //foreach
        } //tegnTerrengMedEffect

        /// <summary>
        /// Tegner et terreng basert på filen heightmap.bmp 
        /// Lys og effekter er basert på BasicEffect
        /// </summary>
        /// <param name="worldMatrix">Matrix - World matrisen</param>
        /// <param name="visFarger">Bool - VertexColorEnabled</param>
        /// <param name="diffusFarge">Color - BasicEffect.DiffuseColor</param>
        private void tegnTerrengMedBasicEffect(Matrix worldMatrix, bool visFarger, Color diffusFarge) {
            //slå av/på farger og lys
            bEffect.VertexColorEnabled = visFarger;
            bEffect.EnableDefaultLighting();
            bEffect.LightingEnabled = true;
            // Setter world-matrisa på effect-objektet (verteks-shaderen):
            bEffect.World = worldMatrix;

            //bEffect.DiffuseColor = diffusFarge.ToVector3(); //alt av terrengets farge = diffusFarge
            bEffect.DirectionalLight0.DiffuseColor = diffusFarge.ToVector3();

            foreach (EffectPass pass in bEffect.CurrentTechnique.Passes) {
                pass.Apply();
                //tegn terreng
                device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertices, 0, vertices.Length, indices, 0, indices.Length / 3, VertexPositionColorNormal.VertexDeclaration);
            } //foreach
        } //tegnTerrengMedBasicEffect
    } //Oppgave1
} //namespace