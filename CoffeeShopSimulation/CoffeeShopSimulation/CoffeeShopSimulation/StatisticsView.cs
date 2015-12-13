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
        private SpriteFont font;

        public StatisticsView(SpriteFont font)
        {
            this.font = font;
        }

        public void Draw(SpriteBatch sb, StatisticsModel statisticsData)
        {
            // Longest, min, max and average wait times
            sb.DrawString(font, "Longest Wait Time: " + statisticsData.MaxWaitTime, new Vector2(0, 0), Color.White);
            sb.DrawString(font, "Fastest Wait Time" + statisticsData.MinWaitTime, new Vector2(0, 0), Color.White);
            sb.DrawString(font, "Average Wait Time" + statisticsData.AvgWaitTime, new Vector2(0, 0), Color.White);

           // for (int i = 0; i< statisticsData.)


        }
    }
}
