/*
* FILE 		: UpdateStoreForm.xaml.cs
* PROJECT 	: PROG2111 Project
* PROGRAMMER 	: Eumee Garcia, Vanesa Robledo, Connar Thompson
* FIRST VERSION : 2025-10-09
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
using System.Windows.Shapes;

namespace GameStoreManagementSystem.Forms.ManageStoreForm
{
    /// <summary>
    /// Interaction logic for UpdateStoreForm.xaml
    /// </summary>
/*
* NAME      : UpdateStoreForm
* Purpose   : The form to update an store from the database.
*/
    public partial class UpdateStoreForm : Window
    {
        internal GamesDatabase db = ((MainWindow)Application.Current.MainWindow).gamesDatabase;
        internal DataGrid grid = ((MainWindow)Application.Current.MainWindow).MainGrid;
        int storeID;
        public UpdateStoreForm()
        {
            InitializeComponent();
        }
        /*
        * METHOD	: Window_Loaded
        * DESCRIPTION	:
        * Loads the textboxes with the appropriate values from the grid.
        * PARAMETERS	:
        * None
        * RETURNS	:
        * None
        */
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Get selected item
            if (grid.SelectedIndex != -1)
            {
                // Get values from DataGrid
                DataRowView dv = ((DataRowView)grid.Items[grid.SelectedIndex]);
                if (dv.Row.ItemArray[0] != DBNull.Value)
                {
                    storeID = (int)dv.Row.ItemArray [0];
                    StoreNameInput.Text = (string)dv.Row.ItemArray[1];
                }
                else
                {
                    MessageBox.Show("Please save data to assign store ID.", "Error");
                    this.Close();
                }
            }
            // If no item is selected
            else
            {
                MessageBox.Show("Please select an Store to update.", "Error");
                this.Close();
            }
        }
        /*
        * METHOD	: UpdateStoreButton_Click
        * DESCRIPTION	:
        * Runs verification and finds the appropriate row in the database to update and then updates the 
        * values.
        * PARAMETERS	:
        * None
        * RETURNS	:
        * None
        */
        private void UpdateStoreButton_Click(object sender, RoutedEventArgs e)
        {
            bool found = false;
            if (StoreNameInput != null)
            {
                for (int i = 0; i < db.Store.Rows.Count && !found; i++)
                {
                    DataRow currentRow = db.Store.Rows[i];
                    if (Convert.ToInt32(currentRow["store_id"]) == storeID)
                    {
                        currentRow["location"] = StoreNameInput.Text;

                        found = true;
                    }
                }
                if (!found)
                {
                    MessageBox.Show("Store could not be updated.", "Error");
                }
            }
        }
        /*
        * METHOD	: CancelButton_Click
        * DESCRIPTION	:
        * Quits out of the window without adding anything to the database.
        * PARAMETERS	:
        * None
        * RETURNS	:
        * None
        */
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
