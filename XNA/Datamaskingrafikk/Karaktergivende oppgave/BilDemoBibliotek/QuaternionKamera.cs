using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BilDemoBibliotek {
    /// <summary>
    /// Dette er en Kamera-klasse basert på Quaternion, som i tillegg 
    /// håndterer input fra bruker (tastaturinput). 
    ///<para>
    /// Dette betyr at klassen har ansvar for posisjonering og forflytting 
    /// både av kamera og bilen som bruker kontrollerer.
    /// </para>
    /// For å unngå problemer med arv og overstyring av metoder er klassen forseglet (sealed).
    /// Klassen er delvis basert på Riemers Quaternion s. 50 - 57 i Game XNA 3.0 Programming Recipes
    /// </summary>
    public sealed class QuaternionKamera : Microsoft.Xna.Framework.GameComponent {
        #region VARIABLER
        //konstanter
        private const int MIN_GIR = 1;
        private const int MAX_GIR = 4;
        /// <summary>
        /// Verdien for Gir: Revers
        /// </summary>
        public const int REVERS = 5;
        private const int MAX_ZOOM = 2;
        private GraphicsDevice device;
        //vektorer
        private static Vector3 posisjon;
        //matriser
        private static Matrix projectionMatrix;
        private static Matrix viewMatrix;
        private static Quaternion kameraRotasjon;
        //verdier for kamera og forflytning
        private float viewAngle;
        private float aspectRatio;
        private float nearPlane;
        private float farPlane;
        private static int girNivaa;
        private int forflytning = 0;
        private static int zoom = 1;
        //verdi for om bruker ønsker å starte et nytt spill
        private static bool nyttSpill = true;
        //KeyBoardState som tar vare på hvilken knapp som ble sist trykket
        //(dette for å unngå f.eks. at giring går fra 1 -> 4 på et tastetrykk)
        //funnet via VS Hjelp: F1
        private KeyboardState forrigeTast;
        private static bool visHjelp;
        //lydkontroll variabler
        private bool muteLyd;
        private LydAvspilling lydAvpilling;
        #endregion

        /// <summary>
        /// Konstruktøren til QuaternionKamera, her opprettes en kobling 
        /// til klassen LydAvspilling som håndterer lyd og musikk.
        /// </summary>
        /// <param name="game">Objekt av Game</param>
        public QuaternionKamera(Game game)
            : base(game) {
                lydAvpilling = new LydAvspilling(game);
                game.Components.Add(lydAvpilling);
        } //konstruktør

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run. This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize() {
            device = Game.GraphicsDevice;
            base.Initialize();            
            //opprett projectionmatrix basert på Properties
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(ViewAngle, AspectRatio, NearPlane, FarPlane);
            ResetSpill();
        } //Initialize

        private void ResetSpill() {
            //stopper bilen
            forflytning = 0;
            //setter forrige tast til gjeldende
            forrigeTast = Keyboard.GetState();
            //setter gir til første gir
            GirNivaa = MIN_GIR;
            Synsvinkel = 1;
            VisHjelp = false;
            StartNyttSpill = true;
            //setter startposisjon for kamera i "spillverdenen"
            posisjon = new Vector3(-25.0f, 0.40f, -6.5f);
            //resetter/tømmer kameraRotasjon
            kameraRotasjon = Quaternion.Identity;
            //start avspilling av sang1
            lydAvpilling.StartAvspilling(1);
            //oversend til SpriteBehandling at sang1 avspilles
            SpriteBehandling.AktivMusikk = 1;
            //oppdaterer view matrisen
            OppdaterViewMatrix();
        } //resetSpill

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime) {
            //les tastatur input fra bruker
            UpdateInput();
            //sjekk om kamera/bil krasjer med skybox
            KrasjerMedSkybox();
            base.Update(gameTime);
        } //Update

        #region UPDATES
        /// <summary>
        /// Oppdaterer kameraposisjon og forflytning basert på hvilken tast som ble trykket, 
        /// og det sjekkes også at gjeldende tast ikke er holdt inne
        /// (dette for å unngå at gir går fra 1 -> 4 på et tastetrykk)
        /// </summary>
        private void UpdateInput() {
            float venstrehoyreRotasjon = 0.0f;
            //sjekk hvilken knapp som ble trykket
            KeyboardState nyTast = Keyboard.GetState();

            //ønsker bruker å avslutte?
            if (nyTast.IsKeyDown(Keys.Escape)) {
                //avslutter og lukker programmet
                Game.Exit();
            } //if (nyTast.IsKeyDown(Keys.Escape))

            #region AVSPILLING AV LYD/MUSIKK
            //er tast 1 trykket; isåfall start avspilling av sang1
            if ((nyTast.IsKeyDown(Keys.D1) && !forrigeTast.IsKeyDown(Keys.D1)) 
                            || (nyTast.IsKeyDown(Keys.NumPad1) && !forrigeTast.IsKeyDown(Keys.NumPad1))) {
                //start avspilling av sang1
                lydAvpilling.StartAvspilling(1);
                //oversend til SpriteBehandling at sang1 avspilles
                SpriteBehandling.AktivMusikk = 1;
            } //if (nyTast.IsKeyDown(Keys.D1) || nyTast.IsKeyDown(Keys.NumPad1))

            if ((nyTast.IsKeyDown(Keys.D2) && !forrigeTast.IsKeyDown(Keys.D2)) 
                            || (nyTast.IsKeyDown(Keys.NumPad2) && !forrigeTast.IsKeyDown(Keys.NumPad2))) {
                //start avspilling av sang1
                lydAvpilling.StartAvspilling(2);
                //oversend til SpriteBehandling at sang2 avspilles
                SpriteBehandling.AktivMusikk = 2;
            } //if (nyTast.IsKeyDown(Keys.D2) || nyTast.IsKeyDown(Keys.NumPad2)) 

            if ((nyTast.IsKeyDown(Keys.D3) && !forrigeTast.IsKeyDown(Keys.D3)) 
                            || (nyTast.IsKeyDown(Keys.NumPad3) && !forrigeTast.IsKeyDown(Keys.NumPad3))) {
                //start avspilling av sang1
                lydAvpilling.StartAvspilling(3);
                //oversend til SpriteBehandling at sang3 avspilles
                SpriteBehandling.AktivMusikk = 3;
            } //if (nyTast.IsKeyDown(Keys.D3) || nyTast.IsKeyDown(Keys.NumPad3))

            //skal lyd slåes av/på?
            if (nyTast.IsKeyDown(Keys.M) && !forrigeTast.IsKeyDown(Keys.M)) {
                muteLyd = !muteLyd;        
                LydAvspilling.Stillhet = muteLyd;                
            } //if (nyTast.IsKeyDown(Keys.M) && !forrigeTast.IsKeyDown(Keys.M))
            #endregion

            //skal bilen stoppe (bremse)?
            if (nyTast.IsKeyDown(Keys.Space) && !forrigeTast.IsKeyDown(Keys.Space)) {
                //stopp forflytning
                forflytning = 0;
                //sett bilen i første gir
                GirNivaa = MIN_GIR;
                //spill av bremselyd
                lydAvpilling.AvspillBremsing();
            } //if (nyTast.IsKeyDown(Keys.Space)

            #region FLYTT BIL FREMOVER
            //skal bilen flyttes fremover?
            if (nyTast.IsKeyDown(Keys.Up) && !forrigeTast.IsKeyDown(Keys.Up)) {
                forflytning = -1;
                //er nåværende gir revers?
                if (GirNivaa == REVERS) {
                    //sett bilen i første gir
                    GirNivaa = MIN_GIR;
                } //if (GirNivaa == REVERS)
            } //if (nyTast.IsKeyDown(Keys.Up)
            if (nyTast.IsKeyDown(Keys.W) && !forrigeTast.IsKeyDown(Keys.W)) {
                forflytning = -1;
                //er nåværende gir revers?
                if (GirNivaa == REVERS) {
                    //sett bilen i første gir
                    GirNivaa = MIN_GIR;
                } //if (GirNivaa == REVERS)            
            } //if (nyTast.IsKeyDown(Keys.Up)
            #endregion

            #region FLYTT BIL BAKOVER
            //skal bilen forflyttes bakover?
            if (nyTast.IsKeyDown(Keys.Down) && !forrigeTast.IsKeyDown(Keys.Down)) {
                forflytning = +1;
                GirNivaa = REVERS;
            } //if (nyTast.IsKeyDown(Keys.Down)
            //skal bilen forflyttes bakover?
            if (nyTast.IsKeyDown(Keys.S) && !forrigeTast.IsKeyDown(Keys.S)) {
                forflytning = +1;
                GirNivaa = REVERS;
            } //if (nyTast.IsKeyDown(Keys.Down)
            #endregion

            //skal bilen forflyttes til høyre?
            if (nyTast.IsKeyDown(Keys.Right) || nyTast.IsKeyDown(Keys.D)) {
                venstrehoyreRotasjon = -0.05f;
            } //if (nyTast.IsKeyDown(Keys.Right)

            //skal bilen forflyttes til venstre?
            if (nyTast.IsKeyDown(Keys.Left) || nyTast.IsKeyDown(Keys.A)) {
                venstrehoyreRotasjon = 0.05f;
            } //if (nyTast.IsKeyDown(Keys.Left)

            //zooming
            if (nyTast.IsKeyDown(Keys.Z) && !forrigeTast.IsKeyDown(Keys.Z)) {
                Synsvinkel++;
            } //if (nyTast.IsKeyDown(Keys.Left)

            #region GIR OPP/NED
            if (nyTast.IsKeyDown(Keys.Q) && !forrigeTast.IsKeyDown(Keys.Q)) {
                //er gjeldende gir lavere enn høyeste gir?
                if (GirNivaa < MAX_GIR && GirNivaa != REVERS) {
                    GirNivaa++;
                } //if (GirNivaa < MAX_GIR)
            } //if (nyTast.IsKeyDown(Keys.Q)) 

            if (nyTast.IsKeyDown(Keys.E) && !forrigeTast.IsKeyDown(Keys.E)) {
                //er gjeldende gir høyere enn laveste gir?
                if (GirNivaa > MIN_GIR && GirNivaa != REVERS) {
                    GirNivaa--;
                } //if (GirNivaa > MIN_GIR)
            } //if (nyTast.IsKeyDown(Keys.Left)) 
            #endregion

            #region HJELP
            //vil bruker vise/skjule hjelp?
            if (nyTast.IsKeyDown(Keys.F1) && !forrigeTast.IsKeyDown(Keys.F1)) {
                VisHjelp = !VisHjelp;
            } //if (nyTast.IsKeyDown(Keys.F1))
            //vil bruker vise/skjule hjelp?
            if (nyTast.IsKeyDown(Keys.H) && !forrigeTast.IsKeyDown(Keys.H)) {
                VisHjelp = !VisHjelp;
            } //if (nyTast.IsKeyDown(Keys.H))
            #endregion

            //vil bruker starte et nytt spill?
            if (nyTast.IsKeyDown(Keys.R) && !forrigeTast.IsKeyDown(Keys.R)) {                
                ResetSpill();
            } //if (nyTast.IsKeyDown(Keys.R))

            //sett forrige tast til tasten som ble trykket nå
            forrigeTast = nyTast;
            //oppdater posisjonen til kamera
            OppdaterPosisjon(new Vector3(0, 0, forflytning), venstrehoyreRotasjon);            
        } //UpdateInput

        /// <summary>
        /// Oppdaterer bilens posisjon basert på knapp som ble trykket
        /// </summary>
        /// <param name="nyPosisjon">Den nye posisjonen: Vector3</param>
        /// <param name="venstrehoyreRotasjon">rotasjon på x-aksen (mot høyre eller mot venstre): float</param>
        private void OppdaterPosisjon(Vector3 nyPosisjon, float venstrehoyreRotasjon) {
            //siden spiller kun skal flytte bil langs x - og z-akse er denne alltid null
            //skulle spiller kunne forflytte seg oppover måtte denne vært med i if-test
            //for Keys.Up/W og Keys.Down/S
            float oppnedRotasjon = 0.0f;
            //beregn en rotasjon som går opp eller ned ved bruk av Quaternion
            Quaternion oppnedRotQuat = Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), oppnedRotasjon);
            //beregn en rotasjon som går til høyre eller venstre ved bruk av Quaternion
            Quaternion vhRotQuat = Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), venstrehoyreRotasjon);
            //finn tilleggsrotasjonen ved å gange rotasjonen rundt aksene med hverandre (rekkefølge likegyldig)
            Quaternion tilleggsRotasjon = oppnedRotQuat * vhRotQuat;
            //beregn kameraets rotasjon ved å multiplisere kameraets nåværende posisjon med tilleggsrotasjonen
            //Quaternion er ikke som vanlig matrisemultiplikasjon, så her har rekkefølgen betydning 
            //(den var likegyldig over pga aksene er loddrette ovenfor hverandre)
            //her vil tilleggsRotasjon først bli rotert rundt aksen til kameraRotasjon
            kameraRotasjon = kameraRotasjon * tilleggsRotasjon;
            //transformerer forflytningen med rotasjonen rundt aksene for å lage ny vektor
            Vector3 rotertVektor = Vector3.Transform(nyPosisjon, kameraRotasjon);
            //oppdaterer posisjonen
            posisjon += Fart * rotertVektor;
            OppdaterViewMatrix();
        } //oppdaterPosisjon

        private void OppdaterViewMatrix() {
            //sett originalverdier
            Vector3 originalZ = new Vector3(0, 0, 1);
            Vector3 originalOppVektor = new Vector3(0, 1, 0);
            //transformer rotasjon om z-aksen
            Vector3 kameraRotertZ = Vector3.Transform(originalZ, kameraRotasjon);
            Vector3 endeligZ = posisjon + kameraRotertZ;
            //transformer rotasjon om y-aksen           
            Vector3 kameraRotertY = Vector3.Transform(originalOppVektor, kameraRotasjon);
            //oppdater view matrisen
            viewMatrix = Matrix.CreateLookAt(endeligZ, posisjon, kameraRotertY);
        } //oppdaterViewMatrix

        private void KrasjerMedSkybox() {
            //hent ut kameraets gjeldende posisjon
            float x = Posisjon.X,
                y = Posisjon.Y,
                z = Posisjon.Z;
            float beregnNyPos = 0.0f;
            //opprett array som inneholder posisjon for veggene til skybox
            //og objekter som er inkludert i skybox
            float[,] krasjArray = {                                      
                                      {
                                          //x-posisjon vegger skybox
                                          -42.2f, //0
                                          43.5f, //1
                                          //x-posisjon garasje (i skybox)
                                          -34.6f, //2
                                          -40.3f, //3
                                          //x-posisjon bensinstasjon (i skybox)
                                          8.7f, //4
                                          10.0f, //5
                                      },                                      
                                      {
                                          //z-posisjon vegger skybox
                                          -41.5f, //0
                                          44.6f, //1
                                          //z-posisjon garasje (i skybox)
                                          -37.0f, //2
                                          -40.3f, //3
                                          //z-posisjon bensinstasjon (i skybox)
                                          -40.2f, //4
                                          -41.2f //5
                                      }
                                 };

            #region IF TESTER GARASJE
            //er kamera innenfor området til garasjen (hjørnekoordinater)
            if (x <= krasjArray[0, 2] && x >= krasjArray[0, 3] && z <= krasjArray[1, 2] && z >= krasjArray[1, 3]) {
                //for å skille mellom veggene og unngå at man går gjennom veggen eller plutselig havner
                //på siden av en annen vegg, så måtte jeg lage begrenseninger som legges til/trekkes fra hjørne
                float frontSide = krasjArray[1, 3] + 2.0f; //ta koordinat for bakvegg (z) og legg øk verdi
                float bakSide = krasjArray[1, 3]; //koordinat for bakside (z)
                float hoyreside = krasjArray[0, 2] - 1.0f; //ta koordinat for høyre hjørne (x) og legg til
                float venstreSide = krasjArray[0, 3] + 1.0f; //ta koordinat for venstre hjørne (x) og trekk fra
                //er kamera innenfor høyre garasjevegg?
                if (x >= hoyreside && z <= krasjArray[1, 2] && z >= krasjArray[1, 3]) {
                    //sett kameraets posisjon på x-aksen
                    x = krasjArray[0, 2];
                    //beregn verdien til hvor på z-aksen kameraet var, ved å ta veggens z-verdi og legge til differansen
                    //eksempel: ved krasj er z = 38; da blir beregnNyPos = 38 - 37 = 1
                    //dette gir da z = 37 + 1 = 38, altså punktet hvor kameraet kolliderte med veggen
                    beregnNyPos = z - krasjArray[1, 2];
                    z = krasjArray[1, 2] + beregnNyPos;
                } else if (x <= venstreSide && z <= krasjArray[1, 2] && z >= krasjArray[1, 3]) { //innenfor venstre vegg?
                    //sett kameraets posisjon på x-aksen
                    x = krasjArray[0, 3];
                    //beregn hvor kameraet kolliderte med veggen
                    beregnNyPos = z - krasjArray[1, 2];                    
                    z = krasjArray[1,2] + beregnNyPos;
                } else if (x <= krasjArray[0, 2] && x >= krasjArray[0, 3] && z >= frontSide) { //innenfor frontveggen?
                    //sett kameraets posisjon på z-aksen
                    z = krasjArray[1, 2];
                    //beregn hvor kameraet kolliderte med veggen
                    beregnNyPos = x - krasjArray[0, 2];
                    x = krasjArray[0, 2] + beregnNyPos;
                } else if (x <= krasjArray[0, 2] && x >= krasjArray[0, 3] && z >= bakSide) { //innenfor bakveggen?
                    //sett kameraets posisjon på z-aksen
                    z = krasjArray[1, 3];
                    //beregn hvor kameraet kolliderte med veggen
                    beregnNyPos = x - krasjArray[0, 2];                    
                    x = krasjArray[0,2] + beregnNyPos;
                } //if (x >= hoyreside ...)
            } //if (x <= krasjArray[0, 2]) ...)
            #endregion

            #region IF TESTER SKYBOX
            //krasjer kamera med en vegg/et objekt?
            if (x <= krasjArray[0, 0]) {
                //hent ut koordinatene til veggen/objektet/objektet
                x = krasjArray[0, 0];
            } //if (x <= krasjArray[0, 0])
            if (x >= krasjArray[0, 1]) {
                //hent ut koordinatene til veggen/objektet
                x = krasjArray[0, 1];
            } //if (x >= krasjArray[0, 1]) 
            if (z <= krasjArray[1, 0]) {
                //hent ut koordinatene til veggen/objektet
                z = krasjArray[1, 0];
            } //if (z <= krasjArray[1, 0])
            if (z >= krasjArray[1, 1]) {
                //hent ut koordinatene til veggen/objektet
                z = krasjArray[1, 1];
            } //if (z >= krasjArray[1, 1])
            //sett (ny) kamera posisjon
            Posisjon = new Vector3(x, y, z);
            #endregion
        } //krasjerMedSkybox
        #endregion

        #region GET & SET METODER
        /// <summary>
        /// Returnerer kameraets gjeldene posisjon (Vector3)
        /// </summary>
        internal static Vector3 Posisjon {
            get {
                return posisjon;
            }
            set {
                posisjon = value;
            }
        }

        /// <summary>
        /// Returnerer kameraets gjeldende rotasjon (Quaternion)
        /// </summary>
        internal static Quaternion Rotasjon {
            get {
                return kameraRotasjon;
            }
        }

        /// <summary>
        /// Hent eller sett verdien for ViewAngle
        /// (brukes til oppretting av ProjectionMatrix)
        /// </summary>
        public float ViewAngle {
            get {
                return viewAngle;
            }
            set {
                viewAngle = value;
            }
        }

        /// <summary>
        /// Hent eller sett verdien for AspectRatio
        /// (brukes til oppretting av ProjectionMatrix)
        /// </summary>
        public float AspectRatio {
            get {
                return aspectRatio;
            }
            set {
                aspectRatio = value;
            }
        }

        /// <summary>
        /// Hent eller sett verdien for NearPlane
        /// (brukes til oppretting av ProjectionMatrix)
        /// </summary>
        public float NearPlane {
            get {
                return nearPlane;
            }
            set {
                nearPlane = value;
            }
        }

        /// <summary>
        /// Hent eller sett verdien for FarPlane
        /// (brukes til oppretting av ProjectionMatrix)
        /// </summary>
        public float FarPlane {
            get {
                return farPlane;
            }
            set {
                farPlane = value;
            }
        }

        /// <summary>
        /// Returnerer view matrisen
        /// </summary>
        internal static Matrix ViewMatrix {
            get {
                return viewMatrix;
            }
        }

        /// <summary>
        /// Returnerer projection matrisen
        /// </summary>
        internal static Matrix ProjectionMatrix {
            get {
                return projectionMatrix;
            }
        }

        /// <summary>
        /// Returnerer aktivt gir
        /// </summary>
        internal static int GirNivaa {
            get {
                return girNivaa;
            }
            private set {
                girNivaa = value;
            }
        }

        /// <summary>
        /// Returnerer farten basert på hvilket gir som er aktivt.
        /// Farten starter på 0.05f og hastighet = fart * gir
        /// Eksempelvis: 3. gir => 0.05f * 3 = 0.15f
        /// Farten ved reversering er 0.05f
        /// </summary>
        private float Fart {
            get {
                float fart = 0.05f;
                switch (GirNivaa) {
                    case MIN_GIR:
                        fart = 0.05f;
                        break;
                    case 2:
                        fart = 0.10f;
                        break;
                    case 3:
                        fart = 0.15f;
                        break;
                    case MAX_GIR:
                        fart = 0.20f;
                        break;
                    case REVERS:                        
                        fart = 0.05f;
                        break;
                } //switch
                return fart;
            } //get
        }

        /// <summary>
        /// Returnerer true/false basert på om bruker vil se/skjule Hjelp
        /// </summary>
        internal static bool VisHjelp {
            get {
                return visHjelp;
            }
            //private slik at den ikke blir overstyrt
            private set {
                visHjelp = value;
            }
        }

        /// <summary>
        /// Returnerer synsvinkelen (zoom)
        /// Det er to visninger; bak bilen og 
        /// frontvindu (ser ikke bilen)
        /// </summary>
        internal static int Synsvinkel {
            get {
                return zoom;
            }
            private set {                
                if (zoom < MAX_ZOOM) {
                    zoom = value;
                } else {
                    zoom = 1;
                } //if (zoom < MAX_ZOOM)
            } //set
        }

        /// <summary>
        /// Returner boolsk verdi basert på om bruker ønsker å 
        /// starte et nytt spill. Hvis nytt spill SKAL startes, 
        /// sett verdi til true. Hvis nytt spill HAR startet, 
        /// sett verdi til false.
        /// </summary>
        internal static bool StartNyttSpill {
            get {
                return nyttSpill;
            }
            set {
                nyttSpill = value;
            }
        }
        #endregion
    } //QuaternionKamera
} //namespace