// Author: Shawn Verma
// Class Name: Statistics.cs
// Date Created: Dec 5th 2015
// Date Modified: Dec 5th 2015
// Description: Keeps track of the statistics of the simulation such as, minimum wait time, max wait time, total wait time, 
//              average wait time, number of visits, and top 5 longest wait times

using Microsoft.Xna.Framework.Graphics;
using System;
namespace CoffeeShopSimulation
{
    class StatisticsModel
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
        private string[] longestWaitCustomer = new string[5];

        /// <summary>
        /// Updates the longest wait times according to the current customers in the store
        /// </summary>
        /// <param name="customers">stores the queue of customers</param>
        public void Update(Queue<CustomerModel> customers)
        {
            //Variable used to store the current customer node
            Node<CustomerModel> curCustomer = customers.Peek();

            string[] data = ToStringArray(customers);
            string[] tempData;
            
            double[] customerWaitTime = new double[data.Length];
            string[] customerName = new string[data.Length];
            CustomerInfo[] customerS = new CustomerInfo[customers.Size];

            for (int i = 0; i < customers.Size; i++)
            {
                customerS[i] = new CustomerInfo(curCustomer.Value.WaitTime, curCustomer.Value.CustomerName);
                curCustomer = curCustomer.GetNext();
            }




            for (int i = 0; i < data.Length; i++)
            {
                tempData = (data[i].Split(','));
                customerWaitTime[i] = Convert.ToDouble(tempData[0]);
                customerName[i] = tempData[1];

            }


            //MergeSort(customerS);



            //Goes through every customer            
            for (int i = 0; i < data.Length; i++)
            {
                if (customerWaitTime[i] > longestWaitTimes[longestWaitTimes.Length - 1])
                {
                    longestWaitTimes[longestWaitTimes.Length - 1] = (float)customerWaitTime[i];
                    longestWaitCustomer[longestWaitTimes.Length - 1] = customerName[i];
                    BubbleSort();
                }
            }
            
            //Goes through every customer
            for(int i = 0; i < customers.Size; i++)
            {
                //If the the current customer has waited longer than the shortest wait time from the top 5 longest wait times
                if (curCustomer.Value.WaitTime > longestWaitTimes[longestWaitTimes.Length - 1])
                {
                    //Set the shortest wait time from the longest wait time to the current customer wait time
                    longestWaitTimes[longestWaitTimes.Length - 1] = curCustomer.Value.WaitTime;

                    //Sort the top 5 longest wait times
                    BubbleSort();
                }

                //Set the current customer to the next customer
                curCustomer = curCustomer.GetNext();                
            }
        }

        /// <summary>
        /// Sorts the longest wait times array using bubble sort method
        /// </summary>
        private void BubbleSort()
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

        private void MergeSort()
        {

        }

        /// <summary>
        /// Calculates the average wait time, checks to change max and minimum wait times
        /// </summary>
        /// <param name="customer">stores the customer that exited the store</param>
        public void ProcessExitingCustomer(Node<CustomerModel> customer)
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

        /// <summary>
        /// Saves each customer's wait time and name in an array that is comma deliminated
        /// </summary>
        /// <param name="customers"></param>
        /// <returns>comma deliminated array that holds the customer wait time and name</returns>
        public string[] ToStringArray(Queue<CustomerModel> customers)
        {
            //Stores the data of the customer
            string[] data = new string[customers.Size];

            //Variable used to store the current customer node
            Node<CustomerModel> curCustomer = customers.Peek();

            //Itterates through each customer
            for (int i = 0; i < customers.Size; i++)
            {
                //Saves the customer data with a comma inbetween the wait time and the customer name
                data[i] = curCustomer.Value.WaitTime.ToString() + "," + curCustomer.Value.CustomerName;

                //Set the current customer to the next customer
                curCustomer = curCustomer.GetNext();
            }

            //Return the data of all the customers
            return data;
        }
    }

    class CustomerInfo
    {
        public string CustomerName { get; private set; }
        public float CustomerWaitTime { get; private set; }

        public CustomerInfo(float customerWaitTime, string customerName )
        {
            this.CustomerName = customerName;
            this.CustomerWaitTime = customerWaitTime;
        
        }
    }
}
