// Author: Sanjay Paraboo, Mark Voong, Shawn Verma
// File Name: LamboView.cs
// Project Name: A5 Data Manipulation Assignment
// Creation Date: Dec 5th, 2015
// Modified Date: Dec 14th, 2015
// Description: Handle the Lambo draw data
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CoffeeShopSimulation
{
    class LamboView
    {
        // Stores the instance of LamboModel
        private LamboModel lamboModel;

        /// <summary>
        /// Used to create an instance of LamboView
        /// </summary>
        /// <param name="lamboModel"> Passes thorugh the LamboModel instance </param>
        public LamboView(LamboModel lamboModel)
        {
            this.lamboModel = lamboModel;
        }

        /// <summary>
        /// Draws the lambo on the screen
        /// </summary>
        /// <param name="sb"></param>
        public void Draw(SpriteBatch sb, Texture2D lamboImg)
        {
            sb.Draw(lamboImg,               // Uses lamboImg texture
                    lamboModel.LamboLocal,  // Gets Vector location from 
                    null,                   // No source rectangle
                    lamboModel.LamboColor,  // Gets random color from lambo model instance
                    lamboModel.Rotation,    // Gets rotation from lambo model instance
                    Vector2.Zero,           // No Vector2 origin
                    1.0f,                   // Use a float scale of 100%
                    SpriteEffects.None,     // No SpriteEffects used
                    0);                     // Draws on layer 0
        }
    }
}
