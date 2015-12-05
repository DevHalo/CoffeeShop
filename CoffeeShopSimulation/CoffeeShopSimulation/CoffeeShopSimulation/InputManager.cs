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
        private MouseState prevMouseState;
        private MouseState currMouseState;

        // Used to check if the mouse is currently being clicked
        public bool IsClicked { get; private set; }

        /// <summary>
        /// Used to create an instance of InstanceManager
        /// </summary>
        public InputManager()
        {

        }

        /// <summary>
        /// Used to check for new mouse state and to see if the user is clicking
        /// </summary>
        public void Update()
        {
            currMouseState = Mouse.GetState();

            if ((currMouseState.LeftButton == ButtonState.Pressed) &&
                (prevMouseState.LeftButton == ButtonState.Released))
            {
                IsClicked = true;
            }

            prevMouseState = currMouseState;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ResetIsClicked()
        {
            IsClicked = false;
        }
    }
}
