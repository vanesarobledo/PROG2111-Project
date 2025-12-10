/*
* FILE          : ManageStores.xaml.cs
* PROJECT       : PROG2111 Project
* PROGRAMMER    : Connar Thompson
* FIRST VERSION : 2025-12-08
* DESCRIPTION   : This is an application contains the interaction logic for managing store 
*                 records in the Game Store Management System. It allows users 
*                 to add, update, and delete stores, and also handles navigation 
*                 back to the main interface.
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameStoreManagementSystem.Views.Stores
{
    /// <summary>
    /// Interaction logic for ManageStores.xaml
    /// </summary>
    public partial class ManageStores : UserControl
    {
        /// <summary>
        /// Select games database from Main window
        /// </summary>
        internal GamesDatabase db = ((MainWindow)Application.Current.MainWindow).gamesDatabase;

        /// <summary>
        /// Select DataGrid from Main menu
        /// </summary>
        internal DataGrid grid = ((MainWindow)Application.Current.MainWindow).MainGrid;

        public ManageStores()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Opens the Add Store form, allowing the user to create a new store record.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddStore_Click(object sender, RoutedEventArgs e)
        {
            Forms.ManageStoreForm.AddStore form = new Forms.ManageStoreForm.AddStore();
            form.ShowDialog();
        }

        /// <summary>
        /// Opens the Update Store form if a store is selected in the DataGrid.
        /// Displays an error message when no store is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateStore_Click(object sender, RoutedEventArgs e)
        {
            if (grid.SelectedIndex != -1)
            {
                Forms.ManageStoreForm.UpdateStoreForm form = new Forms.ManageStoreForm.UpdateStoreForm();
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Select a store to update.", "Error");
            }
        }

        /// <summary>
        /// Handles the deletion process for a selected store record.
        /// Checks if a row is selected, confirms with the user, and calls the delete 
        /// function if the user chooses "Yes". Displays an error when nothing is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteStore_Click(object sender, RoutedEventArgs e)
        {
            // Check for selected index
            if (grid.SelectedIndex != -1)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this store?", "Manage Store", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    ConfirmDelete();
                }
            }
            else
            {
                MessageBox.Show("Select a store to delete.", "Error");
            }
        }

        /// <summary>
        /// Goes back to the main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;

            if (mainWindow != null)
            {
                // Clear the UserControl area
                mainWindow.RightContentArea.Content = null;

                // Show the right-side main button panel again
                mainWindow.RightButtonPanel.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Deletes selected item
        /// </summary>
        private void ConfirmDelete()
        {
            // Get selected product ID
            DataRowView dv = ((DataRowView)grid.Items[grid.SelectedIndex]);
            if (dv != null)
            {
                int storeID = (int)dv.Row.ItemArray[0];

                // Find product row to delete
                bool found = false;
                for (int i = 0; i < db.Store.Rows.Count && !found; i++)
                {
                    DataRow currentRow = db.Store.Rows[i];
                    // If row is found
                    if (Convert.ToInt32(currentRow["store_id"]) == storeID)
                    {
                        // Mark row for deletion
                        currentRow.Delete();

                        // Delete row in table
                        db.Store.AcceptChanges();

                        // Show success
                        MessageBox.Show("Store ID #" + storeID.ToString() + " has been successfuly deleted.\nClick \"Save\" to save changes to database", "Store Deleted");
                        found = true;
                    }
                }
            }
        }
    }
}
