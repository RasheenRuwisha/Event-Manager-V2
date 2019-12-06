using EventManager.DatabaseHelper;
using EventManager.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EventManager.Utility
{
    public class PredictionUtility
    {

        readonly DateTime Today = DateTime.Now;

        public Prediction PredictTimeConsumption()
        {

            double TimeConsumtion = 0.0;
            int TaskCount = 0;
            int AppointmentCount = 0;

            DateTime MonthStart = Today.AddDays(-30).Date;
            DateTime MonthEnd = DateTime.Today.Date;
            List<UserEvent> userEvents = EventHelper.SearchUserEvent(MonthStart, MonthEnd);
            foreach (UserEvent userEvent in userEvents)
            {
                TimeConsumtion += (userEvent.EndDate - userEvent.StartDate).TotalMinutes;
                if (userEvent.Type.Equals("Appointment"))
                {
                    AppointmentCount += 1;
                }
                else if (userEvent.Type.Equals("Task"))
                {
                    TaskCount += 1;
                }
            }
            double DailyConsumption = TimeConsumtion / 31;
            double WeeklyConsumption = TimeConsumtion / 4;

            Prediction prediction = new Prediction()
            {
                TotalMinutes = TimeConsumtion,
                DailyAverage = DailyConsumption,
                WeeklyAverage = WeeklyConsumption,
                MonthlyAverage = TimeConsumtion,
                EventCount = userEvents.Count,
                TaskCount = TaskCount,
                AppointmentCount = AppointmentCount
            };


            return prediction;
        }

    }
}
