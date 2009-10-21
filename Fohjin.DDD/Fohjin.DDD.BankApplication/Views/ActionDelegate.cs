using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Fohjin.DDD.BankApplication.Presenters;

namespace Fohjin.DDD.BankApplication.Views
{
    public delegate void Action();

    public class EventLinker
    {
        public static void Link<TView, TPresenter>(TView view, TPresenter presenter)
            where TView : IView
            where TPresenter : IPresenter
        {
            var actionProperties = typeof (TView).GetEvents().Select(x => x.Name).ToList();
            IDictionary<string, EventInfo> viewEvents = new Dictionary<string, EventInfo>();
            view.GetType().GetEvents().Where(x => Contains(actionProperties, x)).ToList().ForEach(x => viewEvents.Add(new KeyValuePair<string, EventInfo>(x.Name, x)));

            IDictionary<string, MethodInfo> presenterEventHandlers = new Dictionary<string, MethodInfo>();
            presenter.GetType().GetMethods().Where(x => Contains(actionProperties, x)).ToList().ForEach(x => presenterEventHandlers.Add(new KeyValuePair<string, MethodInfo>(x.Name, x)));

            foreach (var actionProperty in actionProperties)
            {
                viewEvents[actionProperty].AddEventHandler(presenter, Delegate.CreateDelegate(typeof(TPresenter), presenterEventHandlers[actionProperty.Substring(2)]));
            }

        }

        private static bool Contains(ICollection<string> actionProperties, EventInfo x)
        {
            return actionProperties.Contains(x.Name);
        }

        private static bool Contains(ICollection<string> actionProperties, MethodInfo x)
        {
            return actionProperties.Contains(string.Format("On{0}", x.Name));
        }
    }
}