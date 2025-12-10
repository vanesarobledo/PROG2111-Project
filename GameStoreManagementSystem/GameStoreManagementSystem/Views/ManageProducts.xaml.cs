/*
* FILE          : ManageProduct.xaml.cs
* PROJECT       : PROG2111 Project
* PROGRAMMER    : Vanesa Robledo
* FIRST VERSION : 2025-12-08
* DESCRIPTION   : This is an application handles loading product-related data, 
*                 navigating back to the main menu, and providing functionality to add, update, 
*                 and delete product records. It also manages user interactions through the WPF 
*                 interface and communicates with the shared GamesDatabase and DataGrid.
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

namespace GameStoreManagementSystem.Views.Products
{
    /// <summary>
    /// Interaction logic for ManageProducts.xaml
    /// </summary>
    public partial class ManageProducts : UserControl
    {
        /// <summary>
        /// Select games database from Main window
        /// </summary>
        internal GamesDatabase db = ((MainWindow)Application.Current.MainWindow).gamesDatabase;

        /// <summary>
        /// Select DataGrid from Main menu
        /// </summary>
        internal DataGrid grid = ((MainWindow)Application.Current.MainWindow).MainGrid;

        public ManageProducts()
        {
            InitializeComponent();
        }

        // ============================================================
        //   LOAD FUNCTION
        // ============================================================

        /// <summary>
        /// Checks if necessary DataTables are loaded and exits if not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (db.Product == null)
            {
                MessageBox.Show("Product data not loaded.", "Error");
                Back_Click(sender, e);
            }
            // If other necessary datasets are not loaded, move out of user control
            else if (db.Game == null || db.Console == null || db.Inventory == null || db.Customer == null || db.Store == null)
            {
                MessageBox.Show("Data required for Product table not loaded.", "Error");
                Back_Click(sender, e);
            }
        }

        // ============================================================
        //   BUTTON FUNCTIONS
        // ============================================================

        /// <summary>
        /// Opens the Add Product form, allowing the user to create a new product entry.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            Forms.ManageProductsForm.AddProductForm form = new Forms.ManageProductsForm.AddProductForm();
            form.ShowDialog();

        }

        /// <summary>
        /// Opens the Update Product form for the currently selected product.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            if (grid.SelectedIndex != -1)
            {
                Forms.ManageProductsForm.UpdateProductForm form = new Forms.ManageProductsForm.UpdateProductForm();
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Select a product item to update.", "Error");
            }
        }

        /// <summary>
        /// Handles the delete button click event by checking if a product is 
        /// selected, confirming the deletion with the user, and then calling 
        /// the delete function if confirmed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            // Check for selected index
            if (grid.SelectedIndex != -1)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this product?", "Manage Product", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    ConfirmDelete();
                }
            }
            else
            {
                MessageBox.Show("Select a product to delete.", "Error");
            }

        }

        /// <summary>
        /// Goes back to the main page
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

        // ============================================================
        //   DELETE FUNCTION
        // ============================================================

        /// <summary>
        /// Deletes selected item
        /// </summary>
        private void ConfirmDelete()
        {
            // Get selected product ID
            DataRowView dv = ((DataRowView)grid.Items[grid.SelectedIndex]);
            if (dv != null)
            {
                int productID = (int)dv.Row.ItemArray[0];

                // Find product row to delete
                bool found = false;
                for (int i = 0; i < db.Product.Rows.Count && !found; i++)
                {
                    DataRow currentRow = db.Product.Rows[i];
                    // If row is found
                    if (Convert.ToInt32(currentRow["product_id"]) == productID)
                    {
                        // Mark row for deletion
                        currentRow.Delete();

                        // Delete row in table
                        db.Inventory.AcceptChanges();

                        // Show success
                        MessageBox.Show("Product ID #" + productID.ToString() + " has been successfuly deleted.\nClick \"Save\" to save changes to database", "Product Deleted");
                        found = true;
                    }
                }
            }
        }
    }
}
