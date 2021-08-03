### Entity Framework Core 2x InMemory unit testng for windows forms

:green_circle: Code samples showing basics of in memory unit testing

# Solution structure

There was a mishap when performing an experiment which messed up where several projects got created. Even so, all code works as expected. To fix the folder structure would require more work than any benefits so the folder structure remains as is.

### Dependency injection
While in `ASP.NET` in memory test is rather easy as dependency injection is done in Startup.cs in ConfigureServices there is no configure service event in windows forms so developers must using a library such as [Simple Injector](https://simpleinjector.readthedocs.io/en/latest/index.html) to setup dependence injection as done in SimpleInjectorWindowsForms1 windows form project.

Simple Injector was picked from a handfull of open source libaries which means if a developer has a go to dependence injection library then replace Simple Injector with that one.

### Unit test
All unit test use [InMemory data provider](https://docs.microsoft.com/en-us/ef/core/providers/in-memory/). `SqlLite` is used in several examples.

### Important
EF’s in-memory database `isn’t a relational database` so it does not `enforce constaints` that a real database would.

A bit slower and more work to setup but is a real database consider SQLLite In-Memory.

# Tidbits

[How can I reset an EF7 InMemory provider between unit tests?](https://stackoverflow.com/questions/33490696/how-can-i-reset-an-ef7-inmemory-provider-between-unit-tests)