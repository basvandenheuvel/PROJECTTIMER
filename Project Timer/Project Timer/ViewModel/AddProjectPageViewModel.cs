using Microsoft.Phone.Controls;
using Project_Timer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private Projects projectsModel;
        private Project projectModel;

        public AddProjectPageViewModel()
        {
            projectsModel = new Projects();
        }

        public void saveProject(String name, String description, String client, DateTime? deadline)
        {
            //Saving not allowed; Name and description must be filled in
            if (checkRequiredFields(name, description))
            {
                //Create new project
                projectModel = projectsModel.addProject(name, description, deadline, client);

                //Redirect to the project page of the new project
                App.RootFrame.Navigate(new Uri("/View/TasksPage.xaml?id=" + projectModel.Id, UriKind.RelativeOrAbsolute));
                App.RootFrame.RemoveBackEntry();
            }
        }

        public void updateProject(String name, String description, String client, DateTime? deadline)
        {
            if (checkRequiredFields(name, description))
            {
                //Saving allowed; Default status is 'In progress'
                projectModel.Name = name;
                projectModel.Description = description;
                projectModel.Client = client;
                projectModel.Deadline = deadline;

                projectModel.save();
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
                projectModel = projectsModel.getProject(value);
            }
        }
        public String Name
        {
            get { return projectModel.Name; }
        }
        public String Description
        {
            get { return projectModel.Description; }
        }
        public DateTime? Deadline
        {
            get { return projectModel.Deadline; }
        }
        public String Client
        {
            get { return projectModel.Client; }
        }
        #endregion
    }
}
