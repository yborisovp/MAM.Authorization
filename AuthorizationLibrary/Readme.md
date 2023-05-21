#Миграции для библиотеки

Создатьь ммиграцию
```
 dotnet ef migrations add InitialCreate --context AuthorizationDatabaseContext --output-dir Migrations/  --project ./AuthorizationLibrary --startup-project ./MAM.Authorization
```
Создать скрипт миграции
```
dotnet ef migrations script --context AuthorizationDatabaseContext --project ./AuthorizationLibrary --startup-project ./MAM.Authorization  -o ./AuthorizationLibrary/Migrations/SQL/1.InitialCreate.sql
```
