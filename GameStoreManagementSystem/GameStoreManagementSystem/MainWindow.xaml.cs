/*
* FILE          : MainWindow.xaml.cs
* PROJECT       : PROG2111 Project
* PROGRAMMER    : Eumee Garcia, Vanesa Robledo
* FIRST VERSION : 2025-12-08
* DESCRIPTION   : This is an application to manage the database for managing the data required for a game store.
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


namespace GameStoreManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Main Window
        public MainWindow()
        {
            InitializeComponent();

            // Initialize games database
            GamesDatabase gamesDatabase = new GamesDatabase();
        }

        // Functions

        /// <summary>
        /// Loads a dataset into the DataGrid
        /// </summary>
        /// <param name="ds">Dataset to load</param>
        internal void LoadDataGrid(DataSet ds)
        {
            if (ds != null)
            {
                //TODO: Load dataset into the DataGrid
            }
        }

        private void OpenGames_Click(object sender, RoutedEventArgs e)
        {
            //GameView window = new GameView();
            //window.ShowDialog();
        }

        private void OpenCustomers_Click(object sender, RoutedEventArgs e)
        {
            // TODO: teammate implements CustomerView + CustomerRepository
            MessageBox.Show("Customer management coming soon!");
        }

        private void OpenStores_Click(object sender, RoutedEventArgs e)
        {
            // TODO: teammate implements StoreView + StoreRepository
            MessageBox.Show("Store management coming soon!");
        }

        private void OpenInventory_Click(object sender, RoutedEventArgs e)
        {
            // TODO: teammate implements InventoryView + InventoryRepository
            MessageBox.Show("Inventory management coming soon!");
        }

        private void OpenProducts_Click(object sender, RoutedEventArgs e)
        {
            // TODO: teammate implements ProductView + ProductRepository
            MessageBox.Show("Product management coming soon!");
        }
    }
}
