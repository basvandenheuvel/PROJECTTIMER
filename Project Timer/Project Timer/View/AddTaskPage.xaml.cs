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
    public partial class AddTaskPage : PhoneApplicationPage
    {
        //Project id
        private int projectId;

        public AddTaskPage()
        {
            InitializeComponent();
        }

        //Method triggerd when navigated to this page
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.ContainsKey("id"))
            {
                projectId = Int32.Parse(NavigationContext.QueryString["id"]);
            }
        }

        private void saveButtonClicked(object sender, EventArgs e)
        {

        }

        private void cancelButtonClicked(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}