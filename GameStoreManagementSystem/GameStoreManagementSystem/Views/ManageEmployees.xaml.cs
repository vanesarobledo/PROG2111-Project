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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameStoreManagementSystem.Views.Employees
{
    /// <summary>
    /// Interaction logic for ManageEmployees.xaml
    /// </summary>
    public partial class ManageEmployees : UserControl
    {
        /// <summary>
        /// Select games database from Main window
        /// </summary>
        internal GamesDatabase db = ((MainWindow)Application.Current.MainWindow).gamesDatabase;

        /// <summary>
        /// Select DataGrid from Main menu
        /// </summary>
        internal DataGrid grid = ((MainWindow)Application.Current.MainWindow).MainGrid;
        public ManageEmployees()
        {
            InitializeComponent();
        }
        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            Forms.ManageEmployeeForm.AddEmployeeForm form = new Forms.ManageEmployeeForm.AddEmployeeForm();
            form.ShowDialog();
        }

        private void UpdateEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (grid.SelectedIndex != -1)
            {
                Forms.ManageEmployeeForm.UpdateEmployeeForm form = new Forms.ManageEmployeeForm.UpdateEmployeeForm();
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Select an employee to update.", "Error");
            }
        }

        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            // Check for selected index
            if (grid.SelectedIndex != -1)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this employee?", "Manage Employee", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    ConfirmDelete();
                }
            }
            else
            {
                MessageBox.Show("Select an employee to delete.", "Error");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;

            if (mainWindow != null)
            {
                // Clear the UserControl area
                mainWindow.RightContentArea.Content = null;

                // Show the right-side main button panel again
                mainWindow.RightButtonPanel.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Deletes selected item
        /// </summary>
        private void ConfirmDelete()
        {
            // Get selected product ID
            DataRowView dv = ((DataRowView)grid.Items[grid.SelectedIndex]);
            if (dv != null)
            {
                int employeeID = (int)dv.Row.ItemArray[0];

                // Find product row to delete
                bool found = false;
                for (int i = 0; i < db.Employee.Rows.Count && !found; i++)
                {
                    DataRow currentRow = db.Employee.Rows[i];
                    // If row is found
                    if (Convert.ToInt32(currentRow["employee_id"]) == employeeID)
                    {
                        // Mark row for deletion
                        currentRow.Delete();

                        // Delete row in table
                        db.Employee.AcceptChanges();

                        // Show success
                        MessageBox.Show("Employee ID #" + employeeID.ToString() + " has been successfuly deleted.\nClick \"Save\" to save changes to database", "Employee Deleted");
                        found = true;
                    }
                }
            }
        }
    }
}
