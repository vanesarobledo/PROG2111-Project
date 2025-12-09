using System;
using System.Collections.Generic;
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
        }

        private void InputQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }
    }
}
