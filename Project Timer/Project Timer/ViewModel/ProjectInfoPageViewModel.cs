using Project_Timer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Timer.ViewModel
{
    public class ProjectInfoPageViewModel : INotifyPropertyChanged
    {
        private int projectId;
        private String name;
        private String description;
        private String client;
        private String deadline;
        private String status;

        public void getData()
        {
            ProjectTable project = DatabaseConnection.conn.Query<ProjectTable>("SELECT * FROM ProjectTable WHERE id = " + projectId)[0];
            Name = project.name;
            Description = project.description;
            Client = project.client;
            Deadline = project.deadline.ToString();

            if(project.finished)
            {
                Status = "Finished";
            }
            else 
            {
                Status = "In progress";
            }
        }

        #region properties
        public int ProjectId
        {
            get { return projectId; }
            set 
            { 
                projectId = value; 
                getData();
            }
        }
        public String Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public String Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }
        public String Client
        {
            get { return client; }
            set
            {
                client = value;
                OnPropertyChanged("Client");
            }
        }
        public String Deadline
        {
            get { return deadline; }
            set
            {
                deadline = value;
                OnPropertyChanged("Deadline");
            }
        }
        public String Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged("Status");
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
