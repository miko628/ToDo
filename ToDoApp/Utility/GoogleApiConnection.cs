using System;
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
        static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        public GoogleApiConnection()
        {
            string CredentialsPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Resource\\credentials.json");
            using (var stream =
                       new FileStream(CredentialsPath, FileMode.Open, FileAccess.Read))
            {
                string Path = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(Path, true)).Result;
                Trace.WriteLine("Credential file saved to: " + Path);
            }
            var service = new CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "ToDoApp"
            }) ;
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            Events events = request.Execute();
            Trace.WriteLine("Upcoming events:");
            if (events.Items == null || events.Items.Count == 0)
            {
                Trace.WriteLine("No upcoming events found.");
                return;
            }
            foreach (var eventItem in events.Items)
            {
                string when = eventItem.Start.DateTime.ToString();
                if (String.IsNullOrEmpty(when))
                {
                    when = eventItem.Start.Date;
                }
                Trace.WriteLine(eventItem.Summary, when);
            }
        }
            
}
}
