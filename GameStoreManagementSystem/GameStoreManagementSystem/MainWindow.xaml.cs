/*
* FILE          : MainWindow.xaml.cs
* PROJECT       : PROG2111 Project
* PROGRAMMER    : Eumee Garcia, Vanesa Robledo, Connar Thompson
* FIRST VERSION : 2025-12-08
* DESCRIPTION   : This is an application to manage the database for managing the data required for a game store.
*                 This contains the main window when the application is first loaded to perform CRUD operations on datasets.
*/

using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Configuration;
using Google.Protobuf.WellKnownTypes;
using System.Security.Cryptography.Xml;
using GameStoreManagementSystem.Views.Console;
using GameStoreManagementSystem.Views.Customers;
using GameStoreManagementSystem.Views.Employees;
using GameStoreManagementSystem.Views.Games;
using GameStoreManagementSystem.Views.Inventory;
using GameStoreManagementSystem.Views.Products;
using GameStoreManagementSystem.Views.Stores;


namespace GameStoreManagementSystem
{
    public partial class MainWindow : Window
    {
        // Properties

        /// <summary>
        /// Class that holds the DataSet & DataTables for the games database
        /// </summary>
        internal GamesDatabase gamesDatabase;

        /// <summary>
        /// Table currently being worked on
        /// </summary>
        string activeTable = "";

        // Main Window
        public MainWindow()
        {
            InitializeComponent();
            
            // Load data in games database
            gamesDatabase = new GamesDatabase();
        }


        // ============================================================
        //   DATA FUNCTIONS
        // ============================================================

        /// <summary>
        /// Loads a dataset into the DataGrid
        /// </summary>
        /// <param name="ds">Dataset to load</param>
        internal void LoadDataGrid(DataTable dt)
        {
            if (dt != null)
            {
                MainGrid.ItemsSource = dt.DefaultView;
            }
        }

        // ============================================================
        //   BUTTON EVENTS 
        // ============================================================

        private void ManageGame_Click(object sender, RoutedEventArgs e)
        {
            RightButtonPanel.Visibility = Visibility.Collapsed;
            RightContentArea.Content = new Views.Games.ManageGames();
            activeTable = "Game";
            LoadDataGrid(gamesDatabase.Game);
        }

        private void ManageCustomer_Click(object sender, RoutedEventArgs e)
        {
            RightButtonPanel.Visibility = Visibility.Collapsed;
            RightContentArea.Content = new Views.Customers.ManageCustomers();
            activeTable = "Customer";
            LoadDataGrid(gamesDatabase.Customer);
        }

        private void ManageConsole_Click(object sender, RoutedEventArgs e)
        {
            RightButtonPanel.Visibility = Visibility.Collapsed;
            RightContentArea.Content = new Views.Console.ManageConsole();
            activeTable = "Console";
            LoadDataGrid(gamesDatabase.Console);
        }

        private void ManageProduct_Click(object sender, RoutedEventArgs e)
        {
            RightButtonPanel.Visibility = Visibility.Collapsed;
            RightContentArea.Content = new Views.Products.ManageProducts();
            activeTable = "Product";
            LoadDataGrid(gamesDatabase.Product);
        }

        private void ManageInventory_Click(object sender, RoutedEventArgs e)
        {
            RightButtonPanel.Visibility = Visibility.Collapsed;
            RightContentArea.Content = new Views.Inventory.ManageInventory();
            activeTable = "Inventory";
            LoadDataGrid(gamesDatabase.Inventory);
        }

        private void ManageEmployee_Click(object sender, RoutedEventArgs e)
        {
            RightButtonPanel.Visibility = Visibility.Collapsed;
            RightContentArea.Content = new Views.Employees.ManageEmployees();
            activeTable = "Employee";
            LoadDataGrid(gamesDatabase.Employee);
        }

        private void ManageStore_Click(object sender, RoutedEventArgs e)
        {
            RightButtonPanel.Visibility = Visibility.Collapsed;
            RightContentArea.Content = new Views.Stores.ManageStores();
            activeTable = "Store";
            LoadDataGrid(gamesDatabase.Store);
        }

        // ============================================================
        //   SAVE BUTTON
        // ============================================================

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                gamesDatabase.Connection.SaveData(gamesDatabase.GamesDataSet, activeTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not save to " + activeTable + ": " + ex, "Error");
            }
        }

        // ============================================================
        //   MENU HANDLERS
        // ============================================================

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuFile_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void MenuHelp_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            //tbd
        }
    }
}

