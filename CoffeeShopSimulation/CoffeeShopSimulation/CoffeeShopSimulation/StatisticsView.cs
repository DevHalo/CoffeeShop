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
        // Used to store instance of statistics model
        StatisticsModel statisticsData;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statisticsData"></param>
        public StatisticsView(StatisticsModel statisticsData)
        {
            this.statisticsData = statisticsData;
        }

        public void Draw(SpriteBatch sb, SpriteFont font)
        {
            // Longest, min, max and average wait times
            sb.DrawString(font,
                          "Longest Wait Time: " + statisticsData.MaxWaitTime +
                          "\nFastest Wait Time: " + statisticsData.MinWaitTime +
                          "\nAverage Wait Time: " + statisticsData.AvgWaitTime,
                          new Vector2(500, 0),
                          Color.White);

            for (int i = 0; i < statisticsData.LongestWaitTimes.Length; i++)
            {
                if (statisticsData.LongestWaitTimes[i] != null)
                {
                    sb.DrawString(font,
                                  Convert.ToString(statisticsData.LongestWaitTimes[i].CustomerName +
                                  " " + statisticsData.LongestWaitTimes[i].CustomerWaitTime),
                                  new Vector2(1100, 5 + (i * 20)),
                                  Color.White);
                }
            }


        }
    }
}
