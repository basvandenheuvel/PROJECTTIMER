using Project_Timer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Timer.ViewModel
{
    public class TaskInfoPageViewModel : INotifyPropertyChanged
    {

        private Tasks tasksModel;
        private Project_Timer.Model.Task taskModel;

        private int taskId;
        private String status;

        public TaskInfoPageViewModel()
        {
            tasksModel = new Tasks();
        }

        public void loadData()
        {
            taskModel = tasksModel.getTask(taskId);
            OnPropertyChanged("Name");
            OnPropertyChanged("Description");

            if (taskModel.Finished) { Status = "Finished"; } else { Status = "In progress"; }
            OnPropertyChanged("Status");
        }

        #region properties
        public int TaskId
        {
            get { return taskId; }
            set 
            { 
                taskId = value; 
                loadData();
            }
        }
        public String Name
        {
            get { return taskModel.Name; }
        }
        public String Description
        {
            get { return taskModel.Description; }
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
