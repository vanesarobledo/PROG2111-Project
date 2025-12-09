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

namespace GameStoreManagementSystem.Forms
{
    /// <summary>
    /// Interaction logic for AddGamesForm.xaml
    /// </summary>
    public partial class AddGamesForm : Window
    {
        public AddGamesForm()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //Add your DB insert logic here 
            MessageBox.Show("Game added successfully!", "Success",
                            MessageBoxButton.OK, MessageBoxImage.Information);

            this.Close();
        }
    }
}
