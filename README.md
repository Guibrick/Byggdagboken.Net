# Byggdagboken.Net

This project has the same objective that the one made in [ByggdagbokenTest](https://github.com/Guibrick/ByggdagbokenTest), store persons data in a 
database and display it in a frontend. In this case the stack used for making it possible is .Net MVC for the Frontend and Backend 
while Microsoft SQL Server for the database.

## Run the application

You should start by creating the database in a Docker container. In order to do that, paste the following line in you command prompt:

```
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Byggdagboken2023" -p 1433:1433 --name "Byggdagboken" -d mcr.microsoft.com/mssql/server:2022-latest
```

Once that the container is running, you can go to the ***Byggdagboken*** folder and run the app from the terminal:

```
dotnet run
```

Last step is go to the broswer, and introduce the following path:

http://localhost:5095/People

Then you have the application up and running.
