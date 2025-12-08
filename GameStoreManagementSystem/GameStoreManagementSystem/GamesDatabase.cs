/*
* FILE          : GamesDatabase.cs
* PROJECT       : PROG2111 Project
* PROGRAMMER    : Eumee Garcia, Vanesa Robledo
* FIRST VERSION : 2025-12-08
* DESCRIPTION   : This is an application to manage the database for managing the data required for a game store.
*                 This contains a repository of datasets with data from the databases
*/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace GameStoreManagementSystem
{
    internal class GamesDatabase
    {
        // Properties

        /// <summary>
        /// Connection to the games database
        /// </summary>
        internal DatabaseConnection Connection;

        /// <summary>
        /// Datasets for each table in the games database
        /// </summary>
        internal DataSet Games;
        internal DataSet Console;
        internal DataSet Inventory;
        internal DataSet Product;
        internal DataSet Customer;
        internal DataSet Store;
        internal DataSet Employee;

        // Constructor
        public GamesDatabase()
        {
            // Connect to the database
            Connection = new DatabaseConnection();

            // Fill datasets
            try
            {
                Games = Connection.LoadData("Game");
                Console = Connection.LoadData("Console");
                Inventory = Connection.LoadData("Inventory");
                Product = Connection.LoadData("Product");
                Customer = Connection.LoadData("Customer");
                Store = Connection.LoadData("Store");
                Employee = Connection.LoadData("Employee");
            }
            catch (Exception e)
            {
                MessageBox.Show("Error loading data from database: " + e.ToString());
            }
        }
    }
}
