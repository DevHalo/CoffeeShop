// Author: Sanjay Paraboo
// File Name: CoffeeShopSimulation.sln
// Project Name: A5 Data Manipulation Assignment
// Creation Date: Dec 5, 2015
// Modified Date:
// Description:

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffeeShopSimulation
{
    class WaypointManager
    {
        // Constant that stores the space in pixels between each customer
        const int SPACE_BETWEEN_CUSTOMERS = 75;

        // Stores the customer number of the first customer in the line and outside the store
        private int firstCustomerInLine;
        private int firstCustomerOutside;

        // Stores the waypoint locations for the customer
        public Vector2 InitialOutsideWaypoint { get; private set; }
        public Vector2[] InLineWaypoints { get; private set; }
        public Vector2[] CashierWaypoints { get; private set; }
        public Vector2 ExitWaypoint { get; private set; }

        
        /// <summary>
        /// Creates an instance of Waypoint Manager
        /// </summary>
        public WaypointManager()
        {
            // Initialize Vectors
        }


        public Vector2 ReturnWaypoint(int customerNumber, CustomerModel.CustomerState customerState)
        {
            //TODO Figure out how to find location when customers are switching between Customer States

            switch (customerState)
            {
                case CustomerModel.CustomerState.Outside:
                    return new Vector2(initialOutsideWaypoint.X + ((customerNumber - firstCustomerOutside) * SPACE_BETWEEN_CUSTOMERS));

                case CustomerModel.CustomerState.InLine:
                    int numberInQueue = customerNumber - firstCustomerInLine;

                    return inLineWaypoints[numberInQueue];

                case CustomerModel.CustomerState.AtCashier:
                    //add logic to see which cashier is available

                    int availableCashier = 0;

                    return cashierWaypoints[availableCashier];

                case CustomerModel.CustomerState.ExitStore:
                    return exitWaypoint;

                default:
                    return Vector2.Zero;
            }

        }
    }
}
