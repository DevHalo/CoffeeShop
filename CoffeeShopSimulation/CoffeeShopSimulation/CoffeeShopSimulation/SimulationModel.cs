// Author: Mark Voong
// Class Name: SimulationModel.cs
// Date Created: Dec 5th 2015
// Date Modified: Dec 6th 2015
// Description: Handles all simulation logic of the coffee shop

using System;
using Microsoft.Xna.Framework;

namespace CoffeeShopSimulation
{
    class SimulationModel
    {
        private InputManager inputManager = new InputManager();  // Controller Class

        private WaypointManager waypointManager;    // Manages waypoints


        /// <summary>
        /// Queue that stores every customer that has entered the store
        /// </summary>
        public Queue<CustomerModel> Customers { get; private set; }

        private const int MAX_CUSTOMERS = 16;   // Maximum number of customers inside the store
        public int CustomersInStore = 0;        // Number of customers inside the store
        private int numCustomers = 0;           // Number of customers that have visited the store

        CustomerModel[] cashiers = new CustomerModel[4];  // Cashiers which serve the customers

        private float simTime;                     // Total time the simulation has run
        private float respawnTimer;                // Timer used to delay time between customer spawning
        private const float SPAWN_TIME = 6.0f;     // Time in seconds between each customer attempting to enter the store
        private const float SIM_DURATION = 300.0f; // Time in seconds for how long the simulation should run
        private Random rand = new Random();         // Used to determine what type of customer to generate

        /// <summary>
        /// Stores all statistics that are tracked during the simulation
        /// </summary>
        public StatisticsModel Statistics { get; private set; } 

        /// <summary>
        /// Returns whether or not the simulation is paused
        /// </summary>
        public bool Paused { get; private set; }

        /// <summary>
        /// Returns a rounded version of the current simulation time
        /// </summary>
        public float SimTime
        {
            get { return (float)Math.Round(simTime, 2); }
            private set { simTime = value; }
        }

        public void Initialize()
        {
            Customers = new Queue<CustomerModel>();
            // Initialize Waypoints
        }

        public void Update(float gameTime)
        {

            if (!Paused)
            {
                // Advance simulation time
                SimTime += gameTime;

                // Advance respawn timer
                respawnTimer += gameTime;

                // If the required amount of time has passed to spawn another customer
                if (respawnTimer >= SPAWN_TIME)
                {
                    // Restart the respawn timer
                    CustomersInStore++;
                    CustomerModel.CustomerType customerType;
                    int randType = rand.Next(0, 3);

                    switch (randType)
                    {
                        case 0:
                            customerType = CustomerModel.CustomerType.Coffee;
                            break;
                        case 1:
                            customerType = CustomerModel.CustomerType.Food;
                            break;
                        case 2:
                            customerType = CustomerModel.CustomerType.Both;
                            break;
                        default:
                            customerType = CustomerModel.CustomerType.Coffee;
                            break;
                    }
                    
                    Customers.Enqueue(new Node<CustomerModel>(CustomersInStore, new CustomerModel(customerType, numCustomers)));
                }

                // If the customer at the front of the queue is at the front of the line
                if (Customers.Peek().Value.Postion == waypointManager.InLineWaypoints[0])
                {
                    // Check if any cashiers are available
                    for (int j = 0; j < cashiers.Length; j++)
                    {
                        // If a cashier is empty
                        if (cashiers[j] == null)
                        {
                            // Dequeue them from the line and pass it onto the cashier
                            cashiers[j] = Customers.Dequeue().Value;
                            // Set the waypoint to the cashier
                            cashiers[j].CurrWaypoint = waypointManager.CashierWaypoints[0];
                        }
                    }
                }

            }
        }
    }
}
