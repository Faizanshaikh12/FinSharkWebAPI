# FinSharkWebAPI
Stock Market / Social Media Platform .NET Core Web API

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) version 8.0 or later

## Installation

1. Clone the repository:
   ```sh
   git clone https://github.com/Faizanshaikh12/FinSharkWebAPI.git
   cd FinSharkWebAPI
   ```

2. Restore the dependencies:
   ```sh
   dotnet restore
   ```

## Running the Project

1. Build the project:
   ```sh
   dotnet build
   ```

2. Run the project:
   ```sh
   dotnet run
   dotnet watch run
   ```

## Entity Framework Core Migrations

### Adding a Migration
```sh
dotnet ef migrations add <MigrationName>
dotnet ef migrations add InitialCreate
```

### Updating the Database
```sh
dotnet ef database update
dotnet ef database update <PreviousMigration>
```

### Removing the Last Migration
```sh
dotnet ef migrations remove
```

### Listing Migrations
```sh
dotnet ef migrations list
```

### Generating SQL Script for Migrations
```sh
dotnet ef migrations script
dotnet ef migrations script <FromMigration> <ToMigration>
dotnet ef migrations script InitialCreate AddNewTable
```