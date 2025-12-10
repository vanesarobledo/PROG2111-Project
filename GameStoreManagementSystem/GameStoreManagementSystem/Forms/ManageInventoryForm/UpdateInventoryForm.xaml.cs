/*
* FILE          : UpdateInventoryForm.xaml.cs
* PROJECT       : PROG2111 Project
* PROGRAMMER    : Vanesa Robledo
* FIRST VERSION : 2025-12-08
* DESCRIPTION   : This is an application provides the logic for updating an existing inventory 
*                 record in the Game Store Management System. It loads the selected 
*                 inventory item, fills the form fields, validates user input, and 
*                 updates the corresponding row in the Inventory table.
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
    /// Interaction logic for UpdateInventory.xaml
    /// </summary>
    public partial class UpdateInventoryForm : Window
    {
        /// <summary>
        /// Select games database from Main window
        /// </summary>
        internal GamesDatabase db = ((MainWindow)Application.Current.MainWindow).gamesDatabase;

        /// <summary>
        /// Select DataGrid from Main menu
        /// </summary>
        internal DataGrid grid = ((MainWindow)Application.Current.MainWindow).MainGrid;

        /// <summary>
        /// Inventory columns
        /// </summary>
        internal int inventoryID;
        internal int gameID = 0;
        internal int consoleID = 0;
        internal int quantity;
        internal int storeID;

        public UpdateInventoryForm()
        {
            InitializeComponent();

            // Load data
            LoadForeignKeys();
        }

        /*
        * With help from:
        * TITLE : “Select cell value from DataGrid.ItemsSource”
        * AUTHOR : Mastersord
        * DATE : 2018-02-26
        * AVAILABIILTY : https://www.reddit.com/r/csharp/comments/7xcxa5/select_cell_value_from_datagriditemssource/duutmqj/
        */
        /// <summary>
        /// Autofills with selected data (or closes if no row is selected)
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
                    inventoryID = (int)dv.Row.ItemArray[0];
                    if (dv.Row.ItemArray[1] != DBNull.Value)
                    {
                        gameID = (int)dv.Row.ItemArray[1];
                    }
                    if (dv.Row.ItemArray[2] != DBNull.Value)
                    {
                        consoleID = (int)dv.Row.ItemArray[2];
                    }
                    quantity = (int)dv.Row.ItemArray[3];
                    storeID = (int)dv.Row.ItemArray[4];

                    // Fill in values
                    UpdateTitle.Text += " #" + inventoryID.ToString();
                    if (gameID != 0)
                    {
                        GameSelect.SelectedValue = gameID;
                    }
                    if (consoleID != 0)
                    {
                        ConsoleSelect.SelectedValue = consoleID;
                    }
                    InputQuantity.Text = quantity.ToString();
                    StoreSelect.SelectedValue = storeID;
                }
                else
                {
                    MessageBox.Show("Please save data to assign inventory ID.", "Error");
                    this.Close();
                }
            }
            // If no item is selected
            else
            {
                MessageBox.Show("Please select an Inventory item to update.", "Error");
                this.Close();
            }
        }

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
        /// Updates inventory row upon successful validation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            // Get values from form
            gameID = 0;
            consoleID = 0;
            storeID = 0;

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
            string quantityStr = InputQuantity.Text;

            // Validate values
            if (((gameID == 0) != (consoleID == 0))) // thank you Lanny for telling me how to XOR <3
            {
                MessageBox.Show("Game or console must be selected", "Error");
            }
            // Values are valid
            else if (Validation.ValidateInventoryValues(db, gameID, consoleID, quantityStr, storeID))
            {
                // Set quantity if validated
                quantity = Int32.Parse(quantityStr);

                // Update row in DataTable
                bool found = false;
                // Find row to update
                for (int i = 0; i < db.Inventory.Rows.Count && !found; i++)
                {
                    DataRow currentRow = db.Inventory.Rows[i];
                    // If row is found
                    if (Convert.ToInt32(currentRow["inventory_id"]) == inventoryID)
                    {
                        // Update data
                        if (gameID != 0)
                        {
                            currentRow["game_id"] = gameID;
                        }
                        if (consoleID != 0)
                        {
                            currentRow["console_id"] = consoleID;
                        }
                        currentRow["quantity"] = quantity;
                        currentRow["store_id"] = storeID;

                        // Show success
                        MessageBox.Show("Inventory item ID #" + inventoryID.ToString() + " has been updated.\nClick \"Save\" to save changes to database", "Inventory Added");
                        found = true;
                    }
                }
                // If inventory item is not found
                if (!found)
                {
                    MessageBox.Show("Inventory item could not be updated.", "Error");
                }

            }
        }

        /// <summary>
        /// Closes Update Inventory window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
