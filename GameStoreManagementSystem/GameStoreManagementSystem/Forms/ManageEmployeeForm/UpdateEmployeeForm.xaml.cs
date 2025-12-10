/*
* FILE 		: UpdateEmployeeForm.xaml.cs
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

namespace GameStoreManagementSystem.Forms.ManageEmployeeForm
{
    /// <summary>
    /// Interaction logic for UpdateEmployeeForm.xaml
    /// </summary>
/*
* NAME      : UpdateEmployeeForm
* Purpose   : The form to update an employee from the database.
*/
    public partial class UpdateEmployeeForm : Window
    {
        internal GamesDatabase db = ((MainWindow)Application.Current.MainWindow).gamesDatabase;
        internal DataGrid grid = ((MainWindow)Application.Current.MainWindow).MainGrid;
        int employeeID;
        public UpdateEmployeeForm()
        {
            InitializeComponent();
            LoadForeignKeys();
        }
        /*
        * METHOD	: LoadForeignKeys
        * DESCRIPTION	:
        * Loads the drop down menu with the store id's.
        * PARAMETERS	:
        * None
        * RETURNS	:
        * None
        */
        internal void LoadForeignKeys()
        {
            foreach (DataRow store in db.Store.Rows)
            {
                int storeID = (int)store["store_id"];
                string storeDisplay = store["store_id"].ToString() + ": " + store["location"];
                storeIDBox.Items.Add(new KeyValuePair<int, string>(storeID, storeDisplay));
            }
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
                    employeeID = (int)dv.Row.ItemArray[0];
                    firstNameBox.Text = (string)dv.Row.ItemArray[1];
                    lastNameBox.Text = (string)dv.Row.ItemArray[2];
                    dobBox.Text = (string)dv.Row.ItemArray[3];
                    emailBox.Text = (string)dv.Row.ItemArray[4];
                    usernameBox.Text = (string)dv.Row.ItemArray[5];
                    passwordBox.Text = (string)dv.Row.ItemArray[6];
                }
                else
                {
                    MessageBox.Show("Please save data to assign employee ID.", "Error");
                    this.Close();
                }
            }
            // If no item is selected
            else
            {
                MessageBox.Show("Please select an employee to update.", "Error");
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
            if (firstNameBox != null && lastNameBox != null && dobBox != null && emailBox != null
                && usernameBox != null && passwordBox != null && storeIDBox.Text != null)
            {
                if (!DateTime.TryParse(dobBox.Text, out testDate))
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

        /*
        * METHOD	: UpdateEmployeeButton_Click
        * DESCRIPTION	:
        * Runs verification and finds the appropriate row in the database to update and then updates the 
        * values.
        * PARAMETERS	:
        * None
        * RETURNS	:
        * None
        */
        private void UpdateEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            bool found = false;
            if (emptyCheck())
            {
                for (int i = 0; i < db.Employee.Rows.Count && !found; i++)
                {
                    DataRow currentRow = db.Employee.Rows[i];
                    if (Convert.ToInt32(currentRow["employee_id"]) == employeeID)
                    {
                        currentRow["first_name"] = firstNameBox.Text;
                        currentRow["last_name"] = lastNameBox.Text;
                        currentRow["date_of_birth"] = dobBox.Text;
                        currentRow["email"] = emailBox.Text;
                        currentRow["username"] = usernameBox.Text;
                        currentRow["password"] = passwordBox.Text;
                        currentRow["store_id"] = storeIDBox.Text;
                        found = true;

                        MessageBox.Show("Employee ID #" + employeeID.ToString() + " has been updated.\nClick \"Save\" to save changes to database", "Success");
                    }
                }
                if (!found)
                {
                    MessageBox.Show("Employee could not be updated.", "Error");
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
