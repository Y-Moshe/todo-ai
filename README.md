# Getting started

Todo-AI, a simple .NET WebAPI project with basic authentication configured using MySQL Database, rest API and with OpenAI API(ChatGPT).

### Environment variables

The application uses .NET "Secret manager" More info can be found [here](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets)

1. `cd` into `API` project and run the following commands to set up the secret keys for this app:
2. `dotnet user-secrets set "OpenAPIKey" "[Value]"`

These commands will create a `secrets.json` on your local machine that will be used for this app!  
And as mentioned in the documentation, that will be stored in:

- For windows - `%APPDATA%\Microsoft\UserSecrets\0889f9dd-f77a-406d-9163-55089035a422\secrets.json`
- For Linux/macOs - `~/.microsoft/usersecrets/0889f9dd-f77a-406d-9163-55089035a422/secrets.json`

### Running the application

1. Before attempting to run, just for the development you need to remove the `PropertyGroup` of the docker container in `API` project!
2. In the root of the project create a text file named `db_root_password.txt` and fill the content as database root password, for development you should set password to `toor12345`, if you choosed something else you need to update the `appsettings.Development.json`!
3. Run `dotnet watch --project .\API\` or any other running command, just make sure you run the `API` as entry project.

### Deploy steps

The application is configured to be deployed in AWS EC2 .NET 7 with Docker container.

1. Change the root password to something strong in `db_root_password.txt`.
2. Configure the database connection in `appsettings.json` in `API` project.
3. Run `dotnet publish -c Release --self-contained` to create docker image and push it to docker images.
