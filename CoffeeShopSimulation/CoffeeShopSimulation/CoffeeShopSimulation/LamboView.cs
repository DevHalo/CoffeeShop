// Author: Sanjay Paraboo, Mark Voong, Shawn Verma
// File Name: LamboView.cs
// Project Name: A5 Data Manipulation Assignment
// Creation Date: Dec 5th, 2015
// Modified Date: Dec 14th, 2015
// Description: Handle the Lambo draw data

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            sb.Draw(lamboImg,
                    lamboModel.LamboLocal,
                    null,
                    Color.White,
                    lamboModel.Rotation,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    0);
        }
    }
}
