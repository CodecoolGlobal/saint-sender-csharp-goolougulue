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
            JsonSerializer jsonSerializer = new JsonSerializer();
            using (StreamWriter txtwriter = File.CreateText($"C:\\Users\\Main\\Desktop\\Codecool_Advanced\\tw_3\\SaintSender\\SaintSender.Core\\SavedMessages\\{originalMsg.Id}.dat"))
            {
                jsonSerializer.Serialize(txtwriter, originalMsg);
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
