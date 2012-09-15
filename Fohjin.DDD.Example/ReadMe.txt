x86 vs x64

When running the example on a x86 machine you have to go into the /Lib/sqlite/bin/ folder 
and copy the three System.Data.SQLite.* files into the /Lib/sqlite/bin/x64/ folder. This 
is because I am developing on a x64 system. An interesting fact is that the TestDriven.Net
test runner does actually run in x86 mode, so the SQLite reference in the Test project is 
the the x86 SQLite version already. Resharper test runner acts the same.

If you have any questions or other feedback then I would love to hear about it at
Mark.Nijhof@Gmail.com

I have also written a few blog posts about this CQRS example application:

CQRS a la Greg Young
Link: http://cre8ivethought.com/blog/2009/11/12/cqrs--la-greg-young
Intro: I have had the pleasure of spending a 2 day course and many
geek beers with Greg Young talking about Domain-Driven Design
specifically focussed on the Command and Query Responsibility
Segregation (CQRS) pattern. Greg has taken Domain-Driven Design from
how Eric Evans describes it in his book and has adapted mostly the
technical implementation of it.

CQRS Domain Events
Link: http://cre8ivethought.com/blog/2009/11/20/cqrs-domain-events
Intro: As you may have seen in my previous post “CQRS à la Greg Young”
now our domain aggregate root is responsible for publishing domain
events indicating that some internal state has changed. In fact state
changes within our aggregate root are only allowed through such domain
events.

CQRS Domain State
Link: http://cre8ivethought.com/blog/2009/12/08/cqrs-domain-state
Intro: This morning Aaron Jensen asked a really interesting question
on Twitter “Should Aggregate Roots en Entities always keep their state
if it is not needed for business decisions? Is firing events and
relying on the reporting store enough?”.

Specifications <-- explaining the base test fixture class
Link: http://cre8ivethought.com/blog/2009/12/22/specifications
Intro: I received a couple questions about the Specification Framework
that I use in the CQRS example and thought lets talk about that for a
bit. The first thing that should be underlined is that this is not a
framework, they are a few classes and extension methods that rely on
NUnit for the actual assertions and and Moq  for mocking of the
dependencies. I got the initial bits from Greg Young at his DDD course
which I extended a little bit for my specific needs.

CQRS Event Sourcing
Link: http://cre8ivethought.com/blog/2010/02/05/cqrs-event-sourcing
Intro: So after reading this blog post by Rob Conery about Reporting
In NoSQL where he explains very well what the problem is when using a
RDBMS for persisting the state of your domain, or really anything that
is written with Object Orientation in mind.

CQRS Event Versioning
Link: http://cre8ivethought.com/blog/2010/02/09/cqrs-event-versioning
Intro: When using Event Sourcing you store your events in an Event
Store. This Event Store can only insert new events and read historical
events, nothing more nothing less. So when you change your domain
logic and also the events belonging to this behavior, then you cannot
go back into the Event Store and do a one time convert of all the
historical events belonging to the same behavior. The Event Store
needs to stay intact, that is one of its powers.

CQRS Scaling
Link: http://cre8ivethought.com/blog/2010/02/09/cqrs-scalability
Intro: Scalability is one of the several different benefits you gain
from applying CQRS and Event Sourcing to your application
architecture. And that is what I wanted to take a closer look at in
this post.

Using conventions with Passive View
Link: http://cre8ivethought.com/blog/2009/12/19/using-conventions-with-passive-view
Intro: I was reading Ayende’s blog post about building UI based on
conventions and thought; hey I have something similar in my CQRS
example. And since this is the least interesting part of the whole
example I guess it will be missed by many, and I can’t let that
happen.

-Mark

