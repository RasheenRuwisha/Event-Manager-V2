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



        /// <summary>
        /// This Method generates the events that are repetaed for a time perioed.
        /// If the event repeats the start date and end date is diiferencieted and a loop will be run for the time difference, which will then add the number of events for the repeat and keep continuing if the start date and end date is between the user asked startdate and end date
        /// If its not the loop will be broken from there onwards
        /// </summary>
        /// <param name="events">The initial event list</param>
        /// <param name="startDate">The start Date provided by the user</param>
        /// <param name="endDate">The end Date provided by the user</param>
        /// <returns>The list of user events for a certain time period with all repeat and non repeating events</returns>
        public static List<UserEvent> GenerateEvents(List<UserEvent> events, DateTime startDate, DateTime endDate)
        {

            int timeDifference = (endDate - startDate).Days;

            List<UserEvent> userEvents = new List<UserEvent>();
            foreach (UserEvent userEvent in events)
            {

                if (userEvent.RepeatType.Equals("Daily"))
                {
                    if (userEvent.StartDate >= startDate && userEvent.EndDate <= endDate)
                    {
                        userEvents.Add(userEvent);
                    }

                    int startDayDiff = (startDate - userEvent.StartDate).Days;
                    int endDayDiff = (startDate - userEvent.EndDate).Days;

                    userEvent.StartDate = userEvent.StartDate.AddDays(startDayDiff > 0 ? startDayDiff : 0);
                    userEvent.EndDate = userEvent.EndDate.AddDays(startDayDiff > 0 ? endDayDiff : 0);

                    if (userEvent.RepeatDuration.Equals("Specific Number Of Times"))
                    {
                        for (int i = 1; i <= timeDifference; i++)

                            if (i <= userEvent.RepeatCount)
                            {
                                UserEvent evnt = new UserEvent();
                                evnt = GenerateEventObject(userEvent);
                                evnt.StartDate = userEvent.StartDate.AddDays(i);
                                evnt.EndDate = userEvent.EndDate.AddDays(i);
                                if (evnt.StartDate >= startDate && evnt.EndDate <= endDate && evnt.EndDate <= evnt.RepeatTill)
                                {
                                    userEvents.Add(evnt);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                    }
                    else if (userEvent.RepeatDuration.Equals("Forever"))
                    {
                        for (int i = 1; i <= timeDifference; i++)
                        {
                            UserEvent evnt = new UserEvent();
                            evnt = GenerateEventObject(userEvent);
                            evnt.StartDate = userEvent.StartDate.AddDays(i);
                            evnt.EndDate = userEvent.EndDate.AddDays(i);
                            if (evnt.StartDate >= startDate && evnt.EndDate <= endDate && evnt.EndDate <= evnt.RepeatTill)
                            {
                                userEvents.Add(evnt);
                            }
                            else
                            {
                                break;
                            }

                        }
                    }
                    else if (userEvent.RepeatDuration.Equals("Until"))
                    {
                            for (int i = 1; i <= timeDifference; i++)
                            {
                                UserEvent evnt = new UserEvent();
                                evnt = GenerateEventObject(userEvent);
                                evnt.StartDate = userEvent.StartDate.AddDays(i);
                                evnt.EndDate = userEvent.EndDate.AddDays(i);
                                if (evnt.StartDate >= startDate && evnt.EndDate <= endDate && evnt.EndDate <= evnt.RepeatTill)
                                {
                                    userEvents.Add(evnt);
                                }
                                else
                                {
                                    break;
                                }
                            }
                    }
                }
                else if (userEvent.RepeatType.Equals("Weekly"))
                {
                    if (userEvent.StartDate >= startDate && userEvent.EndDate <= endDate)
                    {
                        userEvents.Add(userEvent);
                    }

                    int datediff = (startDate - userEvent.StartDate).Days;
                    double mulof = (double)datediff / (double)7;
                    int mulOfInt = (int)Math.Floor(mulof);


                    if (mulof % 1 != 0)
                    {
                        datediff = (mulOfInt + 1) * 7;
                    }

                    userEvent.StartDate = userEvent.StartDate.AddDays(datediff > 0 ? datediff : 0);
                    userEvent.EndDate = userEvent.EndDate.AddDays(datediff > 0 ? datediff : 0);

                    int weekdiff = (startDate - userEvent.StartDate).Days;
                    if (userEvent.RepeatDuration.Equals("Specific Number Of Times"))
                    {
                        for (int i = 1; i <= timeDifference; i++)

                            if (i <= userEvent.RepeatCount)
                            {
                                UserEvent evnt = new UserEvent();
                                evnt = GenerateEventObject(userEvent);
                                evnt.StartDate = userEvent.StartDate.AddDays(i * 7);
                                evnt.EndDate = userEvent.EndDate.AddDays(i * 7);
                                if (evnt.StartDate >= startDate && evnt.EndDate <= endDate && evnt.EndDate <= evnt.RepeatTill)
                                {
                                    userEvents.Add(evnt);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                    }
                    else if (userEvent.RepeatDuration.Equals("Forever"))
                    {
                        for (int i = 1; i <= timeDifference; i++)
                        {
                            UserEvent evnt = new UserEvent();
                            evnt = GenerateEventObject(userEvent);
                            evnt.StartDate = userEvent.StartDate.AddDays(i * 7);
                            evnt.EndDate = userEvent.EndDate.AddDays(i * 7);
                            if (evnt.StartDate >= startDate && evnt.EndDate <= endDate && evnt.EndDate <= evnt.RepeatTill)
                            {
                                userEvents.Add(evnt);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else if (userEvent.RepeatDuration.Equals("Until"))
                    {
                            for (int i = 1; i <= timeDifference; i++)
                            {
                                UserEvent evnt = new UserEvent();
                                evnt = GenerateEventObject(userEvent);
                                evnt.StartDate = userEvent.StartDate.AddDays(i * 7);
                                evnt.EndDate = userEvent.EndDate.AddDays(i * 7);
                                if (evnt.StartDate >= startDate && evnt.EndDate <= endDate && evnt.EndDate <= evnt.RepeatTill)
                                {
                                    userEvents.Add(evnt);
                                }
                                else
                                {
                                    break;
                                }
                            }
                    }
                }
                else if (userEvent.RepeatType.Equals("Monthly"))
                {

                    if (userEvent.StartDate >= startDate && userEvent.EndDate <= endDate)
                    {
                        userEvents.Add(userEvent);
                    }

                    if (startDate >= userEvent.StartDate)
                    {
                        DateTime newStart = new DateTime(startDate.Year, startDate.Month, userEvent.StartDate.Day, userEvent.StartDate.Hour, userEvent.StartDate.Minute, userEvent.StartDate.Second);
                        userEvent.StartDate = newStart;
                        DateTime newEnd = new DateTime(startDate.Year, startDate.Month, userEvent.EndDate.Day, userEvent.EndDate.Hour, userEvent.EndDate.Minute, userEvent.EndDate.Second);
                        userEvent.EndDate = newEnd;
                    }

                    if (userEvent.RepeatDuration.Equals("Specific Number Of Times"))
                    {
                        for (int i = 1; i <= timeDifference; i++)

                            if (i <= userEvent.RepeatCount)
                            {
                                UserEvent evnt = new UserEvent();
                                evnt = GenerateEventObject(userEvent);
                                evnt.StartDate = userEvent.StartDate.AddMonths(i);
                                evnt.EndDate = userEvent.EndDate.AddMonths(i);
                                if (evnt.StartDate >= startDate && evnt.EndDate <= endDate && evnt.EndDate <= evnt.RepeatTill)
                                {
                                    userEvents.Add(evnt);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                    }
                    else if (userEvent.RepeatDuration.Equals("Forever"))
                    {
                        for (int i = 1; i <= timeDifference; i++)
                        {
                            UserEvent evnt = new UserEvent();
                            evnt = GenerateEventObject(userEvent);
                            evnt.StartDate = userEvent.StartDate.AddMonths(i);
                            evnt.EndDate = userEvent.EndDate.AddMonths(i);
                            if (evnt.StartDate >= startDate && evnt.EndDate <= endDate && evnt.EndDate <= evnt.RepeatTill)
                            {
                                userEvents.Add(evnt);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else if (userEvent.RepeatDuration.Equals("Until"))
                    {
                            for (int i = 1; i <= timeDifference; i++)
                            {
                                UserEvent evnt = new UserEvent();
                                evnt = GenerateEventObject(userEvent);
                                evnt.StartDate = userEvent.StartDate.AddMonths(i);
                                evnt.EndDate = userEvent.EndDate.AddMonths(i);
                                if (evnt.StartDate >= startDate && evnt.EndDate <= endDate && evnt.EndDate <= evnt.RepeatTill)
                                {
                                    userEvents.Add(evnt);
                                }
                                else
                                {
                                    break;
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


        /// <summary>
        /// Generates the Event object from the user event that is provided
        /// </summary>
        /// <param name="userEvent"></param>
        /// <returns>UserEvent</returns>
        public static UserEvent GenerateEventObject(UserEvent userEvent)
        {
            UserEvent events = new UserEvent()
            {
                Title = userEvent.Title,
                Description = userEvent.Description,
                StartDate = userEvent.StartDate,
                EndDate = userEvent.EndDate,
                EventId = userEvent.EventId,
                EventContacts = userEvent.EventContacts,
                RepeatCount = userEvent.RepeatCount,
                RepeatDuration = userEvent.RepeatDuration,
                RepeatTill = userEvent.RepeatTill,
                RepeatType = userEvent.RepeatType,
                UserId = userEvent.UserId,
                ParentId = userEvent.ParentId,
                AddressLine1 = userEvent.AddressLine1,
                AddressLine2 = userEvent.AddressLine2,
                State = userEvent.State,
                City = userEvent.City,
                Zipcode = userEvent.Zipcode,
                Type = userEvent.Type

            };
            return events;
        }

    }
}
