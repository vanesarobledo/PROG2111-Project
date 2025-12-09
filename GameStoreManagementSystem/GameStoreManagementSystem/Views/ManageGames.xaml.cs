using System;
using System.Collections.Generic;
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
        }

        private void AddGame_Click(object sender, RoutedEventArgs e)
        {
            GameStoreManagementSystem.Forms.AddGamesForm form = new GameStoreManagementSystem.Forms.AddGamesForm();
            form.ShowDialog();
        }

        private void FilterGame_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = Window.GetWindow(this) as MainWindow;

            if (main != null)
            {
                FilterGamesForm form = new FilterGamesForm();
                form.ShowDialog();
            }
        }

        private void UpdateGame_Click(object sender, RoutedEventArgs e)
        {
            UpdateGamesForm form = new UpdateGamesForm();
            form.ShowDialog();
        }

        private void DeleteGame_Click(object sender, RoutedEventArgs e)
        {
            //delete logic tbd
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
