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
            conn.CreateTable<Project>();
            conn.CreateTable<Task>();
            conn.CreateTable<Worktime>();
            conn.CreateTable<Status>();
        }

        public static void fillStatusTable()
        {
            //Add the default statuses
            if (conn.Table<Status>().Count() == 0)
            {
                conn.Insert(new Status() { name = "In progress" });
                conn.Insert(new Status() { name = "On hold" });
                conn.Insert(new Status() { name = "Done" });
                conn.Insert(new Status() { name = "Canceled" });
            }
        }
    }
}
