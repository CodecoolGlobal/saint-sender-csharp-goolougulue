using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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
        private ObservableCollection<Message> _messages = new ObservableCollection<Message>();
        public GreetService()
        {
            Sample();
        }

        public GreetService(ObservableCollection<Message> list)
        {
            Sample();
            _messages = list;
        }

        public string Greet(string name)
        {
            Sample();
            return $"Welcome {name}, my friend!";
        }

        static string[] Scopes = { GmailService.Scope.MailGoogleCom };
        static string ApplicationName = "Gmail API .NET Quickstart";
        private GmailService _service;

        public void Sample()
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

            _service = service;
        }

        public IList<Label> GetMailFolder ()
        {
            UsersResource.LabelsResource.ListRequest request = _service.Users.Labels.List("me");

            IList<Label> folders = request.Execute().Labels;

            IEnumerable<Label> sortedFolders = folders.OrderBy(f => f.Name);
            IList<Label> sortedList = sortedFolders.ToList();
            IEnumerable<Label> inboxSearch = sortedList.Where(label => label.Name == "INBOX");
            Label inbox = inboxSearch.First();
            sortedList.Remove(inbox);
            sortedList.Insert(0, inbox);

            return sortedList;
        }

        public IList<Message> GetMails (string folderId)
        {
            _messages.Clear();
            UsersResource.MessagesResource.ListRequest messageRequest = _service.Users.Messages.List("me");
            IList<Message> fullMessages = new List<Message>();
            IList<Message> messages = messageRequest.Execute().Messages;

            if (messages != null && messages.Count > 0)
            {
                foreach (var message in messages)
                {
                    var folderIds = _service.Users.Messages.Get("me", message.Id).Execute().LabelIds;
                    if (folderIds.Contains(folderId))
                    {
                        _messages.Add(message);
                        fullMessages.Add(message);
                        if(_messages.Count > 0)
                        {
                            break;
                        }
                    }
                }
            }

            return fullMessages;
        }

        private string DecodeMessageBody (Message message)
        {
            var messageBody = _service.Users.Messages.Get("me", message.Id).Execute().Payload.Body.Data;

            string decodedString = "";
            if (messageBody != null)
            {
                byte[] bytes = Convert.FromBase64String(messageBody.Replace('-', '+').Replace('_', '/').PadRight(4 * ((messageBody.Length + 3) / 4), '='));
                decodedString = Encoding.UTF8.GetString(bytes);
            }

            return decodedString;
        }

    }
}
