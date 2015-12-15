// Author: Sanjay Paraboo, Mark Vuoong, Shawn Verma
// File Name: CustomerView.cs
// Project Name: A5 Data Manipulation Assignment
// Creation Date: Dec 5th, 2015
// Modified Date: Dec 14th, 2015
// Description:
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace CoffeeShopSimulation
{
    class LamboEmitterModel
    {
        private const int MAX_LAMBOS = 2;

        private Vector2 emitLocal;

        public List<LamboModel> Lambos { get; private set; }

        public Vector2 TopSpawn { get; private set; }
        public Vector2 LeftSpawn { get; private set; }
        public Vector2 RightSpawn { get; private set; }
        public Vector2 BottomSpawn { get; private set; }


        /// <summary>
        /// Initialize Lambo spawn positions
        /// </summary>
        public LamboEmitterModel()
        {
            Lambos = new List<LamboModel>();

            TopSpawn = new Vector2();
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void SpawnLambo(LamboModel.LamboDirection lamboDirection)
        {
            if (Lambos.Count < MAX_LAMBOS)
            {
                switch (lamboDirection)
                {
                    case LamboModel.LamboDirection.Up:

                        break;
                    case LamboModel.LamboDirection.Down:
                        break;
                    case LamboModel.LamboDirection.Left:
                        break;
                    case LamboModel.LamboDirection.Right:
                        break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTimeInMilliSeconds"></param>
        public void Update(float gameTimeInMilliSeconds)
        {
            foreach (LamboModel lambo in Lambos)
            {
                lambo.Update(gameTimeInMilliSeconds);
            }
        }
    }
}
