using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Timer.Model
{
    public class Tasks
    {
        public Task getTask(int taskId)
        {
            return new Task(taskId);
        }

        public List<Task> getAllTasks()
        {
            List<Task> taskList = new List<Task>();
            foreach (TaskTable tt in DatabaseConnection.conn.Query<TaskTable>("SELECT * FROM TaskTable"))
            {
                taskList.Add(new Task(tt.id));
            }
            return taskList;
        }

        public List<Task> getProjectTasks(int projectId)
        {
            List<Task> taskList = new List<Task>();
            foreach (TaskTable tt in DatabaseConnection.conn.Query<TaskTable>("SELECT * FROM TaskTable WHERE project_id = " + projectId))
            {
                taskList.Add(new Task(tt.id));
            }
            return taskList;
        }

        public List<Task> getFinishedTasks(int projectId)
        {
            List<Task> taskList = new List<Task>();
            foreach (TaskTable tt in DatabaseConnection.conn.Query<TaskTable>("SELECT * FROM TaskTable WHERE project_id = " + projectId + " AND finished = 1"))
            {
                taskList.Add(new Task(tt.id));
            }
            return taskList;
        }

        public List<Task> getUnfinishedTasks(int projectId)
        {
            List<Task> taskList = new List<Task>();
            foreach (TaskTable tt in DatabaseConnection.conn.Query<TaskTable>("SELECT * FROM TaskTable WHERE project_id = " + projectId + " AND finished = 0"))
            {
                taskList.Add(new Task(tt.id));
            }
            return taskList;
        }

        public void addTask(String name, String description, DateTime? deadline = null, String client = "")
        {
            Task newTask = new Task();
            newTask.Name = "";
            newTask.save();
        }

        public void deleteTask(int taskId)
        {
            new Task(taskId).deleteTask();
        }
    }
}
