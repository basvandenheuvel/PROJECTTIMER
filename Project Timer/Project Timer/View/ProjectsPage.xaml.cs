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

namespace Project_Timer
{
    public partial class ProjectsPage : PhoneApplicationPage
    {
        // Constructor
        public ProjectsPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void AboutClicked(object sender, EventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                App.RootFrame.Navigate(new Uri("/View/AboutPage.xaml?key=3", UriKind.RelativeOrAbsolute));

            });
        }

        private void AddProjectClicked(object sender, EventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                App.RootFrame.Navigate(new Uri("/View/AddProjectPage.xaml", UriKind.RelativeOrAbsolute));

            });
        }
    }
}