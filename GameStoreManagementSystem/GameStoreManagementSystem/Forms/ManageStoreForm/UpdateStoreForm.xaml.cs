/*
* FILE          : UpdateStoreForm.xaml.cs
* PROJECT       : PROG2111 Project
* PROGRAMMER    : Eumee Garcia, Vanesa Robledo, Connar Thompson
* FIRST VERSION : 2025-12-09
* DESCRIPTION   : This file provides the logic for updating an existing store 
*                 record in the Game Store Management System. It loads the 
*                 selected store’s data into the form, allows the user to modify 
*                 the values, and saves the updates back into the database.
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Google.Protobuf.WellKnownTypes;

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
        
        /// <summary>
        /// Loads the selected store’s data from the DataGrid into the form
        /// input fields.If no store is selected or the ID is invalid, an
        /// error message is shown and the form closes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    UpdateStoreTitle.Text += " #" + storeID.ToString();
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
       
        /// <summary>
        /// Validates the input, searches for the store in the Store table, 
        /// and updates it with the new values entered by the user.Displays
        /// an error message if the store cannot be found.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateStoreButton_Click(object sender, RoutedEventArgs e)
        {
            bool found = false;
            if (StoreNameInput.Text != "")
            {
                for (int i = 0; i < db.Store.Rows.Count && !found; i++)
                {
                    DataRow currentRow = db.Store.Rows[i];
                    if (Convert.ToInt32(currentRow["store_id"]) == storeID)
                    {
                        currentRow["location"] = StoreNameInput.Text;
                        found = true;

                        MessageBox.Show("Store ID #" + storeID.ToString() + " has been updated.\nClick \"Save\" to save changes to database", "Success");
                    }
                }
                if (!found)
                {
                    MessageBox.Show("Store could not be updated.", "Error");
                }
            }
        }
        
        /// <summary>
        /// Closes the form without applying any changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
