// Author: Shawn Verma
// Class Names: Queue, Node
// Date Created: Dec 5th 2015
// Date Modified: Dec 5th 2015
// Description: Implementation of queues. Generics allow for multiple data types

namespace CoffeeShopSimulation
{
    class Queue<T>
    {
        int hello = 21;


        private void Enqueue()
        {

        }
    }

    class Node<T>
    {
        public T Value { get; private set; }
        public int Index { get; private set; }
        public Node<T> Next { get; private set; } 
    }
}
