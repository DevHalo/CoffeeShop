// Author: Sanjay Paraboo, Mark Vuoong, Shawn Verma
// File Name: LamboView.cs
// Project Name: A5 Data Manipulation Assignment
// Creation Date: Dec 5th, 2015
// Modified Date: Dec 14th, 2015
// Description:

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

        private Texture2D lamboImg;

        private LamboModel lamboModel;

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
