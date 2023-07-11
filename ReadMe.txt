# CQRS, the book

In 2009 I have had the pleasure of spending a 2 day course and many geek beers with 
Greg Young talking about Domain-Driven Design specifically focussed on Command Query 
Responsibility Segregation (CQRS).

The example project I created based on these discussions was very well received by 
the community and regarded a good reference project to explain and learn the patterns 
that make up CQRS. I decided to add the different blog posts I wrote about the example 
into a single book so it is easy to find and read.

You can find the book here: https://leanpub.com/cqrs

---

# x86 vs x64

When running the example on a x86 machine you have to go into the /Lib/sqlite/bin/ folder 
and copy the three System.Data.SQLite.* files into the /Lib/sqlite/bin/x64/ folder. This 
is because I am developing on a x64 system. An interesting fact is that the TestDriven.Net
test runner does actually run in x86 mode, so the SQLite reference in the Test project is 
the the x86 SQLite version already. Resharper test runner acts the same.

If you have any questions or other feedback then I would love to hear about it at
Mark.Nijhof@Cre8iveThought.com

-Mark

---

While this is based on Mark's book I have been working on updating this to .Net 7.0 

After the effort to convert this to more modern infrastructure hopefully others will find 
this of use.  

-Thanks,
Matt Whited

## Known Issues

All useful tests now pass but there is a concurency issue and some events are not processed
correctly.  The application does not automatically refresh when data is updated.  

