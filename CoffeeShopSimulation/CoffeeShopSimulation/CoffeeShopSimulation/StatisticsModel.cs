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
        private float totalWaitTime;
        public float MinWaitTime { get; private set; }
        public float MaxWaitTime { get; private set; }
        public float AvgWaitTime { get; private set; }

        //Stores the number of visits
        private float visits;
        
        // 
        CustomerInfo[] LongestWaitTimes = new CustomerInfo[5];

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

            string[] customerName = new string[customers.Size];
            CustomerInfo[] customerInfo = new CustomerInfo[customers.Size];

            for (int i = 0; i < customers.Size; i++)
            {
                customerInfo[i] = new CustomerInfo(curCustomer.Value.WaitTime, curCustomer.Value.CustomerName);
                curCustomer = curCustomer.GetNext();
            }

            //Perform the Merge Sort and store the result back in the original array
            customerInfo = MergeSort(customerInfo, 0, customerInfo.Length - 1);

            InsertionSort(customerInfo);

            for (int i = 0; i < data.Length; i++)
            {
                tempData = (data[i].Split(','));
                customerWaitTime[i] = Convert.ToDouble(tempData[0]);
                customerName[i] = tempData[1];
            }
        }
        private static void InsertionSort(CustomerInfo[] customerInfo)
        {
            CustomerInfo temp;
            int sorted = 1;

            for (int i = 0; i < customerInfo.Length - 1; i++)
            {
                for (int j = sorted; j > 0; j--)
                {
                    if (customerInfo[j].CustomerWaitTime < customerInfo[j - 1].CustomerWaitTime)
                    {
                        temp = customerInfo[j];
                        customerInfo[j] = customerInfo[j - 1];
                        customerInfo[j - 1] = temp;
                    }
                }
                sorted++;
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

        /// <summary>
        /// This is step 1 of a Merge Sort.  This subprogram will check for an array of 
        /// 1 or 0 elements and return that as a sorted array, otherwise it will continue
        /// to divide the array in half, only to merge the halves back together later
        /// </summary>
        /// <param name="nums">The array to be sorted</param>
        /// <param name="left">A pointer to the starting index of the nums array to consider</param>
        /// <param name="right">A pointer to the starting index of the nums array to consider</param>
        /// <returns>A sorted array of integers or null if the array empty</returns>
        private CustomerInfo[] MergeSort(CustomerInfo[] customerInfo, int left, int right)
        {
            //Base Case 1: The array passed in was empty, return null
            if (customerInfo == null)
            {
                return null;
            }
            //Base Case 2: The number of elements to be considerd is one, return that one element as
            //an array of 1 element for merging
            else if (right - left < 1)
            {
                //Create a new array of 1 element
                return new CustomerInfo[] { customerInfo[left] };
            }

            //Calculate the midpoint index of the range to be considered
            int mid = (left + right) / 2;

            //Merge the two halves being split, the base case will be two one-element arrays,
            //future cases will continue to build upon this, e.g. two two-element arrays next
            return Merge(MergeSort(customerInfo, left, mid), MergeSort(customerInfo, mid + 1, right));
        }

        /// <summary>
        /// Merge two arrays into a single sorted array of data
        /// </summary>
        /// <param name="left">A sorted array of integers to be merged</param>
        /// <param name="right">A second sorted array of integers to be merged</param>
        /// <returns>An array with size equivalent to the sum of the lengths of 
        /// the two given arrays that holds the merged sorted data of the two arrays</returns>
        private CustomerInfo[] Merge(CustomerInfo[] left, CustomerInfo[] right)
        {
            //Base Case 0: the left array has no elements, return the right array automatically
            //Similarly for the right array
            if (left == null)
            {
                return right;
            }
            else if (right == null)
            {
                return left;
            }

            //Create a new array of size equal to the sum of the lengths of the two given arrays
            CustomerInfo[] result = new CustomerInfo[left.Length + right.Length];

            //integers pointing to the currently considered element of each given array
            int leftIndex = 0;
            int rightIndex = 0;

            //For each element in the merged array, get the next 
            //smallest element between the two given arrays
            for (int i = 0; i < result.Length; i++)
            {
                //If both index values are valid
                if (leftIndex < left.Length && rightIndex < right.Length)
                {
                    //Insert the next lowest value of the two given arrays 
                    //into the merged array and move the index pointer
                    if (left[leftIndex].CustomerWaitTime <= right[rightIndex].CustomerWaitTime)
                    {
                        result[i] = left[leftIndex];
                        leftIndex++;
                    }
                    else
                    {
                        result[i] = right[rightIndex];
                        rightIndex++;
                    }
                }
                //If the left array is all used up, use the next right array element
                else if (leftIndex == left.Length)
                {
                    result[i] = right[rightIndex];
                    rightIndex++;
                }
                //If the right array is all used up, use the next left array element
                else
                {
                    result[i] = left[leftIndex];
                    leftIndex++;
                }
            }

            //Return the merged and sorted array
            return result;
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
            AvgWaitTime = (totalWaitTime / visits);

            //if the current customer being checked has a greater wait time than the max wait time
            if (MaxWaitTime < customer.Value.OrderTime)
            {
                //set the max wait time to the current customer wait time
                MaxWaitTime = customer.Value.WaitTime;
            }

            //If minimum wait time is zero
            if (MinWaitTime <= 0)
            {
                //set the minimum wait time to the customer wait time
                MinWaitTime = customer.Value.WaitTime;
            }
            // If minimum wait time is greater than the customer wait time
            else if (MinWaitTime > customer.Value.WaitTime)
            {
                //Set minimum wait time to customer wait time
                MinWaitTime = customer.Value.WaitTime;
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
