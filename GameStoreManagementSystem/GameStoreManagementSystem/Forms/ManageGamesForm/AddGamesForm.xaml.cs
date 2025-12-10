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

namespace GameStoreManagementSystem.Forms
{

    /// <summary>
    /// Interaction logic for AddGamesForm.xaml
    /// This window allows the user to add a new game record to the in-memory DataTable.
    /// </summary>
    public partial class AddGamesForm : Window
    {

        /// <summary>
        /// Reference to the shared GamesDatabase instance created inside MainWindow.
        /// </summary>
        internal GamesDatabase _db = ((MainWindow)Application.Current.MainWindow).gamesDatabase;

        public AddGamesForm()
        {
            InitializeComponent();

            //Load list of consoles into ComboBox on startup
            LoadConsoles();
        }

        /// <summary>
        /// Close the Add Game window when Cancel is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Validates inputs, checks console selection, parses date,
        /// and inserts a new game row into the Game table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = true;

            //Exctract input values from textboxes
            string title = InputGameName.Text.Trim();
            string genre = InputGenre.Text.Trim();
            string developer = InputDeveloper.Text.Trim();
            string releaseDateText = InputReleaseDate.Text.Trim();

            //Validate text fields
            if (!ValidateInputs(title, genre, developer))
            {
                isValid = false;
            }

            int consoleId = 0;

            //Ensure user selected a console
            if (ConsoleSelect.SelectedItem == null)
            {
                MessageBox.Show("Please select a console.");
                isValid = false;
            }
            else
            {
                DataRowView view = (DataRowView)ConsoleSelect.SelectedItem;

                consoleId = view["console_id"] == DBNull.Value
                    ? 0
                    : Convert.ToInt32(view["console_id"]);
            }

            // Validate that console exists in the Console table (Foreign Key Check)
            if (!_db.ConsoleIDExists(consoleId))
            {
                MessageBox.Show("The selected Console ID does not exist. Cannot add game.",
                                "Foreign Key Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }

            DateTime releaseDate = DateTime.MinValue;


            //Parse invalid date only if previous input are valid
            if (isValid)
            {
                if (!DateTime.TryParse(releaseDateText, out releaseDate))
                {
                    MessageBox.Show("Invalid release date.");
                    isValid = false;
                }
                else
                {
                    releaseDate = releaseDate.Date;
                }
            }

            //If everything is valid, insert new game
            if (isValid)
            {
                //Create new row inside the Game DataTable
                DataRow row = _db.Game.NewRow();

                //Assign column values
                row["title"] = title;
                row["genre"] = genre;
                row["developer"] = developer;
                row["release_date"] = releaseDate;
                row["console_id"] = consoleId;

                //Add the row to the DataTable
                _db.Game.Rows.Add(row);

                MessageBox.Show("Game added.\nClick Save to apply changes to the database.");
                this.Close();
            }

            return;
        }

        /// <summary>
        /// Validates required text fields for non-empty inputs.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="genre"></param>
        /// <param name="developer"></param>
        /// <returns></returns>
        private bool ValidateInputs(string title, string genre, string developer)
        {
            bool isValid;
            isValid = true;

            int minLength;
            minLength = 1;

            if (!Validation.IsValidString(title, minLength))
            {
                MessageBox.Show("Title is invalid.");
                isValid = false;
            }

            if (!Validation.IsValidString(genre, minLength))
            {
                MessageBox.Show("Genre is invalid.");
                isValid = false;
            }

            if (!Validation.IsValidString(developer, minLength))
            {
                MessageBox.Show("Developer is invalid.");
                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// Loads all consoles from the Console DataTable and 
        /// inserts them into the ComboBox for selection.
        /// </summary>
        private void LoadConsoles()
        {
            if (_db.Console == null)
            {
                return;
            }

            ConsoleSelect.ItemsSource = _db.Console.DefaultView;

            ConsoleSelect.DisplayMemberPath = "console_name";
            ConsoleSelect.SelectedValuePath = "console_id";
        }
    }
}
