/*
* FILE          : UpdateCustomerForm.xaml.cs
* PROJECT       : PROG2111 Project
* PROGRAMMER    : Connar Thompson
* FIRST VERSION : 2025-12-08
* DESCRIPTION   : This is an application contains the logic for updating an existing customer 
*                 record in the Game Store Management System. It loads customer 
*                 data, validates user input, and applies updates to the 
*                 Customer DataTable.
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
using System.Windows.Shapes;

namespace GameStoreManagementSystem.Forms.ManageCustomerForm
{
    /// <summary>
    /// Interaction logic for UpdateCustomerForm.xaml
    /// </summary>
    public partial class UpdateCustomerForm : Window
    {
        internal GamesDatabase db = ((MainWindow)Application.Current.MainWindow).gamesDatabase;
        internal DataGrid grid = ((MainWindow)Application.Current.MainWindow).MainGrid;
        int customerID;
        public UpdateCustomerForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads the selected customer's details into the form fields. 
        /// Closes the window if no customer is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Get selected item
            if (grid.SelectedIndex != -1)
            {
                // Get values from DataGrid
                DataRowView dv = ((DataRowView)grid.Items[grid.SelectedIndex]);
                if (dv.Row.ItemArray[0] != DBNull.Value)
                {
                    customerID = (int)dv.Row.ItemArray[0];
                    firstNameBox.Text = (string)dv.Row.ItemArray[1];
                    lastNameBox.Text = (string)dv.Row.ItemArray[2];
                    dobBox.Text = (string)dv.Row.ItemArray[3];
                    emailBox.Text = (string)dv.Row.ItemArray[4];
                    usernameBox.Text = (string)dv.Row.ItemArray[5];
                    passwordBox.Text = (string)dv.Row.ItemArray[6];
                }
                else
                {
                    MessageBox.Show("Please save data to assign customer ID.", "Error");
                    this.Close();
                }
            }
            // If no item is selected
            else
            {
                MessageBox.Show("Please select an customer to update.", "Error");
                this.Close();
            }
        }

        /// <summary>
        /// Validates that all required fields are filled and that the 
        /// date of birth is in a valid format.
        /// </summary>
        /// <returns></returns>
        internal bool emptyCheck()
        {
            bool retValue = false;
            DateTime testDate = DateTime.MinValue;
            if (firstNameBox != null && lastNameBox != null && dobBox != null && emailBox != null
                && usernameBox != null && passwordBox != null)
            {
                if (DateTime.TryParse(dobBox.Text, out testDate))
                {
                    MessageBox.Show("Invalid Date of Birth.");
                }
                else
                {
                    retValue = true;
                }
            }
            return retValue;
        }

        /// <summary>
        /// Updates the selected customer's details in the Customer table 
        /// after validation succeeds.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            bool found = false;
            if (emptyCheck())
            {
                for (int i = 0; i < db.Customer.Rows.Count && !found; i++)
                {
                    DataRow currentRow = db.Customer.Rows[i];
                    if (Convert.ToInt32(currentRow["customer_id"]) == customerID)
                    {
                        currentRow["first_name"] = firstNameBox.Text;
                        currentRow["last_name"] = lastNameBox.Text;
                        currentRow["date_of_birth"] = dobBox.Text;
                        currentRow["email"] = emailBox.Text;
                        currentRow["username"] = usernameBox.Text;
                        currentRow["password"] = passwordBox.Text;
                        found = true;
                    }
                }
                if (!found)
                {
                    MessageBox.Show("Customer could not be updated.", "Error");
                }
            }
        }

        /// <summary>
        /// Closes the Update Customer window without saving.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
