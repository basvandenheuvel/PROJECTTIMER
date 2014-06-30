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
using System.Windows.Threading;
using System.Diagnostics;
using System.ComponentModel;

namespace Project_Timer.View
{
    public partial class AddSessionPage : PhoneApplicationPage
    {
        //Get the viewModel
        private AddSessionPageViewModel vm;

        //Task id
        private int taskId;

        private Boolean isStarted;
        private int timeInMinutes = 15;

        private ApplicationBarIconButton buttonSave;


        public AddSessionPage()
        {
            InitializeComponent();

            //Set the viewmodel of this view
            vm = (AddSessionPageViewModel)LayoutRoot.DataContext;


            buttonSave = (ApplicationBarIconButton)ApplicationBar.Buttons[0];

            isStarted = false;
        }

        //Method triggerd when navigated to this page
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.ContainsKey("tid"))
            {
                taskId = Int32.Parse(NavigationContext.QueryString["tid"]);

                vm.TaskId = taskId;               
            }
        }

        private void btn_Timer_Click(object sender, RoutedEventArgs e)
        {
            if (!buttonSave.IsEnabled)
            {
                buttonSave.IsEnabled = true;
            }

            if (vm.MaxTimeNotReached)
            {
                if (!isStarted)
                {
                    vm.StartTimer();
                    isStarted = true;
                }
                else
                {
                    vm.StopTimer();
                    isStarted = false;
                }
            }            
        }

        private void saveButtonClicked(object sender, EventArgs e)
        {
            if (buttonSave.IsEnabled)
            {
                vm.StopTimer();
                vm.saveSession(txt_Description.Text);
                NavigationService.GoBack();
            }
        }

        private void cancelButtonClicked(object sender, EventArgs e)
        {
            cancelNewSession();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            e.Cancel = true;
            cancelNewSession();
        }

        private void cancelNewSession()
        {
            if (buttonSave.IsEnabled)
            {
                if (MessageBox.Show("Are you sure you want to cancel the new session?", "Confirm cancel",
                                        MessageBoxButton.OKCancel) != MessageBoxResult.Cancel)
                {
                    vm.StopTimer();
                    NavigationService.GoBack();
                }
            }
            else
            {
                vm.StopTimer();
                NavigationService.GoBack();
            }
        }

        private void plusTimerClicked(object sender, RoutedEventArgs e)
        {
            vm.AddTime(timeInMinutes);

            if (!buttonSave.IsEnabled)
            {
                buttonSave.IsEnabled = true;
            }
        }

        private void substractTimerClicked(object sender, RoutedEventArgs e)
        {
            vm.SubstractTime(timeInMinutes);
        }
    }
}