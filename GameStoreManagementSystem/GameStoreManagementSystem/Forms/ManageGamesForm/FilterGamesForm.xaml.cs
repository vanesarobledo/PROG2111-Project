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
using System.Windows.Shapes;

namespace GameStoreManagementSystem.Forms.ManageGamesForm
{
    /// <summary>
    /// Interaction logic for FilterGamesForm.xaml
    /// </summary>
    public partial class FilterGamesForm : Window
    {
        public FilterGamesForm()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;

            if (mainWindow != null)
            {
                mainWindow.RightContentArea.Content = null;
                mainWindow.RightButtonPanel.Visibility = Visibility.Visible;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Back_Click(sender, e);
        }

        private void ViewAll_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Viewing all games (placeholder).");
        }

        private void FilterID_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Filter by ID: {InputID.Text}");
        }

        private void FilterName_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Filter by Name: {InputName.Text}");
        }

        private void FilterGenre_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Filter by Genre: {InputGenre.Text}");
        }
    }
}
