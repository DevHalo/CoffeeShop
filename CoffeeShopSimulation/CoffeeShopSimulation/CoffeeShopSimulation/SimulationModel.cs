// Author: Mark Voong
// Class Name: SimulationModel.cs
// Date Created: Dec 5th 2015
// Date Modified: Dec 6th 2015
// Description: Handles all simulation logic of the coffee shop
using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Queue that handles every customer that is leaving the store
        /// </summary>
        public List<CustomerModel> ExitList { get; private set; }  

        private const int MAX_CUSTOMERS = 16;                               // Maximum number of customers inside the store
        private readonly Random rand = new Random();                                 // Used to determine what type of customer to generate
        public int CustomersOutsideStore { get; private set; }              // Number of customers outside the store
        public int CustomersInStoreLine { get; private set; }               // Number of customers in the line inside the store
        public int CustomersInStore { get; private set; }                   // Number of customers inside the store
        private int numCustomers;                                           // Number of customers that have visited the store
        private Vector2 doorVector = new Vector2(450, 500);                  // Vector that is at the front of the store
        private Vector2 frontInsideLineVector = new Vector2(1150, 500);     // Vector that is at the front of the line inside the store 
        private Vector2 exitVector = new Vector2(1260, 800);                // Vector that is at the exit of the store
        public Rectangle ShopBorder = new Rectangle(100, 200, 800, 700);    // Dimensions of the shop
        public CustomerModel[] Cashiers { get; private set; }               // Cashiers which serve the customers
        public Vector2[] CashierVectors { get; private set; }               // Vectors at which each the customer will goto to be served by the cashier

        // Simulation Variables
        private const float SPAWN_TIME = 1.0f;          // Time in seconds between each customer attempting to enter the store
        private const float SIM_DURATION = 300.0f;      // Time in seconds for how long the simulation should run
        private const float STAT_UPDATE_TIME = 1.0f;    // How often should the simulation update the statistics
        private float simTime;                          // Total time the simulation has run
        private float respawnTimer;                     // Timer used to delay time between customer spawning
        private float updateTimer;                      // Timer used to track how long between each statistic update

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
        public SimulationModel()
        {
            // Initialize queues
            OutsideLine = new Queue<CustomerModel>();
            InsideLine = new Queue<CustomerModel>();
            ExitList = new List<CustomerModel>();
            
            // Initialize cashier and vectors
            Cashiers = new CustomerModel[4];
            CashierVectors = new Vector2[4];

            // Set vectors for cashiers
            for (int i = 0; i < CashierVectors.Length; i++)
            {
                CashierVectors[i] = new Vector2(1280, 400 + (50 * i));
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

                    // Advance statistics timer
                    updateTimer += gameTime;

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
                        OutsideLine.Enqueue(new Node<CustomerModel>(new CustomerModel(customerType, numCustomers, CustomersOutsideStore, doorVector)));

                        // Reset the timer
                        respawnTimer = 0;
                    }

                    if (updateTimer >= STAT_UPDATE_TIME)
                    {

                        updateTimer = 0;
                    }

                    bool checkStore = false; // Whether or not the simulation should check how many people are inside the store

                    // If there is a customer outside the store
                    if (OutsideLine.Size > 0)
                    {
                        // Update each customer
                        Node<CustomerModel> curCustomer = OutsideLine.Peek();
                        for (int i = 0; i < OutsideLine.Size; i++)
                        {
                            curCustomer.Value.Update(gameTime);
                            curCustomer = curCustomer.Next;
                        }

                        // If the customer at the front of the line is at the door
                        if (OutsideLine.Peek().Value.Position == doorVector)
                        {
                            // Check if there arn't too many customers in the store
                            if (CustomersInStore < MAX_CUSTOMERS)
                            {
                                // Increment the number of customers in the line inside the store
                                CustomersInStoreLine++;

                                // Dequeue the customer from the outside line
                                CustomerModel dequeuedCustomer = OutsideLine.Dequeue().Value;

                                // Set up the customer's new position in the line inside the store
                                dequeuedCustomer.GoInside(CustomersInStoreLine, frontInsideLineVector);

                                // Add it to the queue inside the store
                                InsideLine.Enqueue(new Node<CustomerModel>(dequeuedCustomer));

                                // Decrement the number of customers outside the store
                                CustomersOutsideStore--;

                                // Move everyone outside the store up one space
                                curCustomer = OutsideLine.Peek();
                                for (int i = 0; i < OutsideLine.Size; i++)
                                {
                                    curCustomer.Value.Advance(i, doorVector);
                                }
                            }
                        }
                    }

                    // If there is more than one person in the line inside the store
                    if (InsideLine.Size > 0)
                    {
                        checkStore = true;

                        // Update every customer inside the store that is in the line
                        Node<CustomerModel> curCustomer = InsideLine.Peek();
                        for (int i = 0; i < InsideLine.Size; i++)
                        {
                            curCustomer.Value.Update(gameTime);
                            curCustomer = curCustomer.Next;
                        }

                        // If there is a customer that is at the front of the line
                        if (InsideLine.Peek().Value.Position == frontInsideLineVector)
                        {
                            // Check which cashier is available 
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
                                        curCustomer.Value.Advance(j, frontInsideLineVector);
                                        curCustomer = curCustomer.Next;
                                    }
                                    break;
                                }
                            }
                        }

                        for (int i = 0; i < Cashiers.Length; i++)
                        {
                            if (Cashiers[i] != null)
                            {
                                checkStore = true;

                                Cashiers[i].Update(gameTime);

                                if (Cashiers[i].Position == CashierVectors[i] &&
                                    Cashiers[i].CurrentState == CustomerModel.CustomerState.InLine)
                                {
                                    Cashiers[i].ChangeCustomerState(CustomerModel.CustomerState.AtCashier);
                                }

                                if (Cashiers[i].CurrentState == CustomerModel.CustomerState.ExitStore)
                                {
                                    Cashiers[i].ChangeCurrWaypoint(exitVector);
                                    ExitList.Add(Cashiers[i]);
                                    Cashiers[i] = null;
                                }
                            }
                        }
                    }

                    if (ExitList.Count > 0)
                    {
                        checkStore = true;

                        for (int i = 0; i < ExitList.Count; i++)
                        {
                            ExitList[i].Update(gameTime);

                            if (ExitList[i].Position == exitVector)
                            {
                                ExitList.RemoveAt(i);
                            }
                        }
                    }

                    if (checkStore)
                    {
                        CheckNumberOfCustomers();
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
                if (customer != null &&
                    customer.Position.X > 0 &&
                    customer.Position.Y > 0 &&
                    customer.Position.X < 1366 &&
                    customer.Position.Y < 600)
                {
                    customersInStore++;

                    if (customersInStore > MAX_CUSTOMERS)
                    {
                        return false;
                    }
                }
            }

            Node<CustomerModel> curCustomer = InsideLine.Peek();
            for (int i = 0; i < InsideLine.Size; i++)
            {
                if (curCustomer.Value.Position.X > 0 &&
                    curCustomer.Value.Position.Y > 0 &&
                    curCustomer.Value.Position.X < 1366 &&
                    curCustomer.Value.Position.Y < 600)
                {
                    customersInStore++;
                }

                if (customersInStore > MAX_CUSTOMERS)
                {
                    return false;
                }
            }

            foreach (CustomerModel customer in ExitList)
            {
                if (customer.Position.X > 0 &&
                    customer.Position.Y > 0 &&
                    customer.Position.X < 1366 &&
                    customer.Position.Y < 600)
                {
                    customersInStore++;
                }

                if (customersInStore > MAX_CUSTOMERS)
                {
                    return false;
                }
            }

            CustomersInStore = customersInStore;
            return customersInStore <= MAX_CUSTOMERS;
        }
    }
}
