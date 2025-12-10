/*
* FILE 		: AddCustomerForm.xaml.cs
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
    /// Interaction logic for AddCustomerForm.xaml
    /// </summary>
    /*
* NAME      : AddCustomerForm
* Purpose   : The form to add an employee to the database.
*/
    public partial class AddCustomerForm : Window
    {
        internal GamesDatabase db = ((MainWindow)Application.Current.MainWindow).gamesDatabase;
        public AddCustomerForm()
        {
            InitializeComponent();
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
                && usernameBox != null && passwordBox != null)
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
            else
            {
                MessageBox.Show("All fields must have a value.");
            }
            return retValue;
        }
        /*
        * METHOD	: AddCustomerButton_Click
        * DESCRIPTION	:
        * If all the boxes have values in them it loads all the info into the database.
        * PARAMETERS	:
        * none
        * RETURNS	:
        * none
        */
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

                MessageBox.Show("Customer successfully added.", "Add Customer");
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
