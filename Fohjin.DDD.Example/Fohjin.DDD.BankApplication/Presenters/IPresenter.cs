using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Fohjin.DDD.BankApplication.Views;

namespace Fohjin.DDD.BankApplication.Presenters
{
    public interface IPresenter
    {
        void Display();
    }

    public abstract class Presenter<TView> where TView : class, IView
    {
        protected Presenter(TView view)
        {
            HookUpViewEvents(view);
        }

        private void HookUpViewEvents(TView view)
        {
            var viewDefinedEvents = GetViewDefinedEvents();
            var viewEvents = GetViewEvents(view, viewDefinedEvents);
            var presenterEventHandlers = GetPresenterEventHandlers(viewDefinedEvents, this);

            foreach (var actionProperty in viewDefinedEvents)
            {
                var eventInfo = viewEvents[actionProperty];

                var substring = actionProperty.Substring(2);
                if (!presenterEventHandlers.ContainsKey(substring))
                    throw new Exception(string.Format("\n\nThere is no event handler for event '{0}' on presenter '{1}' expected '{2}'\n\n", eventInfo.Name, GetType().FullName, substring));

                var methodInfo = presenterEventHandlers[substring];
                var newDelegate = Delegate.CreateDelegate(typeof(EventAction), this, methodInfo);
                eventInfo.AddEventHandler(view, newDelegate);
            }
        }

        private static IDictionary<string, MethodInfo> GetPresenterEventHandlers<TPresenter>(ICollection<string> actionProperties, TPresenter presenter)
        {
            IDictionary<string, MethodInfo> presenterEventHandlers = new Dictionary<string, MethodInfo>();
            presenter
                .GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .Where(x => Contains(actionProperties, x))
                .ToList()
                .ForEach(x => presenterEventHandlers.Add(new KeyValuePair<string, MethodInfo>(x.Name, x)));
            return presenterEventHandlers;
        }

        private static List<string> GetViewDefinedEvents()
        {
            return typeof(TView).GetEvents().Select(x => x.Name).ToList();
        }

        private static IDictionary<string, EventInfo> GetViewEvents(TView view, ICollection<string> actionProperties)
        {
            IDictionary<string, EventInfo> viewEvents = new Dictionary<string, EventInfo>();
            view
                .GetType()
                .GetEvents()
                .Where(x => Contains(actionProperties, x))
                .ToList()
                .ForEach(x => viewEvents.Add(new KeyValuePair<string, EventInfo>(x.Name, x)));
            return viewEvents;
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