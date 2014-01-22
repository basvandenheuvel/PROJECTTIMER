using Project_Timer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project_Timer.ViewModel
{
    public class AddTaskPageViewModel
    {
        private ObservableCollection<Status> statuses = new ObservableCollection<Status>();

        //Default project name
        private String projectName = "Project Timer";

        //Project id
        private int projectId;

        public AddTaskPageViewModel()
        {
            //Add all statuses to the observable collection
            getStatuses();
        }

        private void getStatuses()
        {
            foreach(var s in DatabaseConnection.conn.Table<Status>())
            {
                statuses.Add(s);
            }
        }

        public void saveTask(String name, String description, int status_id, int project_id)
        {
            //Saving not allowed; Name and description must be filled in
            if (String.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Task can't be saved. The task 'name' is required.");
                return;
            }

            //Saving allowed

            //Save the new project
            DatabaseConnection.conn.Insert(new Project_Timer.Model.Task() { name = name, description = description, status_id = status_id, project_id = project_id  });

            //Redirect to the project page of the new project
            App.RootFrame.Navigate(new Uri("/View/TasksPage.xaml?id="+ project_id, UriKind.RelativeOrAbsolute));
            App.RootFrame.RemoveBackEntry();
            App.RootFrame.RemoveBackEntry();
        }

        #region properties
        public ObservableCollection<Status> Statuses
        {
            get { return statuses; }
        }
        public String ProjectName
        {
            get { return projectName; }
        }
        public int ProjectId
        {
            get { return projectId; }
            set { 
                    projectId = value;
                    projectName = DatabaseConnection.conn.Query<Project>("SELECT name FROM Project WHERE id =" + projectId)[0].name;
                }
        }
        #endregion
    }
}
