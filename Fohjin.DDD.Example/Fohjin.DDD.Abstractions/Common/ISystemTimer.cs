namespace Fohjin.DDD.Common
{
    public interface ISystemTimer
    {
        void Trigger(Action value, int @in);
    }
}
