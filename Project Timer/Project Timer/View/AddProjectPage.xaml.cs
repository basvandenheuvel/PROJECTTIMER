using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Project_Timer.View
{
    public partial class AddProjectPage : PhoneApplicationPage
    {
        public AddProjectPage()
        {
            InitializeComponent();
        }

        private void saveButtonClicked(object sender, EventArgs e)
        {
            //Temp
            MessageBox.Show("Project toevoegen: " + txt_Name.Text + " - " + txt_Description.Text + " - " + date_Deadline.Value.ToString() + " - " + txt_Client.Text);

            //Save the new project in the database

            //Redirect to the project page of the new project
        }

        private void cancelButtonClicked(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}