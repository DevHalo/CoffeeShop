// Author: Sanjay Paraboo, Mark Vuoong, Shawn Verma
// File Name: LamboEmitterView.cs
// Project Name: A5 Data Manipulation Assignment
// Creation Date: Dec 5th, 2015
// Modified Date: Dec 14th, 2015
// Description: Handles the LamboModel logic such as spawning and intializing the lambo instances
using Microsoft.Xna.Framework.Graphics;

namespace CoffeeShopSimulation
{
    class LamboEmitterView
    {
        // Model associated with this view
        private LamboEmitterModel lamboModel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lamboModel"></param>
        public LamboEmitterView(LamboEmitterModel lamboModel)
        {
            this.lamboModel = lamboModel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="lamboTexture"></param>
        public void Draw(SpriteBatch sb, Texture2D lamboTexture)
        {
            foreach (LamboModel i in lamboModel.Lambos)
            {
                if (i != null)
                {
                    i.View.Draw(sb, lamboTexture);
                }
            }
        }
    }
}
