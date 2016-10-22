using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Input;

namespace Oblig3_KnutLucasAndersen
{
    public class Camera : Microsoft.Xna.Framework.GameComponent
    {
        //En referanse til input-komponenten:
        protected IInputHandler input;
        private GraphicsDeviceManager graphics;
        private Matrix projection;
        private Matrix view;
        private Vector3 cameraPosition = new Vector3(0.0f, 0.0f, 1000.0f);
        private Vector3 cameraTarget = Vector3.Zero;
        private Vector3 cameraUpVector = Vector3.Up;
        private Vector3 cameraReference = new Vector3(0.0f, 0.0f, -1.0f);
        private float cameraYaw = 0.0f;
        private float cameraPitch = 0.0f;
        private const float spinRate = 40.0f;
        private const float moveRate = 40.0f; // for FirstPersonCamra.
        protected Vector3 movement = Vector3.Zero; // for FirstPersonCamera.

        //view og projection-matrisene er tilgjengelig via properties:
        public Matrix View {
            get { return view; }
            set { view = value; }
        }

        public Matrix Projection {
            get { return projection; }
            set { projection = value; }
        }

        public Camera(Game game)
        : base(game) {
            graphics = (GraphicsDeviceManager)game.Services.GetService(typeof(IGraphicsDeviceManager));
            //Henter ut en referanse til input-handleren:
            input = (IInputHandler)game.Services.GetService (typeof(IInputHandler));
        }

        public override void Initialize() {
            base.Initialize();
            this.InitializeCamera();
        }

        private void InitializeCamera() {
            float aspectRatio = (float)graphics.GraphicsDevice.Viewport.Width / (float)graphics.GraphicsDevice.Viewport.Height;
            Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1.0f, 1000.0f, out projection);
            Matrix.CreateLookAt(ref cameraPosition, ref cameraTarget, ref cameraUpVector, out view);
        }

        public override void Update(GameTime gameTime) {
            //timeDelta = tiden mellom to kall på Update
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (input.KeyboardState.IsKeyDown(Keys.Left)) {
                cameraYaw = cameraYaw + (spinRate * timeDelta);
            }
            if (input.KeyboardState.IsKeyDown(Keys.Right)) {
                cameraYaw = cameraYaw - (spinRate * timeDelta);
            }
            if (cameraYaw > 360) {
                cameraYaw -= 360;
            } else if (cameraYaw < 0) {
                cameraYaw += 360;
            }

            //OPP/NED (PITCH):
            if (input.KeyboardState.IsKeyDown(Keys.Down)) {
                cameraPitch = cameraPitch - (spinRate * timeDelta);
            }
            if (input.KeyboardState.IsKeyDown(Keys.Up)) {
                cameraPitch = cameraPitch + (spinRate * timeDelta);
            }
            if (cameraPitch > 89) {
                cameraPitch = 89;
            } else if (cameraPitch < -89) {
                cameraPitch = -89;
            }


            Matrix rotationMatrix;
            //Rotasjonsmatrise om Y‐aksen:
            Matrix.CreateRotationY(MathHelper.ToRadians(cameraYaw), out rotationMatrix);
            //Legger til pitch dvs. rotasjon om X‐aksen:
            rotationMatrix = Matrix.CreateRotationX(MathHelper.ToRadians(cameraPitch)) * rotationMatrix;
            // Oppretter en hjelpevektor som peker i retninga kameraet 'ser':
            Vector3 transformedReference;
            // Roterer cameraReference-vektoren:
            Vector3.Transform(ref cameraReference, ref rotationMatrix, out transformedReference);
            // Beregner hva kameraet ser på (cameraTarget) vha.
            // nåværende posisjonsvektor og retningsvektoren:
            Vector3.Add(ref cameraPosition, ref transformedReference, out cameraTarget);

            //FirstPersonCamera, endrer kameraets posisjon:
            movement *= (moveRate * timeDelta);
            if (movement != Vector3.Zero) {
                //Roterer movement-vektoren:
                Vector3.Transform(ref movement, ref rotationMatrix, out movement);
                //Oppdaterer kameraposisjonen med move-vektoren:
                cameraPosition += movement;
            }
            //Oppdaterer view-matrisa vha. posisjons, kameramål og opp-vektorene:
            Matrix.CreateLookAt(ref cameraPosition, ref cameraTarget, ref cameraUpVector, out view);

            base.Update(gameTime);
        }
    }
}