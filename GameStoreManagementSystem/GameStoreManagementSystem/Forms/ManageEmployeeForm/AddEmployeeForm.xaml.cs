/*
* FILE 		: AddEmployeeForm.xaml.cs
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
    /// Interaction logic for AddEmployeeForm.xaml
    /// </summary>
    /// 
/*
* NAME      : AddEmployeeForm
* Purpose   : The form to add an employee to the database.
*/
    public partial class AddEmployeeForm : Window
    {
        internal GamesDatabase db = ((MainWindow)Application.Current.MainWindow).gamesDatabase;
        public AddEmployeeForm()
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
            storeIDBox.SelectedValuePath = "Key";
            storeIDBox.DisplayMemberPath = "Value";
            foreach (DataRow store in db.Store.Rows)
            {
                int storeID = (int)store["store_id"];
                string storeDisplay = store["store_id"].ToString() + ": " + store["location"];
                storeIDBox.Items.Add(new KeyValuePair<int, string>(storeID, storeDisplay));
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
            else
            {
                MessageBox.Show("All fields must have a value.");
            }
            return retValue;
        }
        /*
        * METHOD	: AddEmployeeButton_Click
        * DESCRIPTION	:
        * If all the boxes have values in them it loads all the info into the database.
        * PARAMETERS	:
        * none
        * RETURNS	:
        * none
        */
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

                MessageBox.Show("Employee successfully added!", "Success");
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
