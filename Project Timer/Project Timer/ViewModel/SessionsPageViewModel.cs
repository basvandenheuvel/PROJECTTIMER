using Project_Timer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Timer.ViewModel
{
    public class SessionsPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Session> sessions;   

        private int projectId;
        private int taskId;

        private String title;

        private Model.Task taskModel;
        private Project projectModel;


        public SessionsPageViewModel()
        {
            sessions = new ObservableCollection<Session>();
        }

        public void refreshWorktimes()
        {
            sessions.Clear();

            foreach (Session session in taskModel.getSessions())
            {
                sessions.Add(session);
            }
        }

        public void deleteSession(Session session)
        {
            //TODO: testen!!!!

            //Delete the worktime
            DatabaseConnection.conn.Query<SessionTable>("DELETE " +
                                                    "FROM SessionTable " +
                                                    "WHERE id  = " + session.Id);

            sessions.Remove(session);
        }


        #region properties
        public ObservableCollection<Session> Sessions
        {
            get { return sessions; }
        }
        public String Title
        {
            get { return title; }
            set {
                    title = value;
                    OnPropertyChanged("Title");                    
                }
        }
        public int ProjectId
        {
            get { return projectId; }
            set
            {
                projectId = value;
                projectModel = new Project(projectId);
            }
        }
        public int TaskId
        {
            get { return taskId; }
            set
            {
                taskId = value;
                taskModel = new Model.Task(taskId);
                Title = projectModel.Name + " - " + taskModel.Name;
            }
        }
        public Project Project
        {
            get { return projectModel; }
        }
        public Project_Timer.Model.Task Task
        {
            get { return taskModel; }
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
