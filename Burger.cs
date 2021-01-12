using System;
using System.Collections.Generic;
using System.Text;

namespace BurgerApp
{
    class Burger
    {
        private BurgerType mFlavor;
        private BurgerType Flavor
        {
            get
            {
                return mFlavor;
            }
            set
            {
                mFlavor = value;
            }
        }
        private float mPrice;
        public float Price
        {
            get
            {
                return mPrice;
            }
            set
            {
                mPrice = value;
            }
        }
        public Burger(BurgerType aFlavor) 
        {
            mFlavor = aFlavor;
        }
    }
   
    public enum BurgerType
    {
        Susan,
        Simple,
        Meat,
        Cheese,
        Salad
    }

}
