/*
* FILE          : UpdateConsoleForm.xaml.cs
* PROJECT       : PROG2111 Project
* PROGRAMMER    : Eumee Garcia
* FIRST VERSION : 2025-12-08
* DESCRIPTION   : This is an application contains the logic for updating an existing console 
*                 record in the Console DataTable. It pre-fills the form with 
*                 current values, validates input, and applies changes.
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
    /// <summary>
    /// Update Console form - edits existing Console DataTable row.
    /// </summary>
    public partial class UpdateConsoleForm : Window
    {
        private DataRow _row;
        internal GamesDatabase _db = ((MainWindow)Application.Current.MainWindow).gamesDatabase;

        public UpdateConsoleForm(DataRow row)
        {
            InitializeComponent();
            _row = row;

            // Pre-fill fields with existing console data
            InputConsoleName.Text = _row["console_name"].ToString();
            InputCompany.Text = _row["company"].ToString();
        }

        /// <summary>
        /// Closes the Update Console window without saving any changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Validates user input and updates the selected console entry 
        /// in the Console DataTable if valid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            string name = InputConsoleName.Text.Trim();
            string company = InputCompany.Text.Trim();

            if (!ValidateInputs(name, company))
            {
                return;
            }

            _row["name"] = name;
            _row["company"] = company;

            MessageBox.Show("Console updated.\nClick Save to apply changes.");
            Close();
        }

        /// <summary>
        /// Ensures that the console name and company fields are not empty. 
        /// Displays error messages for invalid entries.
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
