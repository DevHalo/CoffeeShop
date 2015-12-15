// Author: Sanjay Paraboo, Mark Vuoong, Shawn Verma
// File Name: LamboModel.cs
// Project Name: A5 Data Manipulation Assignment
// Creation Date: Dec 5th, 2015
// Modified Date: Dec 14th, 2015
// Description: Used to handle the logic of the lambos

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CoffeeShopSimulation
{
    class LamboModel
    {
        const int LAMBO_SPEED = 15;

        private Texture2D lamboImg;

        public Vector2 LamboLocal { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public float LifeSpan { get; private set; }

        // Used to specify the locaiton of the lambo
        private float rotation;


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
        private LamboDirection Direction { get; private set; }


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
        public void Spawn(Vector2 spawnLocal, LamboDirection direction)
        {
            // Updates the location by setting it to the spawn point
            this.Direction = direction;
            LamboLocal = spawnLocal;

            switch (direction)
            {
                case LamboDirection.Up:
                    rotation = MathHelper.ToRadians(270);
                    break;

                case LamboDirection.Down:
                    rotation = MathHelper.ToRadians(90);
                    break;

                case LamboDirection.Left:
                    rotation = MathHelper.ToRadians(180);
                    break;

                case LamboDirection.Right:
                    break;
            }
        }
    }
}
