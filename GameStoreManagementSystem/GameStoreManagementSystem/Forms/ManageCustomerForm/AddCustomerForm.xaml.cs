/*
* FILE          : AddCustomerForm.xaml.cs
* PROJECT       : PROG2111 Project
* PROGRAMMER    : Connar Thompson
* FIRST VERSION : 2025-12-08
* DESCRIPTION   : This is an application contains the logic for adding a new customer record 
*                 to the Game Store Management System. It validates user input 
*                 and inserts a new row into the Customer table.
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
    /// Interaction logic for AddCustomerForm.xaml
    /// </summary>
    public partial class AddCustomerForm : Window
    {
        internal GamesDatabase db = ((MainWindow)Application.Current.MainWindow).gamesDatabase;
        public AddCustomerForm()
        {
            InitializeComponent();
        }

        /// <summary>
        ///  Validates that all required fields contain values and that the 
        /// date of birth is a valid date. Displays error messages as needed.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Creates a new customer row and inserts it into the Customer table 
        /// if all inputs pass validation. Displays error messages when needed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            DataRow newRow = db.Customer.NewRow();
            if (emptyCheck())
            {
                newRow["first_name"] = firstNameBox.Text;
                newRow["last_name"] = lastNameBox.Text;
                newRow["date_of_birth"] = dobBox.Text;
                newRow["email"] = emailBox.Text;
                newRow["username"] = usernameBox.Text;
                newRow["password"] = passwordBox.Text;
                db.Customer.Rows.Add(newRow);

                MessageBox.Show("Customer successfully added.\nClick \"Save\" to save changes to database", "Success");
            }
        }

        /// <summary>
        ///  Closes the Add Customer window without saving changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
