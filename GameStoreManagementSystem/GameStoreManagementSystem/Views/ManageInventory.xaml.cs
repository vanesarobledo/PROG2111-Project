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

namespace GameStoreManagementSystem.Views.Inventory
{
    /// <summary>
    /// Interaction logic for ManageInventory.xaml
    /// </summary>
    public partial class ManageInventory : UserControl
    {
        /// <summary>
        /// Select games database from Main window
        /// </summary>
        internal GamesDatabase db = ((MainWindow)Application.Current.MainWindow).gamesDatabase;

        /// <summary>
        /// Select DataGrid from Main menu
        /// </summary>
        internal DataGrid grid = ((MainWindow)Application.Current.MainWindow).MainGrid;

        public ManageInventory()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Checks if inventory database is loaded and moves out of the user control if not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (((MainWindow)Application.Current.MainWindow).gamesDatabase.Inventory == null)
            {
                MessageBox.Show("Inventory data not loaded.", "Error");
                Back_Click(sender, e);
            }
        }

        // ============================================================
        //   BUTTON FUNCTIONS
        // ============================================================

        private void AddInventory_Click(object sender, RoutedEventArgs e)
        {
            Forms.ManageInventoryForm.AddInventoryForm form = new Forms.ManageInventoryForm.AddInventoryForm();
            form.ShowDialog();
        }

        private void UpdateInventory_Click(object sender, RoutedEventArgs e)
        {
            if (grid.SelectedIndex != -1)
            {
                Forms.ManageInventoryForm.UpdateInventoryForm form = new Forms.ManageInventoryForm.UpdateInventoryForm();
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Select an inventory item to update.", "Error");
            }
        }

        private void DeleteInventory_Click(object sender, RoutedEventArgs e)
        {
            // Check for selected index
            if (grid.SelectedIndex != -1)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this inventory item?", "Manage Inventory", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    ConfirmDelete();
                }
            }
            else
            {
                MessageBox.Show("Select an inventory item to delete.", "Error");
            }
        }

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

        // ============================================================
        //   DELETE FUNCTION
        // ============================================================

        /// <summary>
        /// Deletes selected item
        /// </summary>
        private void ConfirmDelete()
        {
            // Get selected inventory ID
            DataRowView dv = ((DataRowView)grid.Items[grid.SelectedIndex]);
            int inventoryID = (int)dv.Row.ItemArray[0];

            // Find inventory row to delete
            bool found = false;
            for (int i = 0; i < db.Inventory.Rows.Count && !found; i++)
            {
                DataRow currentRow = db.Inventory.Rows[i];
                // If row is found
                if (Convert.ToInt32(currentRow["inventory_id"]) == inventoryID)
                {
                    // Mark row for deletion
                    currentRow.Delete();

                    // Delete row in table
                    db.Inventory.AcceptChanges();

                    // Show success
                    MessageBox.Show("Inventory item ID #" + inventoryID.ToString() + " has been successfuly deleted.\nClick \"Save\" to save changes to database", "Inventory Added");
                    found = true;
                }
            }
        }

    }
}
