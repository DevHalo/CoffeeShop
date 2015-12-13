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
using Microsoft.Xna.Framework.Graphics;

namespace CoffeeShopSimulation
{
    class CustomerModel
    {
        // Creates constants for the order times in seconds
        const float ORDER_TIME_COFFEE = 12.0f;
        const float ORDER_TIME_FOOD = 18.0f;
        const float ORDER_TIME_BOTH = 30.0f;
        // Constant used to store the customers movement speed
        const int MOVEMENT_SPEED = 10;

        // 
        public CustomerView View { get; private set; }

        // Bool is set to true to when the customer leaves the shop
        public bool IsDone { get; private set; }

        // Integer that stores the customers number
        public int CustomerNumber { get; private set; }

        // Used to store the customers name which will be displayed when the customer is drawn onto the screen
        public string CustomerName { get; private set; }

        // Stores the customers order time and is based on what they ordered
        public float OrderTime { get; private set; }

        // Used to record the amount of time that has passed since the customer was initialized
        public float WaitTime { get; private set; }

        // Stores the cusotmers position on the screen
        public Vector2 Position { get; private set; }

        // Stores the location on the current waypoint
        public Vector2 CurrWaypoint { get; private set; }

        // Stores the location of all the waypoints in the coffee shop
        public Queue<Vector2> Waypoints { get; private set; }

        // Stores the position of the customer in the line
        public int PositionInLine { get; private set; }

        // Used to store the angle and direction between the destination point and the customers current position
        private double angleToDestination;
        private Vector2 customerDirection;

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
        /// Used to store the current state of the customer
        /// </summary>
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
        /// <param name="customerType">
        /// Used to specify what type of customer to create
        /// </param>
        /// <param name="customerNumber">
        /// Used to store the customers current postion in the Queue
        /// </param>
        /// <param name="positionInLine">
        /// Used to store the customer's current position in the line
        /// </param>
        public CustomerModel(CustomerType customerType, int customerNumber, int positionInLine)
        {
            // Sets the class level variables values to the ones obtained from the constructor
            this.Type = customerType;
            this.CustomerNumber = customerNumber;
            PositionInLine = positionInLine;

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

            // Sets the initial state of the customer to outside the coffee shop
            CurrentState = CustomerState.Outside;

            // Set the default position
            Position = new Vector2(50, 1400);
            // Set up the first waypoint
            // Adjust their position in line if there are more than
            // 12 people trying to enter the store
            CurrWaypoint = new Vector2(50, 350 + (positionInLine * 50));

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

            if (Position != CurrWaypoint)
            {
                if (Vector2.Distance(Position, CurrWaypoint) < 20)
                {
                    Position += new Vector2(CurrWaypoint.X - Position.X, CurrWaypoint.Y - Position.Y);
                }

                if (CurrWaypoint.X - Position.X > 0)
                {
                    Position += new Vector2(MOVEMENT_SPEED, 0);
                }
                else if (CurrWaypoint.X - Position.X < 0)
                {
                    Position -= new Vector2(MOVEMENT_SPEED, 0);
                }

                if (CurrWaypoint.Y - Position.Y > 0)
                {
                    Position += new Vector2(0, MOVEMENT_SPEED);
                }
                else if (CurrWaypoint.Y - Position.Y < 0)
                {
                    Position -= new Vector2(0, MOVEMENT_SPEED);
                }
            }

            /*
            // If the customer is not at the waypoint it will calulate the angle towards it and move it towards the waypoint
            if (Position != CurrWaypoint)
            {
                // Calculates the angle and direction from the customer to the waypoint. angleToDestination is only 0 when
                // the angle hasnt been calulated or if the customer is at the waypoint
                if (angleToDestination == 0)
                {
                    angleToDestination = Math.Atan2((int)(CurrWaypoint.Y - Position.Y), (int)(CurrWaypoint.X - Position.X));

                    customerDirection = new Vector2((float)Math.Cos(angleToDestination), (float)Math.Sin(angleToDestination));
                }

                // Adds the customer direction to the customers position  in order to move it towards the waypoint
                Position += (customerDirection) * MOVEMENT_SPEED;

                // Sets the angle to zero when the customer is at the waypoint
                float distance = Vector2.Distance(Position, CurrWaypoint);
                if (distance < 0.5f)
                {
                    Position = CurrWaypoint;
                    angleToDestination = 0;
                }
            }
            */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newWaypoint"></param>
        public void ChangeCurrWaypoint(Vector2 newWaypoint)
        {
            CurrWaypoint = newWaypoint;
        }

        public void ChangePositionInLine(int newPositionInLine)
        {
            PositionInLine = newPositionInLine;
        }

        public void GoInside(int newPositionInLine)
        {
            PositionInLine = newPositionInLine;
            CurrWaypoint = new Vector2(700 - (newPositionInLine * 50), 400);
        }
    }
}
