// Author: Shawn Verma
// Class Name: Statistics.cs
// Date Created: Dec 5th 2015
// Date Modified: Dec 5th 2015
// Description: Keeps track of the statistics of the simulation such as, minimum wait time, max wait time, total wait time, 
//              average wait time, number of visits, and top 5 longest wait times

using System.Linq;
using System.Collections.Generic;
namespace CoffeeShopSimulation
{
    class StatisticsModel
    {
        // Stores the minimum, maximum, total, and average wait time from all customers
        private float totalWaitTime;
        public float MinWaitTime { get; private set; }
        public float MaxWaitTime { get; private set; }
        public float AvgWaitTime { get; private set; }

        // Stores the number of CustomersServed
        public int CustomersServed { get; private set; }

        // Stores the info of all the customers
        public List<CInfoContainer> CustomerInfo {get; private set;}
        
        // Instance of the statistics view class
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
        /// Updates and sorts the customers' info according to the customer wait times
        /// </summary>
        /// <param name="outsideLine">The info of customers lined up outside the store</param>
        /// <param name="insideLine">The info of customers lined up inside the store</param>
        /// <param name="cashiers">The info of customers being served at the cashiers</param>
        /// <param name="exitList">The info of customers leaving the store</param>
        public void Update(Queue<CustomerModel> outsideLine, Queue<CustomerModel> insideLine, CustomerModel[] cashiers, List<CustomerModel> exitList)
        {
            // Set and combine the customer info of the outside line, the inside line, the people being served by cashiers and, customers leaving 
            CustomerInfo = CustomerInfoToList(outsideLine, insideLine, cashiers, exitList);

            // If the customer count is greater than 1
            if (CustomerInfo.Count > 1)
            {
                //Perform merge sort and store the result back in the original list
                CustomerInfo = MergeSort(CustomerInfo.ToArray(), 0, CustomerInfo.Count - 1).ToList();
            }
        }

        /// <summary>
        /// Adds all the customers' info to a single list
        /// </summary>
        /// <param name="outsideLine">The info of customers lined up outside the store</param>
        /// <param name="insideLine">The info of customers lined up inside the store</param>
        /// <param name="cashiers">The info of customers being served at the cashiers</param>
        /// <param name="exitList">The info of customers leaving the store</param>
        /// <returns>a list of all customers' info</returns>
        public List<CInfoContainer> CustomerInfoToList(Queue<CustomerModel> outsideLine, Queue<CustomerModel> insideLine, CustomerModel[] cashiers, List<CustomerModel> exitList)
        {
            // Stores the customer info
            List<CInfoContainer> customerInfo = new List<CInfoContainer>();

            // Variable used to store the current customer node which currently is storing the start of the outside line
            Node<CustomerModel> curCustomer = outsideLine.Peek();

            // For every customer in the outside line add them to the customer info list
            for (int i = 0; i < outsideLine.Size; i++)
            {
                // Add the customer in the outside line to the customer info list
                customerInfo.Add(new CInfoContainer(curCustomer.Value.WaitTime, curCustomer.Value.CustomerName));

                // Set the current customer to the next customer in the outside line
                curCustomer = curCustomer.Next;
            }

            // Set the current customer to the start of the inside line
            curCustomer = insideLine.Peek();

            // For every customer in the inside line add them to the customer info list
            for (int i = 0; i < insideLine.Size; i++)
            {
                // Add the customer in the inside line to the customer info list
                customerInfo.Add(new CInfoContainer(curCustomer.Value.WaitTime, curCustomer.Value.CustomerName));

                // Set the current customer to the next customer in the inside line
                curCustomer = curCustomer.Next;
            }

            // For every customer at the cashier 
            for (int i = 0; i < cashiers.Length; i++)
            {
                // If there is a customer at the cashier
                if (cashiers[i] != null)
                {
                    // Add the customer to the customer info list
                    customerInfo.Add(new CInfoContainer(cashiers[i].WaitTime, cashiers[i].CustomerName));
                }
            }

            // For every customer exiting
            for (int i = 0; i < exitList.Count; i++)
            {
                // Add the customer into the customer info list
                customerInfo.Add(new CInfoContainer(exitList[i].WaitTime, exitList[i].CustomerName));
            }

            // Return the list of customers' info
            return customerInfo;
        }

        /// <summary>
        /// Calculates the average wait time, checks to change max and minimum wait times
        /// </summary>
        /// <param name="exitingCustomer">stores the customer that exited the store</param>
        public void ProcessExitingCustomer(CustomerModel exitingCustomer)
        {
            // Add the exiting customer wait time the the total wait time
            totalWaitTime += (exitingCustomer.WaitTime);

            // Increment the number of CustomersServed
            CustomersServed++;

            // Calculate the average wait time
            AvgWaitTime = (totalWaitTime / CustomersServed);

            // If the current exiting customer being checked has a greater wait time than the max wait time
            if (MaxWaitTime < exitingCustomer.WaitTime)
            {
                // Set the max wait time to the current exiting customer wait time
                MaxWaitTime = exitingCustomer.WaitTime;
            }

            // If minimum wait time is zero
            if (MinWaitTime <= 0)
            {
                // Set the minimum wait time to the exitin customer wait time
                MinWaitTime = exitingCustomer.WaitTime;
            }
            // If minimum wait time is greater than the exiting customer wait time
            else if (MinWaitTime > exitingCustomer.WaitTime)
            {
                //Set minimum wait time to exiting customer wait time
                MinWaitTime = exitingCustomer.WaitTime;
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
                return new[] { cInfoContainer[left] };
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
    }

    class CInfoContainer
    {
        // Variables used to store the customer info
        public string CustomerName { get; set; }
        public float CustomerWaitTime { get; set; }

        /// <summary>
        /// Initializes each customer
        /// </summary>
        /// <param name="customerWaitTime">the wait time of customer until the customer leaves the store</param>
        /// <param name="customerName">the customer name</param>
        public CInfoContainer(float customerWaitTime, string customerName)
        {
            this.CustomerName = customerName;
            this.CustomerWaitTime = customerWaitTime;
        }
    }
}
