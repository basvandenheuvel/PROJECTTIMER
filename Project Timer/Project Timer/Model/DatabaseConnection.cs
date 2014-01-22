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
        }

        //TODO: delete before upload
        public static void fillStatusTable()
        {
            //Add the default statuses
            if (conn.Table<Project>().Count() == 0)
            {
                conn.Insert(new Task() { name = "Back-end maken", description = "Zorgen dat nieuwe boeken kunnen worden toegevoegd", project_id = 1 });
                conn.Insert(new Task() { name = "Beginscherm maken", description = "De 10 goedkoopste boeken laten zien", project_id = 1 });

                conn.Insert(new Project() { name = "Webshop", description = "Voor het vak PHP een webshop maken.", deadline = new DateTime(2014, 05, 05), finished = false });
            }
        }

        public static void emptyDatabase(bool areYouSure)
        {
            if (areYouSure)
            {
                conn.DeleteAll<Worktime>();
                conn.DeleteAll<Task>();
                conn.DeleteAll<Project>();
            }
        }
    }
}
