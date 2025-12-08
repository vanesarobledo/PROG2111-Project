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
    }
}
