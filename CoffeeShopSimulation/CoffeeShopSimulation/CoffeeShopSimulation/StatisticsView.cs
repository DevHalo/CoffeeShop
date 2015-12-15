// Author: Sanjay Paraboo
// File Name: StatisticsView.cs
// Project Name: A5 Data Manipulation Assignment
// Creation Date: Dec 5, 2015
// Modified Date: Dec 14th, 2015
// Description: Handles the draw data from the statistics model
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

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

        /// <summary>
        /// Draws the simulation statistics data
        /// </summary>
        /// <param name="sb"> Passes through SpriteBatch instance in order to use its draw commands </param>
        /// <param name="font"> Passes through a SpriteFont instance for DrawString commands </param>
        public void Draw(SpriteBatch sb, SpriteFont font)
        {
            // Longest, min, max and average wait times
            sb.DrawString(font,
                          "Longest Wait Time: " + Math.Round(statisticsData.MaxWaitTime, 2) + " seconds" +
                          "\nShortest Wait Time: " + Math.Round(statisticsData.MinWaitTime, 2) + " seconds" +
                          "\nAverage Wait Time: " + Math.Round(statisticsData.AvgWaitTime, 2) + " seconds",
                          new Vector2(400, 0),
                          Color.Blue);

            // Used to obtain the length of the Customer Info List.
            int start = statisticsData.CustomerInfo.Count - 1;
            int end = (int)MathHelper.Clamp(start - 4, 0, start);

            // Draws the top 5 longest wait times obtained from the statistics model class
            sb.DrawString(font, "Longest Wait Times:", new Vector2(470, 535), Color.Blue);

            if (statisticsData.CustomerInfo.Count > 0)
            {
                for (int i = start; i >= end; i--)
                {
                    // If the value isnt null then draw the value
                    if (statisticsData.CustomerInfo[i] != null)
                    {
                        // Draws 
                        sb.DrawString(font,
                            (start - i) + 1 + ". " + statisticsData.CustomerInfo[i].CustomerName +
                            "@ " + Math.Round((statisticsData.CustomerInfo[i].CustomerWaitTime), 1) + " seconds",
                            new Vector2(480 + ((start - i) *20), 560 + ((start - i)*20)),
                            Color.Blue);
                    }
                }
            }
        }
    }
}