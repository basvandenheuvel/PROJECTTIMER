﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project_Timer.Model
{
    public class Project
    {
        //Database object
        ProjectTable pt;

        /// <summary>
        /// Start creating a new project
        /// </summary>
        public Project()
        {
            pt = new ProjectTable();
        }

        /// <summary>
        /// Load an existing project
        /// </summary>
        /// <param name="projectId">ID of the project to load from the database</param>
        public Project(int projectId)
        {
            pt = DatabaseConnection.conn.Query<ProjectTable>("SELECT * FROM ProjectTable WHERE id = " + projectId)[0];
        }

        public List<Task> getTasks()
        {
            throw new NotImplementedException("Unfinished method");
        }

        #region database update/save/delete
        public void save()
        {
            if (pt.id > 0)
            {
                updateProject();
            }
            else
            {
                saveNewProject();
            }
        }

        private void updateProject()
        {
            DatabaseConnection.conn.Update(pt);
        }

        private void saveNewProject()
        {
            DatabaseConnection.conn.Insert(pt);
        }

        public void deleteProject()
        {
            //TODO: TESTEN!!!!
            //Delete all worktime belonging to the project
            DatabaseConnection.conn.Query<ProjectTable>("DELETE " +
                                                    "FROM SessionTable " +
                                                    "WHERE task_id IN (SELECT id FROM Task WHERE project_id = " + Id + ")");

            //Delete all tasks belonging to the project
            DatabaseConnection.conn.Query<ProjectTable>("DELETE " +
                                                    "FROM TaskTable " +
                                                    "WHERE project_id = " + Id);

            //Delete the project
            DatabaseConnection.conn.Query<ProjectTable>("DELETE " +
                                                    "FROM ProjectTable " +
                                                    "WHERE id = " + Id);
        }
        #endregion

        #region properties
        public int Id 
        {
            get { return pt.id; }
        }
        public String Name
        {
            get { return pt.name; }
            set { pt.name = value; }
        }
        public String Description
        {
            get { return pt.description; }
            set { pt.description = value; }
        }
        public String Client
        {
            get { return pt.client; }
            set { pt.client = value; }
        }
        public DateTime? Deadline
        {
            get { return pt.deadline; }
            set { pt.deadline = value; }
        }
        public Boolean Finished
        {
            get { return pt.finished; }
            set { pt.finished = value; }
        }
        #endregion
    }
}