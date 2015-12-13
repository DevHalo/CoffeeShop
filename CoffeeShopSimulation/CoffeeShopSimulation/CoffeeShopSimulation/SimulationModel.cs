// Author: Mark Voong
// Class Name: SimulationModel.cs
// Date Created: Dec 5th 2015
// Date Modified: Dec 6th 2015
// Description: Handles all simulation logic of the coffee shop

using System;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;

namespace CoffeeShopSimulation
{
    class SimulationModel
    {
        private InputManager inputManager = new InputManager();  // Controller Class

        /// <summary>
        /// Queue that stores every customer that has outside the store
        /// </summary>
        public Queue<CustomerModel> OutsideLine { get; private set; }

        /// <summary>
        /// Queue that stores every customer that is inside the store
        /// </summary>
        public Queue<CustomerModel> InsideLine { get; private set; }

        private const int MAX_CUSTOMERS = 16;                               // Maximum number of customers inside the store
        private Random rand = new Random();                                 // Used to determine what type of customer to generate
        public int CustomersOutsideStore { get; private set; }              // Number of customers outside the store
        public int CustomersInStoreLine { get; private set; }               // Number of customers in the line inside the store
        public int CustomersInStore { get; private set; }                   // Number of customers inside the store
        private int numCustomers;                                           // Number of customers that have visited the store
        private Vector2 doorVector = new Vector2(50, 400);                  // Vector that is at the front of the store
        private Vector2 frontInsideLineVector = new Vector2(1050, 400);     // Vector that is at the front of the line inside the store 
        public CustomerModel[] Cashiers { get; private set; }               // Cashiers which serve the customers
        public Vector2[] CashierVectors { get; private set; }               // Vectors at which each the customer will goto to be served by the cashier

        // Simulation Variables
        private float simTime;                     // Total time the simulation has run
        private float respawnTimer;                // Timer used to delay time between customer spawning
        private const float SPAWN_TIME = 1.0f;     // Time in seconds between each customer attempting to enter the store
        private const float SIM_DURATION = 300.0f; // Time in seconds for how long the simulation should run

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
            get { return (simTime - (simTime % 0.01f)) ; }
        }

        /// <summary>
        /// Initializes the simulation. This is called once at load
        /// </summary>
        public void Initialize()
        {
            OutsideLine = new Queue<CustomerModel>();
            InsideLine = new Queue<CustomerModel>();
            Cashiers = new CustomerModel[4];
            CashierVectors = new Vector2[4];

            for (int i = 0; i < CashierVectors.Length; i++)
            {
                CashierVectors[i] = new Vector2(1280, 200 + (100 * i));
            }
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
                        OutsideLine.Enqueue(new Node<CustomerModel>(CustomersOutsideStore,
                            new CustomerModel(customerType, numCustomers, CustomersOutsideStore)));
                        respawnTimer = 0;
                    }

                    if (OutsideLine.Size > 0)
                    {
                        Node<CustomerModel> curCustomer = OutsideLine.Peek();
                        for (int i = 0; i < OutsideLine.Size; i++)
                        {
                            curCustomer.Value.Update(gameTime);
                            curCustomer = curCustomer.Next;
                        }

                        if (OutsideLine.Peek().Value.Position == doorVector)
                        {
                            if (CustomersInStore < MAX_CUSTOMERS)
                            {
                                CustomersInStore++;
                                CustomersInStoreLine++;
                                Node<CustomerModel> dequeuedCustomer = OutsideLine.Dequeue();
                                dequeuedCustomer.SetNext(null);
                                dequeuedCustomer.Value.GoInside(CustomersInStoreLine);
                                InsideLine.Enqueue(dequeuedCustomer);
                                CustomersOutsideStore--;

                                curCustomer = OutsideLine.Peek();
                                for (int i = 0; i < OutsideLine.Size; i++)
                                {
                                    curCustomer.Value.Advance();
                                }
                            }
                        }
                    }

                    if (InsideLine.Size > 0)
                    {
                        Node<CustomerModel> curCustomer = InsideLine.Peek();
                        for (int i = 0; i < InsideLine.Size; i++)
                        {
                            curCustomer.Value.Update(gameTime);
                            curCustomer = curCustomer.Next;
                        }

                        if (InsideLine.Peek().Value.Position == frontInsideLineVector)
                        {
                            for (int i = 0; i < Cashiers.Length; i++)
                            {
                                if (Cashiers[i] == null)
                                {
                                    Node<CustomerModel> deqeuedCustomer = InsideLine.Dequeue();
                                    deqeuedCustomer.Value.ChangeCurrWaypoint(CashierVectors[i]);
                                    Cashiers[i] = deqeuedCustomer.Value;
                                    CustomersInStoreLine--;
                                    curCustomer = InsideLine.Peek();
                                    for (int j = 0; j < InsideLine.Size; j++)
                                    {
                                        curCustomer.Value.Advance();
                                        curCustomer = curCustomer.Next;
                                    }
                                    break;
                                }
                            }
                        }
                    }

                    foreach (CustomerModel customer in Cashiers)
                    {
                        if (customer != null)
                        {
                            customer.Update(gameTime);
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
            foreach (CustomerModel customer in Cashiers)
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
            // in the queue (4 with Cashiers/leaving + 12 in the line = 16)
            Node<CustomerModel> curCustomer = OutsideLine.Peek();
            for (int i = 0; i < OutsideLine.Size; i++)
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
