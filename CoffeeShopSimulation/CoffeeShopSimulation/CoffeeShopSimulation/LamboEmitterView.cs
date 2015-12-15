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

                    i.Draw(sb);
                }
            }
        }
    }
}
