using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Timer.Model
{
    public class Projects
    {

        public Project getProject(int projectId)
        {
            return new Project(projectId);
        }

        public List<Project> getAllProjects()
        {
            List<Project> projectList = new List<Project>();
            foreach(ProjectTable pt in DatabaseConnection.conn.Query<ProjectTable>("SELECT * FROM ProjectTable"))
            {
                projectList.Add(new Project(pt.id));
            }
            return projectList;
        }

        public List<Project> getFinishedProjects()
        {
            List<Project> projectList = new List<Project>();
            foreach (ProjectTable pt in DatabaseConnection.conn.Query<ProjectTable>("SELECT * FROM ProjectTable WHERE finished = 1"))
            {
                projectList.Add(new Project(pt.id));
            }
            return projectList;
        }

        public List<Project> getUnfinishedProjects()
        {
            List<Project> projectList = new List<Project>();
            foreach (ProjectTable pt in DatabaseConnection.conn.Query<ProjectTable>("SELECT * FROM ProjectTable WHERE finished = 0"))
            {
                projectList.Add(new Project(pt.id));
            }
            return projectList;
        }

        public Project addProject(String name, String description, DateTime? deadline = null, String client = "")
        {
            Project newProject = new Project();
            newProject.Name = name;
            newProject.Description = description;
            newProject.Deadline = deadline;
            newProject.Client = client;
            newProject.Finished = false;
            newProject.save();

            return newProject;
        }

        public void deleteProject(int projectId)
        {
            new Project(projectId).deleteProject();
        }

    }
}
