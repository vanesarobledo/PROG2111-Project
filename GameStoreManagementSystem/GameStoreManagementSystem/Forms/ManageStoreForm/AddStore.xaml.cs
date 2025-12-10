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
using System.Windows.Shapes;

namespace GameStoreManagementSystem.Forms.ManageStoreForm
{
    /// <summary>
    /// Interaction logic for AddStore.xaml
    /// </summary>
    public partial class AddStore : Window
    {

        internal GamesDatabase db = ((MainWindow)Application.Current.MainWindow).gamesDatabase;
        public AddStore()
        {
            InitializeComponent();
        }

        private void AddStoreButton_Click(object sender, RoutedEventArgs e)
        {
            DataRow newRow = db.Store.NewRow();


            if (StoreNameInput != null)
            { 
                newRow["location"] = StoreNameInput.Text;
                db.Store.Rows.Add(newRow);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
