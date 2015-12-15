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
    class LamboEmitterModel
    {
        private const int MAX_LAMBOS = 2;

        private Vector2 emitLocal;

        LamboModel[] Lambos = new LamboModel[MAX_LAMBOS];

        public Vector2 TopSpawn { get; private set; }
        public Vector2 LeftSpawn { get; private set; }
        public Vector2 RightSpawn { get; private set; }
        public Vector2 BottomSpawn { get; private set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTimeInMilliSeconds"></param>
        public void Update(float gameTimeInMilliSeconds)
        {

        }


    }
}
