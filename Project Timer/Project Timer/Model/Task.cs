using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Timer.Model
{
    public class Task
    {
        //Database object
        TaskTable tt;

        /// <summary>
        /// Start creating a new task
        /// </summary>
        public Task()
        {
            tt = new TaskTable();
        }

        /// <summary>
        /// Load an existing task
        /// </summary>
        /// <param name="projectId">ID of the task to load from the database</param>
        public Task(int taskId)
        {
            tt = DatabaseConnection.conn.Query<TaskTable>("SELECT * FROM TaskTable WHERE id = " + taskId)[0];
        }

        public List<Task> getSessions()
        {
            throw new NotImplementedException("Unfinished method");
        }

        #region database update/save/delete
        public void save()
        {
            if (tt.id > 0)
            {
                updateTask();
            }
            else
            {
                saveNewTask();
            }
        }

        private void updateTask()
        {
            DatabaseConnection.conn.Update(tt);
        }

        private void saveNewTask()
        {
            DatabaseConnection.conn.Insert(tt);
        }

        public void deleteTask()
        {
            //TODO: testen!!!!

            //Delete all worktime belonging to the project
            DatabaseConnection.conn.Query<ProjectTable>("DELETE " +
                                                        "FROM SessionTable " +
                                                        "WHERE task_id  = " + Id);

            //Delete all tasks belonging to the project
            DatabaseConnection.conn.Query<ProjectTable>("DELETE " +
                                                        "FROM TaskTable " +
                                                        "WHERE id = " + Id);
        }
        #endregion

        #region properties
        public int Id 
        {
            get { return tt.id; }
        }
        public String Name
        {
            get { return tt.name; }
            set { tt.name = value; }
        }
        public String Description
        {
            get { return tt.description; }
            set { tt.description = value; }
        }
        public int ProjectId
        {
            get { return tt.project_id; }
            set { tt.project_id = value; }
        }
        public Boolean Finished
        {
            get { return tt.finished; }
            set { tt.finished = value; }
        }
        #endregion
    }
}
