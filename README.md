# AccessControlCountry

This application is designed to manage the personnel accessing private neighborhoods or companies. In it, an employee using the application created in MAUI will be able to log in through a simple portal and register a family member, a visitor, or a temporary worker.

The security staff will only review the documents and allow access.

Additionally, it is expected to:

1.Connect it to a service system that allows interaction with a barrier opening service.
2.Connect it to LPR (License Plate Recognition) systems for intelligent barrier opening when a vehicle is registered as belonging to an owner or temporary personnel.
3.Control the entry and exit times of the workers in question.
4.Monitor the visiting hours and register belongings and items taken from the establishment.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Below are the step-by-step details to start and test this project if needed:

Prerequisites:

1. You must have SQL Server 16.0 or higher installed.
2. You must have IIS version 10.0 or higher installed.
3. You need to have the latest available .NET Core Hosting Bundle installed. Here is the most current version at the time of writing this document => https://dotnet.microsoft.com/download/dotnet/thank-you/runtime-aspnetcore-8.0.10-windows-hosting-bundle-installer

Step by Step:

1.The service requires a database with a structure that will be provided in a file within a folder named "Backup."
2.This file must be restored in SQL Server.
3.Once the database is restored, you should navigate to the "WebServiceAccess" folder, where you will find the appsettings.json.
4.Modify the DefaultConnection property in the JSON, using this format: "Server=Server_Instance;Database=Database_Name;User Id=SQL_Server;Password=User_Password;"
5.After this, you need to start the projects named "AccessControlFront" and "WebServiceAccess."

If there are no issues during execution, you will be able to access the service without any problems.
Error Reporting:

*If you notice any errors or possible improvements, you can contact me directly through the following means:*
GMAIL: *cristianleonardo231197@gmail.com*

I hope you enjoy this project, and feel free to report any issues!
