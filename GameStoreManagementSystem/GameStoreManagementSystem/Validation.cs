/*
* FILE          : Validation.cs
* PROJECT       : PROG2111 Project
* PROGRAMMER    : Vanesa Robledo, Eumee Garcia
* FIRST VERSION : 2025-12-08
* DESCRIPTION   : This is an application to manage the database for managing the data required for a game store.
*                 This contains helper validation functions
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace GameStoreManagementSystem
{
    internal static class Validation
    {
        /// <summary>
        /// Checks if string is empty
        /// </summary>
        /// <param name="str">String to check</param>
        /// <returns><see langword="true" /> if string is empty; otherwise, <see langword="false" /></returns>
        internal static bool IsEmpty(string str)
        {
            return String.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Validates if a given input is a valid integer
        /// </summary>
        /// <param name="input">String to validate</param>
        /// <returns><see langword="true" /> if string is a number; otherwise, <see langword="false" /></returns>
        internal static bool IsNum(string input)
        {
            bool result = false;
            int num;
            if (Int32.TryParse(input, out num))
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Validates if a given input is a positive integer > 0
        /// </summary>
        /// <param name="input"></param>
        /// <returns><see langword="true" /> if input is valid int > 0; otherwise, <see langword="false" /></returns>
        internal static bool IsPositiveNum(string input)
        {
            Regex regexPositive = new Regex("[0-9]+");
            return regexPositive.IsMatch(input);
        }

        /// <summary>
        /// Validates if a given input is a positive float
        /// </summary>
        /// <param name="input"></param>
        /// <returns><see langword="true" /> if input is valid float > 0; otherwise, <see langword="false" /></returns>
        internal static bool IsFloat(string input)
        {
            Regex regexPositive = new Regex("[0-9.]+");
            return regexPositive.IsMatch(input);
        }

        /// <summary>
        /// Checks if given string is a valid Date
        /// </summary>
        /// <param name="dt">DateTime to check</param>
        /// <returns><see langword="true" /> if string is valid Date; otherwise, <see langword="false" /></returns>
        internal static bool IsValidDate(DateTime? dt)
        {
            return dt != null;
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

        /// <summary>
        /// Validates values in a row for the Inventory table
        /// </summary>
        /// <param name="db"></param>
        /// <param name="gameID"></param>
        /// <param name="consoleID"></param>
        /// <param name="quantity"></param>
        /// <param name="storeID"></param>
        /// <returns><see langword="true" /> if Inventory data is valid; otherwise, <see langword="false" /></returns>
        internal static bool ValidateInventoryValues(GamesDatabase db, int gameID, int consoleID, string quantity, int storeID)
        {
            bool valid = true;
            string errorMessage = "Error: ";
            // Check game OR console
            if (gameID != 0)
            {
                if (!db.GameIDExists(gameID))
                {
                    errorMessage += "Game not found.\n";
                    valid = false;
                }
            }
            if (consoleID != 0)
            {
                if (!db.ConsoleIDExists(consoleID))
                {
                    errorMessage += "Console not found.\n";
                    valid = false;
                }
            }
            // Check quantity
            if (IsEmpty(quantity) && ConvertInt(quantity) == -1)
            {
                errorMessage += "Invalid quantity: must be a valid integer.\n";
                valid = false;

            }
            // Check store ID
            if (!db.StoreIDExists(storeID))
            {
                errorMessage += "Store not found.\n";
                valid = false;
            }
            // Show error if invalid
            if (!valid)
            {
                MessageBox.Show(errorMessage, "Error Adding Inventory");
            }
            return valid;
        }

        /// <summary>
        /// Validates values for a row in Product table
        /// </summary>
        /// <param name="db"></param>
        /// <param name="inventoryID"></param>
        /// <param name="customerID"></param>
        /// <param name="cost"></param>
        /// <param name="quantity"></param>
        /// <param name="storeID"></param>
        /// <returns><see langword="true" /> if Product data is valid; otherwise, <see langword="false" /></returns>
        internal static bool ValidateProductValues(GamesDatabase db, int inventoryID, int customerID, string cost, string quantity, string dateOfPurchase, int storeID)
        {
            bool valid = true;
            string errorMessage = "Error: ";

            if (!db.InventoryIDExists(inventoryID))
            {
                errorMessage += "Inventory item not found.\n";
                valid = false;
            }
            if (!db.CustomerIDExists(customerID))
            {
                errorMessage += "Customer not found.\n";
                valid = false;
            }
            if (IsEmpty(cost) && ConvertCost(cost) == 0)
            {
                errorMessage += "Invalid cost. Cost must be greater than $0.00.\n";
                valid = false;
            }
            if (IsEmpty(quantity) && IsPositiveNum(quantity))
            {
                errorMessage += "Invalid quantity. Quantity must be 1 or greater.\n";
                valid = false;
            }
            if (IsEmpty(dateOfPurchase) && IsValidDate(DateTime.Parse(dateOfPurchase)))
            {
                errorMessage += "Invalid date of purchase.\n";
                valid = false;
            }
            if (!db.StoreIDExists(storeID))
            {
                errorMessage += "Store not found.\n";
                valid = false;
            }

            // Show error if invalid
            if (!valid)
            {
                MessageBox.Show(errorMessage, "Error Adding Product");
            }
            return valid;
        }
    }
}

