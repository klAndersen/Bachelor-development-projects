using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Oblig3_KnutLucasAndersen
{
    //Interface som brukes i forbindels med game service:
    public interface IInputHandler
    {
        KeyboardState KeyboardState { get; } 
    };

    public class InputHandler : Microsoft.Xna.Framework.GameComponent, IInputHandler {
        private KeyboardState keyboardState;

        public InputHandler(Game game)
            : base(game) {
                game.Services.AddService(typeof(IInputHandler), this);
        }

        public override void Initialize()
        {
            // TODO: Add your initialization code here
            base.Initialize();
        }

        public KeyboardState KeyboardState
        {
            get { return (keyboardState); }
        }

        public override void Update(GameTime gameTime)
        {
            //Esc for avslutt:
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
            Game.Exit();
            }
            base.Update(gameTime);
        }
    }
}