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
    public class AddTaskPageViewModel
    {
        //Project id
        private int projectId;
        private int taskId;

        private Projects projectsModel;
        private Project projectModel;
        private Tasks tasksModel;
        private Model.Task taskModel;

        public AddTaskPageViewModel()
        {
            projectsModel = new Projects();
            tasksModel = new Tasks();
            taskModel = new Model.Task();
        }

        public void saveTask(String name, String description)
        {
            if (checkRequiredFields(name))
            {
                if (description.Length < 1)
                    description = null;

                //Create new task
                taskModel = tasksModel.addTask(name, description, projectModel.Id);
            }
        }

        public void updateTask(String name, String description)
        {
            if (checkRequiredFields(name))
            {
                if (description.Length < 1)
                    description = null;

                //Update the task
                taskModel.Name = name;
                taskModel.Description = description;
                taskModel.save();
            }
        }

        public Boolean checkRequiredFields(String name)
        {
            //Saving not allowed; Name and description must be filled in
            if (String.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Task can't be saved. The task 'name' is required.");
                return false;
            }
            return true;
        }

        #region properties
        public String ProjectName
        {
            get { return projectModel.Name; }
        }
        public int ProjectId
        {
            get { return projectId; }
            set 
            { 
                projectId = value;
                projectModel = projectsModel.getProject(value);
            }
        }
        public int TaskId
        {
            get { return taskId; }
            set
            {
                taskId = value;
                taskModel = tasksModel.getTask(value);
            }
        }
        public String taskName
        {
            get { return taskModel.Name; }
        }
        public String taskDescription
        {
            get { return taskModel.Description; }
        }
        #endregion
    }
}
