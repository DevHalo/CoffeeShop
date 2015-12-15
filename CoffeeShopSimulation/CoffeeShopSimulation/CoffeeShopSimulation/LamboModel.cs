// Author: Sanjay Paraboo, Mark Vuoong, Shawn Verma
// File Name: CustomerView.cs
// Project Name: A5 Data Manipulation Assignment
// Creation Date: Dec 5th, 2015
// Modified Date: Dec 14th, 2015
// Description:
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CoffeeShopSimulation
{
    class LamboModel
    {
        const int LAMBO_SPEED = 15;

        private Texture2D lamboImg;

        private Vector2 lamboLocal;

        public float LifeSpan { get; private set; }

        // Used to specify the locaiton of the lambo
        public float Rotation { get; private set; }

        /// <summary>
        /// Specifies in which direction the lambo will be travelling
        /// </summary>
        public enum LamboDirection
        {
            Up,
            Down,
            Left,
            Right
        };

        public LamboDirection Direction { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(float gameTime)
        {
            // Adds elasped time to LifeSpan float
            LifeSpan += gameTime;

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
        /// Used to spawn the lamborghinis
        /// </summary>
        /// <param name="spawnLocal"> used to specify spawn location </param>
        /// <param name="direction"> Specifies the lambo direction </param>
        public LamboModel(Vector2 spawnLocal, LamboDirection direction)
        {
            this.Direction = direction;
            lamboLocal = spawnLocal;

            switch (direction)
            {
                case LamboDirection.Up:
                    Rotation = MathHelper.ToRadians(270);
                    break;

                case LamboDirection.Down:
                    Rotation = MathHelper.ToRadians(90);
                    break;

                case LamboDirection.Left:
                    Rotation = MathHelper.ToRadians(180);
                    break;

                case LamboDirection.Right:
                    break;
            }
        }

        /// <summary>
        /// Draws the lambo on the screen
        /// </summary>
        /// <param name="sb"></param>
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(lamboImg,
                    lamboLocal,
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
