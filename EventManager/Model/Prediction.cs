using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Model
{
    public class Prediction
    {

        public double TotalMinutes { get; set; }
        public double DailyAverage { get; set; }
        public double WeeklyAverage { get; set; }
        public double MonthlyAverage { get; set; }
        public int AppointmentCount { get; set; }
        public int TaskCount { get; set; }
        public int EventCount { get; set; }
    }
}
