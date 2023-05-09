namespace Fohjin.DDD.Common
{
    public class SystemTimer : ISystemTimer
    {
        public void Trigger(Action value, int @in)
        {
            Task.Run(async () =>
            {
                await Task.Delay(@in);
                value();
            });
        }
    }
}