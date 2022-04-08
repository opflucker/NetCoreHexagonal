# .NET Core with Hexagonal Architecture
Example .NET Core applications (console and webapi), with hexagonal architecture (ports and adapters).

# About the solution
Hexagonal architecture helps to visualize the solution global workflow because it is implicit in its organization: 

![image](https://user-images.githubusercontent.com/63680171/162287171-07330a99-15bc-4a36-86ee-b5e0c834b93e.png)

The solution has:
1. Two in-adapters corresponding to executable assemblies. These adapters interact with the application (NetCoreHexagonal.Application) using an in-port.
2. One in-port: ISchoolService.
3. One in-port implementation: SchoolService. This implementation interacts with external services that fullfill two out-ports.
4. Two out-ports defined with interfaces in folders EventsDispatching and Persistence.
5. Two out-adapters implementing each out-port.
