
x86 vs x64

When running the example on a x86 machine you have to go into the /Lib/sqlite/bin/ folder 
and copy the three System.Data.SQLite.* files into the /Lib/sqlite/bin/x64/ folder. This 
is because I am developing on a x64 system. An interesting fact is that the TestDriven.Net
test runner does actually run in x86 mode, so the SQLite reference in the Test project is 
the the x86 SQLite version already. Resharper test runner acts the same.

If you have any questions or other feedback then I would love to hear about it at
Mark.Nijhof@Gmail.com

-Mark

