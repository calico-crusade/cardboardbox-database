# CardboardBox.Database
A wrapper around Dapper and various ADO.Net providers to connect to different SQL Engines with built in basic query generation.

## Installation
You will probably want to install one of the ADO.net provider specific packages:

Install Postgres via Nuget:
```
PM> Install-Package CardboardBox.Database.Postgres
```

Install SQLite via Nuget:
```
PM> Install-Package CardboardBox.Database.SQLite
```

Install MSSQL / SQL Server via Nuget:
```
PM> Install-Package CardboardBox.Database.MSSQL
```

## Setup
This package is designed to work with `Microsoft.Extensions.DependencyInjection`:

```csharp
using CardboardBox.Database;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddSqlService(c => 
{
	//Configure the SQL engines you want to use
	c.AddPostgres("<connection string>");

	c.AddSqlite("<connection string>");
});
```

## Query Generation
Query Generation is centered around the `IQueryService` that generates queries based on your POCOs.
Said generation uses property names, Dapper Fluent, and optional attributes to generate SQL queries.

