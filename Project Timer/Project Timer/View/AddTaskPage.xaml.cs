using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Project_Timer.ViewModel;
using Project_Timer.Model;

namespace Project_Timer.View
{
    public partial class AddTaskPage : PhoneApplicationPage
    {
        //ViewModel
        private AddTaskPageViewModel vm;

        //Project id
        private int projectId;

        public AddTaskPage()
        {
            InitializeComponent();

            //Get the viewmodel
            vm = (AddTaskPageViewModel)LayoutRoot.DataContext;
        }

        //Method triggerd when navigated to this page
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.ContainsKey("id"))
            {
                projectId = Int32.Parse(NavigationContext.QueryString["id"]);

                vm.ProjectId = projectId;
            }
        }

        private void saveButtonClicked(object sender, EventArgs e)
        {
            //Call the save method in the viewModel
            vm.saveTask(txt_Name.Text, txt_Description.Text, ((Status)lis_Status.SelectedItem).id, projectId);
        }

        private void cancelButtonClicked(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}