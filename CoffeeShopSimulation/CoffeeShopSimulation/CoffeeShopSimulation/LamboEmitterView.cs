// Author: Sanjay Paraboo, Mark Voong, Shawn Verma
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
        ///  Creates an instance of the Lambo Emitter View and passes through an instance of LamboEmitterModel
        /// </summary>
        /// <param name="lamboModel"></param>
        public LamboEmitterView(LamboEmitterModel lamboModel)
        {
            this.lamboModel = lamboModel;
        }

        /// <summary>
        /// Runs the draw method for every instance of a lambo in lambos
        /// </summary>
        /// <param name="sb"> Passes thorugh an instance of spritebatch in order to use its draw commands </param>
        /// <param name="lamboTexture"> Passes through the lambo texture </param>
        public void Draw(SpriteBatch sb, Texture2D lamboTexture)
        {
            // Draws the lambo models if theyre not null
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
