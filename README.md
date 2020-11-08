# Blazor


### Create the database (taken from [here](https://docs.microsoft.com/en-us/ef/core/get-started/?tabs=netcore-cli))
```bash
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef migrations add InitialCreate
dotnet ef database update
```
