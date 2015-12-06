// Author: Mark Voong
// Class Name: SimulationManager.cs
// Date Created: Dec 5th 2015
// Date Modified: Dec 5th 2015
// Description: Handles all simulation logic of the coffee shop
using Microsoft.Xna.Framework;

namespace CoffeeShopSimulation
{
    class SimulationModel
    {
        private InputManager inputManager = new InputManager();  // Controller Class

        private Queue<Vector2> waypoints; // Locations that allow an AI to follow a certain path
        private Queue<Customer> customers;

        private const int MAX_CUSTOMERS = 16;   // Maximum number of customers in the store
        Customer[] cashiers = new Customer[4];  // Cashiers which serve the customers

        private Statistics statistics;

        private float simTime;

        public void Initialize()
        {
            // Initialize Waypoints
            
        }

        public void Update(float gameTime)
        {
            // Update each customer
        }
    }
}
