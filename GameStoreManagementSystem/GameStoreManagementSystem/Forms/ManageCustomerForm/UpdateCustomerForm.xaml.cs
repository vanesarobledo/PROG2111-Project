/*
* FILE 		: UpdateCustomerForm.xaml.cs
* PROJECT 	: PROG2111 Project
* PROGRAMMER 	: Eumee Garcia, Vanesa Robledo, Connar Thompson
* FIRST VERSION : 2025-10-09
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
    /*
* NAME      : UpdateCustomerForm
* Purpose   : The form to update an customer from the database.
*/
    public partial class UpdateCustomerForm : Window
    {
        internal GamesDatabase db = ((MainWindow)Application.Current.MainWindow).gamesDatabase;
        internal DataGrid grid = ((MainWindow)Application.Current.MainWindow).MainGrid;
        int customerID;
        public UpdateCustomerForm()
        {
            InitializeComponent();
        }
        /*
        * METHOD	: Window_Loaded
        * DESCRIPTION	:
        * Loads the textboxes with the appropriate values from the grid.
        * PARAMETERS	:
        * None
        * RETURNS	:
        * None
        */
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
                    dobBox.Text = dv.Row.ItemArray[3].ToString();
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
                MessageBox.Show("Please select a customer to update.", "Error");
                this.Close();
            }
        }
        /*
        * METHOD	: emptyCheck
        * DESCRIPTION	:
        * Validates that none of the boxes are empty.
        * PARAMETERS	:
        * None
        * RETURNS	:
        * None
        */
        internal bool emptyCheck()
        {
            bool retValue = false;
            DateTime testDate = DateTime.MinValue;
            if (firstNameBox.Text != "" && lastNameBox.Text != "" && dobBox.Text != "" && emailBox.Text != ""
                && usernameBox.Text != "" && passwordBox.Text != "")
            {
                if (!DateTime.TryParse(dobBox.Text, out testDate))
                {
                    MessageBox.Show("Invalid Date of Birth.", "Error");
                }
                else
                {
                    retValue = true;
                }
            }
            else
            {
                MessageBox.Show("All fields must have a value.", "Error");
            }
            return retValue;
        }
        /*
        * METHOD	: UpdateCustomerButton_Click
        * DESCRIPTION	:
        * Runs verification and finds the appropriate row in the database to update and then updates the 
        * values.
        * PARAMETERS	:
        * None
        * RETURNS	:
        * None
        */
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
        /*
        * METHOD	: cancelButton_Click
        * DESCRIPTION	:
        * Quits out of the window without adding anything to the database.
        * PARAMETERS	:
        * None
        * RETURNS	:
        * None
        */
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
