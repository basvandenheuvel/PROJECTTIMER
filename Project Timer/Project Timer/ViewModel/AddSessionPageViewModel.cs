using Project_Timer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Project_Timer.ViewModel
{
    public class AddSessionPageViewModel : INotifyPropertyChanged
    {
        private int taskId;

        private Session sessionModel;
        private Sessions sessionsModel;
        private Model.Task taskModel;
        private Tasks tasksModel;

        private DispatcherTimer timer;
        private Boolean maxTimeNotReached;
        private String buttonTimerText;


        public AddSessionPageViewModel()
        {
            sessionModel = new Session();
            sessionsModel = new Sessions();
            taskModel = new Model.Task();
            tasksModel = new Tasks();

            maxTimeNotReached = true;
            buttonTimerText = "Start";


            // creating timer instance
            timer = new DispatcherTimer();
            // timer interval specified as 1 second
            timer.Interval = TimeSpan.FromMilliseconds(100);
            // Sub-routine OnTimerTick will be called at every 1 second
            timer.Tick += OnTimerTick;
        }
        
        public void saveSession(String description)
        {
            //Create new session 
            sessionModel = sessionsModel.addSession(description, sessionModel.ElapsedTime, sessionModel.FirstStartTime, taskId);
        }

        private void OnTimerTick(Object sender, EventArgs args)
        {
            //if (sessionModel.ElapsedTime.Hours == 23 && sessionModel.ElapsedTime.Minutes == 59 && sessionModel.ElapsedTime.Seconds == 59)
            if (sessionModel.ElapsedTime == sessionModel.MaxAllowedTime)
            {
                StopTimer();
                maxTimeNotReached = false;
                buttonTimerText = "Max time reached";
                OnPropertyChanged("MaxTimeNotReached");
                OnPropertyChanged("ButtonTimerText");
            }

            OnPropertyChanged("ElapsedTimeInString");
        }

        public void StartTimer()
        {
            sessionModel.StartTimer();
            timer.Start();
            buttonTimerText = "Stop";
            OnPropertyChanged("ButtonTimerText");
        }

        public void StopTimer()
        {
            sessionModel.StopTimer();
            timer.Stop();
            buttonTimerText = "Start";
            OnPropertyChanged("ButtonTimerText");
        }

        public void AddTime(int minutes)
        {
            sessionModel.AddTime(new TimeSpan(0, minutes, 0));
            OnPropertyChanged("ElapsedTimeInString");
        }

        public void SubstractTime(int minutes)
        {
            sessionModel.SubstractTime(new TimeSpan(0, minutes, 0));
            OnPropertyChanged("ElapsedTimeInString");
        }


        #region properties
        public int TaskId
        {
            get { return taskId; }
            set
            {
                taskId = value;
                taskModel = tasksModel.getTask(value);
            }
        }
        public String ElapsedTimeInString
        {
            get { return sessionModel.ElapsedTime.ToString().Split('.')[0]; }
        }
        public Boolean MaxTimeNotReached
        {
            get { return maxTimeNotReached; }
        }
        public String ButtonTimerText
        {
            get { return buttonTimerText; }
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
