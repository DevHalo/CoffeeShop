// Author: Sanjay Paraboo, Mark Vuoong, Shawn Verma
// File Name: LamboEmitterView.cs
// Project Name: A5 Data Manipulation Assignment
// Creation Date: Dec 5th, 2015
// Modified Date: Dec 14th, 2015
// Description: Handles the LamboModel logic such as spawning and intializing the lambo instances

using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffeeShopSimulation
{
    class LamboEmitterView
    {

        private Texture2D lamboTexture;

        private LamboEmitterModel lamboModel;

        public LamboEmitterView(LamboEmitterModel lamboModel,Texture2D lamboTexture)
        {
            this.lamboTexture = lamboTexture;
            this.lamboModel = lamboModel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sb"></param>
        public void Draw(SpriteBatch sb)
        {
            foreach (LamboModel i in lamboModel.Lambos)
            {
                if (i != null)
                {

                    i.View.Draw(sb);
                }
            }
        }
    }
}
