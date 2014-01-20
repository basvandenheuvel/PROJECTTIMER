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
        ////Collection of projectmodels
        private ObservableCollection<Project> projects;

        ////Commands
        private DelegateCommand aboutButtonCommand;
        private DelegateCommand settingsButtonCommand;

        public ProjectsPageViewModel()
        {
            projects = new ObservableCollection<Project>();
            //DatabaseConnection.conn.Insert(new Project() { description = "Een heel leuk projectje", name = "Webshopje", deadline = new DateTime(2014, 01, 01) });

            createCommands();
        }

        public void getProjects()
        {
            projects.Clear();

            foreach (var s in DatabaseConnection.conn.Table<Project>())
            {
                projects.Add(s);
            }
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
