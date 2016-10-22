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
    /// Denne klassen tar for seg alt som har med Sprites å gjøre.
    /// Dette gjelder ting som tekst, bilder o.l.
    /// </summary>
    public class SpriteBehandling : Microsoft.Xna.Framework.DrawableGameComponent {
        private Game game;
        private SpriteBatch spriteBatch;
        private Texture2D girSkifte;
        private SpriteFont arialFont, boldArialFont;
        private Partikkeleffekt partikkelEffekt;
        private static int aktivSang;

        /// <summary>
        /// Konstruktøren til SpriteBehandling
        /// </summary>
        /// <param name="game">Objekt av Game</param>
        public SpriteBehandling(Game game)
            : base(game) {
                this.game = game;
                //opprett kobling til partikkeleffekt klassen
                partikkelEffekt = new Partikkeleffekt(game);
                game.Components.Add(partikkelEffekt);
        } //konstruktør

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize() {
            base.Initialize();
        } //Initialize

        /// <summary>
        /// Laster inn Sprites
        /// </summary>
        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            arialFont = game.Content.Load<SpriteFont>(@"Sprites\ArialFont");
            boldArialFont = game.Content.Load<SpriteFont>(@"Sprites\BoldArialFont");
            girSkifte = game.Content.Load<Texture2D>(@"Sprites\Girskifte2");
            base.LoadContent();
        } //LoadContent

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime) {
            //vises hele bilen?
            if (QuaternionKamera.Synsvinkel == 1) {
                //skap en partikkeleffekt som tegnes bak eksospotta (gir inntrykk av eksos)
                partikkelEffekt.SkapPartikkeleffekt(new Vector2(559.0f, 654.0f), 1, 4.0f, 100.0f, gameTime);
            } //if (kamera.Synsvinkel == 1)
            base.Update(gameTime);
        } //Update

        /// <summary>
        /// Utfører selve tegningen av modellene.
        /// Overrider Draw(GameTime)
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime) {
            TegnGirskifte();
            VisInformasjonPaaSkjerm();
            base.Draw(gameTime);
        } //Draw

        #region TEKST PÅ SKJERM
        /// <summary>
        /// Oppretter forskjellige tekststrenger som skal vises på skjerm
        /// </summary>
        public void VisInformasjonPaaSkjerm() {
            string valgtGir, valgtSynsvinkel;
            //hvilken synsvinkel er aktiv?
            if (QuaternionKamera.Synsvinkel == 1) {
                valgtSynsvinkel = "Vis bil";
            } else {
                valgtSynsvinkel = "Frontvindu";
            } // if (kamera.Synsvinkel == 1)
            //er aktivt gir Revers?
            if (QuaternionKamera.GirNivaa == QuaternionKamera.REVERS) {
                //sett string valgtGir med teksten Revers
                valgtGir = "Revers";
            } else { //ikke revers, vis girets tallverdi
                valgtGir = QuaternionKamera.GirNivaa.ToString();
            } //if (kamera.VisGirNivaa == QuaternionKamera.REVERS)
            //informasjonstekst som vises øverst i venstre hjørne
            string info = "Poeng: " + ModellBehandling.Poeng.ToString() + "\n"
                        + "Synsvinkel: " + valgtSynsvinkel + "\n"
                        + "Bakgrunnsmusikk valgt: " + AktivMusikk + "\n";
            //er lyden av?
            if (LydAvspilling.Stillhet) {
                info += "Lyd: Lyd er av";
            } else {
                info += "Lyd: Lyd er paa";
            } //if (LydAvspilling.Stillhet)
            //skriv ut tekst på skjerm
            VisTekstPaaSkjerm(arialFont, info, 1, 1, Color.Black, Color.White);
            //tekst som vises nederst til venstre (rødt for bedre synlighet mot veien)
            VisTekstPaaSkjerm(arialFont, "Trykk H eller F1 for hjelp", 1, 740, Color.White, Color.Red);
            //skal hjelp vises?
            if (QuaternionKamera.VisHjelp) {
                string hjelpetekst = "Hjelpemeny (trykk H eller F1 for aa skjule Hjelp):";
                VisTekstPaaSkjerm(boldArialFont, hjelpetekst, 253, 50, Color.Black, Color.Gold);
                hjelpetekst =  "- For aa bevege bilen: Bruk piltastene eller W-A-S-D\n"
                                + "- For aa sette bilen i revers: Trykk S eller Pil Ned\n"
                                + "- For aa bremse: Trykk Space (Mellomrom)\n"
                                + "- For aa gire opp: Trykk Q\n"
                                + "- For aa gire ned: Trykk E\n"
                                + "- For aa skifte synsvinkel: Trykk Z\n"
                                + "- For aa starte nytt spill: Trykk R\n"
                                + "- For aa avslutte: Trykk ESC\n"
                                + "- For aa veksle mellom bakgrunnsmusikk: Trykk 1, 2 eller 3\n"
                                + "- For aa slaa lyd av: Trykk M\n"
                                + "\n"
                                + " - Du faar poeng for hvert flagg du kjorer over\n"
                                + "  (men poengsum nullstilles ved nytt spill).";
                VisTekstPaaSkjerm(arialFont, hjelpetekst, 253,80, Color.Black, Color.Gold);
                //tekst som vises over giret (rødt for bedre synlighet mot veien)
                hjelpetekst = "Dette er aktivt gir:";
                VisTekstPaaSkjerm(arialFont, hjelpetekst, 796, 629, Color.White, Color.Red);
            } //if (kamera.VisHjelp)
        } //visInformasjonPaaSkjerm

        private void VisTekstPaaSkjerm(SpriteFont font, string tekst, int x, int y, Color bakgrunnsFarge, Color tekstFarge) {
            //Skriver tekst:
            spriteBatch.Begin();
            //Skriver teksten to ganger, først med svart bakgrunn og deretter 
            //med hvitt, en piksel ned og til venstre, slik at teksten blir mer lesbar.
            spriteBatch.DrawString(font, tekst, new Vector2(x, y), bakgrunnsFarge);
            spriteBatch.DrawString(font, tekst, new Vector2(x - 1, y - 1), tekstFarge);
            spriteBatch.End();
            //Tilbakestilling av parametere etter bruk, jfr. Shawn Hargreaves:
            //http://blogs.msdn.com/b/shawnhar/archive/2010/06/18/spritebatch-and-renderstates-in-xna-game-studio-4-0.aspx
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
        } //visTekstPaaSkjerm
        #endregion

        #region BILDE PÅ SKJERM
        //basert på kodeeksempel fra XNA Game Studio 4.0 Programming s.22
        private void TegnGirskifte() {            
            //sortering av billeddeler skjer fra øverst til venstre til øverst til høyre, 
            //deretter midten av venstre til midten av høyre
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null);
            //henter ut girnivå
            int gir = QuaternionKamera.GirNivaa - 1;
            //opprett et rektangel som inneholder "rammen" til bildet
            Rectangle rektangel = new Rectangle((gir % 3) * (girSkifte.Width / 3),
                    //beregner y basert på hvilken del av bildet som blir tegnet (øvre/nedre del)
                    (gir < 3) ? 0 : (girSkifte.Height / 2),
                    //rektangelets bredde og høyde (deler på 3 siden det er tre bilder sidelengs)
                    girSkifte.Width / 3, girSkifte.Height / 2);
            //tegner bildet på skjerm, nederst i høyre hjørne
            spriteBatch.Draw(girSkifte, new Vector2(800, 660), rektangel, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
            spriteBatch.End();
        } //TegnGirskifte
        #endregion

        /// <summary>
        /// Henter/setter en tallverdi for hvilken sang som er aktiv
        /// </summary>
        internal static int AktivMusikk {
            get {
                return aktivSang;
            }
            set {
                aktivSang = value;
            }
        }
    } //SpriteBehandling
} //namespace