// Author: Shawn Verma
// Class Name: Statistics.cs
// Date Created: Dec 5th 2015
// Date Modified: Dec 5th 2015
// Description: 

using Microsoft.Xna.Framework.Graphics;
namespace CoffeeShopSimulation
{
    class Statistics
    {
        private float minWaitTime;
        private float maxWaitTime;
        private float totalWaitTime;
        private float avgWaitTime;
        private float visits;
        
        private float[] longestWaitTimes = new float[5];

        public void Update(Queue<Customer> customers)
        {
            Node<Customer> customer = customers.Peek();

            for(int i = 0; i < customers.Size; i++)
            {


                customer = customer.GetNext();                
            }
        }

        /// <summary>
        /// Calculates the average wait time, checks to change max and minimum wait times
        /// </summary>
        /// <param name="customer">stores the customer that exited the store</param>
        public void ProcessExitingCustomer(Node<Customer> customer)
        {
            //add the customer wait time the the total wait time
            totalWaitTime += (float)(customer.Value.WaitTime);

            //Increment the number of visits
            visits++;

            //Calculate the average wait time
            avgWaitTime = (float)(totalWaitTime / visits);

            //if the current customer being checked has a greater wait time than the max wait time
            if (maxWaitTime < customer.Value.OrderTime)
            {
                //set the max wait time to the current customer wait time
                maxWaitTime = (float)customer.Value.WaitTime;
            }

            //If minimum wait time is zero
            if (minWaitTime == 0)
            {
                //set the minimum wait time to the customer wait time
                minWaitTime = (float)customer.Value.WaitTime;
            }
            // If minimum wait time is greater than the customer wait time
            else if (minWaitTime > customer.Value.WaitTime)
            {
                //Set minimum wait time to customer wait time
                minWaitTime = (float)customer.Value.WaitTime;
            }

            //Set the current customer to the next customer
            customer = customer.GetNext();
        }

        public void Draw(SpriteBatch sb)
        {

        }
    }
}
