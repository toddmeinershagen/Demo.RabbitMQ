using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Demo.RabbitMQ.Serializers
{
    public class BinarySerializer<TMessage> : ISerializer<TMessage>
    {
        public byte[] Serialize(TMessage obj)
        {
            // TODO: do we expect to serialize value types?
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            var binaryFormatter = new BinaryFormatter();
            byte[] contents;
            using (var ms = new MemoryStream())
            {
                binaryFormatter.Serialize(ms, obj);
                contents = ms.GetBuffer();
            }

            return contents;
        }

        public TMessage DeSerialize(byte[] contents)
        {
            if (contents == null)
            {
                throw new ArgumentNullException("contents");
            }

            IFormatter binaryFormater = new BinaryFormatter();
            using (var ms = new MemoryStream(contents))
            {
                ms.Position = 0;
                return (TMessage)binaryFormater.Deserialize(ms);
            }
        }
    }
}
