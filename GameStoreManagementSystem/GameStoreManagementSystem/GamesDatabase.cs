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
        /// DataSet to store games database for display
        /// </summary>
        internal DataSet GamesDataSet;

        /// <summary>
        /// DataTables to store tables from games database
        /// </summary>
        internal DataTable Game;
        internal DataTable Console;
        internal DataTable Inventory;
        internal DataTable Product;
        internal DataTable Customer;
        internal DataTable Store;
        internal DataTable Employee;

        // Constructor
        public GamesDatabase()
        {
            // Connect to the database
            Connection = new DatabaseConnection();

            try
            {
                // Fill games dataset
                GamesDataSet = new DataSet();
                Connection.LoadData(GamesDataSet, "Game");
                Connection.LoadData(GamesDataSet, "Console");
                Connection.LoadData(GamesDataSet, "Inventory");
                Connection.LoadData(GamesDataSet, "Product");
                Connection.LoadData(GamesDataSet, "Customer");
                Connection.LoadData(GamesDataSet, "Store");
                Connection.LoadData(GamesDataSet, "Employee");

                // Fill the DataTables
                Game = GamesDataSet.Tables["Game"];
                Console = GamesDataSet.Tables["Console"];
                Inventory = GamesDataSet.Tables["Inventory"];
                Product = GamesDataSet.Tables["Product"];
                Customer = GamesDataSet.Tables["Customer"];
                Store = GamesDataSet.Tables["Store"];
                Employee = GamesDataSet.Tables["Employee"];
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
            foreach (DataRow row in GamesDataSet.Tables["Game"].Rows)
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
            foreach (DataRow row in GamesDataSet.Tables["Console"].Rows)
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
            foreach (DataRow row in GamesDataSet.Tables["Inventory"].Rows)
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
            foreach (DataRow row in GamesDataSet.Tables["Store"].Rows)
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
            foreach (DataRow row in GamesDataSet.Tables["Customer"].Rows)
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
