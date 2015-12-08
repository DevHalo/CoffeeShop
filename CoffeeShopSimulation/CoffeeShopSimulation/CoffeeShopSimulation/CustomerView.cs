// Author: Sanjay Paraboo
// File Name: CoffeeShopSimulation.sln
// Project Name: A5 Data Manipulation Assignment
// Creation Date: Dec 5, 2015
// Modified Date:
// Description:

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffeeShopSimulation
{
    class CustomerView
    {

        // Stores an instance of Customer Model
        CustomerModel customerModel;

        /// <summary>
        ///
        /// </summary>
        public CustomerView(CustomerModel customerModel)
        {
            this.customerModel = customerModel;
        }

        /// <summary>
        /// Draws the customer instance onto the screen
        /// </summary>
        /// <param name="sb">Passes through the SpriteBatch instance in order to use its draw commands</param>
        public void Draw(SpriteBatch sb, Texture2D customerImg, SpriteFont labelFont)
        {
            // Draws the customer image onto the screen
            sb.Draw(customerImg,
                customerModel.Postion,
                null,
                Color.White,
                0f,
                Vector2.Zero,
                0f,
                SpriteEffects.None,
                0);

            // Draws the customer name above the customer image
            sb.DrawString(labelFont,
                customerModel.CustomerName,
                new Vector2(customerModel.Postion.X - 50, customerModel.Postion.Y - 50),
                Color.White);
        }
    }
}
