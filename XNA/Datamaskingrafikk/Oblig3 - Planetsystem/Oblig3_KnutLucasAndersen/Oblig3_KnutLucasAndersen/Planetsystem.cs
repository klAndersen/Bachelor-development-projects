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

namespace Oblig3_KnutLucasAndersen {
    /// <summary>
    /// Planetsystemet representert ved våre 8 planeter med tilhørende måner.
    /// (begrenset antall måner for bedre visuell "opplevelse").
    /// Solen er i sentrum.
    /// </summary>
    public class Planetsystem : Microsoft.Xna.Framework.Game {
        private GraphicsDeviceManager graphics;
        private GraphicsDevice device;
        private ContentManager content;
        private Model model;
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;
        //liste som inneholder planeter og måner
        private List<Planet> planetListe;
        private List<Planet> maaneListe;
        private BasicEffect effect;

        private InputHandler inputhandler;
        private FirstPersonCamera camera;

        private float rotasjon = 0.0f;

        private Stack<Matrix> matrixStack = new Stack<Matrix>();

        //WVP-matrisene:
        private Matrix world;
        private Matrix projection;
        private Matrix view;

        public Planetsystem() {
            graphics = new GraphicsDeviceManager(this);
            content = new ContentManager(this.Services);
            content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            fyllListeMedPlaneterOgMaaner();
            inputhandler = new InputHandler(this);
            this.Components.Add(inputhandler);
            camera = new FirstPersonCamera(this);
            this.Components.Add(camera);
        }

        private void fyllListeMedPlaneterOgMaaner() {
            planetListe = new List<Planet>();
            maaneListe = new List<Planet>();
            //variabel som brukes for å opprette objekter
            Planet pObjekt;
            //avstand for å få planetene litt utenfor sola
            float solStrl = 332.9f;
            float avstSol = solStrl * 2;
            float jupiterStrl = 100.0f;
            //opprett planeten og legg den til i listen
            pObjekt = new Planet("Solen", solStrl, 0, 0.0f, 0.0f, 0.0f, 0.0f, false);
            planetListe.Add(pObjekt);
            pObjekt = new Planet("Merkur", 0.55f, 0, avstSol + 4.0f, 0.0f, 0.0f, 0.0f, false); //0.4 AU fra sola
            planetListe.Add(pObjekt);
            pObjekt = new Planet("Venus", 8.15f, 0, avstSol + 7.0f, 0.0f, 0.0f, 0.0f, false); //0.7 AU fra sola
            planetListe.Add(pObjekt);
            pObjekt = new Planet("Jorden", 10.0f, 1, avstSol + 10.0f, 0.0f, 0.0f, 0.0f, false); //1.0 AU fra sola
            planetListe.Add(pObjekt);
            pObjekt = new Planet("Mars", 1.07f, 2, avstSol + 15.0f, 0.0f, 0.0f, 0.0f, false); //1.5 AU AU fra sola
            planetListe.Add(pObjekt);
            pObjekt = new Planet("Jupiter", jupiterStrl, 67, avstSol + 52.0f, 0.0f, 0.0f, 0.0f, false); //5.2 AU AU fra sola
            planetListe.Add(pObjekt);
            pObjekt = new Planet("Saturn", 95.0f, 62, avstSol + jupiterStrl + 95.0f, 0.0f, 0.0f, 0.0f, false); //9.5  AU fra sola
            planetListe.Add(pObjekt);
            pObjekt = new Planet("Uranus", 14.0f, 27, avstSol + jupiterStrl + 196.0f, 0.0f, 0.0f, 0.0f, false); //19.6  AU fra sola
            planetListe.Add(pObjekt);
            pObjekt = new Planet("Neptun", 13.0f, 13, avstSol + jupiterStrl + 300.0f, 0.0f, 0.0f, 0.0f, false); //30 AU fra sola
            planetListe.Add(pObjekt);
            //opprett måner for Jorden
            pObjekt = new Planet(0.1f, 0, 0.0f, 0.0f, 0.0f, false, planetListe[3].PlanetNavn);
            maaneListe.Add(pObjekt);
            //opprett måner for Mars
            pObjekt = new Planet(0.1f, 0, 0.0f, 0.0f, 0.0f, false, planetListe[4].PlanetNavn);
            maaneListe.Add(pObjekt);
            //opprett måner for Jupiter
            pObjekt = new Planet(0.1f, 0, 0.0f, 0.0f, 0.0f, false, planetListe[5].PlanetNavn);
            maaneListe.Add(pObjekt);
            //opprett måner for Saturn
            pObjekt = new Planet(0.1f, 0, 0.0f, 0.0f, 0.0f, false, planetListe[6].PlanetNavn);
            maaneListe.Add(pObjekt);
            //opprett måner for Uranus
            pObjekt = new Planet(0.1f, 0, 0.0f, 0.0f, 0.0f, false, planetListe[7].PlanetNavn);
            maaneListe.Add(pObjekt);
            //opprett måner for Neptun
            pObjekt = new Planet(0.1f, 0, 0.0f, 0.0f, 0.0f, false, planetListe[8].PlanetNavn);
            maaneListe.Add(pObjekt);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here            
            base.Initialize();
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            spriteFont = Content.Load<SpriteFont>(@"Content\Arial");
            InitDevice();
        }

        private void InitDevice() {
            device = graphics.GraphicsDevice;
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Planetsystem";
            //Initialiserer Effect-objektet:
            effect = new BasicEffect(graphics.GraphicsDevice);
        } //InitDevice

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            model = content.Load<Model>("sphere");
            (model.Meshes[0].Effects[0] as BasicEffect).EnableDefaultLighting();
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

        private void tegnSola() {
            Matrix matScale, matRotateY, matTrans;
            Planet sol = planetListe[0];
            //Skaleringsmatrise:
            matScale = Matrix.CreateScale(sol.PlanetStrl);
            //Translasjonsmatrise:
            matTrans = Matrix.CreateTranslation(sol.PosisjonX, sol.PosisjonY, sol.PosisjonZ);
            //Rotasjonsmatrise (y-akse):
            matRotateY = Matrix.CreateRotationY(rotasjon);
            rotasjon += (float)TargetElapsedTime.Milliseconds / 5000.0f;
            rotasjon = rotasjon % (float)(2 * Math.PI);
            //Kumulativ world-matrise;
            world = matScale * matRotateY * matTrans;
            //Legger på matrisestack:
            matrixStack.Push(world);
            //Setter planetens world-matrise:
            effect.World = world;
            projection = camera.Projection;
            view = camera.View;
            model.Draw(world, view, projection);
        }

        private void tegnPlanet(GameTime gameTime) {
            Matrix matIdentity, matTrans, matScale, matRotateY, matOrbTranslation, matOrbRotation;

            for (int i = 1; i < planetListe.Count; i++) {
                Planet planet = planetListe[i];
                matIdentity = Matrix.Identity;
                //Skaleringsmatrise:
                matScale = Matrix.CreateScale(planet.PlanetStrl);
                //Rotasjonsmatrise (y-akse):
                matRotateY = Matrix.CreateRotationY(rotasjon);
                rotasjon += (float)TargetElapsedTime.Milliseconds / 5000.0f;
                rotasjon = rotasjon % (float)(2 * Math.PI);
                //Orbit - en translasjon etterfulgt av en rotasjon:
                matOrbTranslation = Matrix.CreateTranslation(0.0f, 0.1f, 0.5f);
                float fart = planet.PlanetFart;
                fart += 0.8f * (float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
                fart = fart % (float)(2 * Math.PI);
                matOrbRotation = Matrix.CreateRotationY(fart);
                planetListe[i].PlanetFart = fart;
                //Translasjonsmatrise:
                planetListe[i].PosisjonX = planet.PosisjonX + fart;
                planetListe[i].PosisjonY = planet.PosisjonY + fart;
                planetListe[i].PosisjonZ = planet.PosisjonZ + fart;
                matTrans = Matrix.CreateTranslation(planet.PosisjonX, planet.PosisjonY, planet.PosisjonZ);
                //Kumulativ world-matrise;
                world = matIdentity * matScale * matRotateY * matOrbTranslation * matOrbRotation * matTrans;
                //Legger på matrisestack:
                matrixStack.Push(world);
                //Setter planetens world-matrise:
                effect.World = world;
                projection = camera.Projection;
                view = camera.View;
                model.Draw(world, view, projection);
            }
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

            device.Clear(Color.Black);

            tegnSola();
            tegnPlanet(gameTime); 

            base.Draw(gameTime);
        }
    }
}
