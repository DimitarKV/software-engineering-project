# software-engineering-project

Проект по СИ (хотелски резервации)

# prerequisite

In order to run the project you need to have a running mssql db on port 1433, the app is using db user and password stored in the user-secrets
This can be configured as follows:

```
dotnet user-secrets set "DbUser" "user"
dotnet user-secrets set "DbPassword" "password"
```

in the project folder using a terminal
