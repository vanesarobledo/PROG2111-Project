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
        public AddInventoryForm()
        {
            InitializeComponent();

            // Auto-populate foreign key fields
            LoadForeignKeys();
        }

        internal void LoadForeignKeys()
        {
            // Select games database
            GamesDatabase db = ((MainWindow)Application.Current.MainWindow).gamesDatabase;

            // Auto-populate games
            GameSelect.SelectedValuePath = "Key";
            GameSelect.DisplayMemberPath = "Value";

            foreach (DataRow game in db.Game.Rows)
            {
                int gameID = (int)game["game_id"];
                string gameDisplay = game["game_id"].ToString() + ": " + game["title"];
                GameSelect.Items.Add(new KeyValuePair<int, string>(gameID, gameDisplay));
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

        private void Add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
