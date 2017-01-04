using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using iTextSharp.text.pdf;

namespace TimeSheet
{
    public class CalendarUpdate
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/calendar-dotnet-quickstart.json
        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "Google Calendar API .NET Quickstart";

        public string dateRange;

        public void AddToCalendar()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/calendar-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                //Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

            ParseHours hours = new ParseHours();
            hours.HoursWorked();
            DateTime dateValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            Form1 listbox = new Form1();

            string calendarDate = "1/1/2017 ";
            
            Event newEvent = new Event()
            {
                Summary = "Working",
                Location = "7000 Dandini BLVD Reno NV 89512",
                Description = "Working during this time",
                Start = new EventDateTime()
                {
                    DateTime = DateTime.Parse(calendarDate + "8:00 AM"),
                    TimeZone = "America/Los_Angeles",
                },
                End = new EventDateTime()
                {
                    DateTime = DateTime.Parse(calendarDate + "8:00 AM"),
                    TimeZone = "America/Los_Angeles",
                },
                Recurrence = new String[] {
                    "RRULE:FREQ=WEEKLY;COUNT=2"
                },
                Reminders = new Event.RemindersData()
                {
                    UseDefault = true,
                }
            };

            String calendarId = "primary";
            EventsResource.InsertRequest request = service.Events.Insert(newEvent, calendarId);
            string dateText;

            int i = 0;
            int x = 0;

            if (dateRange == "1st - 15th")
            {
                i = 1;
                x = i + 7;
            }
            else if (dateRange == "16th - 30th")
            {
                i = 16;
                x = i + 7;
            }
            else if (dateRange == "16th - 31st")
            {
                i = 16;
                x = i + 8;
            }

            while(i < x)
                {
                    if (dateValue.AddDays(i - 1).ToString("ddd") == "Mon" && hours.mondayStart != " 0")
                    {
                        dateText = string.Format("{0}/{1}/{2} ", DateTime.Now.Month, i, DateTime.Now.Year);
                        newEvent.Start.DateTime = DateTime.Parse(dateText + hours.mondayStart);
                        newEvent.End.DateTime = DateTime.Parse(dateText + hours.mondayEnd);
                        Event createdEvent = request.Execute();
                    }
                    else if (dateValue.AddDays(i - 1).ToString("ddd") == "Tue" && hours.tuesdayStart != " 0")
                    {
                        dateText = string.Format("{0}/{1}/{2} ", DateTime.Now.Month, i, DateTime.Now.Year);
                        newEvent.Start.DateTime = DateTime.Parse(dateText + hours.tuesdayStart);
                        newEvent.End.DateTime = DateTime.Parse(dateText + hours.tuesdayEnd);
                        Event createdEvent = request.Execute();
                    }
                    else if (dateValue.AddDays(i - 1).ToString("ddd") == "Wed" && hours.wednesdayStart != " 0")
                    {
                        dateText = string.Format("{0}/{1}/{2} ", DateTime.Now.Month, i, DateTime.Now.Year);
                        newEvent.Start.DateTime = DateTime.Parse(dateText + hours.wednesdayStart);
                        newEvent.End.DateTime = DateTime.Parse(dateText + hours.wednesdayEnd);
                        Event createdEvent = request.Execute();
                    }
                    else if (dateValue.AddDays(i - 1).ToString("ddd") == "Thu" && hours.thursdayStart != " 0")
                    {
                        dateText = string.Format("{0}/{1}/{2} ", DateTime.Now.Month, i, DateTime.Now.Year);
                        newEvent.Start.DateTime = DateTime.Parse(dateText + hours.thursdayStart);
                        newEvent.End.DateTime = DateTime.Parse(dateText + hours.thursdayEnd);
                        Event createdEvent = request.Execute();
                    }
                    else if (dateValue.AddDays(i - 1).ToString("ddd") == "Fri" && hours.fridayStart != " 0")
                    {
                        dateText = string.Format("{0}/{1}/{2} ", DateTime.Now.Month, i, DateTime.Now.Year);
                        newEvent.Start.DateTime = DateTime.Parse(dateText + hours.fridayStart);
                        newEvent.End.DateTime = DateTime.Parse(dateText + hours.fridayEnd);
                        Event createdEvent = request.Execute();
                    }

                    ++i;
                }


            //Console.WriteLine("Event created: {0}", createdEvent.HtmlLink);

        }
    }
}
