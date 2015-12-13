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

        /// <summary>
        /// Queue that stores every customer that has entered the store
        /// </summary>
        public Queue<CustomerModel> outsideLine { get; private set; }

        public Queue<CustomerModel> insideLine { get; private set; }

        private Vector2 doorVector = new Vector2(50, 400);

        private const int MAX_CUSTOMERS = 16;   // Maximum number of customers inside the store
        public int CustomersOutsideStore { get; private set; }        // Number of customers inside the store
        public int CustomersInStore { get; private set; }
        private int numCustomers;           // Number of customers that have visited the store

        CustomerModel[] cashiers = new CustomerModel[4];  // Cashiers which serve the customers

        private float simTime;                     // Total time the simulation has run
        private float respawnTimer;                // Timer used to delay time between customer spawning
        private const float SPAWN_TIME = 1.0f;     // Time in seconds between each customer attempting to enter the store
        private const float SIM_DURATION = 300.0f; // Time in seconds for how long the simulation should run
        private Random rand = new Random();        // Used to determine what type of customer to generate

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
        }

        /// <summary>
        /// Initializes the simulation. This is called once at load
        /// </summary>
        public void Initialize()
        {
            outsideLine = new Queue<CustomerModel>();
            insideLine = new Queue<CustomerModel>();
        }

        /// <summary>
        /// Updates all logic in the simulation
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(float gameTime)
        {
            if (simTime <= SIM_DURATION)
            {
                if (!Paused)
                {
                    // Advance simulation time
                    simTime += gameTime;

                    // Advance respawn timer
                    respawnTimer += gameTime;

                    // If the required amount of time has passed to spawn another customer
                    if (respawnTimer >= SPAWN_TIME)
                    {
                        // Restart the respawn timer
                        CustomersOutsideStore++;
                        numCustomers++;
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
                        outsideLine.Enqueue(new Node<CustomerModel>(CustomersOutsideStore,
                            new CustomerModel(customerType, numCustomers, CustomersOutsideStore)));
                        respawnTimer = 0;
                    }

                    if (outsideLine.Size > 0)
                    {
                        Node<CustomerModel> curCustomer = outsideLine.Peek();
                        for (int i = 0; i < outsideLine.Size; i++)
                        {
                            curCustomer.Value.Update(gameTime);
                            curCustomer = curCustomer.Next;
                        }


                        bool advanceAll = false;
                        if (outsideLine.Peek().Value.Position == doorVector)
                        {
                            if (CustomersInStore < 16)
                            {
                                CustomersInStore++;
                                Node<CustomerModel> dequeuedCustomer = outsideLine.Dequeue();
                                dequeuedCustomer.SetNext(null);
                                dequeuedCustomer.Value.GoInside(CustomersInStore);
                                insideLine.Enqueue(dequeuedCustomer);
                                CustomersOutsideStore--;
                                advanceAll = true;
                            }
                        }

                        if (advanceAll)
                        {
                            curCustomer = outsideLine.Peek();
                            for (int i = 0; i < outsideLine.Size; i++)
                            {
                                curCustomer.Value.Advance();
                            }
                        }

                        /*
                    for (int i = 0; i < cashiers.Length; i++)
                    {
                        if (cashiers[i] == null)
                        {
                            cashiers[i] = outsideLine.Dequeue().Value;
                            break;
                        }
                    }
                    */
                    }

                    if (insideLine.Size > 0)
                    {
                        Node<CustomerModel> curCustomer = insideLine.Peek();
                        for (int i = 0; i < insideLine.Size; i++)
                        {
                            curCustomer.Value.Update(gameTime);
                            curCustomer = curCustomer.Next;
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
            Node<CustomerModel> curCustomer = outsideLine.Peek();
            for (int i = 0; i < outsideLine.Size; i++)
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
