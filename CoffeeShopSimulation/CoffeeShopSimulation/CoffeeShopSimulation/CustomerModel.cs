// Author: Sanjay Paraboo
// File Name: CoffeeShopSimulation.sln
// Project Name: A5 Data Manipulation Assignment
// Creation Date: Dec 5, 2015
// Modified Date: Dec 13th, 2015
// Description:
using Microsoft.Xna.Framework;

namespace CoffeeShopSimulation
{
    class CustomerModel
    {
        // Creates constants for the order times in seconds
        const float ORDER_TIME_COFFEE = 0.5f;//12.0f;
        const float ORDER_TIME_FOOD = 1f;//18.0f;
        const float ORDER_TIME_BOTH = 2f;//30.0f;

        // Distance between each customer
        private const int CUSTOMER_DISTANCE = 50;

        // Constant used to store the customers movement speed
        const int MOVEMENT_SPEED = 10;

        // View class that implements the drawing function of the customer
        public CustomerView View { get; private set; }

        // Bool is set to true to when the customer leaves the shop
        public bool IsDone { get; private set; }

        // Integer that stores the customers number
        public int CustomerNumber { get; private set; }

        // Used to store the customers name which will be displayed when the customer is drawn onto the screen
        public string CustomerName { get; private set; }

        // Stores the customers order time and is based on what they ordered
        public float OrderTime { get; private set; }

        // Stores the customers time that has passed when they reached the cashier
        public float OrderProcessTime { get; private set; }

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
        public float PercentageFinished { get { return OrderProcessTime/OrderTime; } }

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
            CustomerNumber = customerNumber;
            PositionInLine = positionInLine;

            // Sets the customer name by converting the customer type enum to a string and adding the customer
            // number to the end
            CustomerName = Type + " " + CustomerNumber;
            
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
            Position = new Vector2(50, 780);
            // Set up the first waypoint
            // Adjust their position in line if there are more than
            // 12 people trying to enter the store
            CurrWaypoint = new Vector2(doorVector.X, doorVector.Y + ((positionInLine - 1) * CUSTOMER_DISTANCE));

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
                if (Vector2.Distance(Position, CurrWaypoint) > MOVEMENT_SPEED)
                {
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
                else
                {
                    Position += new Vector2(CurrWaypoint.X - Position.X, CurrWaypoint.Y - Position.Y);
                }

            }

            if (CurrentState == CustomerState.AtCashier)
            {
                OrderProcessTime += gameTimeInMilliSeconds;

                if (OrderProcessTime >= OrderTime)
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

        public void GoInside(int newPositionInLine, Vector2 frontOfLineVector)
        {
            PositionInLine = newPositionInLine;
            CurrWaypoint = new Vector2(frontOfLineVector.X - ((newPositionInLine - 1) * CUSTOMER_DISTANCE), frontOfLineVector.Y);
            CurrentState = CustomerState.InLine;
        }

        /// <summary>
        /// Advances the customer up one space using the location of the
        /// front of the line as a reference
        /// </summary>
        /// <param name="frontVector">Coordinate at the front of the line</param>
        public void Advance(Vector2 frontVector)
        {
            PositionInLine--;

            if (CurrentState == CustomerState.InLine)
            {
                CurrWaypoint = new Vector2(frontVector.X - ((PositionInLine - 1) * CUSTOMER_DISTANCE), frontVector.Y);
            }
            else if (CurrentState == CustomerState.Outside)
            {
                CurrWaypoint = new Vector2(frontVector.X, (frontVector.Y + (PositionInLine * CUSTOMER_DISTANCE)));
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
