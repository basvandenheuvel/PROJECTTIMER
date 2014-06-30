using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Timer.Model
{
    public class ChartPoint
    {
        public ChartPoint(String week, double hours, int year)
        {
            Week = week;
            Hours = hours;
            Year = year;
        }

        public String Week { get; set; }
        public double Hours { get; set; }
        public int Year { get; set; }
    }
}
