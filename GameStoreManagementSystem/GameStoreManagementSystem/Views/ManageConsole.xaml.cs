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

        private void AddConsole_Click(object sender, RoutedEventArgs e)
        {
            AddConsoleForm addForm = new AddConsoleForm();
            addForm.ShowDialog();
        }

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

        private void DeleteConsole_Click(object sender, RoutedEventArgs e)
        {
            //DeleteConsoleForm deleteForm = new DeleteConsoleForm();
            //deleteForm.ShowDialog();
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
