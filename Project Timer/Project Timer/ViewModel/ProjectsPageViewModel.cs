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
        private Projects projectsModel;

        private ObservableCollection<Project> projectsInProgress; //Collection of projects in progress
        private ObservableCollection<Project> projectsFinished; //Collection of projects that are finished

        public ProjectsPageViewModel()
        {
            projectsInProgress = new ObservableCollection<Project>();
            projectsFinished = new ObservableCollection<Project>();
            projectsModel = new Projects();
        }

        public void refreshProjects()
        {
            //Projects in progress
            ProjectsInProgress.Clear();

            foreach (Project project in projectsModel.getUnfinishedProjects())
            {
                ProjectsInProgress.Add(project);
            }

            //Finished projects
            ProjectsFinished.Clear();

            foreach (Project project in projectsModel.getFinishedProjects())
            {
                ProjectsFinished.Add(project);
            }
        }

        public void deleteProject(Project project)
        {
            project.deleteProject();

            if (project.Finished)
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
            if (project.Finished)
            {
                project.Finished = false;
                project.save();

                refreshProjects();
            }
            else
            {
                project.Finished = true;
                project.save();

                refreshProjects();
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
