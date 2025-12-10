/*
* FILE          : AddStoreForm.xaml.cs
* PROJECT       : PROG2111 Project
* PROGRAMMER    : Eumee Garcia, Vanesa Robledo, Connar Thompson
* FIRST VERSION : 2025-12-09
* DESCRIPTION   : This file the logic for adding a new store record 
*                 to the Game Store Management System. It gathers input from 
*                 the user and inserts a new store entry into the Store table.
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
    /// Interaction logic for AddStore.xaml
    /// </summary>

    /*
    * NAME      : AddStore
    * Purpose   : The form to add an store to the database.
    */
    public partial class AddStore : Window
    {

        internal GamesDatabase db = ((MainWindow)Application.Current.MainWindow).gamesDatabase;
        public AddStore()
        {
            InitializeComponent();
        }

        /// <summary>
        ///  Creates a new store record using the user-provided input and inserts it into 
        /// the Store table. Displays an error message if no location is entered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddStoreButton_Click(object sender, RoutedEventArgs e)
        {
            DataRow newRow = db.Store.NewRow();

            if (StoreNameInput.Text != "")
            { 
                newRow["location"] = StoreNameInput.Text;
                db.Store.Rows.Add(newRow);
                MessageBox.Show("Store successfully added.\nClick \"Save\" to save changes to database", "Success");
            }
            else
            {
                MessageBox.Show("Must enter a location.", "Error");
            }
        }

        /// <summary>
        /// Closes the Add Store form without saving any new store data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
