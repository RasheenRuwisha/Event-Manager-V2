using EventManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Utility
{
    public class EventGenerator
    {
        public List<UserEvent> GenerateEvents(List<UserEvent> events, DateTime startDate, DateTime endDate)
        {

            int timeDifference = (endDate - startDate).Days;

            List<UserEvent> userEvents = new List<UserEvent>();
            foreach(UserEvent userEvent in events)
            {

                if (userEvent.RepeatType.Equals("Daily"))
                {
                    if (userEvent.StartDate >= startDate && userEvent.EndDate <= endDate)
                    {
                        userEvents.Add(userEvent);
                    }

                    int startDayDiff =(startDate - userEvent.StartDate).Days; 
                    int endDayDiff = (startDate - userEvent.EndDate).Days;

                    userEvent.StartDate = userEvent.StartDate.AddDays(startDayDiff > 0 ? startDayDiff : 0);
                    userEvent.EndDate = userEvent.EndDate.AddDays(startDayDiff > 0 ? endDayDiff : 0);

                    if (userEvent.RepeatDuration.Equals("Specific Number Of Times"))
                    {
                        for(int i = 0; i <= timeDifference; i++)
                        
                            if(i <= userEvent.RepeatCount)
                            {
                                UserEvent evnt =  new UserEvent();
                                evnt = this.GenerateEventObject(userEvent);
                                evnt.StartDate = userEvent.StartDate.AddDays(i);
                                evnt.EndDate = userEvent.EndDate.AddDays(i);
                                if(evnt.StartDate >= startDate && evnt.EndDate <= endDate)
                                {
                                    userEvents.Add(evnt);
                                }
                            }
                    }else if (userEvent.RepeatDuration.Equals("Forever"))
                    {
                        for (int i = 1; i <= timeDifference; i++)
                            {
                                UserEvent evnt = new UserEvent();
                                evnt = this.GenerateEventObject(userEvent);
                                evnt.StartDate = userEvent.StartDate.AddDays(i);
                                evnt.EndDate = userEvent.EndDate.AddDays(i);
                                if (evnt.StartDate >= startDate && evnt.EndDate <= endDate)
                                {
                                    userEvents.Add(evnt);
                                }
                            }
                    }
                    else if (userEvent.RepeatDuration.Equals("Until"))
                    {
                        if(userEvent.RepeatTill >= endDate)
                        for (int i = 1; i <= timeDifference; i++)
                        {
                            UserEvent evnt = new UserEvent();
                            evnt = this.GenerateEventObject(userEvent);
                            evnt.StartDate = userEvent.StartDate.AddDays(i);
                            evnt.EndDate = userEvent.EndDate.AddDays(i);
                            if (evnt.StartDate >= startDate && evnt.EndDate <= endDate)
                            {
                                userEvents.Add(evnt);
                            }
                        }
                    }
                }else if (userEvent.RepeatType.Equals("Weekly"))
                {

                    int datediff = (startDate - userEvent.StartDate).Days;
                    double mulof  = (double)datediff / (double)7;
                    int mulOfInt = (int)Math.Floor(mulof);


                    if (mulof % 1 != 0)
                    {
                        datediff = (mulOfInt + 1) * 7;
                    }

                    userEvent.StartDate = userEvent.StartDate.AddDays(datediff);

                    userEvent.EndDate = userEvent.EndDate.AddDays(datediff);

                    if (userEvent.StartDate >= startDate && userEvent.EndDate <= endDate)
                    {
                        userEvents.Add(userEvent);
                    }

                    int weekdiff = (startDate - userEvent.StartDate).Days;
                    if (userEvent.RepeatDuration.Equals("Specific Number Of Times"))
                    {
                        for (int i = 1; i <= timeDifference; i++)

                            if (i <= userEvent.RepeatCount)
                            {
                                UserEvent evnt = new UserEvent();
                                evnt = this.GenerateEventObject(userEvent);
                                evnt.StartDate = userEvent.StartDate.AddDays(i*7);
                                evnt.EndDate = userEvent.EndDate.AddDays(i*7);
                                if (evnt.StartDate >= startDate && evnt.EndDate <= endDate)
                                {
                                    userEvents.Add(evnt);
                                }
                            }
                    }
                    else if (userEvent.RepeatDuration.Equals("Forever"))
                    {
                        for (int i = 1; i <= timeDifference; i++)
                        {
                            UserEvent evnt = new UserEvent();
                            evnt = this.GenerateEventObject(userEvent);
                            evnt.StartDate = userEvent.StartDate.AddDays(i*7);
                            evnt.EndDate = userEvent.EndDate.AddDays(i*7);
                            if (evnt.StartDate >= startDate && evnt.EndDate <= endDate)
                            {
                                userEvents.Add(evnt);
                            }
                        }
                    }
                    else if (userEvent.RepeatDuration.Equals("Until"))
                    {
                        if (userEvent.RepeatTill >= endDate)
                            for (int i = 1; i <= timeDifference; i++)
                            {
                                UserEvent evnt = new UserEvent();
                                evnt = this.GenerateEventObject(userEvent);
                                evnt.StartDate = userEvent.StartDate.AddDays(i*7);
                                evnt.EndDate = userEvent.EndDate.AddDays(i*7);
                                if (evnt.StartDate >= startDate && evnt.EndDate <= endDate)
                                {
                                    userEvents.Add(evnt);
                                }
                            }
                    }
                }else if (userEvent.RepeatType.Equals("Monthly"))
                {
            

                    DateTime newStart = new DateTime(startDate.Year, startDate.Month, userEvent.StartDate.Day, userEvent.StartDate.Hour, userEvent.StartDate.Minute, userEvent.StartDate.Second);
                    userEvent.StartDate = newStart;


                    DateTime newEnd = new DateTime(startDate.Year, startDate.Month, userEvent.EndDate.Day, userEvent.EndDate.Hour, userEvent.EndDate.Minute, userEvent.EndDate.Second);
                    userEvent.EndDate = newEnd;

                    if (userEvent.StartDate >= startDate && userEvent.EndDate <= endDate)
                    {
                        userEvents.Add(userEvent);
                    }
                    if (userEvent.RepeatDuration.Equals("Specific Number Of Times"))
                    {
                        for (int i = 1; i <= timeDifference; i++)

                            if (i <= userEvent.RepeatCount)
                            {
                                UserEvent evnt = new UserEvent();
                                evnt = this.GenerateEventObject(userEvent);
                                evnt.StartDate = userEvent.StartDate.AddMonths(i);
                                evnt.EndDate = userEvent.EndDate.AddMonths(i);
                                if (evnt.StartDate >= startDate && evnt.EndDate <= endDate)
                                {
                                    userEvents.Add(evnt);
                                }
                            }
                    }
                    else if (userEvent.RepeatDuration.Equals("Forever"))
                    {
                        for (int i = 1; i <= timeDifference; i++)
                        {
                            UserEvent evnt = new UserEvent();
                            evnt = this.GenerateEventObject(userEvent);
                            evnt.StartDate = userEvent.StartDate.AddMonths(i);
                            evnt.EndDate = userEvent.EndDate.AddMonths(i);
                            if (evnt.StartDate >= startDate && evnt.EndDate <= endDate)
                            {
                                userEvents.Add(evnt);
                            }
                        }
                    }
                    else if (userEvent.RepeatDuration.Equals("Until"))
                    {
                        if (userEvent.RepeatTill >= endDate)
                            for (int i = 1; i <= timeDifference; i++)
                            {
                                UserEvent evnt = new UserEvent();
                                evnt = this.GenerateEventObject(userEvent);
                                evnt.StartDate = userEvent.StartDate.AddMonths(i);
                                evnt.EndDate = userEvent.EndDate.AddMonths(i);
                                if (evnt.StartDate >= startDate && evnt.EndDate <= endDate)
                                {
                                    userEvents.Add(evnt);
                                }
                            }
                    }
                }
                else
                {
                    if (userEvent.StartDate >= startDate && userEvent.EndDate <= endDate)
                    {
                        userEvents.Add(userEvent);
                    }
                }
            }

            userEvents.Sort((x, y) => DateTime.Compare(x.StartDate, y.StartDate));
            return userEvents;
        }

        public UserEvent GenerateEventObject(UserEvent userEvent)
        {
            UserEvent events = new UserEvent()
            {
                title = userEvent.title,
                description = userEvent.description,
                StartDate = userEvent.StartDate,
                EndDate = userEvent.EndDate,
                eventid = userEvent.eventid,
                EventContacts = userEvent.EventContacts,
                RepeatCount = userEvent.RepeatCount,
                RepeatDuration = userEvent.RepeatDuration,
                RepeatTill = userEvent.RepeatTill,
                RepeatType = userEvent.RepeatType,
                userid = userEvent.userid,

            };
            return events;
        }

    }
}
