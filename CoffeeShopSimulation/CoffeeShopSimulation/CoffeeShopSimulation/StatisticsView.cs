// Author: Sanjay Paraboo
// File Name: StatisticsView.cs
// Project Name: A5 Data Manipulation Assignment
// Creation Date: Dec 5, 2015
// Modified Date: Dec 13th, 2015
// Description: Handles the draw data from the statistics model

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
        /// Creates an instance of StatisticsView
        /// </summary>
        /// <param name="statisticsData"> Passes through an instance of StatisticsModel </param>
        public StatisticsView(StatisticsModel statisticsData)
        {
            this.statisticsData = statisticsData;
        }

        public void Draw(SpriteBatch sb, SpriteFont font)
        {
            // Longest, min, max and average wait times
            sb.DrawString(font,
                          "Longest Wait Time: " + Math.Round(statisticsData.MaxWaitTime, 2) + " seconds" +
                          "\nShortest Wait Time: " + Math.Round(statisticsData.MinWaitTime, 2) + " seconds" +
                          "\nAverage Wait Time: " + Math.Round(statisticsData.AvgWaitTime, 2) + " seconds",
                          new Vector2(500, 0),
                          Color.Blue);


            // Used to obtain the length of the Customer Info List.
            int length;
            // If the count is less than 5 it will display the 
            if (statisticsData.customerInfo.Count < 5)
            {
                length = statisticsData.customerInfo.Count;
            }
            else
            {
                length = 5;
            }


            // Draws the top 5 longest wait times obtained from the statistics model class
            sb.DrawString(font, "Longest Wait Times:", new Vector2(470, 535), Color.Blue);

            for (int i = 0; i < length; i++)
            {
                // If the value isnt null then draw the value
                if (statisticsData.customerInfo[i] != null)
                {
                    // 
                    sb.DrawString(font,
                                  i + 1 + ". " + statisticsData.customerInfo[length - i - 1].CustomerName +
                                  "@ " + Math.Round((statisticsData.customerInfo[length - i - 1].CustomerWaitTime), 1) + " seconds",
                                  new Vector2(480 + (i*20), 560 + (i * 20)),
                                  Color.Blue);
                }
            }


        }
    }
}
