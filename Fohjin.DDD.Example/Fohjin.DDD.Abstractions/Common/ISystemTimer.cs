namespace Fohjin.DDD.Common
{
    public interface ISystemTimer
    {
        object Trigger(Action value, int @in);
    }
}
