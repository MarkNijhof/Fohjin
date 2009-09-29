using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Fohjin.DDD.Domain.Contracts;

namespace Fohjin.DDD.Infrastructure
{
    public class Serializer : ISerializer
    {
        public string Serialize(object theObject)
        {
            var binaryFormatter = new BinaryFormatter();
            var memoryStream = new MemoryStream();

            try
            {
                binaryFormatter.Serialize(memoryStream, theObject);
                memoryStream.Position = 0;

                return Convert.ToBase64String(memoryStream.ToArray());
            }
            finally
            {
                memoryStream.Close();
            }
        }

        public TType Deserialize<TType>(string serializedObject)
        {
            var binaryFormatter = new BinaryFormatter();
            var bytes = Convert.FromBase64String(serializedObject);
            var memoryStream = new MemoryStream(bytes);
            try
            {
                memoryStream.Position = 0;
                var domainEvent = (TType)binaryFormatter.Deserialize(memoryStream);
                return domainEvent;
            }
            finally
            {
                memoryStream.Close();
            }
        }
    }
}