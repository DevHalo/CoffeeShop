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
        public int CustomersInStore { get; private set; }        // Number of customers inside the store
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
            get { return (float)Math.Round(simTime, 3); }
            private set { simTime = value; }
        }

        /// <summary>
        /// Initializes the simulation. This is called once at load
        /// </summary>
        public void Initialize()
        {
            Customers = new Queue<CustomerModel>();
            // Initialize Waypoints
            waypointManager = new WaypointManager();
        }

        /// <summary>
        /// Updates all logic in the simulation
        /// </summary>
        /// <param name="gameTime"></param>
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

                    // Randomly select the type of customer that will be spawned.
                    // If somehow the integer is not between 0-2, the default customer will be Coffee
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
                    
                    // Add it to the queue
                    Customers.Enqueue(new Node<CustomerModel>(CustomersInStore, new CustomerModel(customerType, numCustomers, CustomersInStore)));
                    respawnTimer = 0;
                }

                Node<CustomerModel> curCustomer = Customers.Peek();
                for (int i = 0; i < Customers.Size; i++)
                {
                    curCustomer.Value.Update(gameTime);
                }

                // If there are any customers that are coming to the shop
                if (Customers.Size > 0)
                {
                    // If the customer at the front of the queue is at the front of the line
                    if (Customers.Peek().Value.Position == waypointManager.InLineWaypoints[0])
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
                                cashiers[j].ChangeCurrWaypoint(waypointManager.CashierWaypoints[0]);

                                // Move every person up one space
                                curCustomer = Customers.Peek();
                                bool moveOutsideLine = false;   // Should the outside line advance
                                for (int k = 0; k < Customers.Size; k++)
                                {
                                    // If the customer is currently inside the building, advance them one space
                                    // Else if the customer is just outside the building, check to see if there is room inside
                                    if (curCustomer.Value.PositionInLine <= 12)
                                    {
                                        curCustomer.Value.Advance(waypointManager.InLineWaypoints);
                                    }
                                    else if (curCustomer.Value.CurrWaypoint == waypointManager.InitialOutsideWaypoint)
                                    {
                                        // If there is room in the building, move everyone
                                        if (CheckNumberOfCustomers())
                                        {
                                            // Move every customer that is outside one space forward
                                            moveOutsideLine = true;
                                        }
                                    }
                                    else if (curCustomer.Value.PositionInLine >= 13)
                                    {
                                        if (moveOutsideLine)
                                        {
                                            // TODO: ADD OUTSIDE LINE WAYPOINTS
                                            curCustomer.Value.Advance(waypointManager.InLineWaypoints);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks if the number of customers inside the building 
        /// </summary>
        /// <returns>Returns true if there are 16 or less customers in the store</returns>
        public bool CheckNumberOfCustomers()
        {
            // Check each cashier to verify where the customer is if they are being processed or
            // leaving the store
            int customersInStore = 0;   // Number of customers in store
            foreach (CustomerModel customer in cashiers)
            {
                if (customer.Position.X > 0 &&
                    customer.Position.Y > 0 &&
                    customer.Position.X < 1366 &&
                    customer.Position.Y < 600)
                {
                    customersInStore++;
                }
            }

            // Go through everyone in the queue and see if they are
            // inside the building (True if they are the 12th customer
            // in line. Don't bother checking if there are more than 12 customers
            // in the queue (4 with cashiers/leaving + 12 in the line = 16)
            Node<CustomerModel> curCustomer = Customers.Peek();
            for (int i = 0; i < Customers.Size; i++)
            {
                // If the number of customers in line is greater than 12,
                // There are customers waiting outside therefore there are too many
                // customers in the store
                if (i > 12)
                {
                    return false;
                }
                
                // If the customer is or is in front of the 12th customer,
                // They are in the store
                if (curCustomer.Value.PositionInLine <= 12)
                {
                    customersInStore++;
                }

            }

            // If there are 16 or less customers in the store, return true
            return customersInStore <= MAX_CUSTOMERS;
        }
    }
}
