﻿// Author: Sanjay Paraboo
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
        // Used to specify the side length of the customer and cashier sprites
        private const int CUSTOMER_SIDE_LENGTH = 20;

        // Used to store textures for the simulation
        private Texture2D customerTexture;
        private Texture2D cashierTexture;
        private Texture2D backgroundTexture;
        private Texture2D pixelTexture;
        private Texture2D counterTexture;

        // Used to store fonts
        private SpriteFont mainFont;
        private SpriteFont smallFont;

        /// <summary>
        /// Initializes the view and loads all assets into memory
        /// </summary>
        /// <param name="device">Graphics device used to generate the texture</param>
        /// <param name="content"> Used to load content </param>
        public SimulationView(GraphicsDevice device, ContentManager content)
        {
            // Create a 1x1 pixel texture
            pixelTexture = new Texture2D(device, 1, 1);
            pixelTexture.SetData(new [] {Color.White});

            // Loads the background Texture
            backgroundTexture = content.Load<Texture2D>("Images/background2");

            // Load Spritefonts
            mainFont = content.Load<SpriteFont>("Fonts/bigFont");
            smallFont = content.Load<SpriteFont>("Fonts/smallFont");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sb"> Passes through SpriteBatch in order to use its Draw commands </param>
        /// <param name="model"> Passes thorugh SimulationModel in order to draw the models data </param>
        public void Draw(SpriteBatch sb, SimulationModel model)
        {
            sb.Draw(backgroundTexture, Vector2.Zero, Color.White);

            // Draw statistics and other important information
            sb.Draw(pixelTexture, new Rectangle(0, 0, 440, 102), Color.White * 0.8f);
            sb.DrawString(mainFont, "Tim Hortons Simulator 2015", Vector2.Zero, Color.Blue);
            sb.DrawString(mainFont, "Simulation Time: " + model.SimTime, new Vector2(0, 25), Color.Blue);
            sb.DrawString(mainFont, " Seconds", new Vector2(280, 25), Color.Blue);
            sb.DrawString(mainFont, "Number of Served Customers: " + model.CustomersServed, new Vector2(0, 50), Color.Blue);

            if (model.OutsideLine.Size > 6)
            {
                sb.DrawString(mainFont, "+" + (model.OutsideLine.Size - 6), new Vector2(330, 730), Color.AliceBlue);
            }
            // When paused it will display Simulation paused
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

            // Sets the current node to the head node in the inside line
            curNode = model.InsideLine.Peek();

            // Runs the Draw method for each of the customers inside the inside store queue
            for (int i = 0; i < model.InsideLine.Size; i++)
            {
                curNode.Value.View.Draw(sb, pixelTexture, smallFont);

                // Gets the next node in the queue
                curNode = curNode.GetNext();
            }

            // Draws the cashiers at their positions
            for (int i = 0; i < model.Cashiers.Length; i++)
            {
                sb.Draw(pixelTexture,                                               // Uses 1x1 pixel texture
                        new Rectangle((int)model.CashierVectors[i].X + 25,          // Draws it at the cashier vectors obtained from SimulationModel
                                      (int)model.CashierVectors[i].Y - 10,
                                      CUSTOMER_SIDE_LENGTH, CUSTOMER_SIDE_LENGTH), // Has a width and length of 25 pixels
                        Color.Brown);                                              // Overlay color is brown
            }

            // Draws customers at the cashiers
            foreach (CustomerModel customer in model.Cashiers)
            {
                if (customer != null)
                {
                    customer.View.Draw(sb, pixelTexture, smallFont);
                }
            }

            // Draws customers that are leaving the store
            foreach (CustomerModel customer in model.ExitList)
            {
                customer.View.Draw(sb, pixelTexture, smallFont);
            }

            model.Statistics.View.Draw(sb, smallFont);
        }
    }
}
