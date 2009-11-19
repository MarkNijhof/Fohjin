using System;
using System.ComponentModel;
using System.Threading;

namespace Fohjin
{
    public interface IKnowWhen
    {
        void In(int miliseconds);
    }

    public class SystemTimer : IKnowWhen
    {
        private readonly Action _action;
        private readonly BackgroundWorker _backgroundWorker;
        private int _miliseconds;

        private static bool _willUseTheTimer;

        public static void ByPassTimer()
        {
            _willUseTheTimer = true;
        }

        public static void Reset()
        {
            _willUseTheTimer = false;
        }

        public SystemTimer(Action action)
        {
            _action = action;

            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += _backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;
        }

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _action();
        }

        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(_miliseconds);
        }

        public static IKnowWhen Trigger(Action action)
        {
            return new SystemTimer(action);
        }

        public void In(int miliseconds)
        {
            if (_willUseTheTimer)
            {
                _action();
                return;
            }

            _miliseconds = miliseconds;
            _backgroundWorker.RunWorkerAsync();
        }
    }
}