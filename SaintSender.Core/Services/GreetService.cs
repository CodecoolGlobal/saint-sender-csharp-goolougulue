using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using SaintSender.Core.Interfaces;

namespace SaintSender.Core.Services
{
    public class GreetService : IGreetService
    {
        public string Greet(string name)
        {
            Sample();
            return $"Welcome {name}, my friend!";
        }

        static string[] Scopes = { GmailService.Scope.GmailReadonly };
        static string ApplicationName = "Gmail API .NET Quickstart";

        static void Sample()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Gmail API service.
            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            UsersResource.LabelsResource.ListRequest request = service.Users.Labels.List("me");
            UsersResource.MessagesResource.ListRequest messageRequest = service.Users.Messages.List("me");

            IList<Message> messages = messageRequest.Execute().Messages;
            Console.WriteLine("Messages:");
            if(messages != null && messages.Count >0)
            {
                foreach(var message in messages)
                {
                    string decodedString = "";
                    if (service.Users.Messages.Get("me", message.Id).Execute().Payload.Body.Data != null)
                    {

                        byte[] bytes = Convert.FromBase64String(service.Users.Messages.Get("me", message.Id).Execute().Payload.Body.Data.Replace('-', '+').Replace('_', '/').PadRight(4 * ((service.Users.Messages.Get("me", message.Id).Execute().Payload.Body.Data.Length + 3) / 4), '='));
                        decodedString = Encoding.UTF8.GetString(bytes);
                    }
                    
                    Console.WriteLine(decodedString);
                    Console.WriteLine("{0}", message.Id);
                }
            }else
            {
                Console.WriteLine("No message found");
            }
            // List labels.
            IList<Label> labels = request.Execute().Labels;
            Console.WriteLine("Labels:");
            if (labels != null && labels.Count > 0)
            {
                foreach (var labelItem in labels)
                {
                    Console.WriteLine("{0}", labelItem.Name);
                }
            }
            else
            {
                Console.WriteLine("No labels found.");
            }
            Console.Read();
        }
    }
}
