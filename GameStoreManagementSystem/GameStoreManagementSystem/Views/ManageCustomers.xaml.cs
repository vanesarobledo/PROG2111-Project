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

namespace GameStoreManagementSystem.Views.Customers

{
    /// <summary>
    /// Interaction logic for ManageCustomers.xaml
    /// </summary>
    public partial class ManageCustomers : UserControl
    {
        public ManageCustomers()
        {
            InitializeComponent();
        }

        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FilterCustomer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateCustomer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteCustomer_Click(object sender, RoutedEventArgs e)
        {

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
