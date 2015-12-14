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
        public void Update()
        {
            currKeyboardState = Keyboard.GetState();

            

            prevKeyboardState = currKeyboardState;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userInput"></param>
        /// <param name="currKbState"></param>
        /// <param name="prevKbState"></param>
        /// <returns></returns>
        public bool CheckForInput(Keys userInput, KeyboardState currKbState, KeyboardState prevKbState)
        {
            if ((currKbState.IsKeyDown(userInput)) && (prevKbState.IsKeyUp(userInput)))
            {
                return true;
            }
            return false;
        }
    }
}
