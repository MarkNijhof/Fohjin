
namespace Fohjin.EventStorage
{
    public interface ISerializer 
    {
        string Serialize(object theObject);
        TType Deserialize<TType>(string serializedObject);
    }
}