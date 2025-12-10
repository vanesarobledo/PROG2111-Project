/*
* FILE          : ManageGames.xaml.cs
* PROJECT       : PROG2111 Project
* PROGRAMMER    : Eumee Garcia
* FIRST VERSION : 2025-12-08
* DESCRIPTION   : This is an application contains the interaction logic for managing games 
*                 within the Game Store Management System. It provides functions 
*                 for adding, updating, deleting game records, and updating UI 
*                 button states based on user selection.
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
using GameStoreManagementSystem.Forms.ManageGamesForm;

namespace GameStoreManagementSystem.Views.Games
{
    /// <summary>
    /// Interaction logic for ManageGames.xaml
    /// </summary>
    public partial class ManageGames : UserControl
    {
        public ManageGames()
        {
            InitializeComponent();

            // Disable both buttons at startup
            UpdateGameButton.IsEnabled = false;
            DeleteGameButton.IsEnabled = false;

            // Subscribe to MainGrid selection changes
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainGrid.SelectionChanged += MainGrid_SelectionChanged;
        }

        /// <summary>
        /// Opens the Add Game form, allowing the user to create a new game entry.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddGame_Click(object sender, RoutedEventArgs e)
        {
            GameStoreManagementSystem.Forms.AddGamesForm form = new GameStoreManagementSystem.Forms.AddGamesForm();
            form.ShowDialog();
        }

        /// <summary>
        /// Opens the Update Game form for the selected game in the DataGrid.
        /// Displays an error message if no game is selected. Passes the selected row
        /// to the update form so the existing game details can be modified.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateSelectedGame_Click(object sender, RoutedEventArgs e)
        {
            bool valid = true;

            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

            if (mainWindow.MainGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a game to update.");
                valid = false;
            }

            if (valid)
            {
                DataRow row = ((DataRowView)mainWindow.MainGrid.SelectedItem).Row;

                UpdateGamesForm form = new UpdateGamesForm(row);
                form.ShowDialog();
            }

            return;  
        }

        /// <summary>
        /// Deletes the selected game record from the DataGrid. Prompts the user for 
        /// confirmation before deleting. If confirmed, marks the row as deleted in the 
        /// underlying DataTable and disables update/delete buttons afterward.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteGame_Click(object sender, RoutedEventArgs e)
        {
            bool valid = true;

            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

            // Make sure a row is selected
            if (mainWindow.MainGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a game to delete.");
                valid = false;
            }

            if (valid)
            {
                // Ask for confirmation
                MessageBoxResult result = MessageBox.Show(
                    "Are you sure you want to delete this game?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    // Get the selected row
                    DataRow row = ((DataRowView)mainWindow.MainGrid.SelectedItem).Row;

                    // Delete the row (flags as deleted in the DataTable)
                    row.Delete();

                    MessageBox.Show("Game deleted.\nClick Save to apply changes to the database.",
                                    "Deleted",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);

                    // Refresh button state
                    UpdateGameButton.IsEnabled = false;
                    DeleteGameButton.IsEnabled = false;
                }
            }

            return;
        }

        /// <summary>
        /// Triggered whenever the user changes the selected row in the MainGrid.
        /// Enables or disables the Update and Delete buttons depending on whether
        /// a row is selected and data exists in the grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

            bool hasSelection = mainWindow.MainGrid.SelectedItem != null;
            bool hasRows = mainWindow.MainGrid.Items.Count > 0;

            // Enable buttons only if there is data AND a selected row
            bool enableButtons = hasSelection && hasRows;

            UpdateGameButton.IsEnabled = enableButtons;
            DeleteGameButton.IsEnabled = enableButtons;
        }

        /// <summary>
        /// Goes back to the main page
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
