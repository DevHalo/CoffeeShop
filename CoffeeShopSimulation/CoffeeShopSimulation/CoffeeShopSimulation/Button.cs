// Author: Sanjay Paraboo
// File Name: Button.cs
// Project Name: A5_DataManipulation
// Creation Date: Dec 6 2015
// Modified Date: 
// Description: This Button class is used to update, check for collision, draw the buttons and change the current screen
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CoffeeShopSimulation
{
    class Button
    {
        // Stores the button coordinates
        private Rectangle buttonRect;

        // Stores button texture
        private Texture2D buttonTexture;

        // Records current mouse state
        private MouseState prevMouseState;

        // Will be used to tell the main class whether the button has been clicked or not
        public bool IsClicked { get; private set; }

        // Used to store the SpriteFont that will be used to draw the button headings
        private SpriteFont btnFont;

        // Used to control the button color
        private Color btnColor;

        // Stores the labels for each of the buttons
        private string buttonLabelName;

        // Used to store string length in order to centre the text on the button
        private Vector2 textDimensions;
        
        /// <summary>
        /// Used to create an instance of a Button
        /// </summary>
        /// <param name="buttonRect"> Used to set the X, Y, Width and Height of the button </param>
        /// <param name="buttonTexture"> Used to store the image of the button </param>
        /// <param name="btnFont"> Font is used to draw labels onto the buttons </param>
        /// <param name="buttonNameState"> Used to specify the destination screen for each button </param>
        /// <param name="buttonLabelName"> This string will be used to draw onto the buttons </param>
        public Button(Rectangle buttonRect, Texture2D buttonTexture, SpriteFont btnFont, string buttonLabelName)
        {
            this.buttonRect = buttonRect;
            this.buttonTexture = buttonTexture;
            this.btnFont = btnFont;
            this.buttonLabelName = buttonLabelName;

            // Gets the width and height of the SpriteFont and stores it in a Vector2
            textDimensions = btnFont.MeasureString(buttonLabelName);
        }


        /// <summary>
        /// Updates the color of the button when the cursor is on or off it and it changes the IsClicked
        /// Boolean if the user clicks on the button
        /// </summary>
        /// <param name="currMouseState"> Used to check the location and the state of the left mouse button </param>
        public void Update(MouseState currMouseState)
        {
            // If the cursor is hovering over the button it will change color to orange
            if ((currMouseState.X > buttonRect.X) &&
                (currMouseState.X < buttonRect.Right) &&
                (currMouseState.Y > buttonRect.Y) &&
                (currMouseState.Y < buttonRect.Bottom))
            {
                btnColor = Color.Orange;

                // If the user clicks on the button it will update the isClicked bool
                if ((currMouseState.LeftButton == ButtonState.Pressed) &&
                    (prevMouseState.LeftButton == ButtonState.Released))
                {
                    IsClicked = true;
                    IsClicked = false;
                }
            }
            else
            {
                // If the user is not hovering over the button the color will change to Yellow
                btnColor = Color.LightBlue;
            }

            //Sets the current mouse state to the previous mouse state
            prevMouseState = currMouseState;
        }

        /// <summary>
        /// Used to draw the buttons onto the screen.
        /// </summary>
        /// <param name="sb"> Passed the SpriteBatch variable in order to use the spritebatch commands </param>
        public void Draw(SpriteBatch sb)
        {
            //Draws the button
            sb.Draw(buttonTexture,      // Uses the button texture
                    buttonRect,         // Draws to the buttonRect Rectangle
                    btnColor);          // Sets the color to the btnColor variable

            // Draws the Button labels onto the button
            sb.DrawString(btnFont,                                                                      // Uses the btnFont SpriteFont variable
                          buttonLabelName,                                                              // Draws the text in the string ButtonLabelName
                          new Vector2((buttonRect.X + ((buttonRect.Width - textDimensions.X) * 0.5f)),  // Sets the Vector2 to the middle of the button
                                      buttonRect.Y + ((buttonRect.Height - textDimensions.Y) * 0.5f)),
                          Color.White);                                                                 // Makes the font white
        }
    }
}
