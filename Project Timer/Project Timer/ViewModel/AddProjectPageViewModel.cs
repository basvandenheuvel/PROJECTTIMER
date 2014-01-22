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
        public void saveProject(String name, String description, String client, DateTime? deadline)
        {
            //Saving not allowed; Name and description must be filled in
            if (String.IsNullOrWhiteSpace(name) || String.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Project can't be saved. The project 'name' and 'description' are required.");
                return;
            }

            //Saving allowed; Default status is 'In progress'

            //Create a new project
            Project newProject = new Project() { name = name, description = description, client = client, deadline = deadline, finished = false };
            //Save the new project
            DatabaseConnection.conn.Insert(newProject);

            //Redirect to the project page of the new project
            App.RootFrame.Navigate(new Uri("/View/TasksPage.xaml?id=" + newProject.id, UriKind.RelativeOrAbsolute));
            App.RootFrame.RemoveBackEntry();
        }
    }
}
