using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Project_Timer.Model
{
    static class DatabaseConnection
    {
        //Database path
        private static string DB_PATH = Path.Combine(Path.Combine(ApplicationData.Current.LocalFolder.Path, "project.sqlite"));
 
        //Connection
        public static SQLiteConnection conn = new SQLiteConnection(DB_PATH);

        public static void createTables()
        {
            //Create the tables they don't exist
            conn.CreateTable<ProjectTable>();
            conn.CreateTable<TaskTable>();
            conn.CreateTable<SessionTable>();
        }

        //TODO: delete before upload
        public static void FillWithTestData()
        {
            //Add the default statuses
            if (conn.Table<ProjectTable>().Count() == 0)
            {

                conn.Insert(new SessionTable() { description = "andere helft gemaakt", elapsed_time = new TimeSpan(22, 0, 0), task_id = 1, start_time = new DateTime(2014, 06, 29, 00, 15, 00) });
                conn.Insert(new SessionTable() { description = "back-end half gemaakt", elapsed_time = new TimeSpan(10, 0, 0), task_id = 1, start_time = new DateTime(2014, 06, 28, 23, 45, 00) });
                conn.Insert(new SessionTable() { description = "back-end half gemaakt", elapsed_time = new TimeSpan(10, 0, 0), task_id = 1, start_time = new DateTime(2013, 06, 28, 23, 45, 00) });
                conn.Insert(new SessionTable() { description = "back-end half gemaakt", elapsed_time = new TimeSpan(10, 0, 0), task_id = 1, start_time = new DateTime(2014, 06, 12, 23, 45, 00) });


                conn.Insert(new TaskTable() { name = "Back-end maken", description = "Zorgen dat nieuwe boeken kunnen worden toegevoegd", project_id = 1 });
                conn.Insert(new TaskTable() { name = "Beginscherm maken", description = "De 10 goedkoopste boeken laten zien", project_id = 1 });

                conn.Insert(new ProjectTable() { name = "Webshop", description = "Voor het vak PHP een webshop maken.", deadline = new DateTime(2014, 05, 05), finished = false });
            }
        }

        public static void emptyDatabase(bool areYouSure)
        {
            if (areYouSure)
            {
                conn.DeleteAll<SessionTable>();
                conn.DeleteAll<TaskTable>();
                conn.DeleteAll<ProjectTable>();
            }
        }
    }
}
