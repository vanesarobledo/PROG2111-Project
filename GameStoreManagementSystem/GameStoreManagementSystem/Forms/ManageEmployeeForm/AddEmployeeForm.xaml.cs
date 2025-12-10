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

namespace GameStoreManagementSystem.Forms.ManageEmployeeForm
{
    /// <summary>
    /// Interaction logic for AddEmployeeForm.xaml
    /// </summary>
    public partial class AddEmployeeForm : Window
    {
        internal GamesDatabase db = ((MainWindow)Application.Current.MainWindow).gamesDatabase;
        public AddEmployeeForm()
        {
            InitializeComponent();
            LoadForeignKeys();
        }

        internal void LoadForeignKeys()
        {
            foreach (DataRow store in db.Store.Rows)
            {
                int storeID = (int)store["store_id"];
                string storeDisplay = store["store_id"].ToString() + ": " + store["location"];
                storeIDBox.Items.Add(new KeyValuePair<int, string>(storeID, storeDisplay));
            }
        }

        internal bool emptyCheck()
        {
            bool retValue = false;
            
            if (firstNameBox != null && lastNameBox != null && dobBox != null && emailBox !=null
                && usernameBox != null && passwordBox != null && storeIDBox.Text != null)
            {
                retValue = true;
            }
            return retValue;
        }
        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            DataRow newRow = db.Employee.NewRow();
            if (emptyCheck())
            {
                newRow["first_name"] = firstNameBox.Text;
                newRow["last_name"] = lastNameBox.Text;
                newRow["date_of_birth"] = dobBox.Text;
                newRow["email"] = emailBox.Text;
                newRow["username"] = usernameBox.Text;
                newRow["password"] = passwordBox.Text;
                newRow["store_id"] = storeIDBox.Text;
                db.Employee.Rows.Add(newRow);
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
