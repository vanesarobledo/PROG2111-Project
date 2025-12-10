/*
* FILE          : AddConsoleForm.xaml.cs
* PROJECT       : PROG2111 Project
* PROGRAMMER    : Eumee Garcia
* FIRST VERSION : 2025-12-08
* DESCRIPTION   : This is an application contains the logic for adding a new console record 
*                 to the Game Store Management System. It validates user input 
*                 and inserts a new row into the Console table.
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

namespace GameStoreManagementSystem.Forms.ManageConsolesForm
{
    // <summary>
    /// Add Console form - allows adding a new console to the Console DataTable.
    /// </summary>
    public partial class AddConsoleForm : Window
    {
        internal GamesDatabase _db = ((MainWindow)Application.Current.MainWindow).gamesDatabase;

        public AddConsoleForm()
        {
            InitializeComponent();
        }

        /// <summary>
        ///  Closes the Add Console form without saving any data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Validates input fields and, if valid, creates a new console record in the 
        /// Console table. Displays a confirmation message upon successful insertion.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string name = InputConsoleName.Text.Trim();
            string company = InputCompany.Text.Trim();

            if (!ValidateInputs(name, company))
            {
                return;
            }
                
            DataRow row = _db.Console.NewRow();
            row["console_name"] = name;
            row["company"] = company;

            _db.Console.Rows.Add(row);

            MessageBox.Show("Console added.\nClick Save to apply changes.");
            Close();
        }

        /// <summary>
        /// Validates that the console name and company fields are not empty or 
        /// whitespace. Displays error messages for invalid input.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        private bool ValidateInputs(string name, string company)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Console name is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(company))
            {
                MessageBox.Show("Company is required.");
                return false;
            }

            return true;
        }
    }
}
