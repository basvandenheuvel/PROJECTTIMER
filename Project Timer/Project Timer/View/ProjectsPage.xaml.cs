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

namespace Project_Timer
{
    public partial class ProjectsPage : PhoneApplicationPage
    {
        // Constructor
        public ProjectsPage()
        {
            InitializeComponent();
            //List<Status> statuses = DatabaseConnection.conn.Table<Status>().ToList<Status>();
            //foreach (var s in statuses)
            //{
            //    MessageBox.Show("" + s);
            //}
        }

        private void AboutClicked(object sender, EventArgs e)
        {
            App.RootFrame.Navigate(new Uri("/View/AboutPage.xaml?key=3", UriKind.RelativeOrAbsolute));
        }

        private void AddProjectClicked(object sender, EventArgs e)
        {           
            App.RootFrame.Navigate(new Uri("/View/AddProjectPage.xaml", UriKind.RelativeOrAbsolute));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            //((ProjectsPageViewModel)DataContext).getProjects();

            ProjectsPageViewModel vm = (ProjectsPageViewModel)LayoutRoot.DataContext;
            vm.getProjects();            
        }
    }
}