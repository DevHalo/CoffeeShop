﻿// Author: Sanjay Paraboo
// Class Name: SimulationView.cs
// Project Name: A5_DataManipulation
// Date Created: Dec 5th 2015
// Date Modified: Dec 14th 2015
// Description: Handles all Drawing of the simulation
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CoffeeShopSimulation
{
    class SimulationView
    {
        // Used to specify the side length of the customer and cashier sprites
        private const int CUSTOMER_SIDE_LENGTH = 20;
        // Used to specify the max number of visible customers outside in the simulation
        private const int MAX_VISIBLE_CUSTOMERS_OUTSIDE = 6;

        // Used to store textures for the simulation
        private Texture2D backgroundTexture;
        private Texture2D pixelTexture;

        // Used to store fonts
        private SpriteFont mainFont;
        private SpriteFont smallFont;

        // Drawing Vectors for Simulation
        private Vector2 SimulationTextLocal = new Vector2(750, 375);
        private Vector2 OffScreenCountTextLocal = new Vector2(330, 730);

        // Lambo texture
        private Texture2D lamboTexture;

        // Logo texture
        private Texture2D timmiesLogo;

        /// <summary>
        /// Initializes the view and loads all assets into memory
        /// </summary>
        /// <param name="device">Graphics device used to generate the texture</param>
        /// <param name="content"> Used to load content </param>
        public SimulationView(GraphicsDevice device, ContentManager content)
        {
            // Create a 1x1 white pixel texture
            pixelTexture = new Texture2D(device, 1, 1);
            pixelTexture.SetData(new[] { Color.White });

            // Loads the background Texture
            backgroundTexture = content.Load<Texture2D>("Images/background");

            // Load lambo texture
            lamboTexture = content.Load<Texture2D>("Images/car");

            // Load timmies logo
            timmiesLogo = content.Load<Texture2D>("Images/timmies");

            // Load Spritefonts
            mainFont = content.Load<SpriteFont>("Fonts/bigFont");
            smallFont = content.Load<SpriteFont>("Fonts/smallFont");
        }

        /// <summary>
        /// Draws Data obtained from the simulation model
        /// </summary>
        /// <param name="sb"> Passes through SpriteBatch in order to use its Draw commands </param>
        /// <param name="model"> Passes thorugh SimulationModel in order to draw the models data </param>
        public void Draw(SpriteBatch sb, SimulationModel model)
        {
            // Draw background
            sb.Draw(backgroundTexture, Vector2.Zero, Color.White);

            // If there are customers in the outside line that are off the screen then it will display the number off screen
            if (model.OutsideLine.Size > MAX_VISIBLE_CUSTOMERS_OUTSIDE)
            {
                sb.DrawString(mainFont, "+" + (model.OutsideLine.Size - MAX_VISIBLE_CUSTOMERS_OUTSIDE),
                    OffScreenCountTextLocal, Color.AliceBlue);
            }

            // When paused it will display Simulation paused
            if (model.Paused)
            {
                sb.DrawString(mainFont, "Simulation Paused", SimulationTextLocal, Color.Red);
            }

            // Get the head of the queue to draw all the customers in the queue
            Node<CustomerModel> curNode = model.OutsideLine.Peek();
            for (int i = 0; i < model.OutsideLine.Size; i++)
            {
                // Draw the customer stored in the node
                curNode.Value.View.Draw(sb, pixelTexture, smallFont);
                // Gets the next node
                curNode = curNode.Next;
            }

            // Sets the current node to the head node in the inside line
            curNode = model.InsideLine.Peek();

            // Runs the Draw method for each of the customers inside the inside store queue
            for (int i = 0; i < model.InsideLine.Size; i++)
            {
                // Passes through the spritebatch instance the pixel texture creates in the constructor and the small font spritefont
                curNode.Value.View.Draw(sb, pixelTexture, smallFont);

                // Gets the next node in the queue
                curNode = curNode.Next;
            }

            // Draws every cashiers at their positions
            for (int i = 0; i < model.Cashiers.Length; i++)
            {
                sb.Draw(pixelTexture, // Uses 1x1 pixel texture
                    new Rectangle((int) model.CashierVectors[i].X + 25,
                        // Draws it at the customer waypoint vector at the cahsher and added 25 pixels so 
                        (int) model.CashierVectors[i].Y - CUSTOMER_SIDE_LENGTH/2,
                        // the cashier texture goes behind the counter
                        CUSTOMER_SIDE_LENGTH, CUSTOMER_SIDE_LENGTH), // Has a width and length of 25 pixels
                    Color.Brown); // Overlay color is brown
            }

            // Draws customers at the cashiers if they are not null
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

            // Draws the emitter
            model.LamboEmitter.View.Draw(sb, lamboTexture);

            // Tell the user that the simulation has finished when it has reached the simtime
            if (model.Finished)
            {
                sb.DrawString(mainFont, "Simulation Finished. Press ESC to Exit", new Vector2(469, 352), Color.Red);
            }

            // Draws title, statistics and other important information
            sb.Draw(pixelTexture, new Rectangle(0, 0, 1366, 102), Color.White*0.8f);
            sb.DrawString(mainFont,
                "Tim Hortons Simulator 2015" +
                "\nSimulation Time: " + model.SimTime + " Seconds" +
                "\nNumber of Served Customers: " + model.Statistics.CustomersServed,
                Vector2.Zero,
                Color.Blue);

            // Runs the Draw method from the statistics view class
            model.Statistics.View.Draw(sb, mainFont);

            // Draw timmies logo
            sb.Draw(timmiesLogo, new Vector2(850, 0), Color.White);
        }
    }
}
