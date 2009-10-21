
namespace Fohjin.DDD.EventStore.SQLite
{
    public interface ISerializer 
    {
        string Serialize(object theObject);
        TType Deserialize<TType>(string serializedObject);
    }
}