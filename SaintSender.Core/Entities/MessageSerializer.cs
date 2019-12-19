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

    public class MessageSerializer
    {

        public static void SerializeMessage(Message originalMsg)
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"SavedEmails\", $"{originalMsg.Id}.dat");
            JsonSerializer jsonSerializer = new JsonSerializer();
            using (StreamWriter txtwriter = File.CreateText(path))
            {
                jsonSerializer.Serialize(txtwriter, originalMsg);
            }
        }

        public static Message Deserialize(string msgId)
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"SavedEmails\", $"{msgId}.dat");
            Message msg = new Message();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (Stream fs = File.Open(path, FileMode.Open))
            {
                msg = (Message)binaryFormatter.Deserialize(fs);
            }
            return msg;
        }
    }
}
