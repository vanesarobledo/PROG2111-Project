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
                DataRowView dv = ((DataRowView)grid.Items[grid.SelectedIndex]);
                // Get values from DataGrid
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
                UpdateTitle.Text += "#" + inventoryID.ToString();
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
                int gameID = (int)game["game_id"];
                string gameDisplay = game["game_id"].ToString() + ": " + game["title"];
                GameSelect.Items.Add(new KeyValuePair<int, string>(gameID, gameDisplay));
            }

            // Auto-populate ConsoleSelect with consoles
            ConsoleSelect.SelectedValuePath = "Key";
            ConsoleSelect.DisplayMemberPath = "Value";
            foreach (DataRow console in db.Console.Rows)
            {
                int consoleID = (int)console["console_id"];
                string consoleDisplay = console["console_id"].ToString() + ": " + console["console_name"];
                ConsoleSelect.Items.Add(new KeyValuePair<int, string>(consoleID, consoleDisplay));
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

        private void Update_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
