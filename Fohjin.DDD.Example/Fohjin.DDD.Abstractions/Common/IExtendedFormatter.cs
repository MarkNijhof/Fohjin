namespace Fohjin.DDD.Common
{
    public interface IExtendedFormatter
    {
        T Deserialize<T>(Stream stream);
        void Serialize<T>(Stream stream, T graph);
    }
}