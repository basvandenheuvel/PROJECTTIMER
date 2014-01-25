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
        private int projectId;

        private ProjectTable project;

        public void saveProject(String name, String description, String client, DateTime? deadline)
        {
            //Saving not allowed; Name and description must be filled in
            if (checkRequiredFields(name, description))
            {
                //Saving allowed; Default status is 'In progress'

                //Create a new project
                ProjectTable newProject = new ProjectTable() { name = name, description = description, client = client, deadline = deadline, finished = false };
                //Save the new project
                DatabaseConnection.conn.Insert(newProject);

                //Redirect to the project page of the new project
                App.RootFrame.Navigate(new Uri("/View/TasksPage.xaml?id=" + newProject.id, UriKind.RelativeOrAbsolute));
                App.RootFrame.RemoveBackEntry();
            }
        }

        public void updateProject(String name, String description, String client, DateTime? deadline)
        {
            if (checkRequiredFields(name, description))
            {
                //Saving allowed; Default status is 'In progress'
                DatabaseConnection.conn.Query<ProjectTable>("UPDATE ProjectTable SET name = '" + name  + "', description = '" + description + "', client = '" + client + "', deadline = '" + deadline + "' WHERE id =" + projectId);
            }
        }

        private Boolean checkRequiredFields(String name, String description)
        {
            //Saving not allowed; Name and description must be filled in
            if (String.IsNullOrWhiteSpace(name) || String.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Project can't be saved. The project name and description are required.");
                return false;
            }

            return true;
        }

        #region properties
        public int ProjectId
        {
            set
            { 
                projectId = value;
                project = DatabaseConnection.conn.Query<ProjectTable>("SELECT * FROM ProjectTable WHERE id =" + projectId)[0];
            }
        }
        public ProjectTable Project
        {
            get { return project; }
        }
        #endregion
    }
}
