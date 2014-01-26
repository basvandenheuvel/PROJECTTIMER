﻿using System;
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
        //Project id
        private int projectId;

        private Boolean newProject = true;
        private Boolean firstTimeNavigatedTo = true;

        //Viewmodel
        private AddProjectPageViewModel vm;

        public AddProjectPage()
        {
            InitializeComponent();
        }

        //Method triggerd when navigated to this page
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            //Set the viewmodel
            vm = (AddProjectPageViewModel)LayoutRoot.DataContext;

            if (NavigationContext.QueryString.ContainsKey("id"))
            {
                projectId = Int32.Parse(NavigationContext.QueryString["id"]);
                
                //Set the project id in the viewmodel
                vm.ProjectId = projectId;

                //Fill the view elements with the correct information
                getData();

                newProject = false;
                firstTimeNavigatedTo = false;
            }
        }

        private void getData()
        {
            if (firstTimeNavigatedTo)
            {
                txt_Name.Text = vm.Name;
                txt_Description.Text = vm.Description;
                txt_Client.Text = "" + vm.Client;
                date_Deadline.Value = vm.Deadline;
            }
        }

        private void saveButtonClicked(object sender, EventArgs e)
        {
            if (newProject)
            {
                //Call the save method in the viewModel
                vm.saveProject(txt_Name.Text, txt_Description.Text, txt_Client.Text, date_Deadline.Value);
            }
            else
            {
                //Call the update method in the viewmodel
                vm.updateProject(txt_Name.Text, txt_Description.Text, txt_Client.Text, date_Deadline.Value);
                NavigationService.GoBack();
            }
        }

        private void cancelButtonClicked(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}