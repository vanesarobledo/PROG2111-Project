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

namespace GameStoreManagementSystem.Forms.ManageGamesForm
{
    /// <summary>
    /// This form allows the user to update an existing game record.
    /// </summary>
    public partial class UpdateGamesForm : Window
    {
        //The DataRow representing the game being edited
        private DataRow _row;

        // Shared GamesDatabase instance
        internal GamesDatabase _db = ((MainWindow)Application.Current.MainWindow).gamesDatabase;

        public UpdateGamesForm(DataRow row)
        {
            InitializeComponent();
            _row = row;

            LoadConsoles();
            LoadRowData();
        }

        /// <summary>
        /// Loads all consoles into the ComboBox for selection.
        /// </summary>
        private void LoadConsoles()
        {
            ConsoleSelect.SelectedValuePath = "console_id";
            ConsoleSelect.DisplayMemberPath = "console_name";

            foreach (DataRow row in _db.Console.Rows)
            {
                ConsoleSelect.Items.Add(row);
            }
        }

        /// <summary>
        /// Fills the form fields with the selected row's existing data.
        /// </summary>
        private void LoadRowData()
        {
            InputGameName.Text = _row["title"].ToString();
            InputGenre.Text = _row["genre"].ToString();
            InputDeveloper.Text = _row["developer"].ToString();

            //Handle release date
            if (_row["release_date"] != DBNull.Value)
            {
                DateTime date = (DateTime)_row["release_date"];
                InputReleaseDate.Text = date.ToString("yyyy-MM-dd");
            }
            else
            {
                InputReleaseDate.Text = "";
            }

            // Pre-select the console
            int consoleId = (int)_row["console_id"];
            ConsoleSelect.SelectedValue = consoleId;
        }

        /// <summary>
        /// Cancels the changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Updates the selected existing data 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = true;

            string title = InputGameName.Text.Trim();
            string genre = InputGenre.Text.Trim();
            string developer = InputDeveloper.Text.Trim();
            string releaseDateText = InputReleaseDate.Text.Trim();

            // Validate text fields
            if (!ValidateInputs(title, genre, developer))
            {
                isValid = false;
            }

            // Validate console selection
            int consoleId = 0;
            if (ConsoleSelect.SelectedItem == null)
            {
                MessageBox.Show("Please select a console.");
                isValid = false;
            }
            else
            {
                DataRow selectedConsole = (DataRow)ConsoleSelect.SelectedItem;
                consoleId = (int)selectedConsole["console_id"];
            }

            // Validate release date
            DateTime releaseDate = DateTime.MinValue;
            if (isValid)
            {
                if (!DateTime.TryParse(releaseDateText, out releaseDate))
                {
                    MessageBox.Show("Invalid release date.");
                    isValid = false;
                }
            }

            if (isValid)
            {
                //Update DataRow with new values
                _row["title"] = title;
                _row["genre"] = genre;
                _row["developer"] = developer;
                _row["release_date"] = releaseDate;

                MessageBox.Show("Game updated successfully!\nClick Save to apply changes to the database.",
                                "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                this.Close();
            }

            return;
        }

        /// <summary>
        /// Validates required input fields.
        /// </summary>
        private bool ValidateInputs(string title, string genre, string developer)
        {
            bool valid = true;
            int minLength = 1;

            if (!Validation.IsValidString(title, minLength))
            {
                MessageBox.Show("Title is invalid.");
                valid = false;
            }

            if (!Validation.IsValidString(genre, minLength))
            {
                MessageBox.Show("Genre is invalid.");
                valid = false;
            }

            if (!Validation.IsValidString(developer, minLength))
            {
                MessageBox.Show("Developer is invalid.");
                valid = false;
            }

            return valid;
        }
    }
}
