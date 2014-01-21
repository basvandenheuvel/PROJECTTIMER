﻿using Project_Timer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project_Timer.ViewModel
{
    public class ProjectsPageViewModel 
    {
        //Collection of projects
        private ObservableCollection<Project> projects;

        //Commands
        private DelegateCommand aboutButtonCommand;
        private DelegateCommand settingsButtonCommand;

        public ProjectsPageViewModel()
        {
            projects = new ObservableCollection<Project>();

            createCommands();
        }

        public void refreshProjects()
        {
            Projects.Clear();

            foreach (var s in DatabaseConnection.conn.Table<Project>())
            {
                Projects.Add(s);
            }
        }

        public void deleteProject(Project project)
        {
            //TODO: TESTEN!!!!

            //Delete all worktime belonging to the project
            DatabaseConnection.conn.Query<Project>( "DELETE " +
                                                    "FROM Worktime " +
                                                    "WHERE task_id IN (SELECT id FROM Task WHERE project_id = " + project.id + ")");

            //Delete all tasks belonging to the project
            DatabaseConnection.conn.Query<Project>( "DELETE " +
                                                    "FROM Task " +
                                                    "WHERE project_id = " + project.id);

            //Delete the project
            DatabaseConnection.conn.Query<Project>( "DELETE " +
                                                    "FROM Project " +
                                                    "WHERE id = " + project.id);

            Projects.Remove(project);
        }

        #region properties
        public ObservableCollection<Project> Projects
        {
            get { return projects; }
        }
        #endregion

        #region ICommands
        private void createCommands()
        {
            aboutButtonCommand = new DelegateCommand(aboutButton);
            settingsButtonCommand = new DelegateCommand(settingsButton);
        }

        //ICommands
        public ICommand AboutButtonCommand 
        {
            get { return aboutButtonCommand; } 
        }

        public ICommand SettingsButtonCommand 
        {
            get { return settingsButtonCommand; }  
        }

        //Methods used in ICommands
        public void aboutButton()
        {
            MessageBox.Show("About clicked");
        }

        public void settingsButton()
        {
            MessageBox.Show("Settings clicked");
        }
        #endregion

    }
}
