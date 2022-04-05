# Migrations
When performing EF Core tools commands in Visual Studio Package Manager Console, specify `NetCoreManualDI.Persistence` as *tarjet* and *startup* projects. For example, when adding a new migration *New-Migration-Name*, use:

```
Add-Migration -Name New-Migration-Name -Project NetCoreManualDI.Persistence -StartupProject NetCoreManualDI.Persistence
```
