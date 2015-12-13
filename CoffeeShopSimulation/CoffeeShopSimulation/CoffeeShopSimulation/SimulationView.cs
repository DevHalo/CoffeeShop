// Author: Sanjay Paraboo
// Class Name: SimulationView.cs
// Date Created: Dec 5th 2015
// Date Modified: Dec 6th 2015
// Description: Handles all rendering of the simulation
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CoffeeShopSimulation
{
    class SimulationView
    { 
        // Assets
        private Texture2D customerTexture;
        private Texture2D cashierTexture;
        private Texture2D backgroundTexture;
        private Texture2D pixelTexture;
        private Texture2D counterTexture;
        private SpriteFont mainFont;
        private SpriteFont smallFont;

        /// <summary>
        /// Initializes the view and loads all assets into memory
        /// </summary>
        /// <param name="device">Graphics device used to generate the texture</param>
        /// <param name="content"></param>
        public SimulationView(GraphicsDevice device, ContentManager content)
        {
            // Create a 1x1 pixel texture
            pixelTexture = new Texture2D(device, 1, 1);
            pixelTexture.SetData(new [] {Color.White});

            // Load the rest of the textures
            backgroundTexture = content.Load<Texture2D>("Images/background2");

            // Load fonts
            mainFont = content.Load<SpriteFont>("Fonts/bigFont");
            smallFont = content.Load<SpriteFont>("Fonts/smallFont");
        }

        public void Draw(SpriteBatch sb, SimulationModel model)
        {
            sb.Draw(backgroundTexture, Vector2.Zero, Color.White);

            // Draw statistics and other important information
            sb.Draw(pixelTexture, new Rectangle(0, 0, 420, 100), Color.White * 0.8f);
            sb.DrawString(mainFont, "Tim Hortons Simulator 2015", Vector2.Zero, Color.Blue);
            sb.DrawString(mainFont, "Simulation Time: " + model.SimTime + " Seconds", new Vector2(0, 25), Color.Blue);
            sb.DrawString(mainFont, "Number of outsideLine: " + model.CustomersOutsideStore, new Vector2(0, 50), Color.Blue);

            if (model.Paused)
            {
                sb.DrawString(mainFont, "Simulation Paused", new Vector2(0, 50), Color.Red);
            }

            // Get the head of the queue to draw all the customers in the queue
            Node<CustomerModel> curNode = model.OutsideLine.Peek();
            for (int i = 0; i < model.OutsideLine.Size; i++)
            {
                // Draw the customer stored in the node
                curNode.Value.View.Draw(sb, pixelTexture, smallFont);
                // Iterate to the next node
                curNode = curNode.GetNext();
            }

            curNode = model.InsideLine.Peek();
            for (int i = 0; i < model.InsideLine.Size; i++)
            {
                curNode.Value.View.Draw(sb, pixelTexture, smallFont);

                curNode = curNode.GetNext();
            }

            for (int i = 0; i < model.Cashiers.Length; i++)
            {
                sb.Draw(pixelTexture, new Rectangle((int)model.CashierVectors[i].X + 25, (int)model.CashierVectors[i].Y - 10, 20, 20), Color.Brown);
            }

            foreach (CustomerModel customer in model.Cashiers)
            {
                if (customer != null)
                {
                    customer.View.Draw(sb, pixelTexture, smallFont);
                }
            }

            foreach (CustomerModel customer in model.ExitList)
            {
                customer.View.Draw(sb, pixelTexture, smallFont);
            }
        }
    }
}
