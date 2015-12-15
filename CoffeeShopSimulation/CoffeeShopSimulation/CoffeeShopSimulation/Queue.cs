﻿// Author: Shawn Verma
// File Name: Queue.cs
// Class Name: Queue, Node
// Project Name: A5 Data Manipulation Assignment
// Creation Date: Dec 5, 2015
// Modified Date: Dec 13th, 2015
// Description: Implementation of queues. Generics allow for multiple data types
namespace CoffeeShopSimulation
{
    class Queue<T>
    {
        // Holds the head node of the linked list
        private Node<T> head;

        // Stores the size of the queue
        public int Size { get; private set; }

        /// <summary>
        /// Creates a queue in the form of a linked list
        /// </summary>
        public Queue()
        {
            // Set size to zero
            Size = 0;
        }

        /// <summary>
        /// Add item to back of queue(tail)
        /// </summary>
        public void Enqueue(Node<T> newNode)
        {
            // Tracks the current node that is being checked
            Node<T> curNode = head;

            // If the size of the queue is zero add the new node to the head, otherwise add the new node to the end
            switch (Size)
            {
                case 0:
                    // Set the head as the new node
                    head = newNode;
                    break;

                default:

                    // Loops through the linked list until it reaches the tail
                    while (curNode.Next != null)
                    {
                        curNode = curNode.Next;
                    }

                    // Adds in the new node as the tail
                    curNode.SetNext(newNode);
                    break;
            }

            // Increment the size of the queue
            Size++;
        }

        /// <summary>
        /// Remove and return the front item of the queue
        /// </summary>
        /// <returns>the head before the head is removed</returns>
        public Node<T> Dequeue()
        {
            // Temporary holds the current head
            Node<T> tempHead = head;

            // Set the head to the next node in the linked list
            head = tempHead.Next;

            // Decrement the size of the queue
            Size--;

            // Returns the temporary head
            return tempHead;
        }

        /// <summary>
        /// Return the front item of the queue
        /// </summary>
        /// <returns>the head node</returns>
        public Node<T> Peek()
        {
            // Returns the head of the queue
            return head;
        }

        /// <summary>
        /// Returns true if the Size of the queue is zero, false otherwise
        /// </summary>
        /// <returns>whether or not the queue is empty</returns>
        public bool IsEmpty()
        {
            // Returns true or false if the size of the queue is zero
            return (Size <= 0);
        }
    }

    class Node<T>
    {
        // Stores the value
        public T Value { get; private set; }

        /// <summary>
        /// Creates a node with the specified type
        /// </summary>
        /// <param name="value">the cargo of the node</param>
        public Node(T value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets the next node 
        /// </summary>
        /// <returns>next node</returns>
        public Node<T> Next { get; private set; }

        /// <summary>
        /// Sets the next node
        /// </summary>
        /// <param name="node">The node you are adding to be next node</param>
        public void SetNext(Node<T> node)
        {
            // Set the next node
            Next = node;
        }
    }
}
