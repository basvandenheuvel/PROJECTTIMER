﻿using System;
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
using System.Diagnostics;

namespace Project_Timer.View
{
    public partial class TasksPage : PhoneApplicationPage
    {
        //Get the viewModel
        private TasksPageViewModel vm;

        private int projectId;

        public TasksPage()
        {
            InitializeComponent();

            //Set the viewmodel of this view
            vm = (TasksPageViewModel)LayoutRoot.DataContext;
        }

        //Method triggerd when navigated to this page
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.ContainsKey("id"))
            {
                projectId = Int32.Parse(NavigationContext.QueryString["id"]);

                //Set the project id in the viewmodel
                vm.ProjectId = projectId;

                //Refresh the tasks
                refreshTasks();

                if (vm.Project.Finished)
                {
                    //Disable the add button if the project is finished
                    ApplicationBarIconButton buttonAdd = (ApplicationBarIconButton)ApplicationBar.Buttons[0];
                    buttonAdd.IsEnabled = false;

                    //Disable the edit project button if the project is finished
                    ApplicationBarMenuItem itemEdit = (ApplicationBarMenuItem)ApplicationBar.MenuItems[0];
                    itemEdit.IsEnabled = false;
                }
            }

        }

        private void AddTaskClicked(object sender, EventArgs e)
        {
            App.RootFrame.Navigate(new Uri("/View/AddTaskPage.xaml?pid=" + projectId, UriKind.RelativeOrAbsolute));
        }

        private void deleteTaskClicked(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (!vm.Project.Finished)
            {
                //Get the project
                Task task = (Task)((MenuItem)sender).DataContext;

                //Prompt the user if he/she is sure 
                MessageBoxResult mbr = MessageBox.Show("Are you sure you want to delete the task " + task.Name + "?", "Delete task?", MessageBoxButton.OKCancel);

                if (mbr == MessageBoxResult.OK)
                {
                    //Delete project
                    vm.deleteTask(task);

                    if (task.Finished)
                    {
                        checkAmountOfFinishedTasks();
                    }
                    else
                    {
                        checkAmountOfTasks();
                    }

                    RefreshContextMenu(sender);
                }
            }
        }
        
        private void refreshTasks()
        {
            vm.refreshTasks();

            checkAmountOfTasks();
            checkAmountOfFinishedTasks();

            columnChart.YAxis.Interval = vm.GraphInterval;
        }

        private void checkAmountOfTasks()
        {
            //If there are 0 tasks, show error message
            if (vm.Tasks.Count == 0)
            {
                ErrorMessage.Visibility = Visibility.Visible;
            }
            else
            {
                ErrorMessage.Visibility = Visibility.Collapsed;
            }
        }

        private void checkAmountOfFinishedTasks()
        {
            //If there are 0 tasks, show error message
            if (vm.FinishedTasks.Count == 0)
            {
                ErrorMessageFinished.Visibility = Visibility.Visible;
            }
            else
            {
                ErrorMessageFinished.Visibility = Visibility.Collapsed;
            }
        }

        private void editProjectClicked(object sender, EventArgs e)
        {
            App.RootFrame.Navigate(new Uri("/View/AddProjectPage.xaml?id=" + projectId, UriKind.RelativeOrAbsolute));
        }

        private void taskClicked(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //Get the task
            Task task = (Task)((Grid)sender).DataContext;

            App.RootFrame.Navigate(new Uri("/View/SessionsPage.xaml?pid=" + projectId + "&tid="+ task.Id, UriKind.RelativeOrAbsolute));
        }

        private void toggleFinished(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (!vm.Project.Finished)
            {
                //Get the task
                Task task = (Task)((MenuItem)sender).DataContext;

                //Mark the project as finished
                vm.toggleFinished(task);

                checkAmountOfFinishedTasks();
                checkAmountOfTasks();

                RefreshContextMenu(sender);
            }
        }

        private void projectInfoClicked(object sender, EventArgs e)
        {
            //Show project info page.
            App.RootFrame.Navigate(new Uri("/View/ProjectInfoPage.xaml?id=" + projectId, UriKind.RelativeOrAbsolute));
        }

        private void RefreshContextMenu(object sender)
        {
            ContextMenu cm = (ContextMenu)((MenuItem)sender).Parent;
            cm.ClearValue(FrameworkElement.DataContextProperty);
        }

        private void Grid_Hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (vm.Project.Finished)
            {
                e.Handled = true;
            }
        } 
    }
}