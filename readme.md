### Entity Framework Core 2x InMemory unit testng for windows forms

Code samples showing basics of in memory unit testing

### Dependency injection
While in ASP.NET in memory test is rather easy as dependency injection is done in Startup.cs in ConfigureServices there is no configure service event in windows forms so developers must using a library such as [Simple Injector](https://simpleinjector.readthedocs.io/en/latest/index.html) to setup dependence injection as done in SimpleInjectorWindowsForms1 windows form project.

Simple Injector was picked from a handfull of open source libaries which means if a developer has a go to dependence injection library then replace Simple Injector with that one.

### Unit test
All unit test use [InMemory data provider](https://docs.microsoft.com/en-us/ef/core/providers/in-memory/).