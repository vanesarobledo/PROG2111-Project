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

namespace GameStoreManagementSystem.Views.Customers

{
    /// <summary>
    /// Interaction logic for ManageCustomers.xaml
    /// </summary>
    public partial class ManageCustomers : UserControl
    {
        /// <summary>
        /// Select games database from Main window
        /// </summary>
        internal GamesDatabase db = ((MainWindow)Application.Current.MainWindow).gamesDatabase;

        /// <summary>
        /// Select DataGrid from Main menu
        /// </summary>
        internal DataGrid grid = ((MainWindow)Application.Current.MainWindow).MainGrid;

        public ManageCustomers()
        {
            InitializeComponent();
        }

        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            Forms.ManageCustomerForm.AddCustomerForm form = new Forms.ManageCustomerForm.AddCustomerForm();
            form.ShowDialog();
        }

        private void UpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (grid.SelectedIndex != -1)
            {
                Forms.ManageCustomerForm.UpdateCustomerForm form = new Forms.ManageCustomerForm.UpdateCustomerForm();
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Select a customer to update.", "Error");
            }
        }

        private void DeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            // Check for selected index
            if (grid.SelectedIndex != -1)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this customer?", "Manage Customer", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    ConfirmDelete();
                }
            }
            else
            {
                MessageBox.Show("Select a customer to delete.", "Error");
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

        /// <summary>
        /// Deletes selected item
        /// </summary>
        private void ConfirmDelete()
        {
            // Get selected product ID
            DataRowView dv = ((DataRowView)grid.Items[grid.SelectedIndex]);
            if (dv != null)
            {
                int customerID = (int)dv.Row.ItemArray[0];

                // Find product row to delete
                bool found = false;
                for (int i = 0; i < db.Customer.Rows.Count && !found; i++)
                {
                    DataRow currentRow = db.Customer.Rows[i];
                    // If row is found
                    if (Convert.ToInt32(currentRow["customer_id"]) == customerID)
                    {
                        // Mark row for deletion
                        currentRow.Delete();

                        // Delete row in table
                        db.Store.AcceptChanges();

                        // Show success
                        MessageBox.Show("Customer ID #" + customerID.ToString() + " has been successfuly deleted.\nClick \"Save\" to save changes to database", "Customer Deleted");
                        found = true;
                    }
                }
            }
        }
    }
}
