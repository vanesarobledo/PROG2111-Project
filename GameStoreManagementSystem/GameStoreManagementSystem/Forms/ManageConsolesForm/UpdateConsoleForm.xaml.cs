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

            InputConsoleName.Text = _row["console_name"].ToString();
            InputCompany.Text = _row["company"].ToString();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

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
