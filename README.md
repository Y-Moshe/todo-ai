# Getting started

.NET WebAPI project with basic authentication configured using SQLite.

#### Environment variables

The application uses .NET "Secret manager" More info can be found [here](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets)

1. `cd` into `API` project and run the following commands to set up the secret keys for this app:
2. `dotnet user-secrets set "OpenAPIKey" "[Value]"`

These commands will create a `secrets.json` on your local machine that will be used for this app!  
And as mentioned in the documentation, that will be stored in:

- For windows - `%APPDATA%\Microsoft\UserSecrets\0889f9dd-f77a-406d-9163-55089035a422\secrets.json`
- For Linux/macOs - `~/.microsoft/usersecrets/0889f9dd-f77a-406d-9163-55089035a422/secrets.json`
