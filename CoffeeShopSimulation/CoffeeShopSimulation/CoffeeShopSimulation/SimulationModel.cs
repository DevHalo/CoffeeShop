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

        private Queue<Vector2> waypoints; // Locations that allow the customer to follow a certain path

        /// <summary>
        /// Queue that stores every customer that has entered the store
        /// </summary>
        public Queue<Customer> Customers { get; private set; }

        private const int MAX_CUSTOMERS = 16;   // Maximum number of customers inside the store
        public int CustomersInStore = 0;        // Number of customers inside the store
        private int numCustomers = 0;           // Number of customers that have visited the store

        Customer[] cashiers = new Customer[4];  // Cashiers which serve the customers

        private float simTime;                     // Total time the simulation has run
        private float respawnTimer;                // Timer used to delay time between customer spawning
        private const float SPAWN_TIME = 6.0f;     // Time in seconds between each customer attempting to enter the store
        private const float SIM_DURATION = 300.0f; // Time in seconds for how long the simulation should run
        private Random rand = new Random();         // Used to determine what type of customer to generate

        /// <summary>
        /// Stores all statistics that are tracked during the simulation
        /// </summary>
        public Statistics Statistics { get; private set; } 

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
            Customers = new Queue<Customer>();
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
                    Customer.CustomerType customerType;
                    int randType = rand.Next(0, 2);

                    switch (randType)
                    {
                        case 0:
                            customerType = Customer.CustomerType.Coffee;
                            break;
                        case 1:
                            customerType = Customer.CustomerType.Food;
                            break;
                        case 2:
                            customerType = Customer.CustomerType.Both;
                            break;
                    }
                    
                    Customers.Enqueue(new Node<Customer>(CustomersInStore, new Customer(waypoints, customerType, numCustomers, )));
                }

                // Get the next customer in the queue
                Node<Customer> curCustomer = Customers.Peek();

                // Update each customer
                for (int i = 0; i < Customers.Size; i++)
                {
                    
                    curCustomer.Value.Update(gameTime);

                    switch (curCustomer.Value.CurrentState)
                    {
                        case Customer.CustomerState.Outside:
                            break;
                        case Customer.CustomerState.InLine:
                            break;
                        case Customer.CustomerState.AtCashier:
                            break;
                        case Customer.CustomerState.ExitStore:
                            // If the customer has made it to the final waypoint
                            if (curCustomer.Value.Waypoints.Size == 1 &&
                                curCustomer.Value.Postion == curCustomer.Value.Waypoints.Peek().Value)
                            {
                                
                                // Dequeue the current customer
                                Customers.Dequeue();
                            }
                            break;
                    }

                    curCustomer = curCustomer.GetNext();
                }
            }
        }
    }
}