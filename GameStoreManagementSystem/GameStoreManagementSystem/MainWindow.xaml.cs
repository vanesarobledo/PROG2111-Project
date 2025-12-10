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
        internal string activeTable = "";

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

        /// <summary>
        /// Reloads the DataGrid based on the activeTable
        /// </summary>
        internal void RefreshDataGrid()
        {
            switch (activeTable)
            {
                case "Game":
                    LoadDataGrid(gamesDatabase.Game);
                    break;
                case "Customer":
                    LoadDataGrid(gamesDatabase.Customer);
                    break;
                case "Store":
                    LoadDataGrid(gamesDatabase.Store);
                    break;
                case "Inventory":
                    LoadDataGrid(gamesDatabase.Inventory);
                    break;
                case "Product":
                    LoadDataGrid(gamesDatabase.Product);
                    break;
                case "Console":
                    LoadDataGrid(gamesDatabase.Console);
                    break;
                case "Employee":
                    LoadDataGrid(gamesDatabase.Employee);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Changes the active table and adds it to the list of tables that have been opened
        /// </summary>
        /// <param name="table"></param>
        internal void SwitchTable(string table)
        {
            activeTable = table;
        }

        // ============================================================
        //   BUTTON EVENTS 
        // ============================================================

        /// <summary>
        /// Selects the manage game option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManageGame_Click(object sender, RoutedEventArgs e)
        {
            RightButtonPanel.Visibility = Visibility.Collapsed;
            RightContentArea.Content = new Views.Games.ManageGames();
            SwitchTable("Game");
            LoadDataGrid(gamesDatabase.Game);

            FormatDateColumn();
        }

        /// <summary>
        /// Selects the manage customer option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManageCustomer_Click(object sender, RoutedEventArgs e)
        {
            RightButtonPanel.Visibility = Visibility.Collapsed;
            RightContentArea.Content = new Views.Customers.ManageCustomers();
            SwitchTable("Customer");
            LoadDataGrid(gamesDatabase.Customer);
        }

        /// <summary>
        /// Selects the manage console option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManageConsole_Click(object sender, RoutedEventArgs e)
        {
            RightButtonPanel.Visibility = Visibility.Collapsed;
            RightContentArea.Content = new Views.Console.ManageConsole();
            SwitchTable("Console");
            LoadDataGrid(gamesDatabase.Console);
        }

        /// <summary>
        /// Selects the manage product option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManageProduct_Click(object sender, RoutedEventArgs e)
        {
            RightButtonPanel.Visibility = Visibility.Collapsed;
            RightContentArea.Content = new Views.Products.ManageProducts();
            SwitchTable("Product");
            LoadDataGrid(gamesDatabase.Product);
        }

        /// <summary>
        /// Selects the manage inventory option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManageInventory_Click(object sender, RoutedEventArgs e)
        {
            RightButtonPanel.Visibility = Visibility.Collapsed;
            RightContentArea.Content = new Views.Inventory.ManageInventory();
            SwitchTable("Inventory");
            LoadDataGrid(gamesDatabase.Inventory);
        }

        /// <summary>
        /// Selects the manage employee option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManageEmployee_Click(object sender, RoutedEventArgs e)
        {
            RightButtonPanel.Visibility = Visibility.Collapsed;
            RightContentArea.Content = new Views.Employees.ManageEmployees();
            SwitchTable("Employee");
            LoadDataGrid(gamesDatabase.Employee);
        }

        /// <summary>
        /// Selects the manage store option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManageStore_Click(object sender, RoutedEventArgs e)
        {
            RightButtonPanel.Visibility = Visibility.Collapsed;
            RightContentArea.Content = new Views.Stores.ManageStores();
            SwitchTable("Store");
            LoadDataGrid(gamesDatabase.Store);
        }

        // ============================================================
        //   SAVE BUTTON
        // ============================================================

        /// <summary>
        /// allows data to be saved inside the MySQL database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Save data to database
                // Tables must be saved in specific order to verify foreign keys, starting with the
                // most relations (Product) to the least (Customer)
                gamesDatabase.Connection.SaveData(gamesDatabase.GamesDataSet, "Product");
                gamesDatabase.Connection.SaveData(gamesDatabase.GamesDataSet, "Inventory");
                gamesDatabase.Connection.SaveData(gamesDatabase.GamesDataSet, "Console");
                gamesDatabase.Connection.SaveData(gamesDatabase.GamesDataSet, "Game");
                gamesDatabase.Connection.SaveData(gamesDatabase.GamesDataSet, "Employee");
                gamesDatabase.Connection.SaveData(gamesDatabase.GamesDataSet, "Store");
                gamesDatabase.Connection.SaveData(gamesDatabase.GamesDataSet, "Customer");

                // Reload data
                gamesDatabase.FillGamesDataSet();
                RefreshDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not save to database: " + ex, "Error");
            }
        }

        // ============================================================
        //   MENU HANDLERS
        // ============================================================

        /// <summary>
        /// Closes the window when "Exit" is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Shows the about windows 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void About_Click(object sender, RoutedEventArgs e)
        {
            About aboutWindow = new About();
            aboutWindow.ShowDialog();
        }


        // ============================================================
        //   Formats
        // ============================================================
        /// <summary>
        /// Formats the date so that it wont take the time
        /// </summary>
        private void FormatDateColumn()
        {
            foreach (DataGridColumn column in MainGrid.Columns)
            {
                if (column.Header.ToString() == "releasedate" || column.Header.ToString() == "release_date")
                {
                    DataGridTextColumn textColumn = column as DataGridTextColumn;
                    if (textColumn != null)
                    {
                        textColumn.Binding = new Binding("release_date")
                        {
                            StringFormat = "yyyy-MM-dd"
                        };
                    }
                }
            }
        }
    }
}

