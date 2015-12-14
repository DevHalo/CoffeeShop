// Author: Sanjay Paraboo
// Class Name: InputManager.cs
// Date Created: Dec 5th 2015
// Date Modified: Dec 5th 2015
// Description: Handles all input by the user
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace CoffeeShopSimulation
{
    class InputManager
    {
        // Used to store the previous and current mouse states
        private KeyboardState currKeyboardState;
        private KeyboardState prevKeyboardState;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userInput"></param>
        /// <param name="currKbState"></param>
        /// <param name="prevKbState"></param>
        /// <returns></returns>
        public bool CheckForInput(Keys userInput, KeyboardState currKbState, KeyboardState prevKbState)
        {
            // Checks to see if the key to check for has been currently down and previously up
            return ((currKbState.IsKeyDown(userInput)) && (prevKbState.IsKeyUp(userInput)));
        }

        /// <summary>
        /// Gets latest input
        /// </summary>
        public void Tick()
        {
            currKeyboardState = Keyboard.GetState();
        }

        /// <summary>
        /// Stores current input to be compared with the next input
        /// </summary>
        public void Tock()
        {
            prevKeyboardState = currKeyboardState;
        }
    }
}
