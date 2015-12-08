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

        //public bool IsClicked()
        //{

        //}

    }
}
