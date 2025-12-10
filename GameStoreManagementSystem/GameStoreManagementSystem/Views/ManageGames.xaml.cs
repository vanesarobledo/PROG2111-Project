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

        private void AddGame_Click(object sender, RoutedEventArgs e)
        {
            GameStoreManagementSystem.Forms.AddGamesForm form = new GameStoreManagementSystem.Forms.AddGamesForm();
            form.ShowDialog();
        }

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
