
namespace Fohjin.DDD.Domain.Contracts
{
    public interface ISerializer 
    {
        string Serialize(object theObject);
        TType Deserialize<TType>(string serializedObject);
    }
}