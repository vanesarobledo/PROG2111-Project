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

namespace GameStoreManagementSystem.Forms.ManageProductsForm
{
    /// <summary>
    /// Interaction logic for UpdateProductForm.xaml
    /// </summary>
    public partial class UpdateProductForm : Window
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
        /// Key-value pairs of game IDs and their titles
        /// </summary>
        internal Dictionary<int, string> gamesList;

        /// <summary>
        /// Key-value pairs of console IDs and their names
        /// </summary>
        internal Dictionary<int, string> consolesList;

        /// <summary>
        /// Product Columns
        /// </summary>
        internal int productID = 0;
        int inventoryID = 0;
        int customerID = 0;
        int quantity;
        float cost;
        string dateOfPurchase;
        int storeID = 0;

        public UpdateProductForm()
        {
            InitializeComponent();

            // Load inventory 
            LoadInventory();

            // Auto-populate comboxes with data
            LoadForeignKeys();
        }

        /// <summary>
        /// Loads list of game titles and console names with their IDs as keys in dictionaries
        /// </summary>
        internal void LoadInventory()
        {
            gamesList = new Dictionary<int, string>();
            consolesList = new Dictionary<int, string>();

            foreach (DataRow game in db.Game.Rows)
            {
                gamesList.Add((int)game["game_id"], game["title"].ToString());
            }

            foreach (DataRow console in db.Console.Rows)
            {
                consolesList.Add((int)console["console_id"], console["console_name"].ToString());
            }
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
            // Auto-populate Inventory with games or consoles
            InventorySelect.SelectedValuePath = "Key";
            InventorySelect.DisplayMemberPath = "Value";
            foreach (DataRow inventory in db.Inventory.Rows)
            {
                int inventoryID = (int)inventory["inventory_id"];
                string inventoryDisplay = inventory["inventory_id"].ToString() + ": ";

                // Look up game title or console name to display in inventory
                if (inventory["game_id"] != DBNull.Value)
                {
                    if (gamesList.ContainsKey((int)inventory["game_id"]))
                    {
                        inventoryDisplay += gamesList[(int)inventory["game_id"]].ToString();
                    }
                }
                else if (inventory["console_id"] != DBNull.Value)
                {
                    if (consolesList.ContainsKey((int)inventory["console_id"]))
                    {
                        inventoryDisplay += consolesList[(int)inventory["console_id"]].ToString();
                    }
                }

                InventorySelect.Items.Add(new KeyValuePair<int, string>(inventoryID, inventoryDisplay));
            }

            // Auto-populate CustomerSelect with customers
            CustomerSelect.SelectedValuePath = "Key";
            CustomerSelect.DisplayMemberPath = "Value";
            foreach (DataRow customer in db.Customer.Rows)
            {
                int storeID = (int)customer["customer_id"];
                string consoleDisplay = customer["customer_id"].ToString() + ": " + customer["first_name"] + " " + customer["last_name"];
                CustomerSelect.Items.Add(new KeyValuePair<int, string>(storeID, consoleDisplay));
            }

            // Auto-populate StoreSelect with stores
            StoreSelect.SelectedValuePath = "Key";
            StoreSelect.DisplayMemberPath = "Value";
            foreach (DataRow store in db.Store.Rows)
            {
                int storeID = (int)store["store_id"];
                string consoleDisplay = store["store_id"].ToString() + ": " + store["location"];
                StoreSelect.Items.Add(new KeyValuePair<int, string>(storeID, consoleDisplay));
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Get selected item
            if (grid.SelectedIndex != -1)
            {
                // Get values from DataGrid
                DataRowView dv = ((DataRowView)grid.Items[grid.SelectedIndex]);
                productID = (int)dv.Row.ItemArray[0];
                inventoryID = (int)dv.Row.ItemArray[1];
                customerID = (int)dv.Row.ItemArray[2];
                cost = (float)dv.Row.ItemArray[3];
                dateOfPurchase = dv.Row.ItemArray[4].ToString();
                quantity = (int)dv.Row.ItemArray[5];
                storeID = (int)dv.Row.ItemArray[6];

                // Fill in values
                UpdateTitle.Text += " #" + productID.ToString();
                InputQuantity.Text = quantity.ToString();
                InputCost.Text = cost.ToString();
                DateOfPurchaseInput.Text = dateOfPurchase;
                InventorySelect.SelectedValue = inventoryID;
                CustomerSelect.SelectedValue = customerID;
                StoreSelect.SelectedValue = storeID;
            }
            // If no item is selected
            else
            {
                MessageBox.Show("Please select a Product item to update.", "Error");
                this.Close();
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
        /// Only allow positive numeric input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Validation.IsPositiveNum(e.Text);
        }

        /// <summary>
        /// Only allow positive numeric input as a float
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputCost_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Validation.IsFloat(e.Text);
        }
    }
}
