using Project_Timer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project_Timer.ViewModel
{
    public class TasksPageViewModel : INotifyPropertyChanged 
    {
        //Collection of tasks
        private ObservableCollection<Project_Timer.Model.Task> tasks;
        
        //Collection of finished tasks
        private ObservableCollection<Project_Timer.Model.Task> finishedTasks;

        //Default project name
        private String projectName = "Project Timer";

        //Project id
        private int projectId;

        public TasksPageViewModel()
        {
            tasks = new ObservableCollection<Project_Timer.Model.Task>();
            finishedTasks = new ObservableCollection<Project_Timer.Model.Task>();
        }

        public void refreshTasks()
        {
            //Tasks in progress
            tasks.Clear();

            foreach (var t in DatabaseConnection.conn.Query<Project_Timer.Model.Task>("SELECT * FROM Task WHERE project_id = " + projectId))
            {
                tasks.Add(t);
            }

            //Finished tasks
            finishedTasks.Clear();

            foreach (var t in DatabaseConnection.conn.Query<Project_Timer.Model.Task>("SELECT * FROM Task WHERE project_id = " + projectId + " AND finished = 1"))
            {
                finishedTasks.Add(t);
            }
        }

        public void deleteTask(Model.Task task)
        {
            //TODO: testen!!!!

            //Delete all worktime belonging to the project
            DatabaseConnection.conn.Query<Project>( "DELETE " +
                                                    "FROM Worktime " +
                                                    "WHERE task_id  = " + task.id);

            //Delete all tasks belonging to the project
            DatabaseConnection.conn.Query<Project>( "DELETE " +
                                                    "FROM Task " +
                                                    "WHERE id = " + task.id);

            if (task.finished)
            {
                finishedTasks.Remove(task);
            }
            else
            {
                tasks.Remove(task);
            }
        }

        public void toggleFinished(Project_Timer.Model.Task task)
        {
            if (task.finished)
            {
                //Set the task to in progress
                DatabaseConnection.conn.Query<Project>("UPDATE Task SET finished = 0 WHERE id =" + task.id);

                task.finished = false;

                //Place the task in the other collection
                FinishedTasks.Remove(task);
                Tasks.Add(task);
            }
            else
            {
                //Set the task to finished
                DatabaseConnection.conn.Query<Project>("UPDATE Task SET finished = 1 WHERE id =" + task.id);

                task.finished = true;

                //Place the project in the other collection
                Tasks.Remove(task);
                FinishedTasks.Add(task);
            }
        }

        #region properties
        public ObservableCollection<Project_Timer.Model.Task> Tasks
        {
            get { return tasks; }
        }
        public ObservableCollection<Project_Timer.Model.Task> FinishedTasks
        {
            get { return finishedTasks; }
        }
        public String ProjectName
        {
            get { return projectName; }
            set { 
                    projectName = value;  
                    OnPropertyChanged("ProjectName"); 
                }
        }
        public int ProjectId
        {
            set { 
                    projectId = value;
                    ProjectName = DatabaseConnection.conn.Query<Project>("SELECT name FROM Project WHERE id =" + projectId)[0].name;
                }
        }
        #endregion

        #region propertychanged
        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event 
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion
    }
}
