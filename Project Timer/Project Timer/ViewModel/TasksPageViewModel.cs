using Project_Timer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project_Timer.ViewModel
{
    public class TasksPageViewModel : INotifyPropertyChanged 
    {

        private ObservableCollection<Project_Timer.Model.Task> tasks; //Collection of unfinished tasks
        private ObservableCollection<Project_Timer.Model.Task> finishedTasks; //Collection of finished tasks
        private ObservableCollection<ChartPoint> graphCollection; //Collection of all chartpoints for the graphic

        //Default project name
        private Projects projectsModel;
        private Project projectModel;

        //Amount of tasks a project has
        private int amountOfTasks;
        //Amount of finished tasks a project has
        private int amountOfFinishedTasks;

        private int graphWidth;
        private int graphInterval;
        private int columnWidth = 30;

        //Project id
        private int projectId;

        public TasksPageViewModel()
        {
            tasks = new ObservableCollection<Project_Timer.Model.Task>();
            finishedTasks = new ObservableCollection<Project_Timer.Model.Task>();
            graphCollection = new ObservableCollection<ChartPoint>();
            projectsModel = new Projects();
        }

        public void refreshTasks()
        {
            projectModel = new Project(projectId);

            tasks.Clear();
            finishedTasks.Clear();

            //Tasks finished
            foreach (Model.Task task in projectModel.getUnfinishedTasks())
            {
                tasks.Add(task);
            }
            
            //Tasks in progress
            foreach (Model.Task task in projectModel.getFinishedTasks())
            {
                finishedTasks.Add(task);
            }

            graphCollection.Add(new ChartPoint(" ", 0, 0));

            AmountOfTasks = Tasks.Count;
            AmountOfFinishedTasks = FinishedTasks.Count;

            OnPropertyChanged("ProjectName");
            OnPropertyChanged("TotalHours");

            prepInfoForDiagram();
            setGraphInterval();

            graphCollection.Add(new ChartPoint("\r\n", 0, 0));

            setGraphWidth();
        }

        private void prepInfoForDiagram()
        {
            List<ChartPoint> tempList = new List<ChartPoint>();

            foreach (Model.Task task in projectModel.getTasks())
            {
                foreach (Session session in task.getSessions())
                {
                    tempList.Add(new ChartPoint(getWeekNumberOfDateTime(session.StartTime), session.ElapsedTimeInTimeSpan.TotalHours, session.StartTime.Year));                    
                }
            }

            var sortedList = (
                from point in tempList
                group point by new { point.Year, point.Week }
                    into grp
                    orderby grp.Key.Year ascending, grp.Key.Week ascending
                    select new
                    {
                        grp.Key.Week,
                        grp.Key.Year,
                        Hours = grp.Sum(point => point.Hours)
                    }).ToList();

            foreach (var group in sortedList)
            {
                graphCollection.Add(new ChartPoint(group.Week + "\r\n" +  group.Year, group.Hours, group.Year));
            }
        }

        private void setGraphWidth()
        {
            int screenWidth = Int32.Parse(Application.Current.Host.Content.ActualWidth.ToString());
            int minGraphWidth = screenWidth - 70;

            int graphWidthWeWant = graphCollection.Count * columnWidth;
            if (graphWidthWeWant < minGraphWidth)
            {
                graphWidth = minGraphWidth;
            }
            else
            {
                graphWidth = graphWidthWeWant;
            }
        }

        private void setGraphInterval()
        {
            ChartPoint highestChartPoint = new ChartPoint("", 0, 0);

            foreach (ChartPoint point in graphCollection)
            {
                if (point.Hours > highestChartPoint.Hours)
                {
                    highestChartPoint = point;
                }
            }

            if (highestChartPoint.Hours <= 6)
            {
                graphInterval = 1;
            }
            else if (highestChartPoint.Hours > 6 && highestChartPoint.Hours <= 20)
            {
                graphInterval = 2;
            }
            else
            {
                graphInterval = 5;
            }
        }

        private String getWeekNumberOfDateTime(DateTime date)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            return cal.GetWeekOfYear(date, dfi.CalendarWeekRule, dfi.FirstDayOfWeek).ToString();
        }

        public void deleteTask(Model.Task task)
        {
            task.deleteTask();

            if (task.Finished)
            {
                finishedTasks.Remove(task);
                AmountOfFinishedTasks--;
            }
            else
            {
                tasks.Remove(task);
                AmountOfTasks--;
            }
        }

        public void toggleFinished(Project_Timer.Model.Task task)
        {
            if (task.Finished)
            {
                //Set the task to in progress
                task.Finished = false;
                task.save();

                refreshTasks();
            }
            else
            {
                //Set the task to finished
                task.Finished = true;
                task.save();
                
                refreshTasks();
            }
        }

        #region properties
        public ObservableCollection<Project_Timer.Model.Task> Tasks
        {
            get { return tasks; }
        }
        public ObservableCollection<Project_Timer.Model.Task> FinishedTasks
        {
            get { return finishedTasks; }
        }
        public ObservableCollection<ChartPoint> GraphCollection
        {
            get { return graphCollection; }
        }
        public int GraphWidth
        {
            get { return graphWidth; }
        }
        public int GraphInterval
        {
            get { return graphInterval; }
        }
        public int AmountOfTasks
        {
            get { return amountOfTasks; }
            set {
                    amountOfTasks = value;
                    OnPropertyChanged("AmountOfTasks"); 
                }
        }
        public int AmountOfFinishedTasks
        {
            get { return amountOfFinishedTasks; }
            set
            {
                amountOfFinishedTasks = value;
                OnPropertyChanged("AmountOfFinishedTasks");
            }
        }
        public double TotalHours
        {
            get { return projectModel.getAmountOfHours(); }
        }
        public Project Project
        {
            get { return projectModel; }
        }
        public String ProjectName
        {
            get { return projectModel.Name; }
        }
        public int ProjectId
        {
            get { return projectId; }
            set { projectId = value; }
        }
        #endregion

        #region propertychanged
        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event 
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion
    }
}
