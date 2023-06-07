# Todo AI - RESTful API Project

.NET WebAPI project with basic authentication configured using MySQL Database, RESTful API, and OpenAI API(ChatGPT).

## Getting Started

**Requirements**:

1. .NET 7 SDK installed.
2. Docker installed. (Required only for MySQL Database, if you have already MySQL installed, you can skip to step 5 but you need to update the `appsettings.Development.json` connection strings)

## Running the app

1. Using Docker secrets, in the root of the project create a text file named `db_root_password.txt` and fill the content as the database root password, you should set the password to `toor12345`, if you choose something else you need to update the `appsettings.Development.json`!
2. Run `docker-compose up -d` to add & install MySQL container to Docker.

The application uses .NET "Secret manager" More info can be found [here](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets)

3. `cd` into `API` project / folder and run the following command to set up the required secret key for this app:
4. `dotnet user-secrets set "OpenAPIKey" "[Value]"`

This command will create a `secrets.json` on your local machine that will be used only for this app!  
As mentioned in the documentation, that will be stored in:

- For windows - `%APPDATA%\Microsoft\UserSecrets\0889f9dd-f77a-406d-9163-55089035a422\secrets.json`
- For Linux/macOs - `~/.microsoft/usersecrets/0889f9dd-f77a-406d-9163-55089035a422/secrets.json`

5. Run `dotnet watch --project .\API\` or any other running command, just make sure you run the `API` as an entry project.

## Deployment steps

The application is configured to be deployed and hosted in **AWS Elastic Beanstalk** using **CodePipeline** together with **CodeBuild** and **CodeDeploy** services to archive `CI/CD` a software development best practice and in order to prevent too frequent deployments it has been set up to track any changes to `production` branch, so any change to production branch will trigger a new deployment process.

To trigger a new deployment process:

1. Do any changes to `master` branch and test it, make sure it works, once that ready make a `git commit`.
2. Switch to `production` branch then make a `git merge master` to sync with the master branch.
3. Upload the changed code to `production` branch using `git push -u origin production` command.
4. Done. Switch back to `master` to avoid mistakes.

## Sources

1. https://aws.amazon.com/blogs/devops/building-net-7-applications-with-aws-codebuild/ - as for the current project building time, support for `.NET 7.0` on **AWS** yet to be released and a **manual installation** is required and archived by connecting to the **AWS Elastic Beanstalk** instance with `SSH` Client using **AWS EC2** service with a `Key Pair`.
2. https://codewithmukesh.com/blog/deploying-aspnet-core-web-api-to-aws-elastic-beanstalk-using-aws-codepipeline/

# Swagger API Documentation

http://todo-ai-server.us-east-1.elasticbeanstalk.com/swagger/index.html
