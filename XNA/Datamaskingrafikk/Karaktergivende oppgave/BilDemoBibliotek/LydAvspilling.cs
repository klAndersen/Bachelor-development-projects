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
    /// Denne klassen tar seg av alt som har med lyd og musikk å gjøre.
    /// </summary>
    internal class LydAvspilling : Microsoft.Xna.Framework.DrawableGameComponent {
        private Game game;
        private Song[] bakgrunnsMusikk;
        private SoundEffect bremsing;
        private bool erPauset = false;

        internal LydAvspilling(Game game) : base(game) {
            this.game = game;
        } //konstruktør

        protected override void  LoadContent() {
            //opprett array og fyll den med bakgrunnsmusikk
            bakgrunnsMusikk = new Song[3];
            bakgrunnsMusikk[0] = game.Content.Load<Song>(@"Lyd&Musikk\Atmos");
            bakgrunnsMusikk[1] = game.Content.Load<Song>(@"Lyd&Musikk\DrDestru");
            bakgrunnsMusikk[2] = game.Content.Load<Song>(@"Lyd&Musikk\partybie");
            bremsing = game.Content.Load<SoundEffect>(@"Lyd&Musikk\skid");
            //sett at bakgrunnsmusikken skal repeteres (loopes)
            MediaPlayer.IsRepeating = true;
            base.LoadContent();
        } //LoadContent

        /// <summary>
        /// Starter avspilling av bakgrunnsmusikk.
        /// </summary>
        /// <param name="sang">Nummeret (int) på hvilken sang som skal avspilles (1 - 3)</param>
        internal void StartAvspilling(int sang) {
            //trekk fra en, siden array starter på null
            sang--;
            //start avspilling av valgt sang
            MediaPlayer.Play(bakgrunnsMusikk[sang]);
        } //StartAvspilling

        internal void AvspillBremsing() {
            //er spillets lyd slått av?
            if (!Stillhet) {
                //avspill bremselyd
                bremsing.Play();
            } //if (!Stillhet)
        } //AvspillBremsing

        public override void Update(GameTime gameTime) {
            //er lyden av og musikken ikke pauset?
            if (Stillhet && !erPauset) {
                //sett musikk på pause
                MediaPlayer.Pause();
                erPauset = true;
            } else if (erPauset && !Stillhet) { //er musikken pauset og lyden på
                //fortsett avspilling av musikk
                MediaPlayer.Resume();
                erPauset = false;
            } //if (!Stillhet)
            base.Update(gameTime);
        } //Update

        #region GET OG SET METODER
        internal static bool Stillhet {
            get {
                return MediaPlayer.IsMuted;
            }
            set {
                MediaPlayer.IsMuted = value;
            }
        }
        #endregion
    } //LydAvspilling
} //namespace