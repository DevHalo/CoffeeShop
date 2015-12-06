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

        Customer[] cashiers = new Customer[4];  // Cashiers which serve the customers

        private double simTime;                     // Total time the simulation has run
        private double respawnTimer;                // Timer used to delay time between customer spawning
        private const double SPAWN_TIME = 5.0d;     // Time in seconds between each customer attempting to enter the store
        private const double SIM_DURATION = 300.0d; // Time in seconds for how long the simulation should run

        /// <summary>
        /// Stores all statistics that are tracked during the simulation
        /// </summary>
        public Statistics Statistics { get; private set; } 

        /// <summary>
        /// Returns whether or not the simulation is paused
        /// </summary>
        public bool Paused { get; private set; }

        /// <summary>
        /// Returns a rounded version of the
        /// </summary>
        public double SimTime
        {
            get { return Math.Round(simTime, 2); }
            private set { simTime = value; }
        }

        public void Initialize()
        {
            Customers = new Queue<Customer>();
            // Initialize Waypoints
        }

        public void Update(float gameTime)
        {
            inputManager.Update(gameTime);

            // Pause the simulation if the left mouse button is called
            if (inputManager.IsClicked)
            {
                Paused = !Paused;
                inputManager.ResetIsClicked();
            }

            if (!Paused)
            {
                // Advance simulation time
                SimTime += gameTime;

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
