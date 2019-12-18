using Google.Apis.Gmail.v1.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SaintSender.Core.Entities
{
    [Serializable]
    public class SerializableMessage:Message
    {
        //
        // Summary:
        //     The ID of the last history record that modified this message.
        [JsonProperty("historyId")]
        public virtual ulong? HistoryId { get; set; }
        //
        // Summary:
        //     The immutable ID of the message.
        [JsonProperty("id")]
        public virtual string Id { get; set; }
        //
        // Summary:
        //     The internal message creation timestamp (epoch ms), which determines ordering
        //     in the inbox. For normal SMTP-received email, this represents the time the message
        //     was originally accepted by Google, which is more reliable than the Date header.
        //     However, for API-migrated mail, it can be configured by client to be based on
        //     the Date header.
        [JsonProperty("internalDate")]
        public virtual long? InternalDate { get; set; }
        //
        // Summary:
        //     List of IDs of labels applied to this message.
        [JsonProperty("labelIds")]
        public virtual IList<string> LabelIds { get; set; }
        //
        // Summary:
        //     The parsed email structure in the message parts.
        [JsonProperty("payload")]
        public virtual MessagePart Payload { get; set; }
        //
        // Summary:
        //     The entire email message in an RFC 2822 formatted and base64url encoded string.
        //     Returned in messages.get and drafts.get responses when the format=RAW parameter
        //     is supplied.
        [JsonProperty("raw")]
        public virtual string Raw { get; set; }
        //
        // Summary:
        //     Estimated size in bytes of the message.
        [JsonProperty("sizeEstimate")]
        public virtual int? SizeEstimate { get; set; }
        //
        // Summary:
        //     A short part of the message text.
        [JsonProperty("snippet")]
        public virtual string Snippet { get; set; }
        //
        // Summary:
        //     The ID of the thread the message belongs to. To add a message or draft to a thread,
        //     the following criteria must be met: - The requested threadId must be specified
        //     on the Message or Draft.Message you supply with your request. - The References
        //     and In-Reply-To headers must be set in compliance with the RFC 2822 standard.
        //     - The Subject headers must match.
        [JsonProperty("threadId")]
        public virtual string ThreadId { get; set; }
        //
        // Summary:
        //     The ETag of the item.
        [JsonProperty("eTag")]
        public virtual string ETag { get; set; }
        public SerializableMessage(Message originalMsg) {
            this.HistoryId = originalMsg.HistoryId;
            this.Id = originalMsg.Id;
            this.InternalDate = originalMsg.InternalDate;
            this.LabelIds = originalMsg.LabelIds;
            this.Payload = originalMsg.Payload;
            this.Raw = originalMsg.Raw;
            this.SizeEstimate = originalMsg.SizeEstimate;
            this.Snippet = originalMsg.Snippet;
            this.ThreadId = originalMsg.ThreadId;
            this.ETag = originalMsg.ETag;



        }




        public void SerializeMessage()
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            using (StreamWriter txtwriter = File.CreateText($"C:\\Users\\Main\\Desktop\\Codecool_Advanced\\tw_3\\SaintSender\\SaintSender.Core\\SavedMessages\\{Id}.dat"))
            {
                jsonSerializer.Serialize(txtwriter, this);
            }
        }

        public static Message Deserialize(string msgId)
        {
            Message msg = new Message();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (Stream fs = File.Open($"C:\\Users\\Main\\Desktop\\Codecool_Advanced\\tw_3\\Serialization\\Serialisation\\SavedObjects\\{msgId}.dat", FileMode.Open))
            {
                msg = (Message)binaryFormatter.Deserialize(fs);
            }
            return msg;
        }
    }
}
