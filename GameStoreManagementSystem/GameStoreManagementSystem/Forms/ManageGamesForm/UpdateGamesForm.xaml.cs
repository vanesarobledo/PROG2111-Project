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
    /// Interaction logic for UpdateGamesForm.xaml
    /// </summary>
    public partial class UpdateGamesForm : Window
    {
        public UpdateGamesForm()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Game updated successfully!", "Success",
                            MessageBoxButton.OK, MessageBoxImage.Information);

            this.Close();
        }
    }
}
