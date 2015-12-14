// Author: Sanjay Paraboo
// File Name: CoffeeShopSimulation.sln
// Project Name: A5 Data Manipulation Assignment
// Creation Date: Dec 5, 2015
// Modified Date: Dec 13th, 2015
// Description:
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffeeShopSimulation
{
    class StatisticsView
    {
        public void Draw(SpriteBatch sb, StatisticsModel statisticsData, SpriteFont font)
        {
            // Longest, min, max and average wait times
            sb.DrawString(font,
                          "Longest Wait Time: " + statisticsData.MaxWaitTime +
                          "\nFastest Wait Time: " + statisticsData.MinWaitTime +
                          "\nAverage Wait Time: " + statisticsData.AvgWaitTime,
                          new Vector2(500, 0),
                          Color.White);

            for (int i = 0; i< statisticsData.LongestWaitTimes.Length; i++)
            {

            }


        }
    }
}
