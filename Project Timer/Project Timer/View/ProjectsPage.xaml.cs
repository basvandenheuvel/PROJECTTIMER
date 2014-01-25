using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Project_Timer.Resources;
using Project_Timer.Model;
using Project_Timer.ViewModel;
using System.Windows.Controls.Primitives;

namespace Project_Timer
{
    public partial class ProjectsPage : PhoneApplicationPage
    {
        //Get the viewModel
        private ProjectsPageViewModel vm;

        // Constructor
        public ProjectsPage()
        {
            InitializeComponent();

            //Set the viewmodel of this view
            vm = (ProjectsPageViewModel)LayoutRoot.DataContext;
        }

        private void AboutClicked(object sender, EventArgs e)
        {
            App.RootFrame.Navigate(new Uri("/View/AboutPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void SettingsClicked(object sender, EventArgs e)
        {
            App.RootFrame.Navigate(new Uri("/View/SettingsPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void AddProjectClicked(object sender, EventArgs e)
        {           
            App.RootFrame.Navigate(new Uri("/View/AddProjectPage.xaml", UriKind.RelativeOrAbsolute));
        }
        
        //Method triggerd when navigated to this page
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            //Reload the project list
            refreshProjects();  

            //Set default pivot page
            mainPivot.SelectedIndex = 0;
        }

        private void deleteProjectClicked(object sender, RoutedEventArgs e)
        {
            //Get the project
            Project project = (Project)((MenuItem)sender).DataContext;
            
            //Prompt the user if he/she is sure 
            MessageBoxResult mbr = MessageBox.Show("Are you sure you want to delete the project " + project.Name + "?", "Delete project?", MessageBoxButton.OKCancel);
            
            if (mbr == MessageBoxResult.OK)
            {
                //Delete project
                vm.deleteProject(project);

                if (project.Finished)
                {
                    checkAmountOfFinishedProjects();
                }
                else
                {
                    checkAmountOfProjects();
                }
            }
        }

        private void projectClicked(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Project project = (Project)((Grid)sender).DataContext;
            
            //Open taskpage
            App.RootFrame.Navigate(new Uri("/View/TasksPage.xaml?id="+ project.Id, UriKind.RelativeOrAbsolute));
        }


        private void refreshProjects()
        {
            vm.refreshProjects();

            checkAmountOfProjects();
            checkAmountOfFinishedProjects();
        }

        private void checkAmountOfProjects()
        {
            //If there are 0 projects, show error message
            if (vm.ProjectsInProgress.Count == 0)
            {
                ErrorMessage.Visibility = Visibility.Visible;
            }
            else
            {
                ErrorMessage.Visibility = Visibility.Collapsed;
            }
        }

        private void checkAmountOfFinishedProjects()
        {
            //If there are 0 finished projects, show error message
            if (vm.ProjectsFinished.Count == 0)
            {
                ErrorMessageFinished.Visibility = Visibility.Visible;
            }
            else
            {
                ErrorMessageFinished.Visibility = Visibility.Collapsed;
            }
        }

        private void toggleFinished(object sender, RoutedEventArgs e)
        {
            //Get the project
            Project project = (Project)((MenuItem)sender).DataContext;

            //Mark the project as finished
            vm.toggleFinished(project);

            checkAmountOfFinishedProjects();
            checkAmountOfProjects();
        }
    }
}