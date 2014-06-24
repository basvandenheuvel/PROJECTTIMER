using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Timer.Model
{
    public class Sessions
    {
        public Session getSession(int sessionId)
        {
            return new Session(sessionId);
        }

        public List<Session> getAllSessions()
        {
            List<Session> sessionList = new List<Session>();
            foreach (SessionTable st in DatabaseConnection.conn.Query<SessionTable>("SELECT * FROM SessionTable"))
            {
                sessionList.Add(new Session(st.id));
            }
            return sessionList;
        }

        public List<Session> getTaskSessions(int taskId)
        {
            List<Session> sessionList = new List<Session>();
            foreach (SessionTable st in DatabaseConnection.conn.Query<SessionTable>("SELECT * FROM SessionTable WHERE task_id = " + taskId))
            {
                sessionList.Add(new Session(st.id));
            }
            return sessionList;
        }

        public Session addSession(String description, TimeSpan elapsedTime, DateTime startTime, int taskId)
        {
            Session newSession = new Session();
            newSession.Description = description;
            newSession.ElapsedTime = elapsedTime;
            newSession.StartTime = startTime;
            newSession.TaskId = taskId;
            newSession.saveSession();

            return newSession;
        }

        public void deleteSession(int sessionId)
        {
            new Session(sessionId).deleteSession();
        }
    }
}
