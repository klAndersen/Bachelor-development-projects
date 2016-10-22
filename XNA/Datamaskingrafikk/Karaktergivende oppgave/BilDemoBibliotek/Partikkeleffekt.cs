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

namespace BilDemoBibliotek {

    /// <summary>
    /// Struct som inneholder informasjonen/oppbyggingen av partikkelen(e)
    /// </summary>
    internal struct PartikkelInformasjon {
        public float PartikkelSkapt;
        public float MaxAlder;
        public Vector2 StartPosisjon;
        public Vector2 Akselerasjon;
        public Vector2 Retning;
        public Vector2 Posisjon;
        public float Skalering;
        public Color Farge;
    } //PartikkelInformasjon

    /// <summary>
    /// Denne klassen tar for seg partikkeleffekter.
    /// Grunnlaget er basert på oppsett fra Riemer;
    /// http://www.riemers.net/eng/Tutorials/XNA/Csharp/Series2D/Particles.php
    /// </summary>
    public class Partikkeleffekt : Microsoft.Xna.Framework.DrawableGameComponent {
        private SpriteBatch spriteBatch;
        private Game game;
        //teksturen som inneholder bilde av en eksplosjon
        private Texture2D partikkelTekstur;
        //liste som skal inneholde partiklene
        private List<PartikkelInformasjon> partikkelListe = new List<PartikkelInformasjon>();
        private Random randomizer = new Random();

        /// <summary>
        /// Konstruktøren til PartikkelEffekt klassen
        /// </summary>
        /// <param name="game">Objekt av Game</param>
        public Partikkeleffekt(Game game)
            : base(game) {
            this.game = game;
        } //konstruktør

        /// <summary>
        /// Oppretter/skaper en partikkeleffekt
        /// </summary>
        /// <param name="startPosisjon">Startposisjonen for partikkeleffekten</param>
        /// <param name="antallPartikler">Antall partikler som skal lages</param>
        /// <param name="strl">Størrelsen på partikkelen</param>
        /// <param name="maxAlder">Partikkelens levetid</param>
        /// <param name="gameTime">Objekt av GameTime</param>
        internal void SkapPartikkeleffekt(Vector2 startPosisjon, int antallPartikler, float strl, float maxAlder, GameTime gameTime) {
            //løkke som skaper/oppretter partikler
            for (int i = 0; i < antallPartikler; i++) {
                OpprettPartikkel(startPosisjon, strl, maxAlder, gameTime);
            } //for
        } //SkapPartikkeleffekt

        private void OpprettPartikkel(Vector2 startPosisjon, float eksplosjonStrl, float maxAlder, GameTime gameTime) {
            //opprett et partikkel objekt
            PartikkelInformasjon partikkel = new PartikkelInformasjon();
            //sett startpoisjon for partikkelen
            partikkel.StartPosisjon = startPosisjon;
            partikkel.Posisjon = partikkel.StartPosisjon;
            //sett når partikkelen ble skapt
            partikkel.PartikkelSkapt = (float)gameTime.TotalGameTime.TotalMilliseconds;
            partikkel.MaxAlder = maxAlder;
            partikkel.Skalering = 0.25f;
            partikkel.Farge = Color.White;
            //randomiser partikkelens distanse
            float partikkelDistanse = (float)randomizer.NextDouble() * eksplosjonStrl;
            Vector2 displacement = new Vector2(partikkelDistanse, 0);
            float vinkel = MathHelper.ToRadians(randomizer.Next(360));
            displacement = Vector2.Transform(displacement, Matrix.CreateRotationZ(vinkel));
            //sett retning og akselerasjon
            partikkel.Retning = displacement * 2.0f;
            partikkel.Akselerasjon = -partikkel.Retning;
            //legg til partikkel i listen
            partikkelListe.Add(partikkel);
        } //OpprettPartikkel

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            partikkelTekstur = game.Content.Load<Texture2D>(@"Sprites\explosion");
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
        public override void Update(GameTime gameTime) {
            //finnes det partikler i listen?
            if (partikkelListe.Count > 0) {
                //oppdater partiklene som finnes i listen
                OppdaterPartikler(gameTime);
            } //if (partikkelListe.Count > 0)
            base.Update(gameTime);
        } //Update

        private void OppdaterPartikler(GameTime gameTime) {
            //hent ut gjeldende tidspunkt
            float naaTid = (float)gameTime.TotalGameTime.TotalMilliseconds;
            //loop gjennom eksisterende partikler i listen
            for (int i = partikkelListe.Count - 1; i >= 0; i--) {
                //hent partikkel fra listen
                PartikkelInformasjon partikkel = partikkelListe[i];
                float levetid = naaTid - partikkel.PartikkelSkapt;
                //har partikkelen levd over sin gitte levealder?
                if (levetid > partikkel.MaxAlder) {
                    //fjern partikkel fra listen
                    partikkelListe.RemoveAt(i);
                } else {
                    float relAge = levetid / partikkel.MaxAlder;
                    //flytt partikkel
                    partikkel.Posisjon = 0.5f * partikkel.Akselerasjon * relAge * relAge + partikkel.Retning * relAge + partikkel.StartPosisjon;
                    //sett ny farge på partikkel basert på alder
                    float invAge = 1.0f - relAge;
                    partikkel.Farge = new Color(new Vector4(invAge, invAge, invAge, invAge));
                    //hent ut hvor mye partikkelen har forflyttet seg
                    Vector2 posisjonFraSenter = partikkel.Posisjon - partikkel.StartPosisjon;
                    float distanse = posisjonFraSenter.Length();
                    partikkel.Skalering = (50.0f + distanse) / 200.0f;
                    //legg oppdatert partikkel tilbake i listen
                    partikkelListe[i] = partikkel;
                } //if (levetid > partikkel.MaxAlder)
            } //for
        } //OppdaterPartikler

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime) {
            //tegn tekstur uten den svarte bakgrunnen
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
            TegnEksplosjon();
            spriteBatch.End();
            //tilbakestill verdier etter å ha brukt SpriteBatch
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
            base.Draw(gameTime);
        } //Draw

        private void TegnEksplosjon() {
            for (int i = 0; i < partikkelListe.Count; i++) {
                PartikkelInformasjon partikkel = partikkelListe[i];
                //tegn partikkelen
                spriteBatch.Draw(partikkelTekstur, partikkel.Posisjon, null, partikkel.Farge, i,
                                    new Vector2(256, 256), partikkel.Skalering, SpriteEffects.None, 1);
            } //for
        } //TegnEksplosjon
    } //PartikkelEffekter
} //namespace