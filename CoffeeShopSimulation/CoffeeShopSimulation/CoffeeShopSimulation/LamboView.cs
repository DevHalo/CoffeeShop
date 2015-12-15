// Author: Sanjay Paraboo, Mark Vuoong, Shawn Verma
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
        // Stores the lambo texture
        private Texture2D lamboImg;

        // Stores the instance of LamboModel
        private LamboModel lamboModel;

        /// <summary>
        /// Used to create an instance of LamboView
        /// </summary>
        /// <param name="lamboImg"> Passes through the lambo texture </param>
        /// <param name="lamboModel"> Passes thorugh the LamboModel instance </param>
        public LamboView(Texture2D lamboImg, LamboModel lamboModel)
        {
            this.lamboImg = lamboImg;
            this.lamboModel = lamboModel;
        }

        /// <summary>
        /// Draws the lambo on the screen
        /// </summary>
        /// <param name="sb"></param>
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(lamboImg,
                    lamboModel.LamboLocal,
                    null,
                    Color.White,
                    0f,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    0);
        }
    }
}
