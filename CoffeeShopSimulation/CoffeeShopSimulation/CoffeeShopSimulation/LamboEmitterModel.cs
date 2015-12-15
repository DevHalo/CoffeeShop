// Author: Sanjay Paraboo, Mark Voong, Shawn Verma
// File Name: LamboEmitterModel.cs
// Project Name: A5 Data Manipulation Assignment
// Creation Date: Dec 5th, 2015
// Modified Date: Dec 14th, 2015
// Description: Handles all of the lambo spawning and removing
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

namespace CoffeeShopSimulation
{
    class LamboEmitterModel
    {
        // Specifies the max number of lambos and their lifespans
        private const int MAX_LAMBOS = 10;
        private const float MAX_LIFE = 2.0f;

        public List<LamboModel> Lambos { get; private set; }

        /// <summary>
        /// Spawn vector for lambos spawning from the top
        /// </summary>
        public Vector2 TopSpawn { get; private set; }

        /// <summary>
        /// Spawn vector for lambos spawning from the bottom
        /// </summary>
        public Vector2 LeftSpawn { get; private set; }
        
        /// <summary>
        /// Spawn vector for lambos spawning from the right
        /// </summary>
        public Vector2 RightSpawn { get; private set; }

        /// <summary>
        /// Spawn vector for lambos spawning from the left
        /// </summary>
        public Vector2 BottomSpawn { get; private set; }

        /// <summary>
        /// View associated with the model
        /// </summary>
        public LamboEmitterView View { get; private set; }

        /// <summary>
        /// Initialize Lambo spawn positions
        /// </summary>
        public LamboEmitterModel()
        {
            // Initialize Lambo list
            Lambos = new List<LamboModel>();
            
            // Initialize view
            View = new LamboEmitterView(this);

            // Initialize vectors
            TopSpawn = new Vector2(92, -150);
            LeftSpawn = new Vector2(1460, 100);
            RightSpawn = new Vector2(-125, 250);
            BottomSpawn = new Vector2(250, 860);
        }
        
        /// <summary>
        /// Spawns a lambo in a certain direction
        /// </summary>
        /// <param name="lamboDirection"> Direction at which the lambo is traveling</param>
        /// <param name="randNum"> Passes through random num generator </param>
        public void SpawnLambo(LamboModel.LamboDirection lamboDirection, Random randNum)
        {
            // If there arn't too many lambos generate a lambo based on its direction
            if (Lambos.Count < MAX_LAMBOS)
            {
                switch (lamboDirection)
                {
                    case LamboModel.LamboDirection.Up:
                        Lambos.Add(new LamboModel(BottomSpawn, LamboModel.LamboDirection.Up, randNum.Next(0,5)));
                        break;

                    case LamboModel.LamboDirection.Down:
                        Lambos.Add(new LamboModel(TopSpawn, LamboModel.LamboDirection.Down, randNum.Next(0, 5)));
                        break;

                    case LamboModel.LamboDirection.Left:
                        Lambos.Add(new LamboModel(RightSpawn, LamboModel.LamboDirection.Right, randNum.Next(0, 5)));
                        break;

                    case LamboModel.LamboDirection.Right:
                        Lambos.Add(new LamboModel(LeftSpawn, LamboModel.LamboDirection.Left, randNum.Next(0, 5)));
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
            for (int i = 0; i < Lambos.Count; i++)
            {
                Lambos[i].Update(gameTimeInMilliSeconds);

                if (Lambos[i].LifeSpan >= MAX_LIFE)
                {
                    Lambos.RemoveAt(i);
                }
            }
        }
    }
}
