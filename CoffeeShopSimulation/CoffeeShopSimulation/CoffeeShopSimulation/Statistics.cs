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
        private float avgWaitTime;
        private float visits;
        private float[] longestWaitTimes = new float [4];

        public void Update(Queue<Customer> customers)
        {
            Node<Customer> curCustomer = customers.Peek();

            for(int i = 0; i < customers.Size; i++)
            {

            }
        }

        public void Draw(SpriteBatch sb)
        {

        }
    }
}
