/*
* FILE          : DatabaseConnection.cs
* PROJECT       : PROG2111 Project
* PROGRAMMER    : Eumee Garcia, Vanesa Robledo
* FIRST VERSION : 2025-12-08
* DESCRIPTION   : This is an application to manage the database for managing the data required for a game store.
*                 This contains the main class to connect to the MySQL Database.
*/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace GameStoreManagementSystem
{
    internal class DatabaseConnection
    {
        // Properties
        /// <summary>
        /// Password for connecting to MySql database
        /// </summary>
        private string password = "";

        /// <summary>
        /// Connection string to connect to database
        /// </summary>
        private string connectionString = "";

        /// <summary>
        /// DataAdapter to connect to MySql database
        /// </summary>
        private MySqlDataAdapter adapter;

        // Constructor
        public DatabaseConnection()
        {
            // Fill in the password required for the connection string
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["password"]))
            {
                password = ConfigurationManager.AppSettings["password"];
            }
            connectionString = "Server=localhost;Port=3306;Uid=root;Pwd=" + password + ";Database=games;";

            // Start the adapter
            adapter = new MySqlDataAdapter();
        }

        // Functions

        /// <summary>
        /// Loads the data of a table from the games database to a dataset
        /// </summary>
        /// <param name="ds">Dataset to load data into</param>
        /// <param name="table">Name of the table to load</param>
        internal void LoadData(DataSet ds, string table)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM " + table;
                    adapter = new MySqlDataAdapter(query, conn);
                    ds = new DataSet();
                    adapter.Fill(ds, table);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Updates the data from a dataset to a table in the games database
        /// </summary>
        /// <param name="ds">Dataset with updated data</param>
        /// <param name="table">Name of table to update</param>
        internal void SaveData(DataSet ds, string table)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM " + table;
                    adapter = new MySqlDataAdapter(query, conn);
                    MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
                    adapter.Update(ds, table);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
