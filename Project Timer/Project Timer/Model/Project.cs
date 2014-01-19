using SQLiteWinRT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Timer.Model
{
    public class Project : INotifyPropertyChanged
    {
        private int id;
        private String name;
        private String description;
        private String client;
        private String deadline;
        private int status_id;
        private String status;
        private int taskAmount;

        public event PropertyChangedEventHandler PropertyChanged;

        #region Constructors
        //Create new project.
        public Project(String name, String description, String deadline = null, String client = null)
        {
            this.id = -1;
            this.name = name;
            this.description = description;
            this.deadline = deadline;
            this.client = client;
            this.status_id = 1;
            this.status = "In progress";
            this.taskAmount = 0;
        }

        //Retrieve existing project information.
        public Project(int id)
        {
            loadFromDB(id);
        }
        #endregion

        private async void loadFromDB(int id)
        {
            String query = "Select * FROM project WHERE id = " + id;
            Statement statement = await DatabaseConnection.Db.PrepareStatementAsync(query);
            await statement.StepAsync();
            name = statement.GetTextAt(1);
            description = statement.GetTextAt(2);
            deadline = statement.GetTextAt(3);
            client = statement.GetTextAt(4);
            status_id = Convert.ToInt32(statement.GetTextAt(5));
        }

        #region save/update database
        //If the project has an ID, update the database record. If not, create new database record
        public void saveToDB()
        {
            if (id != -1)
            {
                dbUpdate();
            }
            else
            {
                dbInsert();
            }
        }

        private async void dbUpdate()
        {
            string query = "UPDATE project SET name = '" + name + "', description = '" + description + "', client = '" + client + "', deadline = '" + deadline + "', statuses_id = " + status_id + " WHERE id = " + id;
            await DatabaseConnection.Db.ExecuteStatementAsync(query);
        }

        private async void dbInsert()
        {
            string query = "INSERT INTO project (name, description, client, deadline, statuses_id) VALUES ('" + name + "','" + description + "', '" + client + "', '" + deadline + "', " + status_id + ")";
            await DatabaseConnection.Db.ExecuteStatementAsync(query);
        }
        #endregion

        #region properties
        public int ProjectID
        {
            get { return id; }
            set
            {
                id = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("ProjectID");
            }
        }

        public string ProjectName
        {
            get { return name; }
            set
            {
                name = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("ProjectName");
            }
        }

        public string ProjectDescription
        {
            get { return description; }
            set
            {
                description = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("ProjectDescription");
            }
        }

        public string ProjectDeadline
        {
            get { return deadline; }
            set
            {
                deadline = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("ProjectDeadline");
            }
        }

        public int ProjectStatus_id
        {
            get { return status_id; }
            set
            {
                status_id = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("ProjectStatusId");
            }
        }

        public String ProjectStatus
        {
            get { return status; }
            set
            {
                status = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("ProjectStatus");
            }
        }

        public int ProjectTasks
        {
            get { return taskAmount; }
            set
            {
                taskAmount = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("ProjectTasks");
            }
        }

        public int TaskAmount
        {
            get { return taskAmount; }
            set
            {
                taskAmount = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("ProjectTaskAmount");
            }
        }
        #endregion

        // Create the OnPropertyChanged method to raise the event 
        protected void OnPropertyChanged(string name)
        {

            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
