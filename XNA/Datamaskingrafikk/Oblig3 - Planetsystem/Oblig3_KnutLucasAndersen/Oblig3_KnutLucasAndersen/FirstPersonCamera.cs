using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Input;

namespace Oblig3_KnutLucasAndersen {
    public class FirstPersonCamera : Camera {

        public FirstPersonCamera(Game game)
            : base(game) {
        }

        public override void Update(GameTime gameTime) {
            movement = Vector3.Zero;
            if (input.KeyboardState.IsKeyDown(Keys.A))
                movement.X--;
            if (input.KeyboardState.IsKeyDown(Keys.D))
                movement.X++;
            if (input.KeyboardState.IsKeyDown(Keys.S))
                movement.Z++;
            if (input.KeyboardState.IsKeyDown(Keys.W))
                movement.Z--;
            //Sikrer oss at farta ikke øker dersom vi holder nede
            //både D og S samtidig (diagonal bevegelse):
            if (movement.LengthSquared() != 0)
                movement.Normalize();
            base.Update(gameTime);
        }
    }
}