A complete project for authentication using .net core

Using clean archtecture, it is a "modularized monolyth" but it could transform it micro services easilly if needed

Features:
creation and update of users

a simple but effective and secure way of using permissions that will work in the whole system

password is hashed before saving into database using aspnetcore identity PasswordHasher

a pattern to return to front end the treated errors, ATTENTION: THE ERRORS ARE RETURNING IN PORTUGUESE

it is using fluent validator to validate most of the cases

a middleware to treat the exceptions, in the log it is putting the controller name, the exception the action(method name) to check errors in the application

a base repository archtecture to facilitate development

a context pre-loaded to not have problems if you want to create tests for it(having HttpContext in the constructor of services and repositories would have problems to be mocked in tests)

it is using Sql Server but could be easily changed to Postgress or MySQL if needed

to run migrations 

for a new migration

dotnet ef migrations add AddNewColumnsUser --project .\CleanArchMonolit.Infrastructure.csproj --startup-project ..\App.WebAPI\App.WebAPI.csproj --context AuthDbContext --output-dir Auth\Data\Migrations

for update

dotnet ef database update --project .\CleanArchMonolit.Infrastructure.csproj --startup-project ..\App.WebAPI\App.WebAPI.csproj --context AuthDbContext
