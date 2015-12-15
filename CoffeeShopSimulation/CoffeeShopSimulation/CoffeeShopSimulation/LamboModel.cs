// Author: Sanjay Paraboo, Mark Vuoong, Shawn Verma
// File Name: LamboModel.cs
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
        // Specifies the lambo speed
        const int LAMBO_SPEED = 15;

        // Stores the lambo texture
        private Texture2D lamboImg;

        /// <summary>
        /// Used to store the lambo location
        /// </summary>
        public Vector2 LamboLocal { get; private set; }

        /// <summary>
        /// Stores the current lifespan of the lambo
        /// </summary>
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
        /// Stores an instance of the lambo view
        /// </summary>
        public LamboView View { get; private set; }

        /// <summary>
        /// Creates an instance of Lambo Model and intializes the LamboView instance
        /// </summary>
        public LamboModel()
        {
            View = new LamboView(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(float gameTime)
        {
            // Adds elasped time to LifeSpan float
            LifeSpan += gameTime;

            // Based on the direction it will update the vector
            switch (Direction)
            {
                case LamboDirection.Up:
                    LamboLocal -= new Vector2(0, LAMBO_SPEED);
                    break;

                case LamboDirection.Down:
                    LamboLocal += new Vector2(0, LAMBO_SPEED);
                    break;

                case LamboDirection.Left:
                    LamboLocal -= new Vector2(LAMBO_SPEED, 0);
                    break;

                case LamboDirection.Right:
                    LamboLocal += new Vector2(LAMBO_SPEED, 0);
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
            // Updates the location by setting it to the spawn point
            this.Direction = direction;
            LamboLocal = spawnLocal;

            // Depending on the direction of the lambo it will update the rotation float
            switch (direction)
            {
                case LamboDirection.Up:
                    Rotation = MathHelper.ToRadians(180);
                    break;

                case LamboDirection.Down:
                    break;

                case LamboDirection.Left:
                    Rotation = MathHelper.ToRadians(90);
                    break;

                case LamboDirection.Right:
                    Rotation = MathHelper.ToRadians(270);
                    break;
            }
        }
    }
}
