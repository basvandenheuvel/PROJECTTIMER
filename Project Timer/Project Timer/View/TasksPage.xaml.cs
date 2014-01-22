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

                //Set default pivot page
                mainPivot.SelectedIndex = 0;
            }
        }

        private void AddTaskClicked(object sender, EventArgs e)
        {
            App.RootFrame.Navigate(new Uri("/View/AddTaskPage.xaml?id=" + projectId, UriKind.RelativeOrAbsolute));
        }

        private void deleteTaskClicked(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //Get the project
            Task task = (Task)((MenuItem)sender).DataContext;

            //Prompt the user if he/she is sure 
            MessageBoxResult mbr = MessageBox.Show("Are you sure you want to delete the task " + task.name + "?", "Delete task?", MessageBoxButton.OKCancel);

            if (mbr == MessageBoxResult.OK)
            {
                //Delete project
                vm.deleteTask(task);

                if (task.finished)
                {
                    checkAmountOfFinishedTasks();
                }
                else
                {
                    checkAmountOfTasks();
                }
            }
        }
        
        private void refreshTasks()
        {
            vm.refreshTasks();

            checkAmountOfTasks();
            checkAmountOfFinishedTasks();
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
            //Navigate to worktime view... daar controleren we of het PROJECT op finished staat, dan mag er NIKS worden toegevoegd
        }

        private void toggleFinished(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //Get the task
            Task task = (Task)((MenuItem)sender).DataContext;

            //Mark the project as finished
            vm.toggleFinished(task);

            checkAmountOfFinishedTasks();
            checkAmountOfTasks();
        }
    }
}