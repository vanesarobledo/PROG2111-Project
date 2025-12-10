/*
* FILE          : AddEmployeeForm.xaml.cs
* PROJECT       : PROG2111 Project
* PROGRAMMER    : Connar Thompson
* FIRST VERSION : 2025-12-08
* DESCRIPTION   : This is an application contains the logic for adding a new employee 
*                 record to the Game Store Management System. It loads 
*                 foreign key values, validates user input, and inserts 
*                 a new row into the Employee table.
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
    /// Interaction logic for AddEmployeeForm.xaml
    /// </summary>
    /// 
    public partial class AddEmployeeForm : Window
    {
        internal GamesDatabase db = ((MainWindow)Application.Current.MainWindow).gamesDatabase;
        public AddEmployeeForm()
        {
            InitializeComponent();
            LoadForeignKeys();
        }

        /// <summary>
        /// Loads available store IDs and locations into the Store ComboBox.
        /// </summary>
        internal void LoadForeignKeys()
        {
            storeIDBox.SelectedValuePath = "Key";
            storeIDBox.DisplayMemberPath = "Value";
            foreach (DataRow store in db.Store.Rows)
            {
                if (store.RowState != DataRowState.Deleted)
                {
                    int storeID = (int)store["store_id"];
                    string storeDisplay = store["store_id"].ToString() + ": " + store["location"];
                    storeIDBox.Items.Add(new KeyValuePair<int, string>(storeID, storeDisplay));
                }
            }
        }

        /// <summary>
        /// Validates that all employee input fields contain valid values.
        /// Ensures the date of birth is in a valid date format.
        /// Shows error messages when needed.
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

        /// <summary>
        /// Attempts to add a new employee record. 
        /// If validation succeeds, a new row is inserted into the Employee table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            DataRow newRow = db.Employee.NewRow();
            int storeID = 0;
            if (storeIDBox.SelectedValue != null)
            {
                storeID = (int)storeIDBox.SelectedValue;
            }
            if (emptyCheck())
            {
                newRow["first_name"] = firstNameBox.Text;
                newRow["last_name"] = lastNameBox.Text;
                newRow["date_of_birth"] = dobBox.Text;
                newRow["email"] = emailBox.Text;
                newRow["username"] = usernameBox.Text;
                newRow["password"] = passwordBox.Text;
                newRow["store_id"] = storeID;
                db.Employee.Rows.Add(newRow);

                MessageBox.Show("Employee successfully added.\nClick \"Save\" to save changes to database", "Success");
            }
        }

        /// <summary>
        /// Closes the Add Employee window without saving.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
