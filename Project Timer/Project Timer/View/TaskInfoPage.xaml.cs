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

namespace Project_Timer.View
{
    public partial class TaskInfoPage : PhoneApplicationPage
    {
        private TaskInfoPageViewModel vm;
        private int taskId;

        public TaskInfoPage()
        {
            InitializeComponent();
            //Set the viewmodel of this view
            vm = (TaskInfoPageViewModel)LayoutRoot.DataContext;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.ContainsKey("id"))
            {
                taskId = Int32.Parse(NavigationContext.QueryString["id"]);

                //Set the project id in the viewmodel
                vm.TaskId = taskId;
            }
        }
    }
}