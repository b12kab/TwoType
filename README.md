July 2021 Update Message
-------------------------

I've updated this example showing the error. Only the SQLite-net was updated from 1.5.231 to the current version at the time of publishing, which is [1.7.335](https://www.nuget.org/packages/sqlite-net-pcl/1.7.335).

The original source version is listed under the [v1.0](https://github.com/b12kab/TwoType/releases/tag/v1.0) release. The updated 1.7.335 version is on the master branch.

The SQLiteConnection has changed slightly, where as there isn't any fourth parameter; I've modified the connection to remove the third and fourth parameter.

The error message from the 2021 log file [here](https://github.com/b12kab/TwoType/tree/master/problem.vid.and.log/2021_logfile.txt)
2021-07-17 16:13:51.015466-0400 TwoTypeExample.iOS[3734:122935] [logging] API call with invalid database connection pointer

Given no action in two years; if multithreaded database is needed, use another database.

Original Message
-----------------
This is a repurposing of my existing [SQLite Sample](https://github.com/b12kab/SQLiteSample) adding complexity, now with two different types: a contact and a message (for the contact).

The reason I created this was that I noticed an error showing with the iOS version of [SQLite-net-pcl](https://www.nuget.org/packages/sqlite-net-pcl/) showing "API call with invalid database connection pointer". The specific version used is [1.5.231](https://www.nuget.org/packages/sqlite-net-pcl/1.5.231) (the production version at the time of writing). 

When this is compared with the [SQLite Sample](https://github.com/b12kab/SQLiteSample), it's differing in the implementation of how the SQLiteConnection is used. In [SQLite Sample](https://github.com/b12kab/SQLiteSample) the underlying [DatabaseHelper](https://github.com/b12kab/SQLiteSample/blob/master/SQLiteSample/Helpers/DatabaseHelper.cs) creates and keeps a single static SQLiteConnection. In this project, that isn't done for the simple reason that isn't unit testable. Under the [SQLite Sample](https://github.com/b12kab/SQLiteSample), it's not possible to unit test under a Mac (or Windows) - with the way it's done here, the connection is created and passed into the [Repository](https://github.com/b12kab/TwoType/blob/master/TwoTypeExample/Services/Repository.cs). That way the solution can be tested and found working (or not).

Both iOS and Android [DependencyService](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/dependency-service/) open the SQLite in this way:

> SQLiteConnection conn = new SQLiteConnection(_filespec,
>                         SQLiteOpenFlags.SharedCache |
>                         SQLiteOpenFlags.ReadWrite |
>                         SQLiteOpenFlags.Create |
>                         SQLiteOpenFlags.FullMutex,
> true, null);

It works without issue on Android and gives the "[logging] API call with invalid database connection pointer" on iOS. I've tried with and without the SQLiteOpenFlags.SharedCache option, but I thought I'd leave it in for now. While I'm not using this in a multi-threading way, the error falls under that multi-threading category.

The log file and video showing the error are located [here](https://github.com/b12kab/TwoType/tree/master/problem.vid.and.log).

Based on the log, the error comes up in the Finalize of the SQLiteConnection. In my expermientation, this shows up (and is shown in the video) after waiting a few minutes between going into and out of the contact list / add new contact. I put debug write information into the destructors in the various classes to determine this. I've left it in this version for you to see yourself.

Information from SQLite about the open flags is [here](https://www.sqlite.org/threadsafe.html).

A useful Xamarin forms forum entry on multi-threading errors is [here](https://forums.xamarin.com/discussion/549/sqlite-net-and-multiple-threads). 

License
-------

The source for this project is released under the MIT license.