using Project_Timer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Timer.ViewModel
{
    public class WorktimePageViewModel
    {
        //Collection of worktimes
        private ObservableCollection<Worktime> worktimes;   

        //Project id
        private int projectId;
        //Project name
        private String projectName;

        //Task id
        private int taskId;
        //Taks name
        private String taskName;

        //Title
        private String title;


        public WorktimePageViewModel()
        {
            worktimes = new ObservableCollection<Worktime>();
        }

        public void refreshWorktimes()
        {
            worktimes.Clear();

            foreach (var w in DatabaseConnection.conn.Query<Worktime>("SELECT * FROM Worktime WHERE task_id = " + taskId))
            {
                worktimes.Add(w);
            }
        }

        public void deleteWorktime(Worktime worktime)
        {
            //TODO: testen!!!!

            //Delete the worktime
            DatabaseConnection.conn.Query<Worktime>("DELETE " +
                                                    "FROM Worktime " +
                                                    "WHERE id  = " + worktime.id);

            worktimes.Remove(worktime);
        }


        #region properties
        public ObservableCollection<Worktime> Worktimes
        {
            get { return worktimes; }
        }
        public String Title
        {
            get { return title; }
            set {
                    title = value;
                    OnPropertyChanged("Title");
                    
                }
        }
        private String ProjectName
        {
            set
            {
                projectName = value;
                Title = projectName + " - " + taskName;
            }
        }
        private String TaskName
        {
            set
            {
                taskName = value;
                Title = projectName + " - " + taskName;
            }
        }
        public int ProjectId
        {
            get { return projectId; }
            set
            {
                projectId = value;
                ProjectName = DatabaseConnection.conn.Query<Project>("SELECT name FROM Project WHERE id =" + projectId)[0].name;
            }
        }
        public int TaskId
        {
            get { return taskId; }
            set
            {
                taskId = value;
                TaskName = DatabaseConnection.conn.Query<Project>("SELECT name FROM Task WHERE id =" + taskId)[0].name;
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
