using Microsoft.Phone.Controls;
using Project_Timer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace Project_Timer.ViewModel
{
    public class AddProjectPageViewModel
    {
        private static int DEFAULT_STATUS_ID = 1;

        public void saveProject(String name, String description, String client, DateTime? deadline)
        {
            //Saving not allowed; Name and description must be filled in
            if (String.IsNullOrWhiteSpace(name) || String.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Project can't be saved. The project 'name' and 'description' are required.");
                return;
            }

            //Saving allowed; Default status is 'In progress'

            //Save the new project
            DatabaseConnection.conn.Insert(new Project() { name = name, description = description, client = client, deadline = deadline, status_id = DEFAULT_STATUS_ID });

            //Redirect to the project page of the new project
            //Vergeet GET (ID) niet, en maar 1 removebackentry wanneer projectpage gebruikt word
            App.RootFrame.Navigate(new Uri("/View/ProjectsPage.xaml", UriKind.RelativeOrAbsolute));
            App.RootFrame.RemoveBackEntry();
            App.RootFrame.RemoveBackEntry();
        }
    }
}
