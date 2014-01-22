using Project_Timer.Model;
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
        //Collection of projects in progress
        private ObservableCollection<Project> projectsInProgress;
        //Collection of projects that are finished
        private ObservableCollection<Project> projectsFinished;

        public ProjectsPageViewModel()
        {
            projectsInProgress = new ObservableCollection<Project>();
            projectsFinished = new ObservableCollection<Project>();
        }

        public void refreshProjects()
        {
            //Projects in progress
            ProjectsInProgress.Clear();

            foreach (var s in DatabaseConnection.conn.Query<Project>("SELECT * FROM Project WHERE finished = 0"))
            {
                ProjectsInProgress.Add(s);
            }

            //Finished projects
            ProjectsFinished.Clear();

            foreach (var s in DatabaseConnection.conn.Query<Project>("SELECT * FROM Project WHERE finished = 1"))
            {
                ProjectsFinished.Add(s);
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

            if (project.finished)
            {
                ProjectsFinished.Remove(project);
            }
            else
            {
                ProjectsInProgress.Remove(project);
            }
        }

        public void toggleFinished(Project project)
        {
            if (project.finished)
            {
                //Set the project to in progress
                DatabaseConnection.conn.Query<Project>("UPDATE Project SET finished = 0 WHERE id =" + project.id);

                project.finished = false;

                //Place the project in the other collection
                ProjectsFinished.Remove(project);
                ProjectsInProgress.Add(project);
            }
            else
            {
                //Set the project to finished
                DatabaseConnection.conn.Query<Project>("UPDATE Project SET finished = 1 WHERE id =" + project.id);

                project.finished = true;

                //Place the project in the other collection
                ProjectsInProgress.Remove(project);
                ProjectsFinished.Add(project);
            }
        }

        #region properties
        public ObservableCollection<Project> ProjectsInProgress
        {
            get { return projectsInProgress; }
        }
        public ObservableCollection<Project> ProjectsFinished
        {
            get { return projectsFinished; }
        }
        #endregion
    }
}
