# Getting started

Todo-AI, a simple .NET WebAPI project with basic authentication configured using MySQL Database, rest API and with OpenAI API(ChatGPT).

### Environment variables (Development)

The application uses .NET "Secret manager" More info can be found [here](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets)

1. `cd` into `API` project and run the following commands to set up the secret keys for this app:
2. `dotnet user-secrets set "OpenAPIKey" "[Value]"`

These commands will create a `secrets.json` on your local machine that will be used for this app!  
And as mentioned in the documentation, that will be stored in:

- For windows - `%APPDATA%\Microsoft\UserSecrets\0889f9dd-f77a-406d-9163-55089035a422\secrets.json`
- For Linux/macOs - `~/.microsoft/usersecrets/0889f9dd-f77a-406d-9163-55089035a422/secrets.json`

### Running the application (Development)

1. In the root of the project create a text file named `db_root_password.txt` and fill the content as database root password, you should set password to `toor12345`, if you choosed something else you need to update the `appsettings.Development.json`!
2. Remove the
    ```
    api:
        container_name: todo-ai-service
        image: apicontainer:latest
        environment:
            - ASPNETCORE_ENVIRONMENT=docker
            - ASPNETCORE_URLS=http://+:80
        ports:
          - 80:80
        depends_on:
            db:
                condition: service_healthy
    ```
    From the docker-compose, it is meant only for deployment and you only need mysql db.
3. Run `docker-compose up -d` to add mysql container.
4. Remove the `PropertyGroup` of the docker container in `API` project! (used only for publish command)
5. Run `dotnet watch --project .\API\` or any other running command, just make sure you run the `API` as entry project.

### Deploy steps (on local machine)

The application is configured to be deployed in AWS EC2 .NET 7 with Docker container.

1. Change the root password to something strong in `db_root_password.txt`.
2. Configure the database connection in `appsettings.docker.json` in `API` project.
3. Run `dotnet publish -c Release --self-contained` to create docker image and push it to docker images.
4. If you remove the code of the `api` in `docker-compose.yml` restore it, Then run `docker-compose up -d` to add the required containers.

### AWS EC2 Setup & deployment

1. On AWS EC2, create new `instance` choose `ubuntu` linux and select or create `Key pair` for ssh login.
2. Login using `ssh client` or with `AWS connect button`.
3. On the ubuntu system install `dotnet-sdk-7.0` instructions: https://learn.microsoft.com/en-us/dotnet/core/install/linux-scripted-manual as it yet to be supported by aws, manual install is needed.
4. Install `docker & docker-compose` instructions: https://docs.docker.com/engine/install/ubuntu/
5. Run `cd ~` and `git clone https://github.com/Y-Moshe/todo-ai.git`.
6. cd into `todo-ai` project and Run `dotnet restore`
7. Follow the "Deploy steps (on local machine)" as explained above.
8. After successfully completed step 7, For safety reasons, run `docker restart todo-ai-service`.
9. Return to `AWS Console EC2 service` go to the instance and select it > Security tab and click the Security groups.
10. In the Security groups, click `Edit inbound rules` and add new Rule > type Custom TCP, Port 80, and allow Anywhere-IPv4.
11. That's it, Learn by the blog of `By Mukesh Murugan`, src: https://codewithmukesh.com/blog/hosting-aspnet-core-webapi-on-amazon-ec2/.

### Swagger Documentation

http://ec2-44-202-84-235.compute-1.amazonaws.com/swagger/index.html