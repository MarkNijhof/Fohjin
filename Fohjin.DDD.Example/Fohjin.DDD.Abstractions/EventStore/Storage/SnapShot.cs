using Fohjin.DDD.EventStore.Storage.Memento;
using System.Text.Json.Serialization;

namespace Fohjin.DDD.EventStore.Storage
{
    public class SnapShot : ISnapShot
    {
        [JsonConstructor]
        public SnapShot() { }
        public SnapShot(Guid eventProviderId, int version, IMemento memento)
        {
            EventProviderId = eventProviderId;
            Version = version;
            Memento = memento;
        }

        public Guid EventProviderId { get; set; }
        public int Version { get; set; }
        public IMemento Memento { get; set; }
    }
}