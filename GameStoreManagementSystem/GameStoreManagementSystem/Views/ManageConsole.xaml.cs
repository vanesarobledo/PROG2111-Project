/*
* FILE          : ManageConsoles.xaml.cs
* PROJECT       : PROG2111 Project
* PROGRAMMER    : Eumee Garcia
* FIRST VERSION : 2025-12-08
* DESCRIPTION   : This is an application contains the interaction logic for managing console
*                 records within the Game Store Management System. It handles
*                 adding, updating, and navigating away from the console
*                 management view.
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using GameStoreManagementSystem.Forms.ManageConsolesForm;

namespace GameStoreManagementSystem.Views.Console
{
    /// <summary>
    /// Interaction logic for ManageConsole.xaml
    /// </summary>
    public partial class ManageConsole : UserControl
    {
        public ManageConsole()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Opens the Add Console form, allowing the user to enter a new console record.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddConsole_Click(object sender, RoutedEventArgs e)
        {
            AddConsoleForm addForm = new AddConsoleForm();
            addForm.ShowDialog();
        }

        /// <summary>
        /// Opens the Update Console form using the currently selected console row
        /// from the MainGrid in the MainWindow. If no console is selected, the update 
        /// form will not open.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateConsole_Click(object sender, RoutedEventArgs e)
        {
            // You must get selected row from MainGrid in MainWindow
            MainWindow mw = (MainWindow)Window.GetWindow(this);
            DataRowView row = (DataRowView)mw.MainGrid.SelectedItem;

            if (row != null)
            {
                UpdateConsoleForm updateForm = new UpdateConsoleForm(row.Row);
                updateForm.ShowDialog();
            }
        }

        /// <summary>
        /// Intended to delete the selected console record.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteConsole_Click(object sender, RoutedEventArgs e)
        {
            bool canContinue = true;
            string message = "";

            MainWindow main = (MainWindow)Application.Current.MainWindow;

            //Validate selection
            DataRowView selectedView = null;

            if (main.MainGrid.SelectedItem == null)
            {
                message = "Please select a console to delete.";
                canContinue = false;
            }
            else
            {
                selectedView = main.MainGrid.SelectedItem as DataRowView;
                if (selectedView == null)
                {
                    message = "Invalid selection.";
                    canContinue = false;
                }
            }

            DataRow row = null;
            int consoleId = 0;
            string consoleName = "";

            //Extract row
            if (canContinue)
            {
                row = selectedView.Row;
                consoleId = (int)row["console_id"];
                consoleName = row["console_name"].ToString();
            }

            //Confirmation dialog for deleting
            if (canContinue)
            {
                MessageBoxResult result = MessageBox.Show(
                    $"Are you sure you want to delete console:\n\n{consoleName}?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result != MessageBoxResult.Yes)
                {
                    message = "Delete cancelled.";
                    canContinue = false;
                }
            }

            //delete selected console
            if (canContinue)
            {
                main.gamesDatabase.Console.Rows.Remove(row);
                main.RefreshDataGrid();

                if (!main.tablesOpened.Contains("Console"))
                {
                    main.tablesOpened.Add("Console");
                }
                
                message = "Console deleted.\nClick SAVE to write changes to MySQL.";
            }

            MessageBox.Show(message);
        }

        /// <summary>
        /// Returns the user back to the main window view by clearing the console 
        /// management UserControl and re-displaying the right-side navigation panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
    }
}
