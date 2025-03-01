# Introduction

# Database Connection

## Migrations
```csharp
dotnet ef migrations add <MigrationName> --project <Project> --context <Context> --output-dir Infrastructure/Data/Migrations
```

```csharp
dotnet ef database update --context <Context>
```

