using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Project_Timer.Model;
using Microsoft.Phone.Tasks;

namespace Project_Timer.View
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void deleteDataClicked(object sender, RoutedEventArgs e)
        {
            //Prompt the user if he/she is sure 
            MessageBoxResult mbr = MessageBox.Show("All data will be removed. Are you sure you want to delete all data?", "Warning!", MessageBoxButton.OKCancel);

            if (mbr == MessageBoxResult.OK)
            {
                //Delete project
                DatabaseConnection.emptyDatabase(true);
            }
        }
    }
}