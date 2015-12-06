// Author: Sanjay Paraboo
// File Name: CoffeeShopSimulation.sln
// Project Name: A5 Data Manipulation Assignment
// Creation Date: Dec 5, 2015
// Modified Date:
// Description:

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CoffeeShopSimulation
{
    class CustomerModel
    {
        // Creates constants for the order times in seconds
        const float ORDER_TIME_COFFEE = 12.0f;
        const float ORDER_TIME_FOOD = 18.0f;
        const float ORDER_TIME_BOTH = 30.0f;

        // Bool is set to true to when the customer leaves the shop
        public bool IsDone { get; private set; }

        // Integer that stores the customers number
        public int CustomerNumber { get; private set; }

        // Stores the customer info
        public string CustomerName { get; private set; }

        //
        public double OrderTime { get; private set; }

        //
        public Vector2 Postion { get; private set; }

        //
        public Vector2 currWaypoint { get; private set; }

        public Queue<Vector2> Waypoints { get; private set; }


        public enum CustomerType
        {
            Coffee,
            Food,
            Both
        };

        public CustomerType Type { get; private set; }

        public enum CustomerState
        {
            Outside,
            InLine,
            AtCashier,
            ExitStore
        };

        public CustomerState CurrentState { get; private set; }

        /// <summary>
        /// Used to create an instance of a customer
        /// </summary>
        /// <param name="waypoints">
        /// Passes through the waypoints in order to set the location of the customer
        /// </param>
        /// <param name="customerType">
        /// Used to specify what type of customer to create
        /// </param>
        /// <param name="customerNumber">
        /// Used to store the customers current postion in the Queue
        /// </param>
        public CustomerModel(Queue<Vector2> waypoints, CustomerType customerType, int customerNumber)
        {
            // Sets the class level variables values to the ones obtained from the constructor
            this.Type = customerType;
            this.Waypoints = waypoints;
            this.CustomerNumber = customerNumber;

            // Sets the customer name by converting the customer type enum to a string and adding the customer
            // number to the end
            CustomerName = Type.ToString() + " " + CustomerNumber;

            // Switch statement used to set the customers order time based upon its Type
            switch (Type)
            {
                case CustomerType.Coffee:
                    OrderTime = ORDER_TIME_COFFEE;
                    break;

                case CustomerType.Food:
                    OrderTime = ORDER_TIME_FOOD;
                    break;

                case CustomerType.Both:
                    OrderTime = ORDER_TIME_BOTH;
                    break;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(float gameTime)
        {
            switch (CurrentState)
            {
                case CustomerState.Outside:

                    break;

                case CustomerState.InLine:

                    break;

                case CustomerState.AtCashier:

                    break;

                case CustomerState.ExitStore:

                    break;
            }

        }
    }
}
