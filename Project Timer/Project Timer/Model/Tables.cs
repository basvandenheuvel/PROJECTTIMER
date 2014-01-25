using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Timer.Model
{
    public sealed class ProjectTable
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public string client { get; set; }

        public DateTime? deadline { get; set; }

        public Boolean finished { get; set; }

        public override string ToString()
        {
            return "Project: " + id + " - " + name + " - " + description;
        }
    }

    public sealed class TaskTable
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public int project_id { get; set; }

        public Boolean finished { get; set; }

        public override string ToString()
        {
            return "Task: " + id + " - " + name + " - " + description;
        }
    }

    public sealed class SessionTable
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        public string description { get; set; }

        public DateTime start_time { get; set; }

        public DateTime stop_time { get; set; }

        public int task_id { get; set; }

        public override string ToString()
        {
            return "Worktime: " + id + " - " + description;
        }
    }

    //public sealed class Status
    //{
    //    [PrimaryKey, AutoIncrement]
    //    public int id { get; set; }

    //    public string name { get; set; }

    //    public override string ToString()
    //    {
    //        return name;
    //    }
    //}
}
