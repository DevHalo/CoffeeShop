// Author: Shawn Verma
// Class Name: Statistics.cs
// Date Created: Dec 5th 2015
// Date Modified: Dec 5th 2015
// Description: Keeps track of the statistics of the simulation such as, minimum wait time, max wait time, total wait time, 
//              average wait time, number of visits, and top 5 longest wait times

using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
namespace CoffeeShopSimulation
{
    class StatisticsModel
    {
        //Stores the minimum, maximum, total, and average wait time from all customers
        private float totalWaitTime;
        public float MinWaitTime { get; private set; }
        public float MaxWaitTime { get; private set; }
        public float AvgWaitTime { get; private set; }

        //Stores the number of CustomersServed
        public int CustomersServed { get; private set; }

        // 
        public CInfoContainer[] LongestWaitTimes { get; private set; }
        public List<CInfoContainer> CustomerInfo {get; private set;}
        // 
        public StatisticsView View { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public StatisticsModel()
        {
            CustomerInfo = new List<CInfoContainer>();

            View = new StatisticsView(this);
        }

        /// <summary>
        /// Updates the longest wait times according to the current customers in the store
        /// </summary>
        /// <param name="customers">stores the queue of customers</param>
        public void Update(Queue<CustomerModel> outsideLine, Queue<CustomerModel> insideLine, CustomerModel[] cashiers, List<CustomerModel> exitList)
        {
            CustomerInfo = ToCustomerInfo(outsideLine, insideLine, cashiers, exitList);

            //Perform the Merge Sort and store the result back in the original array
            if (CustomerInfo.Count > 0)
            {
                CustomerInfo = MergeSort(CustomerInfo.ToArray(), 0, CustomerInfo.Count - 1).ToList();
            }

            //InsertionSort(CustomerInfo);
        }
        private static void InsertionSort(List<CInfoContainer> customerInfo)
        {
            CInfoContainer temp;
            int sorted = 1;

            for (int i = 0; i < customerInfo.Count - 1; i++)
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
        /// This is step 1 of a Merge Sort.  This subprogram will check for an array of 
        /// 1 or 0 elements and return that as a sorted array, otherwise it will continue
        /// to divide the array in half, only to merge the halves back together later
        /// </summary>
        /// <param name="nums">The array to be sorted</param>
        /// <param name="left">A pointer to the starting index of the nums array to consider</param>
        /// <param name="right">A pointer to the starting index of the nums array to consider</param>
        /// <returns>A sorted array of integers or null if the array empty</returns>
        private CInfoContainer[] MergeSort(CInfoContainer[] cInfoContainer, int left, int right)
        {
            //Base Case 1: The array passed in was empty, return null
            //if (CInfoContainer.Count == 0)
            //{
            //    return new List<CInfoContainer>() {};
            //}
            //Base Case 2: The number of elements to be considerd is one, return that one element as
            //an array of 1 element for merging
            if (right - left < 1)
            {
                //Create a new array of 1 element
                return new [] { cInfoContainer[left] };
            }

            //Calculate the midpoint index of the range to be considered
            int mid = (left + right) / 2;

            //Merge the two halves being split, the base case will be two one-element arrays,
            //future cases will continue to build upon this, e.g. two two-element arrays next
            return Merge(MergeSort(cInfoContainer, left, mid), MergeSort(cInfoContainer, mid + 1, right));
        }

        /// <summary>
        /// Merge two arrays into a single sorted array of data
        /// </summary>
        /// <param name="left">A sorted array of integers to be merged</param>
        /// <param name="right">A second sorted array of integers to be merged</param>
        /// <returns>An array with size equivalent to the sum of the lengths of 
        /// the two given arrays that holds the merged sorted data of the two arrays</returns>
        private CInfoContainer[] Merge(CInfoContainer[] left, CInfoContainer[] right)
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
            CInfoContainer[] result = new CInfoContainer[left.Length + right.Length];

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

        public List<CInfoContainer> ToCustomerInfo(Queue<CustomerModel> outsideLine, Queue<CustomerModel> insideLine, CustomerModel[] cashiers, List<CustomerModel> exitList)
        {
            List<CInfoContainer> customerInfo = new List<CInfoContainer>();

            //Variable used to store the current customer node
            Node<CustomerModel> curCustomer = outsideLine.Peek();
            for (int i = 0; i < outsideLine.Size; i++)
            {
                customerInfo.Add(new CInfoContainer(curCustomer.Value.WaitTime, curCustomer.Value.CustomerName));
                curCustomer = curCustomer.Next;
            }

            curCustomer = insideLine.Peek();
            for (int i = 0; i < insideLine.Size; i++)
            {
                customerInfo.Add(new CInfoContainer(curCustomer.Value.WaitTime, curCustomer.Value.CustomerName));
                curCustomer = curCustomer.Next;
            }

            for (int i = 0; i < cashiers.Length; i++)
            {
                if (cashiers[i] != null)
                {
                    customerInfo.Add(new CInfoContainer(cashiers[i].WaitTime, cashiers[i].CustomerName));
                }
            }

            for (int i = 0; i < exitList.Count; i++)
            {
                customerInfo.Add(new CInfoContainer(exitList[i].WaitTime, exitList[i].CustomerName));
            
            }

            return customerInfo;
        }
        /// <summary>
        /// Calculates the average wait time, checks to change max and minimum wait times
        /// </summary>
        /// <param name="customer">stores the customer that exited the store</param>
        public void ProcessExitingCustomer(CustomerModel customer)
        {
            //add the customer wait time the the total wait time
            totalWaitTime += (customer.WaitTime);

            //Increment the number of CustomersServed
            CustomersServed++;

            //Calculate the average wait time
            AvgWaitTime = (totalWaitTime / CustomersServed);

            //if the current customer being checked has a greater wait time than the max wait time
            if (MaxWaitTime < customer.WaitTime)
            {
                //set the max wait time to the current customer wait time
                MaxWaitTime = customer.WaitTime;
            }

            //If minimum wait time is zero
            if (MinWaitTime <= 0)
            {
                //set the minimum wait time to the customer wait time
                MinWaitTime = customer.WaitTime;
            }
            // If minimum wait time is greater than the customer wait time
            else if (MinWaitTime > customer.WaitTime)
            {
                //Set minimum wait time to customer wait time
                MinWaitTime = customer.WaitTime;
            }
        }
    }

    class CInfoContainer
    {
        public string CustomerName { get; set; }
        public float CustomerWaitTime { get; set; }

        public CInfoContainer(float customerWaitTime, string customerName)
        {
            this.CustomerName = customerName;
            this.CustomerWaitTime = customerWaitTime;
        }
    }
}
