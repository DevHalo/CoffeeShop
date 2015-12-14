// Author: Sanjay Paraboo
// File Name: CustomerView.cs
// Project Name: A5 Data Manipulation Assignment
// Creation Date: Dec 5th, 2015
// Modified Date: Dec 14th, 2015
// Description: Used to handle draw data for each customer model instance

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CoffeeShopSimulation
{
    class CustomerView
    {
        // Specifies
        private const int CUSTOMER_SIZE = 20;

        // Stores an instance of Customer Model
        CustomerModel customerModel;

        /// <summary>
        /// Initializes the view class and stores the model its associated with
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
                new Rectangle((int)customerModel.Position.X - 10, (int)customerModel.Position.Y - 10, 20, 20), Color.Red);

            // Draws the customer name above the customer image
            sb.DrawString(labelFont,
                          customerModel.CustomerName,
                          new Vector2((int)(customerModel.Position.X - labelFont.MeasureString(customerModel.CustomerName).X *0.5f),
                                      customerModel.Position.Y - 25),
                          Color.Blue);

            
            sb.Draw(customerImg,                                            
                    new Rectangle((int)customerModel.Position.X - 10,           
                                  (int)customerModel.Position.Y - 10,           
                                  (int)(customerModel.PercentageFinished * 20f),
                              20),                                          
                    Color.Green);
        }
    }
}
