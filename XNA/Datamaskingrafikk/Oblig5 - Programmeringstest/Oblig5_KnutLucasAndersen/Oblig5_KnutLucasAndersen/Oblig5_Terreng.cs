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

namespace Oblig5_KnutLucasAndersen {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Oblig5_Terreng : Microsoft.Xna.Framework.Game {
        
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
        VertexPositionColorNormal[] vertices;
        //VertexPositionColor[] vertices;
        Matrix viewMatrix;
        Matrix projectionMatrix;
        int[] indices;


        VertexBuffer myVertexBuffer;
        IndexBuffer myIndexBuffer;

        private float angle = 0f;
        private int terrainWidth = 4;
        private int terrainHeight = 3;
        private float[,] heightData;

        public Oblig5_Terreng() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
             graphics.PreferredBackBufferWidth = 500;
             graphics.PreferredBackBufferHeight = 500;
             graphics.IsFullScreen = false;
             graphics.ApplyChanges();
             this.IsMouseVisible = true;
             Window.Title = "Oblig 5 - Knut Lucas Andersen";
 
             base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            device = graphics.GraphicsDevice;
            Texture2D heightMap = Content.Load<Texture2D>("heightmap");
            effect = Content.Load<Effect>("effects"); 
            
            SetUpCamera();
            LoadHeightData(heightMap);
            SetUpVertices();
            SetUpIndices();
            CalculateNormals();
            CopyToBuffers();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
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

        /*
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

            vertices = new VertexPositionColor[terrainWidth * terrainHeight];
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
        }*/

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
            viewMatrix = Matrix.CreateLookAt(new Vector3(0, -40, 100), new Vector3(0, 50, 0), new Vector3(0, 1, 0));
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, device.Viewport.AspectRatio, 1.0f, 300.0f);
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.A) || keyState.IsKeyDown(Keys.Left))
                angle += 0.05f;
            if (keyState.IsKeyDown(Keys.D) || keyState.IsKeyDown(Keys.Right))
                angle -= 0.05f;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);

            RasterizerState rs = new RasterizerState();
            rs.CullMode = CullMode.None;
           // rs.FillMode = FillMode.WireFrame;
            device.RasterizerState = rs;

            Matrix worldMatrix = Matrix.CreateTranslation(-terrainWidth / 2.0f, 0, terrainHeight / 2.0f) * Matrix.CreateRotationY(angle);
            effect.CurrentTechnique = effect.Techniques["Colored"];
            effect.Parameters["xView"].SetValue(viewMatrix);
            effect.Parameters["xProjection"].SetValue(projectionMatrix);
            effect.Parameters["xWorld"].SetValue(worldMatrix);
            Vector3 lightDirection = new Vector3(1.0f, -1.0f, -1.0f);
            lightDirection.Normalize();
            effect.Parameters["xLightDirection"].SetValue(lightDirection);
            effect.Parameters["xAmbient"].SetValue(0.1f);
            effect.Parameters["xEnableLighting"].SetValue(true); 

 /*
            effect.CurrentTechnique = effect.Techniques["Colored"];
            effect.Parameters["xView"].SetValue(viewMatrix);
            effect.Parameters["xProjection"].SetValue(projectionMatrix);
            effect.Parameters["xWorld"].SetValue(Matrix.Identity);
            effect.Parameters["xEnableLighting"].SetValue(true);
            Vector3 lightDirection = new Vector3(0.5f, 0, -1.0f);
            lightDirection.Normalize();
            effect.Parameters["xLightDirection"].SetValue(lightDirection);

           
            Matrix worldMatrix = Matrix.CreateTranslation(-terrainWidth / 2.0f, 0, terrainHeight / 2.0f) * Matrix.CreateRotationY(angle);
            effect.CurrentTechnique = effect.Techniques["ColoredNoShading"];
            effect.Parameters["xView"].SetValue(viewMatrix);
            effect.Parameters["xProjection"].SetValue(projectionMatrix);
            effect.Parameters["xWorld"].SetValue(worldMatrix);
            */
            foreach (EffectPass pass in effect.CurrentTechnique.Passes) {
                pass.Apply();
                device.Indices = myIndexBuffer;
                device.SetVertexBuffer(myVertexBuffer);
                device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertices.Length, 0, indices.Length / 3); 
                //device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertices, 0, vertices.Length, indices, 0, indices.Length / 3, VertexPositionColorNormal.VertexDeclaration);
                //device.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, 2, VertexPositionColorNormal.VertexDeclaration);
                //device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertices, 0, vertices.Length, indices, 0, indices.Length / 3, VertexPositionColor.VertexDeclaration);
            }

            base.Draw(gameTime);
        }

        //index og vertex buffers
        private void CopyToBuffers() {
            myVertexBuffer = new VertexBuffer(device, VertexPositionColorNormal.VertexDeclaration, vertices.Length, BufferUsage.WriteOnly);
            myVertexBuffer.SetData(vertices);
            myIndexBuffer = new IndexBuffer(device, typeof(int), indices.Length, BufferUsage.WriteOnly);
            myIndexBuffer.SetData(indices);
        }
    }
}