using Project_Timer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Timer.ViewModel
{
    public class AddTaskPageViewModel
    {
        private ObservableCollection<Status> statuses = new ObservableCollection<Status>();

        public AddTaskPageViewModel()
        {
            //Add all statuses to the observable collection
            getStatuses();
        }

        private void getStatuses()
        {
            foreach(var s in DatabaseConnection.conn.Table<Status>())
            {
                statuses.Add(s);
            }
        }

        //public void saveProject(String name, String description, String client, DateTime? deadline)
        //{
        //    //Saving not allowed; Name and description must be filled in
        //    if (String.IsNullOrWhiteSpace(name) || String.IsNullOrWhiteSpace(description))
        //    {
        //        MessageBox.Show("Project can't be saved. The project 'name' and 'description' are required.");
        //        return;
        //    }

        //    //Saving allowed; Default status is 'In progress'

        //    //Save the new project
        //    DatabaseConnection.conn.Insert(new Project() { name = name, description = description, client = client, deadline = deadline, status_id = DEFAULT_STATUS_ID });

        //    //Redirect to the project page of the new project
        //    //Vergeet GET (ID) niet, en maar 1 removebackentry wanneer projectpage gebruikt word
        //    App.RootFrame.Navigate(new Uri("/View/ProjectsPage.xaml", UriKind.RelativeOrAbsolute));
        //    App.RootFrame.RemoveBackEntry();
        //    App.RootFrame.RemoveBackEntry();
        //}

        #region properties
        public ObservableCollection<Status> Statuses
        {
            get { return statuses; }
        }
        #endregion
    }
}
