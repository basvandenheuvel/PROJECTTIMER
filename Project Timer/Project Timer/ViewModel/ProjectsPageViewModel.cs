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
        private Projects projectsModel = new Projects();

        //Collection of projectmodels
        private ObservableCollection<Project> projects = new ObservableCollection<Project>();

        //Commands
        private DelegateCommand aboutButtonCommand;
        private DelegateCommand settingsButtonCommand;

        public ProjectsPageViewModel()
        {
            projectsModel.addProject("Project c# 2013", "In dit project gaan we met c# een programma maken om van alles bij te kunnen houden.", "11-11-2013", "Contrive");
            projectsModel.addProject("Aapjes gooien", "Geweldig spel waarbij je met apen moet gooien!", "03-10-2014", "BCC");
            projectsModel.addProject("Webshop", "Webshop maken zonder back-end voor het vak PHP", "03-10-2014", "Avans");

            getInformation();
            createCommands();
        }

        private async void getInformation()
        {
            List<Project> projectsList = await projectsModel.getProjects();
            foreach (Project project in projectsList)
            {
                projects.Add(project);
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
