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
    /// Denne klassen tar for seg tegning av modeller på skjerm og i tillegg har den 
    /// kollisjonsdeteksjon. Kollisjonsdeteksjonen sjekker om bil og flagg kolliderer, 
    /// og hvis de kolliderer så genereres poeng og flagg fjernes fra skjerm.
    /// </summary>
    public class ModellBehandling : Microsoft.Xna.Framework.DrawableGameComponent {
        #region VARIABLER
        private Game game;
        //innlasting av Skybox og modeller
        private Model skyboxModell;
        private Model bilModell;
        //tilhørende matriser
        private Matrix[] flaggMatrise;
        private Matrix[] bilMatrise;
        //array som inneholder informasjon om flagg
        private FlaggStruktur[] flaggArray;
        //array som inneholder koordinater for hvor flagg skal plassers ut
        private Vector3[] flaggKoordinater;        
        //verdier for kollisjon og poeng
        private static int poeng;
        private int resterendeFlagg = 10;
        #endregion

        /// <summary>
        /// Konstruktøren til ModellBehandling
        /// </summary>
        /// <param name="game">Objekt av Game</param>
        public ModellBehandling(Game game)
            : base(game) {
                this.game = game;         
        } //konstruktør

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize() {
            float y = 0.0f;
            flaggKoordinater = new Vector3[10];
            flaggKoordinater[0] = new Vector3(-14.0f, y, -20.0f);
            flaggKoordinater[1] = new Vector3(-23.0f, y, -12.0f);
            flaggKoordinater[2] = new Vector3(-18.0f, y, 10.0f);
            flaggKoordinater[3] = new Vector3(21.0f, y, 14.0f);
            flaggKoordinater[4] = new Vector3(24.0f, y, -9.0f);
            flaggKoordinater[5] = new Vector3(0.0f, y, -27.0f);
            flaggKoordinater[6] = new Vector3(33.0f, y, 12.0f);
            flaggKoordinater[7] = new Vector3(10.0f, y, 29.0f);
            flaggKoordinater[8] = new Vector3(-30.0f, y, 31.0f);
            flaggKoordinater[9] = new Vector3(15.0f, y, 19.0f);
            base.Initialize();
        } //Initialize

        /// <summary>
        /// Override LoadContent; 
        /// Laster inn modellene som skal brukes
        /// </summary>
        protected override void LoadContent() {
            //last inn modeller
            skyboxModell = game.Content.Load<Model>(@"Modeller\Tribune");
            bilModell = LoadModelWithBoundingSphere(@"Modeller\Bil", ref bilMatrise);
            //opprett array
            flaggArray = new FlaggStruktur[10];
            //løkke som fyller array'n med verdier
            for (int i = 0; i < 10; i++) {
                //opprett et objekt av strukturen
                FlaggStruktur flagg = new FlaggStruktur();
                //sett verdiene til strukturen
                flagg.FlaggModell = LoadModelWithBoundingSphere(@"Modeller\Flagg", ref flaggMatrise);
                flagg.FlaggMatrise = flaggMatrise;
                //henter ut gjeldende flaggs posisjon basert på satt koordinat i array
                flagg.Posisjon = flaggKoordinater[i];                
                flagg.TegnFlagg = true;
                //legg objektet av strukturen i array
                flaggArray[i] = flagg;
            } //for
            base.LoadContent();
        } //LoadContent

        //tatt fra forelesningsnotat "Del14 - Kollisjonstest"
        private Model LoadModelWithBoundingSphere(String modelName, ref Matrix[] matrix) {
            Model model = game.Content.Load<Model>(modelName);
            matrix = new Matrix[model.Bones.Count];
            //Legger komplett transformasjonsmatrise for hver ModelMesh i matrisetabellen:
            model.CopyAbsoluteBoneTransformsTo(matrix);
            //Finner BoundingSphere for hele modellen:
            BoundingSphere completeBoundingSphere = new BoundingSphere();
            foreach (ModelMesh mesh in model.Meshes) {
                //Henter ut BoundigSphere for aktuell ModelMesh:
                BoundingSphere origMeshSphere = mesh.BoundingSphere;
                //Denne transformeres i forhold til sitt Bone:
                origMeshSphere = XNAUtils.TransformBoundingSphere(origMeshSphere, matrix[mesh.ParentBone.Index]);
                //Slår sammen:
                completeBoundingSphere = BoundingSphere.CreateMerged(completeBoundingSphere, origMeshSphere);
            } //foreach
            model.Tag = completeBoundingSphere;
            return model;
        } //LoadModelWithBoundingSphere

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime) {
            //har bruker startet nytt spill?
            if (QuaternionKamera.StartNyttSpill) {
                //nullstill poengsum
                Poeng = 0;
                //sett at alle flagg er på skjerm
                resterendeFlagg = 10;
                //loop gjennom array og sett at alle flaggene skal tegnes
                for (int i = 0; i < flaggArray.Length; i++) {
                    flaggArray[i].TegnFlagg = true;
                } //for
                //sett at nytt spill har startet
                QuaternionKamera.StartNyttSpill = false;
            } //if (kamera.StartNyttSpill)
            base.Update(gameTime);
        } //Update
        
        /// <summary>
        /// Utfører selve tegningen av modellene.
        /// Overrider Draw(GameTime)
        /// </summary>
        /// <param name="gameTime">Objekt av GameTime</param>
        public override void Draw(GameTime gameTime) {
            OpprettSkybox();
            //siden det kun er to verdier for zoom, så settes det her at 
            //bilen kun skal tegnes dersom bruker ønsker å se bilen
            //(dersom det blir lagt til flere, må denne oppdateres/endres)
            if (QuaternionKamera.Synsvinkel == 1) {
                //legg ut bilmodellen på skjermen
                TegnBil();
            } //if (kamera.Synsvinkel == 1)
            //sjekk om bil og flagg har kollidert
            SjekkEtterKollisjon();
            //tegn flagg på skjerm
            TegnFlagg();
            base.Draw(gameTime);
        } //Draw

        #region KOLLISJONSMETODER
        private void SjekkEtterKollisjon() {
            //loop gjennom alle flaggene som finnes i array
            for (int i = 0; i < flaggArray.Length; i++) {
                FlaggStruktur flagg = flaggArray[i];
                //kjør if-test kun på flagg som skal tegnes på skjerm
                if (flagg.TegnFlagg) {
                    //world matrisen for flagg, skrevet på egen linje for bedre oversikt
                    Matrix worldMatrixFlagg = ReturnWorldMatrixFlagg(flagg.Posisjon);
                    //hent verdi for kollisjon
                    bool modellerKollidert = FinkornetModellKollisjon(bilModell, ReturnWorldMatrixBil, 
                                            flagg.FlaggModell, worldMatrixFlagg);                    
                    //sjekk om bil og flagg kolliderer
                    if (modellerKollidert) {
                            //øk poeng
                            Poeng++;
                            //trekk fra resterende flagg
                            resterendeFlagg--;
                            //sett at dette flagget ikke skal tegnes, m.a.o. det er påkjørt
                            flaggArray[i].TegnFlagg = false;
                    } //if (modellerKollidert)
                } //if (flagg.TegnFlagg)
            } //for
            //er alle flaggene sanket inn?
            if (resterendeFlagg == 0) {
                //loop gjennom array og sett at alle flaggene skal tegnes
                for (int i = 0; i < flaggArray.Length; i++) {
                    flaggArray[i].TegnFlagg = true;
                } //for
                resterendeFlagg = 10;
            } //if (resterendeFlagg == 0) 
        } //SjekkEtterKollisjon

        //tatt fra XNA Game Programming Recipes s. 321
        private bool ModellKollisjon(Model model1, Matrix world1, Model model2, Matrix world2) {
            //henter den orginale boundingsphere
            BoundingSphere origSphere1 = (BoundingSphere)model1.Tag;
            //oppretter en ny boundingsphere basert på modellens forflytning
            BoundingSphere sphere1 = XNAUtils.TransformBoundingSphere(origSphere1, world1);
            BoundingSphere origSphere2 = (BoundingSphere)model2.Tag;
            BoundingSphere sphere2 = XNAUtils.TransformBoundingSphere(origSphere2, world2);
            //henter verdi basert på om modellene etter forflytning kolliderer
            bool collision = sphere1.Intersects(sphere2);
            return collision;
        } //ModelsCollide

        //basert på XNA Game Programming Recipes s.325
        private bool FinkornetModellKollisjon(Model model1, Matrix world1, Model model2, Matrix world2) {
            //sjekk om de globale boundingsphere kolliderer
            if (ModellKollisjon(model1, world1, model2, world2) == false) {
                return false;
            } //if (ModelsCollide(...)
            //opprett variabler med verdiene til model1
            Matrix[] model1Transforms = new Matrix[model1.Bones.Count];
            model1.CopyAbsoluteBoneTransformsTo(model1Transforms);
            //array som inneholder 
            BoundingSphere[] model1Spheres = new BoundingSphere[model1.Meshes.Count];
            //loop gjennom og opprett boundingsphere for hver mesh
            for (int i = 0; i < model1.Meshes.Count; i++) {
                ModelMesh mesh = model1.Meshes[i];
                BoundingSphere origSphere = mesh.BoundingSphere;
                Matrix trans = model1Transforms[mesh.ParentBone.Index] * world1;
                BoundingSphere transSphere = XNAUtils.TransformBoundingSphere(origSphere, trans);
                //legg hver mesh sin boundingsphere inn i array
                model1Spheres[i] = transSphere;
            } //for
            //gjør det samme som over for model2
            Matrix[] model2Transforms = new Matrix[model2.Bones.Count];
            model2.CopyAbsoluteBoneTransformsTo(model2Transforms);
            BoundingSphere[] model2Spheres = new BoundingSphere[model2.Meshes.Count];
            //loop gjennom og opprett boundingsphere for hver mesh
            for (int i = 0; i < model2.Meshes.Count; i++) {
                ModelMesh mesh = model2.Meshes[i];
                BoundingSphere origSphere = mesh.BoundingSphere;
                Matrix trans = model2Transforms[mesh.ParentBone.Index] * world2;
                BoundingSphere transSphere = XNAUtils.TransformBoundingSphere(origSphere, trans);
                model2Spheres[i] = transSphere;
            } //for
            //variabel for om kollisjon har oppstått
            bool collision = false;
            //loop gjennom array'ene med boundingspheres
            for (int i = 0; i < model1Spheres.Length; i++) {
                for (int j = 0; j < model2Spheres.Length; j++) {
                    //har modellene kollidert?
                    if (model1Spheres[i].Intersects(model2Spheres[j])) {
                        collision = true;
                    } //if (transSphere1.Intersects(transSphere2))
                } //indre for
            } //ytre for
            return collision;
        } //ModelsCollideFineCheck
        #endregion

        #region TEGN MODELLER
        private void OpprettSkybox() {
            Matrix[] skyMatrise = new Matrix[skyboxModell.Bones.Count];
            skyboxModell.CopyAbsoluteBoneTransformsTo(skyMatrise);
            //start å tegne modellen (skybox)
            foreach (ModelMesh mesh in skyboxModell.Meshes) {
                foreach (BasicEffect effect in mesh.Effects) {
                    //setter opp skyboxen
                    effect.World = skyMatrise[mesh.ParentBone.Index] * ReturnWorldMatrix;
                    effect.View = QuaternionKamera.ViewMatrix;
                    effect.Projection = QuaternionKamera.ProjectionMatrix;
                    //slår på lys
                    effect.LightingEnabled = true;
                    effect.AmbientLightColor = new Vector3(0.1f, 0.1f, 0.1f);
                    effect.PreferPerPixelLighting = true;
                    //setter på lysretning og retning for lyset
                    effect.DirectionalLight0.Direction = new Vector3(-1, -1, -1);
                    effect.DirectionalLight0.DiffuseColor = Color.White.ToVector3();
                    effect.DirectionalLight0.Enabled = true;
                    effect.DirectionalLight1.Enabled = true;
                    effect.DirectionalLight2.Enabled = false;
                } //indre foreach
                mesh.Draw();
            } //ytre foreach
        } //opprettSkybox

        private void TegnBil() {
            //start å tegne modellen (bilen)
            foreach (ModelMesh mesh in bilModell.Meshes) {
                foreach (BasicEffect effect in mesh.Effects) {
                    effect.EnableDefaultLighting();
                    effect.World = bilMatrise[mesh.ParentBone.Index] * ReturnWorldMatrixBil;
                    effect.View = QuaternionKamera.ViewMatrix;
                    effect.Projection = QuaternionKamera.ProjectionMatrix;
                } //indre foreach
                mesh.Draw();
            } //ytre foreach
        } //tegnBil

        private void TegnFlagg() {
            //loop gjennom alle flagg som ligger i array
            foreach (FlaggStruktur flagg in flaggArray) {
                //skal flagget tegnes på skjerm?
                if (flagg.TegnFlagg) {
                    //start å tegne modellen (flagg)
                    foreach (ModelMesh mesh in flagg.FlaggModell.Meshes) {
                        foreach (BasicEffect effect in mesh.Effects) {
                            effect.EnableDefaultLighting();
                            effect.World = flagg.FlaggMatrise[mesh.ParentBone.Index] * ReturnWorldMatrixFlagg(flagg.Posisjon);
                            effect.View = QuaternionKamera.ViewMatrix;
                            effect.Projection = QuaternionKamera.ProjectionMatrix;
                        } //indre foreach
                        mesh.Draw();
                    } //ytre foreach
                } //
            } //foreach
        } //tegnFlagg
        #endregion

        #region GET METODER
        private Matrix ReturnWorldMatrixBil {
            get {
                Matrix skalering = Matrix.CreateScale(1.0f / 350.0f);
                Matrix rotasjon = Matrix.CreateFromQuaternion(QuaternionKamera.Rotasjon);
                Matrix translasjon = Matrix.CreateTranslation(QuaternionKamera.Posisjon);
                //opprett worldmatrise basert på ovenstående matriser
                Matrix worldMatrix = skalering * rotasjon * translasjon;
                return worldMatrix;
            }
        }

        private Matrix ReturnWorldMatrix {
            get {
                Matrix worldMatrix = Matrix.CreateScale(0.01f, 0.01f, 0.01f);
                return worldMatrix;
            }
        }

        private Matrix ReturnWorldMatrixFlagg(Vector3 posisjon) {
            //beregn translasjon før skalering 
            //(flagg skal settes ut på forskjellige plasser)
            Matrix worldMatrix = ReturnWorldMatrix * Matrix.CreateTranslation(posisjon);
            return worldMatrix;
        }

        /// <summary>
        /// Returnerer brukers poengsum
        /// </summary>
        internal static int Poeng {
            get {
                return poeng;
            }
            private set {
                poeng = value;
            }
        }    
        #endregion
    } //ModellBehandling
} //namespace