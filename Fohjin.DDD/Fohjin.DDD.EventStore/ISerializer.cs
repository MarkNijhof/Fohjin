namespace Fohjin.DDD.EventStore
{
    public interface ISerializer 
    {
        string Serialize(object theObject);
        TType Deserialize<TType>(string serializedObject);
    }
}