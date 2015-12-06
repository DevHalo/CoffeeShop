using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffeeShopSimulation
{
    class CustomerView
    {
        CustomerModel customerModel;

        public CustomerView(CustomerModel customerModel)
        {
            this.customerModel = customerModel;
        }

        /// <summary>
        /// Used to draw the customer instance onto the screen
        /// </summary>
        /// <param name="sb">
        /// Used to pass through an instance of SpriteBatch in order to use its draw commands
        /// </param>
        public void Draw(SpriteBatch sb)
        {
        }
    }
}
