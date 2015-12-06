// Authors: Mark Voong, Sanjay Paraboo, Shawn Verma
// Class Name: CoffeeShop.cs
// Project Name: A5
// Date Created: Dec 5th 2015
// Date Modified: Dec 5th 2015
// Description: A simulation involving processing customers orders as they enter a coffee shop.
// The customer has the ability to 
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CoffeeShopSimulation
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class CoffeeShop : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private SimulationModel simulationModel = new SimulationModel();        // Model Class
        private SimulationView simulationView;                                  // View Class

        public CoffeeShop()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferHeight = 768;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            simulationModel.Initialize();
            simulationView = new SimulationView(GraphicsDevice, Content);
            spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
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
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            
            // Draw the simulation
            simulationView.Draw(spriteBatch, simulationModel);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
