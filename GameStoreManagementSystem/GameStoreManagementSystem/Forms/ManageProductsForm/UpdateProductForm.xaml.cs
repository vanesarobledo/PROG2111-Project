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
        /// Key-value pairs of store IDs and their locations
        /// </summary>
        internal Dictionary<int, string> storesList;

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
            storesList = new Dictionary<int, string>();

            foreach (DataRow game in db.Game.Rows)
            {
                gamesList.Add((int)game["game_id"], game["title"].ToString());
            }

            foreach (DataRow console in db.Console.Rows)
            {
                consolesList.Add((int)console["console_id"], console["console_name"].ToString());
            }

            foreach (DataRow store in db.Store.Rows)
            {
                storesList.Add((int)store["store_id"], store["location"].ToString());
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
                if (inventory["inventory_id"] != DBNull.Value)
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
                    if (inventory["store_id"] != DBNull.Value)
                    {
                        inventoryDisplay += " (" + storesList[(int)inventory["store_id"]].ToString() + ")";
                    }

                    InventorySelect.Items.Add(new KeyValuePair<int, string>(inventoryID, inventoryDisplay));
                }
            }

            // Auto-populate CustomerSelect with customers
            CustomerSelect.SelectedValuePath = "Key";
            CustomerSelect.DisplayMemberPath = "Value";
            foreach (DataRow customer in db.Customer.Rows)
            {
                if (customer["customer_id"] != DBNull.Value)
                {
                    int storeID = (int)customer["customer_id"];
                    string consoleDisplay = customer["customer_id"].ToString() + ": " + customer["first_name"] + " " + customer["last_name"];
                    CustomerSelect.Items.Add(new KeyValuePair<int, string>(storeID, consoleDisplay));
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

        /// <summary>
        /// Fills the values of the fields dynamically based on selection
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
                else
                {
                    MessageBox.Show("Please save data to assign product ID.", "Error");
                    this.Close();
                }

            }
            // If no item is selected
            else
            {
                MessageBox.Show("Please select a Product item to update.", "Error");
                this.Close();
            }
        }

        /// <summary>
        /// Updates a row in Product upon successful validation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            // Get values from form
            int inventoryID = 0;
            int customerID = 0;
            int quantity = -1;
            string quantityStr = InputQuantity.Text;
            float cost = -1;
            string costStr = InputCost.Text;
            string dateOfPurchase = DateOfPurchaseInput.Text;
            int storeID = 0;

            if (InventorySelect.SelectedValue != null)
            {
                inventoryID = (int)InventorySelect.SelectedValue;
            }

            if (CustomerSelect.SelectedValue != null)
            {
                customerID = (int)CustomerSelect.SelectedValue;
            }

            if (StoreSelect.SelectedValue != null)
            {
                storeID = (int)StoreSelect.SelectedValue;
            }

            // Validate values
            if (Validation.ValidateProductValues(db, inventoryID, customerID, costStr, quantityStr, dateOfPurchase, storeID))
            {
                // Set quantity & cost if validated
                quantity = Int32.Parse(quantityStr);
                cost = float.Parse(costStr);

                // Update row in DataTable
                bool found = false;
                // Find row to update
                for (int i = 0; i < db.Product.Rows.Count && !found; i++)
                {
                    DataRow currentRow = db.Product.Rows[i];
                    // If row is found
                    if (Convert.ToInt32(currentRow["product_id"]) == productID)
                    {
                        currentRow["inventory_id"] = inventoryID;
                        currentRow["customer_id"] = customerID;
                        currentRow["cost"] = cost;
                        currentRow["quantity"] = quantity;
                        currentRow["date_of_purchase"] = dateOfPurchase;
                        currentRow["store_id"] = storeID;

                        // Show success
                        MessageBox.Show("Product ID #" + productID.ToString() + " has been updated.\nClick \"Save\" to save changes to database", "Product Added");
                        found = true;
                    }
                }
                // If inventory item is not found
                if (!found)
                {
                    MessageBox.Show("Product item could not be updated.", "Error");
                }
            }
        }

        /// <summary>
        /// Closes the Update Product window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
