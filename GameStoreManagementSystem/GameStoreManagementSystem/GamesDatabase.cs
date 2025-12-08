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
        internal DataSet Game;
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
                Game = Connection.LoadData("Game");
                Console = Connection.LoadData("Console");
                Inventory = Connection.LoadData("Inventory");
                Product = Connection.LoadData("Product");
                Customer = Connection.LoadData("Customer");
                Store = Connection.LoadData("Store");
                Employee = Connection.LoadData("Employee");
            }
            catch (Exception e)
            {
                MessageBox.Show("Error loading data from database: " + e);
            }
        }

        // Functions

        // Check if ID Exists (needed for foreign key constraints)

        /// <summary>
        /// Checks if Game ID exists in dataset
        /// </summary>
        /// <param name="id">ID to check</param>
        /// <returns><see langword="true" /> if ID exists; otherwise, <see langword="false" /></returns>
        internal bool GameIDExists(string id)
        {
            bool exists = false;
            foreach (DataRow row in Game.Tables["Game"].Rows)
            {
                if (row["game_id"] == id)
                {
                    exists = true;
                }
            }
            return exists;
        }

        /// <summary>
        /// Checks if Console ID exists in dataset
        /// </summary>
        /// <param name="id">ID to check</param>
        /// <returns><see langword="true" /> if ID exists; otherwise, <see langword="false" /></returns>
        internal bool ConsoleIDExists(string id)
        {
            bool exists = false;
            foreach (DataRow row in Console.Tables["Console"].Rows)
            {
                if (row["console_id"] == id)
                {
                    exists = true;
                }
            }
            return exists;
        }

        /// <summary>
        /// Checks if Inventory ID exists in dataset
        /// </summary>
        /// <param name="id">ID to check</param>
        /// <returns><see langword="true" /> if ID exists; otherwise, <see langword="false" /></returns>
        internal bool InventoryIDExists(string id)
        {
            bool exists = false;
            foreach (DataRow row in Inventory.Tables["Inventory"].Rows)
            {
                if (row["inventory_id"] == id)
                {
                    exists = true;
                }
            }
            return exists;
        }

        /// <summary>
        /// Checks if Store ID exists in dataset
        /// </summary>
        /// <param name="id">ID to check</param>
        /// <returns><see langword="true" /> if ID exists; otherwise, <see langword="false" /></returns>
        internal bool StoreIDExists(string id)
        {
            bool exists = false;
            foreach (DataRow row in Store.Tables["Store"].Rows)
            {
                if (row["store_id"] == id)
                {
                    exists = true;
                }
            }
            return exists;
        }

        /// <summary>
        /// Checks if Customer ID exists in dataset
        /// </summary>
        /// <param name="id">ID to check</param>
        /// <returns><see langword="true" /> if ID exists; otherwise, <see langword="false" /></returns>
        internal bool CustomerIDExists(string id)
        {
            bool exists = false;
            foreach (DataRow row in Customer.Tables["Customer"].Rows)
            {
                if (row["customer_id"] == id)
                {
                    exists = true;
                }
            }
            return exists;
        }

    }
}
