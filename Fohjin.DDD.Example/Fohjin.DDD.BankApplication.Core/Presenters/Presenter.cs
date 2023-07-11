using Fohjin.DDD.BankApplication.Views;
using System.Diagnostics;
using System.Reflection;

namespace Fohjin.DDD.BankApplication.Presenters
{
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

            foreach (var viewDefinedEvent in viewDefinedEvents)
            {
                var eventInfo = viewEvents[viewDefinedEvent];
                var methodInfo = GetTheEventHandler(viewDefinedEvent, presenterEventHandlers);

                if (methodInfo == null)
                {
                    Debug.WriteLine($"There is no event handler for event '{eventInfo.Name}' on presenter '{GetType().FullName}' expected '{viewDefinedEvent}");
                    continue;
                }
                WireUpTheEventAndEventHandler(view, eventInfo, methodInfo);
            }
        }

        private static MethodInfo? GetTheEventHandler(string viewDefinedEvent, IDictionary<string, MethodInfo>? presenterEventHandlers)
        {
            var substring = viewDefinedEvent[2..];
            if (!presenterEventHandlers?.ContainsKey(substring) ?? false)
                return null;

            return presenterEventHandlers?[substring];
        }

        private void WireUpTheEventAndEventHandler(TView view, EventInfo eventInfo, MethodInfo methodInfo)
        {
            var newDelegate = Delegate.CreateDelegate(typeof(EventAction), this, methodInfo);
            eventInfo.AddEventHandler(view, newDelegate);
        }

        private static IDictionary<string, MethodInfo>? GetPresenterEventHandlers<TPresenter>(ICollection<string> actionProperties, TPresenter presenter)
        {
            return presenter?
                .GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .Where(x => Contains(actionProperties, x))
                .ToList()
                .Select(x => new KeyValuePair<string, MethodInfo>(x.Name, x))
                .ToDictionary(x => x.Key, x => x.Value);
        }

        private static List<string> GetViewDefinedEvents()
        {
            return typeof(TView).GetEvents().Select(x => x.Name).ToList();
        }

        private static IDictionary<string, EventInfo> GetViewEvents(TView view, ICollection<string> actionProperties)
        {
            return view
                .GetType()
                .GetEvents()
                .Where(x => Contains(actionProperties, x))
                .ToList()
                .Select(x => new KeyValuePair<string, EventInfo>(x.Name, x))
                .ToDictionary(x => x.Key, x => x.Value);
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