using SQLiteWinRT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project_Timer.Model
{
    class Projects
    {
        public async Task<List<Project>> getProjects()
        {
            List<Project> projects = new List<Project>();
            String query = "Select * FROM project";
            Statement statement = await DatabaseConnection.Db.PrepareStatementAsync(query);
            while (await statement.StepAsync())
            {
                MessageBox.Show("test");
                //projects.Add(new Project(Convert.ToInt32(statement.GetTextAt(0))));
            }
            return projects;
        }

        public void addProject(String name, String description, String deadline = null, String client = null)
        {
            Project project = new Project(name, description, deadline, client);
            project.saveToDB();
        }
    }
}
