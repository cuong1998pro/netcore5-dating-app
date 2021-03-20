## Tạo solution

### Create solution file

```
dotnet new sln
```

### Create Api project (-o output directory)

```
dotnet new webapi -o API
```

### Add Api to solution

```
dotnet sln add API
```

### Extensions:

> C# Microsoft: Main
> C# extension (2nd): Tạo class, trick
> Nuget Gallery
> SqlLite

---

## Ctrl + Shift + P

### Tao file debug

> Generate Assets for Build and Debug

### Hide obj, bin:

> File exclude -> Add **/obj, **/bin

### Tat compact folder:

> Compact folder -> turn off

### Cai dat khac

> Private member prefix: \_
> Use this for ctor assignment: false

---

## Lenh khi code

### Build and run:

```
dotnet run
dotnet dev-certs https --trust
dotnet watch run
```

### Cai dotnet ef:

```
Len nuget tim lenh cai dotnet-ef
dotnet tool install --global dotnet-ef --version 6.0.0-preview.2.21154.2
```

### Create migration:

```
dotnet ef migrations add InitialCreate -o Data/Migrations
```

### Create database:

```
dotnet ef update
dotnet ef database update
```
