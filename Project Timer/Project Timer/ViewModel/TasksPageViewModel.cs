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

        private ObservableCollection<Project_Timer.Model.TaskTable> tasks; //Collection of unfinished tasks
        private ObservableCollection<Project_Timer.Model.TaskTable> finishedTasks; //Collection of finished tasks

        //Default project name
        private Projects projectsModel;
        private Project projectModel;

        //Amount of tasks a project has
        private int amountOfTasks;
        //Amount of finished tasks a project has
        private int amountOfFinishedTasks;
        //Total hours spend on this project
        private double totalHours = 12.5;

        //Project id
        private int projectId;

        public TasksPageViewModel()
        {
            tasks = new ObservableCollection<Project_Timer.Model.TaskTable>();
            finishedTasks = new ObservableCollection<Project_Timer.Model.TaskTable>();
            projectsModel = new Projects();
        }

        public void refreshTasks()
        {
            projectModel = new Project(projectId);
            OnPropertyChanged("ProjectName"); 

            //Tasks in progress
            tasks.Clear();
            AmountOfTasks = 0;

            foreach (var t in DatabaseConnection.conn.Query<Project_Timer.Model.TaskTable>("SELECT * FROM TaskTable WHERE project_id = " + projectId + " AND finished = 0"))
            {
                tasks.Add(t);
                AmountOfTasks++;
            }

            //Finished tasks
            finishedTasks.Clear();
            AmountOfFinishedTasks = 0;

            foreach (var t in DatabaseConnection.conn.Query<Project_Timer.Model.TaskTable>("SELECT * FROM TaskTable WHERE project_id = " + projectId + " AND finished = 1"))
            {
                finishedTasks.Add(t);
                AmountOfFinishedTasks++;
            }
        }

        public void deleteTask(Model.TaskTable task)
        {
            //TODO: testen!!!!

            //Delete all worktime belonging to the project
            DatabaseConnection.conn.Query<ProjectTable>( "DELETE " +
                                                    "FROM SessionTable " +
                                                    "WHERE task_id  = " + task.id);

            //Delete all tasks belonging to the project
            DatabaseConnection.conn.Query<ProjectTable>( "DELETE " +
                                                    "FROM TaskTable " +
                                                    "WHERE id = " + task.id);

            if (task.finished)
            {
                finishedTasks.Remove(task);
                AmountOfFinishedTasks--;
            }
            else
            {
                tasks.Remove(task);
                AmountOfTasks--;
            }
        }

        public void toggleFinished(Project_Timer.Model.TaskTable task)
        {
            if (task.finished)
            {
                //Set the task to in progress
                DatabaseConnection.conn.Query<TaskTable>("UPDATE TaskTable SET finished = 0 WHERE id =" + task.id);

                task.finished = false;

                //Place the task in the other collection
                FinishedTasks.Remove(task);
                Tasks.Add(task);

                AmountOfTasks++;
                AmountOfFinishedTasks--;
            }
            else
            {
                //Set the task to finished
                DatabaseConnection.conn.Query<TaskTable>("UPDATE TaskTable SET finished = 1 WHERE id =" + task.id);

                task.finished = true;

                //Place the project in the other collection
                Tasks.Remove(task);
                FinishedTasks.Add(task);

                AmountOfTasks--;
                AmountOfFinishedTasks++;
            }
        }

        #region properties
        public ObservableCollection<Project_Timer.Model.TaskTable> Tasks
        {
            get { return tasks; }
        }
        public ObservableCollection<Project_Timer.Model.TaskTable> FinishedTasks
        {
            get { return finishedTasks; }
        }
        public int AmountOfTasks
        {
            get { return amountOfTasks; }
            set {
                    amountOfTasks = value;
                    OnPropertyChanged("AmountOfTasks"); 
                }
        }
        public int AmountOfFinishedTasks
        {
            get { return amountOfFinishedTasks; }
            set
            {
                amountOfFinishedTasks = value;
                OnPropertyChanged("AmountOfFinishedTasks");
            }
        }
        public double TotalHours
        {
            get { return totalHours; }
            set
            {
                totalHours = value;
                OnPropertyChanged("TotalHours");
            }
        }
        public String ProjectName
        {
            get { return projectModel.Name; }
        }
        public int ProjectId
        {
            get { return projectId; }
            set { projectId = value; }
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
