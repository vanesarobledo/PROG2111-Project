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


namespace GameStoreManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Properties
        internal DatabaseConnection databaseConnection;
        internal GamesDatabase gamesDatabase;
        //To tell what table is being worked on.
        string activeTable = "";

        // Main Window
        public MainWindow()
        {
            InitializeComponent();

            // Initialize MySQL database connection
            databaseConnection = new DatabaseConnection();

            // Load data in games database
            gamesDatabase = new GamesDatabase();

            
        }

        // Functions

        /// <summary>
        /// Loads a dataset into the DataGrid
        /// </summary>
        /// <param name="ds">Dataset to load</param>
        internal void LoadDataGrid(DataTable dt)
        {
            if (dt != null)
            {
                testGrid.ItemsSource = dt.DefaultView;
            }
        }


        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            //Option Chosen
            if (chooseAction.SelectedItem != null)
            {
                //testGrid.DataContext = gamesDatabase.Game;
                switch (chooseAction.Text)
                {
                    case "Manage Games":
                        activeTable = "Game";
                        LoadDataGrid(gamesDatabase.Game);
                        break;
                    case "Manage Customers":
                        activeTable = "Customer";
                        LoadDataGrid(gamesDatabase.Customer);
                        break;
                    case "Manage Stores":
                        activeTable = "Store";
                        LoadDataGrid(gamesDatabase.Store);
                        break;
                    case "Manage Inventory":
                        activeTable = "Inventory";
                        LoadDataGrid(gamesDatabase.Inventory);
                        break;
                    case "Manage Products":
                        activeTable = "Product";
                        LoadDataGrid(gamesDatabase.Product);
                        break;
                    case "Manage Console":
                        activeTable = "Console";
                        LoadDataGrid(gamesDatabase.Console);
                        break;
                    case "Manage Employee":
                        activeTable = "Employee";
                        LoadDataGrid(gamesDatabase.Product);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                //NOTHING WAS CHOSEN
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (activeTable != "")
            {
                DataSet bufferSet = new DataSet();

                bufferSet.Tables.Add(gridToTable(testGrid));
                gamesDatabase.Connection.SaveData(bufferSet, activeTable);
            }
            else
            {
                //NO TABLE WAS OPENED
            }
        }

        private DataTable gridToTable(DataGrid inputGrid)
        {
            DataTable table = new DataTable();
            DataView data = (DataView)inputGrid.ItemsSource;
            table = data.ToTable();
            return table;
        }
    }
}
