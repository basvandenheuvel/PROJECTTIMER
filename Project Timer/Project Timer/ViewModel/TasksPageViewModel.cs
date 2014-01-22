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
    public class TasksPageViewModel
    {
        //Collection of tasks
        private ObservableCollection<Project_Timer.Model.Task> tasks;

        //Default project name
        private String projectName = "Project Timer";

        //Project id
        private int projectId;

        public TasksPageViewModel()
        {
            tasks = new ObservableCollection<Project_Timer.Model.Task>();
        }

        public void refreshTasks()
        {
            tasks.Clear();

            foreach (var t in DatabaseConnection.conn.Query<Project_Timer.Model.Task>("SELECT * FROM Task WHERE project_id = " + projectId))
            {
                tasks.Add(t);
            }
        }

        public void deleteTask(Model.Task task)
        {
            //Delete all worktime belonging to the project
            DatabaseConnection.conn.Query<Project>( "DELETE " +
                                                    "FROM Worktime " +
                                                    "WHERE task_id  = " + task.id);

            //Delete all tasks belonging to the project
            DatabaseConnection.conn.Query<Project>( "DELETE " +
                                                    "FROM Task " +
                                                    "WHERE id = " + task.id);
            tasks.Remove(task);
        }

        #region properties
        public ObservableCollection<Project_Timer.Model.Task> Tasks
        {
            get { return tasks; }
        }
        public String ProjectName
        {
            get { return projectName; }
        }
        public int ProjectId
        {
            set { 
                    projectId = value;
                    projectName = DatabaseConnection.conn.Query<Project>("SELECT name FROM Project WHERE id =" + projectId)[0].name;
                }
        }
        #endregion
    }
}
