/*
* FILE          : UpdateEmployeeForm.xaml.cs
* PROJECT       : PROG2111 Project
* PROGRAMMER    : Connar Thompson
* FIRST VERSION : 2025-12-08
* DESCRIPTION   : This is an application contains the logic for updating an existing employee 
*                 record in the Game Store Management System. It loads employee 
*                 data, validates input, and applies changes to the Employee table.
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

        /// <summary>
        /// Loads available store IDs into the ComboBox.
        /// </summary>
        internal void LoadForeignKeys()
        {
            storeIDBox.SelectedValuePath = "Key";
            storeIDBox.DisplayMemberPath = "Value";
            foreach (DataRow store in db.Store.Rows)
            {
                int storeID = (int)store["store_id"];
                string storeDisplay = store["store_id"].ToString() + ": " + store["location"];
                storeIDBox.Items.Add(new KeyValuePair<int, string>(storeID, storeDisplay));
            }
        }

        /// <summary>
        /// Loads the selected employee's data into the fields,
        /// or closes the form if no employee is selected.
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
                    employeeID = (int)dv.Row.ItemArray[0];
                    firstNameBox.Text = (string)dv.Row.ItemArray[1];
                    lastNameBox.Text = (string)dv.Row.ItemArray[2];
                    dobBox.Text = dv.Row.ItemArray[3].ToString();
                    emailBox.Text = (string)dv.Row.ItemArray[4];
                    usernameBox.Text = (string)dv.Row.ItemArray[5];
                    passwordBox.Text = (string)dv.Row.ItemArray[6];
                    storeIDBox.SelectedValue = (int)dv.Row.ItemArray[7];
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

        /// <summary>
        /// Validates that all required fields are filled and the date of birth is valid.
        /// </summary>
        /// <returns></returns>
        internal bool emptyCheck()
        {
            bool retValue = false;
            DateTime testDate = DateTime.MinValue;
            if (firstNameBox.Text != "" && lastNameBox.Text != "" && dobBox.Text != "" && emailBox.Text != ""
                && usernameBox.Text != "" && passwordBox.Text != "" && storeIDBox.SelectedValue != null)
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

        /// <summary>
        /// Updates the selected employee record with new values
        /// after validation succeeds.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            bool found = false;
            int storeID = 0;
            if (storeIDBox.SelectedValue != null)
            {
                storeID = (int)storeIDBox.SelectedValue;
            }
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
                        currentRow["store_id"] = storeID;
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

        /// <summary>
        /// Closes the Update Employee window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
