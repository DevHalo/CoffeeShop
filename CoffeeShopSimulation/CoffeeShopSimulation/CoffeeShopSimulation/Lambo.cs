// Author: Sanjay Paraboo, Mark Vuoong, Shawn Verma
// File Name: CustomerView.cs
// Project Name: A5 Data Manipulation Assignment
// Creation Date: Dec 5th, 2015
// Modified Date: Dec 14th, 2015
// Description:
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CoffeeShopSimulation
{
    class Lambo
    {
        const int LAMBO_SPEED = 15;

        private Texture2D lamboImg;

        private Vector2 lamboLocal;

        /// <summary>
        /// 
        /// </summary>
        public enum LamboDirection
        {
            Up,
            Down,
            Left,
            Right
        };

        private LamboDirection Direction { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Lambo(Texture2D lamboImg, LamboDirection direction)
        {
            this.lamboImg = lamboImg;
            this.Direction = direction;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(float gameTime)
        {
            switch (Direction)
            {
                case LamboDirection.Up:
                    lamboLocal.Y -= LAMBO_SPEED;
                    break;

                case LamboDirection.Down:
                    lamboLocal.Y += LAMBO_SPEED;
                    break;

                case LamboDirection.Left:
                    lamboLocal.X -= LAMBO_SPEED;
                    break;

                case LamboDirection.Right:
                    lamboLocal.X += LAMBO_SPEED;
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Spawn()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sb"></param>
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(lamboImg, lamboLocal, null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
        }
    }
}
