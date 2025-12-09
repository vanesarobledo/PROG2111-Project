using System.Windows;
using System.Windows.Controls;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        // ============================================================
        //   BUTTON EVENTS 
        // ============================================================

        private void ManageGame_Click(object sender, RoutedEventArgs e)
        {
            RightButtonPanel.Visibility = Visibility.Collapsed;
            RightContentArea.Content = new Views.Games.ManageGames();
        }

        private void ManageCustomer_Click(object sender, RoutedEventArgs e)
        {
            RightButtonPanel.Visibility = Visibility.Collapsed;
            RightContentArea.Content = new Views.Customers.ManageCustomers();
        }

        private void ManageConsole_Click(object sender, RoutedEventArgs e)
        {
            RightButtonPanel.Visibility = Visibility.Collapsed;
            RightContentArea.Content = new Views.Console.ManageConsole();
        }

        private void ManageProduct_Click(object sender, RoutedEventArgs e)
        {
            RightButtonPanel.Visibility = Visibility.Collapsed;
            RightContentArea.Content = new Views.Products.ManageProducts();
        }

        private void ManageInventory_Click(object sender, RoutedEventArgs e)
        {
            RightButtonPanel.Visibility = Visibility.Collapsed;
            RightContentArea.Content = new Views.Inventory.ManageInventory();
        }

        private void ManageEmployee_Click(object sender, RoutedEventArgs e)
        {
            RightButtonPanel.Visibility = Visibility.Collapsed;
            RightContentArea.Content = new Views.Employees.ManageEmployees();
        }

        private void ManageStore_Click(object sender, RoutedEventArgs e)
        {
            RightButtonPanel.Visibility = Visibility.Collapsed;
            RightContentArea.Content = new Views.Stores.ManageStores();
        }

        // ============================================================
        //   SAVE BUTTON
        // ============================================================

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Save functionality will be connected to your database later.",
                            "Save", MessageBoxButton.OK, MessageBoxImage.Information);
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

