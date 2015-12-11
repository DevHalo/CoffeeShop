// Author: SimulationModel
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
        private Texture2D lineTexture;
        private Texture2D floorTexture;
        private Texture2D wallTexture;
        private Texture2D pixelTexture;
        private Texture2D counterTexture;
        private SpriteFont mainFont;

        /// <summary>
        /// Initializes the view and loads all assets into memory
        /// </summary>
        /// <param name="device"></param>
        /// <param name="content"></param>
        public SimulationView(GraphicsDevice device, ContentManager content)
        {
            // Create a 1x1 pixel texture
            pixelTexture = new Texture2D(device, 1, 1);
            pixelTexture.SetData(new [] {Color.White});

            // Load the rest of the textures

            // Load fonts
            mainFont = content.Load<SpriteFont>("Fonts/bigFont");
        }

        public void Draw(SpriteBatch sb, SimulationModel model)
        {
            // Draw statistics and other important information
            sb.DrawString(mainFont, "Tim Hortons Simulator 2015", Vector2.Zero, Color.Red);
            sb.DrawString(mainFont, "Simulation Time: " + model.SimTime + " Seconds", new Vector2(0, 25), Color.Red);
            sb.DrawString(mainFont, "Number of Customers: " + model.CustomersInStore, new Vector2(0, 50), Color.Red);

            if (model.Paused)
            {
                sb.DrawString(mainFont, "Simulation Paused", new Vector2(0, 50), Color.Red);
            }

            // Get the head of the queue to draw all the customers in the queue
            Node<CustomerModel> curNode = model.Customers.Peek();
            for (int i = 0; i < model.Customers.Size; i++)
            {
                // Draw the customer stored in the node
                curNode.Value.View.Draw(sb, pixelTexture, mainFont);
                // Iterate to the next node
                curNode = curNode.GetNext();
            }
        }
    }
}
