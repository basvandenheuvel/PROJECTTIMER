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

        private Projects projectsModel;
        private Project projectModel;

        private int projectId;
        private String status;

        public ProjectInfoPageViewModel()
        {
            projectsModel = new Projects();
        }

        public void loadData()
        {
            projectModel = projectsModel.getProject(projectId);
            OnPropertyChanged("Name");
            OnPropertyChanged("Description");
            OnPropertyChanged("Client");
            OnPropertyChanged("Deadline");
            
            if(projectModel.Finished) { Status = "Finished"; } else  { Status = "In progress"; }
            OnPropertyChanged("Status");
        }

        #region properties
        public int ProjectId
        {
            get { return projectId; }
            set 
            { 
                projectId = value; 
                loadData();
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
        public String Client
        {
            get { return projectModel.Client; }
        }
        public String Deadline
        {
            get { return projectModel.Deadline.Value.ToShortDateString(); }
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
