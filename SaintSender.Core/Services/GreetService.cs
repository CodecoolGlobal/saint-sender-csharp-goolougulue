using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using SaintSender.Core.Entities;
using SaintSender.Core.Interfaces;

namespace SaintSender.Core.Services
{
    public class GreetService : IGreetService
    {
        public GreetService()
        {
            Sample();
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

        public void GetMails (string folderId,ObservableCollection<Message> _messages, object _itemslock)
        {
            //_messages.Clear();
            UsersResource.MessagesResource.ListRequest messageRequest = _service.Users.Messages.List("me");
            IList<Message> messages = messageRequest.Execute().Messages;

            if (messages != null && messages.Count > 0)
            {
                foreach (var message in messages)
                {
                    var fullMessage = _service.Users.Messages.Get("me", message.Id).Execute();
                    var folderIds = fullMessage.LabelIds;
                    if (folderIds.Contains(folderId))
                    {
                        lock (_itemslock)
                        {
                            _messages.Add(fullMessage);
                        }
                        if(_messages.Count > 20)
                        {
                            break;
                        }
                    }
                }
            }
         }

        public void SendEmail(CostumMail email)
        {
            MessageCoding coder = new MessageCoding();
            var mailMessage = MakeMessage(email);

            var mimeMessage = MimeKit.MimeMessage.CreateFromMailMessage(mailMessage);

            var gmailMessage = new Google.Apis.Gmail.v1.Data.Message
            {
                Raw = coder.Encode(mimeMessage.ToString())
            };

            UsersResource.MessagesResource.SendRequest request = _service.Users.Messages.Send(gmailMessage, "me");

            request.Execute();
        }

        public MailMessage MakeMessage(CostumMail email)
        {
            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(address: email.FromAddress);
            mailMessage.To.Add(email.ToRecipients);
            mailMessage.ReplyToList.Add(email.FromAddress);
            mailMessage.Subject = email.Subject;
            mailMessage.Body = email.Body;
            mailMessage.IsBodyHtml = email.IsHtml;


            foreach (Attachment attachment in email.Attachments)
            {
                mailMessage.Attachments.Add(attachment);
            }

            return mailMessage;
        }

        public string Greet(string name)
        {
            throw new NotImplementedException();
        }
    }
}
