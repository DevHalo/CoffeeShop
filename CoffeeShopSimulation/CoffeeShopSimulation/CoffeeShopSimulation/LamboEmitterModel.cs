// Author: Sanjay Paraboo, Mark Voong, Shawn Verma
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
        // Specifies the max number of lambos and their lifespans
        private const int MAX_LAMBOS = 2;
        private const float MAX_LIFE = 6.0f;

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
            // Initialize Lambo list
            Lambos = new List<LamboModel>();

            // Initialize vectors
            TopSpawn = new Vector2(92, -150);
            LeftSpawn = new Vector2(1460, 102);
            RightSpawn = new Vector2(-125, 260);
            BottomSpawn = new Vector2(203, 860);
        }
        
        /// <summary>
        /// Spawns a lambo in a certain direction
        /// </summary>
        /// <param name="lamboDirection"> Direction at which the lambo is traveling</param>
        public void SpawnLambo(LamboModel.LamboDirection lamboDirection)
        {
            // If there arn't too many lambos
            if (Lambos.Count < MAX_LAMBOS)
            {
                switch (lamboDirection)
                {
                    case LamboModel.LamboDirection.Up:
                        Lambos.Add(new LamboModel(BottomSpawn, LamboModel.LamboDirection.Up));
                        break;
                    case LamboModel.LamboDirection.Down:
                        Lambos.Add(new LamboModel(TopSpawn, LamboModel.LamboDirection.Down));
                        break;
                    case LamboModel.LamboDirection.Left:
                        Lambos.Add(new LamboModel(RightSpawn, LamboModel.LamboDirection.Left));
                        break;
                    case LamboModel.LamboDirection.Right:
                        Lambos.Add(new LamboModel(LeftSpawn, LamboModel.LamboDirection.Right));
                        break;
                }
            }
        }

        /// <summary>
        /// Updates all lambos on the screen
        /// </summary>
        /// <param name="gameTimeInMilliSeconds"></param>
        public void Update(float gameTimeInMilliSeconds)
        {
            // Update each lambo and remove them if neccessary
            foreach (LamboModel lambo in Lambos)
            {
                lambo.Update(gameTimeInMilliSeconds);

                if (lambo.LifeSpan >= lambo.)
            }
        }
    }
}
