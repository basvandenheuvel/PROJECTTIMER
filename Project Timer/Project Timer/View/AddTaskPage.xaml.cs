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
        //Project id
        private int projectId;
        private int taskId;
        private Boolean newTask = true;

        //ViewModel
        private AddTaskPageViewModel vm;

        public AddTaskPage()
        {
            InitializeComponent();

            //Get the viewmodel
            vm = (AddTaskPageViewModel)LayoutRoot.DataContext;
        }

        //Method triggerd when navigated to this page
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.ContainsKey("pid"))
            {
                projectId = Int32.Parse(NavigationContext.QueryString["pid"]);
                vm.ProjectId = projectId;
            }

            if (NavigationContext.QueryString.ContainsKey("id"))
            {
                taskId = Int32.Parse(NavigationContext.QueryString["id"]);
                vm.TaskId = taskId;
                newTask = false;
            }
        }

        private void saveButtonClicked(object sender, EventArgs e)
        {
            if (newTask)
            {
                //Call the save method in the viewModel
                vm.saveTask(txt_Name.Text, txt_Description.Text);
                NavigationService.GoBack();
            }
            else
            {
                //Call the update method in the viewModel
                vm.updateTask(txt_Name.Text, txt_Description.Text);
                NavigationService.GoBack();
            }
        }

        private void cancelButtonClicked(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}