using System;
using System.Linq;
using System.Text;
using Fohjin.DDD.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fohjin.DDD.Events
{
    [TestClass]
    public class All_domain_events_must_have_a_handler
    {
        [TestMethod]
        public void Verify_that_each_event_has_atleast_one_event_handler()
        {
            Assert.Inconclusive("This needs done a different way");
            //var events = EventHandlerHelper.GetEvents();
            //var eventHandlers = EventHandlerHelper.GetEventHandlers();

            //var stringBuilder = new StringBuilder();
            //foreach (var theEvent in events.Where(theEvent => !eventHandlers.ContainsKey(theEvent)))
            //{
            //    stringBuilder.AppendLine(string.Format("No event handler found for event '{0}'", theEvent.FullName));
            //    continue;
            //}
            //if (stringBuilder.Length > 0)
            //    throw new Exception(string.Format("\n\nEvent handler exceptions:\n{0}\n", stringBuilder));
        }
    }
}