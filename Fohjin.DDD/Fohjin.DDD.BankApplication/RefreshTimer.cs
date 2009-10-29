using System;
using System.ComponentModel;
using System.Threading;

namespace Fohjin.DDD.BankApplication
{
    public interface IKnowWhen
    {
        void In(int miliseconds);
    }
    public class RefreshTimer : IKnowWhen
    {
        private readonly Action _action;
        private readonly BackgroundWorker _backgroundWorker;
        private int _miliseconds;

        public RefreshTimer(Action action)
        {
            _action = action;

            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += _backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;
        }

        void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _action();
        }

        void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(_miliseconds);
        }

        public static IKnowWhen Trigger(Action action)
        {
            return new RefreshTimer(action);
        }

        public void In(int miliseconds)
        {
            _miliseconds = miliseconds;
            _backgroundWorker.RunWorkerAsync();
        }
    }
}