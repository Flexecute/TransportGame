using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Transport
{
    class InputHandler
    {
        City city;

        // Mouse input
        MouseState lastMouseState;
        MouseState currentMouseState;

        public InputHandler(City city)
        {
            this.city = city;
        }

        public Boolean handleInput(MouseState currentMouseState, KeyboardState currentKeyboardState) {

            // If they hit esc, exit
            if (currentKeyboardState.IsKeyDown(Keys.Escape))
            {
                return true;
            }

                // Get the mouse state relevant for this frame
                //currentMouseState = Mouse.GetState();

                // Recognize a single click of the left mouse button
                if (lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
            {
                // React to the click

            }

            lastMouseState = currentMouseState;

            return false;


        }
    }
}
