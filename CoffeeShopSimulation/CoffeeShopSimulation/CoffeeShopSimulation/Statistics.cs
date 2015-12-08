// Author: Shawn Verma
// Class Name: Statistics.cs
// Date Created: Dec 5th 2015
// Date Modified: Dec 5th 2015
// Description: Keeps track of the statistics of the simulation such as, minimum wait time, max wait time, total wait time, 
//              average wait time, number of visits, and top 5 longest wait times

using Microsoft.Xna.Framework.Graphics;
namespace CoffeeShopSimulation
{
    class Statistics
    {
        //Stores the minimum, maximum, total, and average wait time from all customers
        private float minWaitTime;
        private float maxWaitTime;
        private float totalWaitTime;
        private float avgWaitTime;

        //Stores the number of visits
        private float visits;
        
        //Stores the top 5 longest wait time for the customers still waiting
        private float[] longestWaitTimes = new float[5];

        /// <summary>
        /// Updates the longest wait times according to the current customers in the store
        /// </summary>
        /// <param name="customers">stores the queue of customers</param>
        public void Update(Queue<Customer> customers)
        {
            //Variable used to store the current customer node
            Node<Customer> curCustomer = customers.Peek();

            //Goes throught every customer
            for(int i = 0; i < customers.Size; i++)
            {
                //If the the current customer has waited longer than the shortest wait time from the top 5 longest wait times
                if (curCustomer.Value.WaitTime > longestWaitTimes[longestWaitTimes.Length - 1])
                {
                    //Set the shortest wait time from the longest wait time to the current customer wait time
                    longestWaitTimes[longestWaitTimes.Length - 1] = curCustomer.Value.WaitTime;

                    //Sort the top 5 longest wait times
                    Sort();
                }

                //Set the current customer to the next customer
                curCustomer = curCustomer.GetNext();                
            }
        }

        /// <summary>
        /// Sorts the longest wait times array using bubble sort method
        /// </summary>
        private void Sort()
        {
            //Holds temp data of the next index of the array
            float temp;

            //Goes through every index of the longest wait time array
            for (int i = longestWaitTimes.Length - 1; i > 0; i++)
            {
                //
                if (longestWaitTimes[i] > longestWaitTimes[i - 1])
                {
                    //Set the temporary variable to the longest wait time being replaced
                    temp = longestWaitTimes[i - 1];

                    //Replace
                    longestWaitTimes[i - 1] = longestWaitTimes[i];

                    //Set
                    longestWaitTimes[i] = temp;
                }
            }
        }

        /// <summary>
        /// Calculates the average wait time, checks to change max and minimum wait times
        /// </summary>
        /// <param name="customer">stores the customer that exited the store</param>
        public void ProcessExitingCustomer(Node<Customer> customer)
        {
            //add the customer wait time the the total wait time
            totalWaitTime += (customer.Value.WaitTime);

            //Increment the number of visits
            visits++;

            //Calculate the average wait time
            avgWaitTime = (totalWaitTime / visits);

            //if the current customer being checked has a greater wait time than the max wait time
            if (maxWaitTime < customer.Value.OrderTime)
            {
                //set the max wait time to the current customer wait time
                maxWaitTime = customer.Value.WaitTime;
            }

            //If minimum wait time is zero
            if (minWaitTime <= 0)
            {
                //set the minimum wait time to the customer wait time
                minWaitTime = customer.Value.WaitTime;
            }
            // If minimum wait time is greater than the customer wait time
            else if (minWaitTime > customer.Value.WaitTime)
            {
                //Set minimum wait time to customer wait time
                minWaitTime = customer.Value.WaitTime;
            }
        }

        public void Draw(SpriteBatch sb)
        {

        }
    }
}
