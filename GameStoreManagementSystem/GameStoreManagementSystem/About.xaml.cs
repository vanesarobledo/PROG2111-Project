/*
* FILE          : About.xaml.cs
* PROJECT       : PROG2111 Project
* PROGRAMMER    : Eumee Garcia
* FIRST VERSION : 2025-12-08
* DESCRIPTION   : This window shows the about window.
*/

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

namespace GameStoreManagementSystem
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
        }

        private void CloseAbout_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
