﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Google.Apis;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
namespace ToDoApp.Utility
{
    class GoogleApiConnection
    {

        private UserCredential credential;
        private CalendarService service;
        static string[] Scopes = { CalendarService.Scope.Calendar, CalendarService.Scope.CalendarEvents, CalendarService.Scope.CalendarEventsReadonly };
        public GoogleApiConnection()
        {
            string CredentialsPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Resource\\credentials.json");
            using (var stream =
                       new FileStream(CredentialsPath, FileMode.Open, FileAccess.Read))
            {
                string TokenPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Resource\\token.json");
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(TokenPath, true)).Result;
                Trace.WriteLine("Credential file saved to: " + TokenPath);
            }
            service = new CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "ToDoApp"
            });

        }
        public Event? GetEvent(string id)
        {
            Event _event=null;
            try
            {
                _event=service.Events.Get("primary",id).Execute();
            }
            catch (Exception ex) 
            {
                Trace.WriteLine(ex, "Wystąpił błąd przy pobieraniu zadania! (GoogleApiConnection)");
                return null;
            }
            return _event;
        }
        public Events? GetAllTasks()
        {
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            try
            {
                Events events = request.Execute();
                if (events.Items == null || events.Items.Count == 0)
                {
                    Trace.WriteLine("No upcoming events found.");
                   ;
                }else
                {
                    foreach (var x in events.Items)
                    {
                        Trace.WriteLine(x.Summary);
                    }
                    return events;
                }
            }catch (Exception ex)
            {
                Trace.WriteLine(ex, "Wystąpił błąd przy pobieraniu zadań! (GoogleApiConnection)");
            }
          
            return null;
        }
        public bool UpdateEvent(Event task,string name,string description,DateTime date)
        {
            try
            {
                task.Summary = name;
                task.Description = description;
                //task.
                EventDateTime start = new EventDateTime();

                start.DateTime = date;
                task.Start = start;
                task.End = start;
                service.Events.Update(task, "primary", task.Id).Execute();
            }
            catch (Exception ex) 
            {

                Trace.WriteLine(ex, "Wysyąpił błąd przy aktualizowaniu zdarzenia! (GoogleApiConnection)");
                return false;            
            }
            return true;
        }
        public bool DeleteEvent(Event task)
        {
            try 
            {
                service.Events.Delete("primary", task.Id).Execute();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, "Wysyąpił błąd podczas usuwania zdarzenia! (GoogleApiConnection)");
                return false;
            }
            return true;
        }
        public void PostTask(ToDoTask task)
        {
            Event body = new Event();
            body.Summary = task.Name;
            body.Description = task.Description;
            EventDateTime start = new EventDateTime();
            start.DateTime = task.TaskToDoDate;
            //var end = start;
            body.Start = start;
            body.End = start;
            EventsResource.InsertRequest request = new EventsResource.InsertRequest(service, body, "primary");
            try
            {
                Event response = request.Execute();

            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, "Wystąpił błąd podczas wysyłania zdarzenia! (GoogleApiConnection)");
            }
        }
        public void SyncrhonizeTasks(List<ToDoTask> tasks)
        {
            List<Event> eventsToAdd = new List<Event>();
            foreach (ToDoTask task in tasks)
            {
                Event body = new Event();
                body.Summary = task.Name;
                body.Description = task.Description;
                EventDateTime start = new EventDateTime();
                start.DateTime = task.TaskToDoDate;
                body.Start = start;
                body.End = start;
                //eventsToAdd.Add(body);
                // try catch
                Events existingEvents = service.Events.List("primary").Execute();
                bool eventExists = false;
                foreach (var existingEvent in existingEvents.Items)
                {
                    if (existingEvent.Summary == task.Name && existingEvent.Start.DateTimeDateTimeOffset == task.TaskToDoDate )
                    {
                        if(task.Description=="")
                        {
                            if(existingEvent.Description==null)
                            {
                                eventExists = true;
                                break;
                            }
                        }else { eventExists = true; break; }
                        
                    }
                }

                if (!eventExists)
                {
                    EventsResource.InsertRequest request = new EventsResource.InsertRequest(service, body, "primary");
                    Event response = request.Execute();
                    Trace.WriteLine("Dodano wydarzenie: " + response.Summary + " " + response.Description);
                    Trace.WriteLine("Istniejacego wydarzenia: " + task.Name + " " + task.Description);
                    //Trace.WriteLine("Dodano wydarzenie: " + response.Summary+" "+response.Start.DateTimeDateTimeOffset + " "+response.Description);
                    //Trace.WriteLine("Istniejacego wydarzenia: " + task.Name + " " + task.TaskToDoDate + " " + task.Description);
                }
                else
                {
                    Trace.WriteLine("Wydarzenie już istnieje: " + task.Name);
                }
            }


        }
    }
}
