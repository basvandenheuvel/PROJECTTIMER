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
    public partial class SessionPage : PhoneApplicationPage
    {
        //Get the viewModel
        private SessionsPageViewModel vm;

        //Project id
        private int projectId;

        //Task id
        private int taskId;


        public SessionPage()
        {
            InitializeComponent();

            //Set the viewmodel of this view
            vm = (SessionsPageViewModel)LayoutRoot.DataContext;
        }

        //Method triggerd when navigated to this page
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.ContainsKey("pid"))
            {
                projectId = Int32.Parse(NavigationContext.QueryString["pid"]);

                if (NavigationContext.QueryString.ContainsKey("tid"))
                {
                    taskId = Int32.Parse(NavigationContext.QueryString["tid"]);
                }

                //Set the project id in the viewmodel
                vm.ProjectId = projectId;
                //Set the task id in the viewmodel
                vm.TaskId = taskId;

                //Refresh the worktimes
                refreshWorktimes();

                if (vm.Project.Finished || vm.Task.Finished)
                {
                    //Disable the add button if the project or task is finished
                    ApplicationBarIconButton buttonAdd = (ApplicationBarIconButton)ApplicationBar.Buttons[0];
                    buttonAdd.IsEnabled = false;

                    //Disable the edit project button if the project or task is finished
                    ApplicationBarMenuItem itemEdit = (ApplicationBarMenuItem)ApplicationBar.MenuItems[0];
                    itemEdit.IsEnabled = false;
                }
            }
        }

        private void refreshWorktimes()
        {
            vm.refreshWorktimes();

            checkAmountOfWorktimes();
        }

        private void checkAmountOfWorktimes()
        {
            //If there are 0 worktimes, show error message
            if (vm.Sessions.Count == 0)
            {
                ErrorMessage.Visibility = Visibility.Visible;
            }
            else
            {
                ErrorMessage.Visibility = Visibility.Collapsed;
            }
        }

        private void editTaskClicked(object sender, EventArgs e)
        {
            App.RootFrame.Navigate(new Uri("/View/AddTaskPage.xaml?pid=" + projectId + "&id=" + taskId, UriKind.RelativeOrAbsolute));
        }

        private void taskInfoClicked(object sender, EventArgs e)
        {
            //Show task info page.
            App.RootFrame.Navigate(new Uri("/View/TaskInfoPage.xaml?id=" + taskId, UriKind.RelativeOrAbsolute));
        }

        private void deleteSessionClicked(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (!vm.Project.Finished && !vm.Task.Finished)
            {
                //Get the worktime
                Session session = (Session)((MenuItem)sender).DataContext;

                //Prompt the user if he/she is sure 
                MessageBoxResult mbr = MessageBox.Show("Are you sure you want to delete the session?", "Delete session?", MessageBoxButton.OKCancel);

                if (mbr == MessageBoxResult.OK)
                {
                    //Delete project
                    vm.deleteSession(session);

                    checkAmountOfWorktimes();
                }
            }
        }

        private void addSessionClicked(object sender, EventArgs e)
        {
            App.RootFrame.Navigate(new Uri("/View/AddSessionPage.xaml?tid=" + taskId, UriKind.RelativeOrAbsolute));
        }

        private void Grid_Hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (vm.Project.Finished || vm.Task.Finished)
            {
                e.Handled = true;
            }
        } 
    }
}