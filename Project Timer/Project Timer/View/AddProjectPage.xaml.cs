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
using Project_Timer.ViewModel;

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
            //Get the viewmodel
            AddProjectPageViewModel vm = (AddProjectPageViewModel)LayoutRoot.DataContext;

            //Call the save method in the viewModel
            vm.saveProject(txt_Name.Text, txt_Description.Text, txt_Client.Text, date_Deadline.Value);
        }

        private void cancelButtonClicked(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}