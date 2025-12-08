/*
* FILE          : Validation.cs
* PROJECT       : PROG2111 Project
* PROGRAMMER    : Vanesa Robledo
* FIRST VERSION : 2025-12-08
* DESCRIPTION   : This is an application to manage the database for managing the data required for a game store.
*                 This contains helper validation functions
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStoreManagementSystem
{
    internal static class Validation
    {
        /// <summary>
        /// Checks if string is a valid, positive integer
        /// </summary>
        /// <param name="ID">ID string to check</param>
        /// <returns><see langword="true" /> if ID is valid integer; otherwise, <see langword="false" /></returns>
        internal static bool IsValidID(string ID)
        {
            bool valid = false;
            int IDnum;
            if (Int32.TryParse(ID, out IDnum))
            {
                if (IDnum >= 0)
                {
                    valid = true;
                }
            }
            return valid;
        }

        /// <summary>
        /// Converts a string containing a number to an integer
        /// </summary>
        /// <param name="num">Number to convert</param>
        /// <returns>int; -1 if not an int</returns>
        internal static int ConvertInt(string num)
        {
            int number;
            if (!Int32.TryParse(num, out number))
            {
                number = -1;
            }
            return number;
        }

        /// <summary>
        /// Converts a string containing cost to a float
        /// </summary>
        /// <param name="cost">Cost to convert</param>
        /// <returns>float containing price; 0 if price is invalid</returns>
        internal static float ConvertCost(string cost)
        {
            float price = 0;
            if (float.TryParse(cost, out price))
            {
                if (price <= 0)
                {
                    price = 0;
                }
            }
            return price;
        }
    }
}
