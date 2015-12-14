// Author: Mark Voong
// Class Name: SimulationModel.cs
// Date Created: Dec 5th 2015
// Date Modified: Dec 14th 2015
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
        private readonly Random rand = new Random();                        // Used to determine what type of customer to generate
        public int CustomersOutsideStore { get; private set; }              // Number of customers outside the store
        public int CustomersInStoreLine { get; private set; }               // Number of customers in the line inside the store
        public int CustomersInStore { get; private set; }                   // Number of customers inside the store
        public int TotalCustomers { get; private set; }                     // Number of customers that have visited the store
        public int CustomersServed { get; private set; }                    // Number of customers that have been served
        private Vector2 frontOutsideLineVector = new Vector2(420, 500);     // Vector that is at the front of the line outside the store
        private Vector2 frontInsideLineVector = new Vector2(1200, 500);     // Vector that is at the front of the line inside the store 
        private Vector2 exitVector = new Vector2(1260, 800);                // Vector that is at the exit of the store
        private Rectangle shopBorder = new Rectangle(452, 333, 914, 355);   // Dimensions of the shop
        public CustomerModel[] Cashiers { get; private set; }               // Cashiers which serve the customers
        public Vector2[] CashierVectors { get; private set; }               // Vectors at which each the customer will goto to be served by the cashier

        // Simulation Variables
        private const float SPAWN_TIME = 6.0f;          // Time in seconds between each customer attempting to enter the store
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
            get { return (float)Math.Round((simTime - (simTime % 0.01f)),2) ; }
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

            // Initialize statistics class
            Statistics = new StatisticsModel();

            // Set vectors for cashiers
            for (int i = 0; i < CashierVectors.Length; i++)
            {
                CashierVectors[i] = new Vector2(1280, 400 + (50 * i));
            }
        }

        /// <summary>
        /// Updates all logic in the simulation
        /// </summary>
        /// <param name="gameTime"> Wall time in milliseconds</param>
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
                        // Increase the amount of customers outside the store
                        CustomersOutsideStore++;

                        // Increase the total number of customers
                        TotalCustomers++;

                        CustomerModel.CustomerType customerType;    // Stores what type of customer it will be
                        int randType = rand.Next(0, 3);             // Picks a number between 0 and 2

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
                        OutsideLine.Enqueue(new Node<CustomerModel>(
                            new CustomerModel(
                                customerType,
                                TotalCustomers, CustomersOutsideStore - 1, 
                                frontOutsideLineVector)));

                        // Reset the timer
                        respawnTimer = 0;
                    }

                    // TODO: STATISTICS UPDATE
                    if (updateTimer >= STAT_UPDATE_TIME )
                    {
                        Statistics.Update(OutsideLine, InsideLine, Cashiers, ExitList);
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
                        if (OutsideLine.Peek().Value.Position == frontOutsideLineVector)
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
                                    curCustomer.Value.Advance(frontOutsideLineVector);
                                    curCustomer = curCustomer.Next;
                                }
                            }
                        }
                    }

                    // If there is more than one person in the line inside the store
                    if (InsideLine.Size > 0)
                    {
                        checkStore = true;      // Check the inside of the building for the number of people

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
                                    // Dequeue the customer from the inside line
                                    Node<CustomerModel> deqeuedCustomer = InsideLine.Dequeue();

                                    // Change the customer's waypoint to an empty customer
                                    deqeuedCustomer.Value.ChangeCurrWaypoint(CashierVectors[i]);

                                    // Transfer the customer to the cashier
                                    Cashiers[i] = deqeuedCustomer.Value;

                                    // Subtract the number of customers in the store line
                                    CustomersInStoreLine--;

                                    // Move every customer in line up one space
                                    curCustomer = InsideLine.Peek();
                                    for (int j = 0; j < InsideLine.Size; j++)
                                    {
                                        curCustomer.Value.Advance(frontInsideLineVector);
                                        curCustomer = curCustomer.Next;
                                    }
                                    break;
                                }
                            }
                        }

                        // Update each cashier
                        for (int i = 0; i < Cashiers.Length; i++)
                        {
                            // If the cashier is not empty
                            if (Cashiers[i] != null)
                            {
                                // Update the customer at the cashier
                                Cashiers[i].Update(gameTime);

                                // If the customer's state is not "AtCashier" and is at the counter,
                                // change the state so that it is
                                if (Cashiers[i].Position == CashierVectors[i] &&
                                    Cashiers[i].CurrentState == CustomerModel.CustomerState.InLine)
                                {
                                    Cashiers[i].ChangeCustomerState(CustomerModel.CustomerState.AtCashier);
                                }

                                // If the customer is now leaving the store and has not moved from the cashier,
                                // set the waypoint to the exit and add it to the list of exiting customers.
                                // Also set the cashier to null, or empty.
                                if (Cashiers[i].CurrentState == CustomerModel.CustomerState.ExitStore)
                                {
                                    Cashiers[i].ChangeCurrWaypoint(exitVector);
                                    ExitList.Add(Cashiers[i]);
                                    Cashiers[i] = null;
                                }
                            }
                        }
                    }

                    // If there is a customer leaving the store
                    if (ExitList.Count > 0)
                    {
                        // Check the number of people in the building
                        checkStore = true;

                        // Update each customer
                        for (int i = 0; i < ExitList.Count; i++)
                        {
                            ExitList[i].Update(gameTime);

                            // If the customer has made it to the exit coordinate,
                            // delete them from the simulation
                            if (ExitList[i].Position == exitVector)
                            {
                                Statistics.ProcessExitingCustomer(ExitList[i]);
                                ExitList.RemoveAt(i);
                                CustomersServed++;
                            }
                        }
                    }

                    // If the simulation requires a check of how many customers
                    // are in the store
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
            CustomersInStore = 0;   // Number of customers in store

            // Check each customer at the cashiers
            // A customer who is part of the cashier array will either be
            // walking to the cashier, or at the cashier. Therefore only increment
            // if the cashier is not null
            foreach (CustomerModel customer in Cashiers)
            {
                // If the customer is at one of the cashiers
                if (customer != null)
                {
                    CustomersInStore++;
                }
            }

            // Check each customer inside the store that is in the line
            Node<CustomerModel> curCustomer = InsideLine.Peek();
            for (int i = 0; i < InsideLine.Size; i++)
            {
                // If the customer is inside the borders of the store
                if (curCustomer.Value.Position.X > shopBorder.Left &&
                    curCustomer.Value.Position.Y > shopBorder.Top &&
                    curCustomer.Value.Position.X < shopBorder.Right &&
                    curCustomer.Value.Position.Y < shopBorder.Bottom)
                {
                    CustomersInStore++;

                    // Return false immediately if there are too many customers in the store
                    if (CustomersInStore > MAX_CUSTOMERS)
                    {
                        return false;
                    }
                }
            }

            // Check each customer that is leaving the store
            foreach (CustomerModel customer in ExitList)
            {
                if (customer.Position.X > shopBorder.Left &&
                    customer.Position.Y > shopBorder.Top &&
                    customer.Position.X < shopBorder.Right &&
                    customer.Position.Y < shopBorder.Bottom)
                {
                    CustomersInStore++;

                    // Return false immediately if there are too many customers in the store
                    if (CustomersInStore > MAX_CUSTOMERS)
                    {
                        return false;
                    }
                }
            }

            return CustomersInStore <= MAX_CUSTOMERS;
        }
    }
}
