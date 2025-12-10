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

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

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
