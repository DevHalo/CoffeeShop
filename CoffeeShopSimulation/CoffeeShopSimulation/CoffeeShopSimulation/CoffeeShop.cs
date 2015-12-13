// Authors: Mark Voong, Sanjay Paraboo, Shawn Verma
// Class Name: CoffeeShop.cs
// Project Name: A5
// Date Created: Dec 5th 2015
// Date Modified: Dec 13th 2015
// Description: A simulation involving processing customers orders as they enter a coffee shop.
// The customer has the ability to purchase food, coffee, or a combination of the two.
// The simulation tracks how long each customer is waiting up until they leave the store
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CoffeeShopSimulation
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class CoffeeShop : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private SimulationModel simulationModel;        // Model Class
        private SimulationView simulationView;          // View Class

        public CoffeeShop()
        {
            graphics = new GraphicsDeviceManager(this);

            // Dimensions of the screen
            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferHeight = 768;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            simulationModel = new SimulationModel();
            simulationView = new SimulationView(GraphicsDevice, Content);
            spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Get wall time in seconds
            float gTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000f;

            // Simulate
            simulationModel.Update(gTime);
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Clear the screen and begin spritebatch
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            
            // Draw the simulation
            simulationView.Draw(spriteBatch, simulationModel);
            
            // End draw call
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
