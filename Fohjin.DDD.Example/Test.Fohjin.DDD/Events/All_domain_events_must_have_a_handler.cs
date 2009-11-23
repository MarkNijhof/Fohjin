using System;
using System.Linq;
using System.Text;
using Fohjin.DDD.Configuration;
using NUnit.Framework;

namespace Test.Fohjin.DDD.Events
{
    [TestFixture]
    public class All_domain_events_must_have_a_handler
    {
        [Test]
        public void Verify_that_each_event_has_atleast_one_event_handler()
        {
            var events = EventHandlerHelper.GetEvents();
            var eventHandlers = EventHandlerHelper.GetEventHandlers();

            var stringBuilder = new StringBuilder();
            foreach (var theEvent in events.Where(theEvent => !eventHandlers.ContainsKey(theEvent)))
            {
                stringBuilder.AppendLine(string.Format("No event handler found for event '{0}'", theEvent.FullName));
                continue;
            }
            if (stringBuilder.Length > 0)
                throw new Exception(string.Format("\n\nEvent handler exceptions:\n{0}\n", stringBuilder));
        }
    }
}