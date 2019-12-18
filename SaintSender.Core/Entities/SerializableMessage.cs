using Google.Apis.Gmail.v1.Data;
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
    class SerializableMessage : Message
    {
    
        public void SerializeMessage(Message originalMessage)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (Stream stream = File.Open($"C:\\Users\\Main\\Desktop\\Codecool_Advanced\tw_3\\SaintSender\\SaintSender.Core\\SavedMessages\\{originalMessage.Id}.dat", FileMode.Create))
            {
                binaryFormatter.Serialize(stream, this);
            }
        }

        public static Message Deserialize(string msgId)
        {
            Message msg = new Message();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (Stream fs = File.Open($"C:\\Users\\Main\\Desktop\\Codecool_Advanced\\si_3\\Serialization\\Serialisation\\SavedObjects\\{msgId}.dat", FileMode.Open))
            {
                msg = (Message)binaryFormatter.Deserialize(fs);
            }
            return msg;
        }
    }
}
