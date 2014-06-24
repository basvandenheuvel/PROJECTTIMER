using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Project_Timer.Model
{
    public class Session
    {
        //Database object
        SessionTable st;

        private TimeSpan elapsedTime;
        private DateTime startTime;
        private DateTime firstStartTime;

        private Boolean isRunning = false;

        /// <summary>
        /// Start creating a new session
        /// </summary>
        public Session()
        {
            st = new SessionTable();

            elapsedTime = new TimeSpan();
            firstStartTime = DateTime.Now;
        }

         /// <summary>
        /// Load an existing session
        /// </summary>
        /// <param name="sessionId">ID of the session to load from the database</param>
        public Session(int sessionId)
        {
            st = DatabaseConnection.conn.Query<SessionTable>("SELECT * FROM SessionTable WHERE id = " + sessionId)[0];
        }

        public void StartTimer()
        {
            startTime = DateTime.Now;
            isRunning = true;
        }

        public void StopTimer()
        {
            if (isRunning)
            {
                elapsedTime += DateTime.Now - startTime;
                isRunning = false;
            }
        }



        #region database save/delete
        public void saveSession()
        {
            DatabaseConnection.conn.Insert(st);
        }

        public void deleteSession()
        {
            //TODO: testen!!!!

            //Delete the session
            DatabaseConnection.conn.Query<SessionTable>("DELETE " +
                                                        "FROM SessionTable " +
                                                        "WHERE id = " + Id);
        }
        #endregion

        #region properties
        public TimeSpan ElapsedTime
        {
            get
            {
                if (isRunning)
                {
                    return elapsedTime + (DateTime.Now - startTime);
                }
                else
                {
                    return elapsedTime;
                }
            }
            set { st.elapsed_time = value; }
        }
        public String ElapsedTimeInString
        {
            get { return st.elapsed_time.ToString().Split('.')[0]; }
        }
        public DateTime FirstStartTime
        {
            get { return firstStartTime; }
        }
        public int Id
        {
            get { return st.id; }
        }
        public String Description
        {
            get { return st.description; }
            set { st.description = value; }
        }
        public DateTime StartTime
        {
            get { return st.start_time; }
            set { st.start_time = value; }
        }
        public String StartTimeInString
        {
            get { return st.start_time.ToString("dd/MM/yyyy"); }
        }
        public int TaskId
        {
            get { return st.task_id; }
            set { st.task_id = value; }
        }
        #endregion
    }
}
