/*
* FILE          : AddInventoryForm.xaml.cs
* PROJECT       : PROG2111 Project
* PROGRAMMER    : Vanesa Robledo
* FIRST VERSION : 2025-12-08
* DESCRIPTION   : This is an application provides the logic for adding new inventory items 
*                 into the Game Store Management System. It loads foreign key 
*                 data, validates user input, and inserts a new inventory record 
*                 into the Inventory table.
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

namespace GameStoreManagementSystem.Forms.ManageInventoryForm
{
    /// <summary>
    /// Interaction logic for AddInventoryForm.xaml
    /// </summary>
    public partial class AddInventoryForm : Window
    {

        /// <summary>
        /// Select games database from Main window
        /// </summary>
        internal GamesDatabase db = ((MainWindow)Application.Current.MainWindow).gamesDatabase;

        public AddInventoryForm()
        {
            InitializeComponent();

            // Auto-populate foreign key fields
            LoadForeignKeys();
        }

        /*
        * With help from:
        * TITLE : “WPF combobox value and display text”
        * AUTHOR : Alexander Abakumov
        * DATE : 2016-01-08
        * AVAILABIILTY : https://stackoverflow.com/a/34679195
        */

        /// <summary>
        /// Auto-populates the comboboxes with foreign key data
        /// </summary>
        internal void LoadForeignKeys()
        {
            // Auto-populate GameSelect with games
            GameSelect.SelectedValuePath = "Key";
            GameSelect.DisplayMemberPath = "Value";
            foreach (DataRow game in db.Game.Rows)
            {
                if (game["game_id"] != DBNull.Value)
                {
                    int gameID = (int)game["game_id"];
                    string gameDisplay = game["game_id"].ToString() + ": " + game["title"];
                    GameSelect.Items.Add(new KeyValuePair<int, string>(gameID, gameDisplay));
                }
            }

            // Auto-populate ConsoleSelect with consoles
            ConsoleSelect.SelectedValuePath = "Key";
            ConsoleSelect.DisplayMemberPath = "Value";
            foreach (DataRow console in db.Console.Rows)
            {
                if (console["console_id"] != DBNull.Value)
                {
                    int consoleID = (int)console["console_id"];
                    string consoleDisplay = console["console_id"].ToString() + ": " + console["console_name"];
                    ConsoleSelect.Items.Add(new KeyValuePair<int, string>(consoleID, consoleDisplay));
                }
            }

            // Auto-populate StoreSelect with stores
            StoreSelect.SelectedValuePath = "Key";
            StoreSelect.DisplayMemberPath = "Value";
            foreach (DataRow store in db.Store.Rows)
            {
                if (store["store_id"] != DBNull.Value)
                {
                    int storeID = (int)store["store_id"];
                    string consoleDisplay = store["store_id"].ToString() + ": " + store["location"];
                    StoreSelect.Items.Add(new KeyValuePair<int, string>(storeID, consoleDisplay));
                }
            }
        }

        /*
        * With help from:
        * TITLE : “WPF TextBox Only Accepts Numbers”
        * AUTHOR : Mike
        * DATE : 2020-05-16
        * VERSION : 1.0
        * AVAILABIILTY : https://begincodingnow.com/wpf-textbox-only-accepts-numbers/
        */
        /// <summary>
        /// Only allow numeric input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Validation.IsNum(e.Text);
        }

        // Mutually exclusive comboboxes - deselect console if game is selected, and vice versa

        /// <summary>
        /// Deselects console when game is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ConsoleSelect.SelectedIndex = -1;
        }

        /// <summary>
        /// Delects game when console is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConsoleSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GameSelect.SelectedIndex = -1;
        }

        /// <summary>
        /// Adds inventory item after validation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            // Get values from form
            int gameID = 0;
            int consoleID = 0;
            int storeID = 0;

            if (ConsoleSelect.SelectedValue != null)
            {
                consoleID = (int)ConsoleSelect.SelectedValue;
            }
            if (GameSelect.SelectedValue != null)
            {
                gameID = (int)GameSelect.SelectedValue;
            }
            if (StoreSelect.SelectedValue != null)
            {
                storeID = (int)StoreSelect.SelectedValue;
            }
            string quantity = InputQuantity.Text;

            // Validate values
            if (gameID == 0 && consoleID == 0)
            {
                MessageBox.Show("Game or console must be selected", "Error");
            }
            else if (gameID != 0 && consoleID !=0)
            {
                MessageBox.Show("Only one of game or console must be selected", "Error");
            }
            // Values are valid
            else if (Validation.ValidateInventoryValues(db, gameID, consoleID, quantity, storeID))
            {
                // Add row to DataTable
                DataRow newRow = db.Inventory.NewRow();
                if (gameID != 0)
                {
                    newRow["game_id"] = gameID;
                }
                else if (consoleID != 0)
                {
                    newRow["console_id"] = consoleID;
                }
                newRow["quantity"] = quantity;
                newRow["store_id"] = storeID;
                db.Inventory.Rows.Add(newRow);

                // Show success
                MessageBox.Show("Inventory item successfully added.\nClick \"Save\" to save changes to database", "Inventory Added");
            }
        }

        /// <summary>
        /// Closes the Add Inventory form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
