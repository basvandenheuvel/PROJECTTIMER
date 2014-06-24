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

        private ObservableCollection<Project_Timer.Model.Task> tasks; //Collection of unfinished tasks
        private ObservableCollection<Project_Timer.Model.Task> finishedTasks; //Collection of finished tasks

        //Default project name
        private Projects projectsModel;
        private Project projectModel;

        //Amount of tasks a project has
        private int amountOfTasks;
        //Amount of finished tasks a project has
        private int amountOfFinishedTasks;
        //Total hours spend on this project
        private double totalHours;

        //Project id
        private int projectId;

        public TasksPageViewModel()
        {
            tasks = new ObservableCollection<Project_Timer.Model.Task>();
            finishedTasks = new ObservableCollection<Project_Timer.Model.Task>();
            projectsModel = new Projects();
        }

        public void refreshTasks()
        {
            projectModel = new Project(projectId);
            OnPropertyChanged("ProjectName");

            tasks.Clear();
            finishedTasks.Clear();

            //Tasks finished
            foreach (Model.Task task in projectModel.getUnfinishedTasks())
            {
                tasks.Add(task);
            }
            
            //Tasks in progress
            foreach (Model.Task task in projectModel.getFinishedTasks())
            {
                finishedTasks.Add(task);
            }

            AmountOfTasks = Tasks.Count;
            AmountOfFinishedTasks = FinishedTasks.Count;
        }

        public void deleteTask(Model.Task task)
        {
            task.deleteTask();

            if (task.Finished)
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

        public void toggleFinished(Project_Timer.Model.Task task)
        {
            if (task.Finished)
            {
                //Set the task to in progress
                task.Finished = false;
                task.save();

                refreshTasks();
            }
            else
            {
                //Set the task to finished
                task.Finished = true;
                task.save();
                
                refreshTasks();
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
            get { return projectModel.getAmountOfHours(); }
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
