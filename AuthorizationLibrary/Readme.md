Этот пакет разработан в рамках дипломной работы Борисова Ярослава Павловича, студента кафедры МОП ЭВМ

Он является частью сервиса по покупке и продаже автомобилей и предоставляет возможность авторизовывать пользователя.

This package was developed as part of the thesis of Yaroslav Borisov.

It is part of the service for buying and selling cars and provides an opportunity to authorize the user.

# Миграции для библиотеки

Создать миграцию
``` shell
 dotnet ef migrations add InitialCreate --context AuthorizationDatabaseContext --output-dir Migrations/  --project ../AuthorizationLibrary --startup-project ../MAM.Authorization
```
Создать скрипт миграции
``` shell
dotnet ef migrations script --context AuthorizationDatabaseContext --project ../AuthorizationLibrary --startup-project ../MAM.Authorization  -o ../AuthorizationLibrary/Migrations/SQL/1.InitialCreate.sql
```
Опубликовать пакет
```shell
dotnet nuget push bin/Debug/AuthorizationLibrary.VERSION.nupkg --api-key your-api-key --source https://api.nuget.org/v3/index.json
```