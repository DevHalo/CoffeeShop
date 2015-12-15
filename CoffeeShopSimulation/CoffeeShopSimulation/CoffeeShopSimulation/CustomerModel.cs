// Author: Sanjay Paraboo
// File Name: CustomerModel.cs
// Project Name: A5 Data Manipulation Assignment
// Creation Date: Dec 5, 2015
// Modified Date: Dec 14th, 2015
// Description: A customer instance stores their current location, next waypoint, Order time, current wait time and

using Microsoft.Xna.Framework;
using System;

namespace CoffeeShopSimulation
{
    class CustomerModel
    {
        // Constants
        // Creates constants for the order times in seconds
        private const float ORDER_TIME_COFFEE = 12.0f;
        private const float ORDER_TIME_FOOD = 18.0f;
        private const float ORDER_TIME_BOTH = 30.0f;
        // Constant used to store the customers movement speed
        private const int MOVEMENT_SPEED = 12;
        // Distance between each customer
        private const int CUSTOMER_DISTANCE = 50;
        
        // Customer Draw Data
        // View class that implements the drawing function of the customer
        public CustomerView View { get; private set; }
        // Integer that stores the customers number
        private int customerNumber;
        // Used to store the customers name which will be displayed when the customer is drawn onto the screen
        public string CustomerName { get; private set; }

        // Customer Time Data
        // Stores the customers order time based on what they ordered
        private float orderTime;
        // Stores the customers time that has passed when they reached the cashier
        private float orderProcessTime; 
        // Used to record the amount of time that has passed since the customer was initialized
        public float WaitTime { get; private set; }

        // Customer Position Data
        // Stores the customer position on the screen and the position of their current waypoint
        public Vector2 Position { get; private set; }
        public Vector2 CurrWaypoint { get; private set; }
        // Stores the position of the customer in the line
        public int PositionInLine { get; private set; }

        /// <summary>
        /// Used to specify what type of customer will be initialized
        /// </summary>
        public enum CustomerType
        {
            Coffee,
            Food,
            Both
        };
        public CustomerType Type { get; private set; }

        /// <summary>
        /// Different states of the customer
        /// </summary>
        public enum CustomerState
        {
            Outside,
            InLine,
            AtCashier,
            ExitStore
        };

        /// <summary>
        /// The current state of the customer
        /// </summary>
        public CustomerState CurrentState { get; private set; }

        /// <summary>
        /// Returns the percentage
        /// </summary>
        public float PercentageFinished { get { return orderProcessTime / orderTime; } }

        /// <summary>
        /// Used to create an instance of a customer
        /// </summary>
        /// <param name="customerType">
        /// Used to specify what type of customer to create
        /// </param>
        /// <param name="customerNumber">
        /// Used to store the customers current postion in the Queue
        /// </param>
        /// <param name="positionInLine">
        /// Used to store the customer's current position in the line
        /// </param>
        /// <param name= "doorVector">
        /// Used to store the vector outside the door of the store
        /// </param>
        public CustomerModel(CustomerType customerType, int customerNumber, int positionInLine, Vector2 doorVector)
        {
            // Sets the class level variables values to the ones obtained from the constructor
            Type = customerType;
            this.customerNumber = customerNumber;
            PositionInLine = positionInLine;

            // Sets the customer name by converting the customer type enum to a string and adding the customer number to the end
            CustomerName = Type + String.Empty + customerNumber;

            // Switch statement used to set the customers order time based upon its Type
            switch (Type)
            {
                case CustomerType.Coffee:
                    orderTime = ORDER_TIME_COFFEE;
                    break;

                case CustomerType.Food:
                    orderTime = ORDER_TIME_FOOD;
                    break;

                case CustomerType.Both:
                    orderTime = ORDER_TIME_BOTH;
                    break;
            }

            // Sets the initial state and location of the customer to outside the coffee shop
            CurrentState = CustomerState.Outside;
            Position = new Vector2(doorVector.X, 800);

            // Set up the first waypoint Adjust their position in line if there are more than 12 people trying to enter the store
            CurrWaypoint = new Vector2(doorVector.X, doorVector.Y + (positionInLine * CUSTOMER_DISTANCE));

            // Intializes the Customer view instance and passes through the current customer model class
            View = new CustomerView(this);
        }

        /// <summary>
        /// Runs the update logic for the customer instance
        /// </summary>
        /// <param name="gameTimeInMilliSeconds">Passes through the elapsed time in milliseconds</param>
        public void Update(float gameTimeInMilliSeconds)
        {
            // Adds the elasped time to the customers wait time
            WaitTime += gameTimeInMilliSeconds;

            // If the customer is not at the current waypoint
            if (Position != CurrWaypoint)
            {
                // If the customer is 
                if (Vector2.Distance(Position, CurrWaypoint) > MOVEMENT_SPEED)
                {
                    // If the X or Y value of the customer is more or less than the waypoint than
                    // it will add or subtract to the vector in order to get it to the waypoint
                    if (CurrWaypoint.X > Position.X)
                    {
                        Position += new Vector2(MOVEMENT_SPEED, 0);
                    }
                    else if (CurrWaypoint.X < Position.X)
                    {
                        Position -= new Vector2(MOVEMENT_SPEED, 0);
                    }

                    if (CurrWaypoint.Y > Position.Y)
                    {
                        Position += new Vector2(0, MOVEMENT_SPEED);
                    }
                    else if (CurrWaypoint.Y < Position.Y)
                    {
                        Position -= new Vector2(0, MOVEMENT_SPEED);
                    }
                }
                else // If the distance between the position and waypoint is less than the movement speed radius
                     // It will adjust the poistion to the waypoint
                {
                    Position += new Vector2(CurrWaypoint.X - Position.X, CurrWaypoint.Y - Position.Y);
                }

            }

            // If the customers at the cashier it will incriment the order processing time and change
            // the state to exit store when the order is finished
            if (CurrentState == CustomerState.AtCashier)
            {
                orderProcessTime += gameTimeInMilliSeconds;

                if (orderProcessTime >= orderTime)
                {
                    CurrentState = CustomerState.ExitStore;
                }
            }
        }

        /// <summary>
        /// Changes the current waypoint of the customer to the specified one
        /// </summary>
        /// <param name="newWaypoint">Target waypoint</param>
        public void ChangeCurrWaypoint(Vector2 newWaypoint)
        {
            CurrWaypoint = newWaypoint;
        }

        /// <summary>
        /// Used to change the customers position from outside to inside. Its called when there is room inside the store
        /// </summary>
        /// <param name="newPositionInLine"> Specifies the new position in the in store line </param>
        /// <param name="frontOfLineVector"> Specifies the location of the front of the in store line  </param>
        public void GoInside(int newPositionInLine, Vector2 frontOfLineVector)
        {
            // Updates the new position
            PositionInLine = newPositionInLine;

            // Updates the current locaiton in the line by offsetting the position from the front of the line by their poition in the line
            CurrWaypoint = new Vector2(frontOfLineVector.X - ((PositionInLine - 1) * CUSTOMER_DISTANCE), frontOfLineVector.Y);

            // Updates Customer State enum
            CurrentState = CustomerState.InLine;
        }

        /// <summary>
        /// Runs when the customers position needs to me moved up in the line
        /// </summary>
        /// <param name="frontVector"> Specifies the front vector of the cusotmers current line </param>
        public void Advance(Vector2 frontVector)
        {
            // Decrements position in the line
            PositionInLine--;

            // Updates CurrWaypoint vector based on their location in the simulation
            switch (CurrentState)
            {
                case CustomerState.InLine:
                    CurrWaypoint = new Vector2(frontVector.X - ((PositionInLine - 1) * 50), frontVector.Y);
                    break;

                case CustomerState.Outside:
                    CurrWaypoint = new Vector2(frontVector.X, (frontVector.Y + ((PositionInLine) * 50)));
                    break;
            }
        }

        /// <summary>
        /// Used to update the current customer state
        /// </summary>
        /// <param name="newCustomerState"> Passes through the new customer state </param>
        public void ChangeCustomerState(CustomerState newCustomerState)
        {
            CurrentState = newCustomerState;
        }
    }
}
