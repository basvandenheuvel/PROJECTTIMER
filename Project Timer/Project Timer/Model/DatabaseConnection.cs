using SQLiteWinRT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Project_Timer.Model
{
    static class DatabaseConnection
    {
        private static Database db = new Database(ApplicationData.Current.LocalFolder, "projecttimer.db");

        public static Database Db
        {
            get { return DatabaseConnection.db; }
        }

        //Create the tables in the database
        public static void createTables()
        {
            var open = db.OpenAsync();

            //Create project table
            string query_project = "create TABLE if not exists project " +
                                    "(" +
                                    "id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL," +
                                    "name VARCHAR(45) NOT NULL," +
                                    "description LONGTEXT NOT NULL," +
                                    "client VARCHAR(45)," +
                                    "deadline DATE," +
                                    "statuses_id INTEGER NOT NULL" +
                                    ")";


            //Execute the create query
            var e1 = db.ExecuteStatementAsync(query_project);

            //Create project task
            string query_task = "create TABLE if not exists task " +
                                    "(" +
                                    "id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL," +
                                    "name VARCHAR(45) NOT NULL," +
                                    "description LONGTEXT," +
                                    "project_id INTEGER NOT NULL," +
                                    "status_id INTEGER NOT NULL" +
                                    ")";

            //Execute the create query
            var e2 = db.ExecuteStatementAsync(query_task);

            //Create project task
            string query_worktime = "create TABLE if not exists worktime " +
                                    "(" +
                                    "id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL," +
                                    "description VARCHAR(199)," +
                                    "start_time DATETIME NOT NULL," +
                                    "stop_time DATETIME NOT NULL," +
                                    "date DATE NOT NULL," +
                                    "task_id INTEGER NOT NULL" +
                                    ")";

            //Execute the create query
            var e3 = db.ExecuteStatementAsync(query_worktime);


            //Create project task
            string query_status = "create TABLE if not exists status " +
                                    "(" +
                                    "id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL," +
                                    "name VARCHAR(45)" +
                                    ")";

            //Execute the create query
            var e4 = db.ExecuteStatementAsync(query_status);
        }

        //Fill the status table with the default statuses
        public static void fillStatusTable()
        {
            string query = "INSERT OR IGNORE INTO status (id, name) VALUES ('1','In progress'), ('2','On hold'), ('3','Done'), ('4','Canceled')";
            var e5 = db.ExecuteStatementAsync(query);
        }
    }
}
