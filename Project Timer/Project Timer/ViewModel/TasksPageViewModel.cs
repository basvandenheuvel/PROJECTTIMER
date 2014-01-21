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

        #region properties
        public ObservableCollection<Project_Timer.Model.Task> Tasks
        {
            get { return tasks; }
        }

        public int ProjectId
        {
            set { projectId = value; }
        }
        #endregion
    }
}
