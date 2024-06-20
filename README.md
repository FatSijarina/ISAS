**Case Management System**

This project is a case management application developed using the latest backend technologies. The application currently runs in Swagger. This document provides detailed instructions for installing, configuring, and running the project.

**Table of Contents**

 - System Requirements
 - Installing Packages
 - Creating the Database
 - Configuring Database Connection
 - Running the Application
 - Automation Building
 - System Monitoring

**System Requirements**

 - .NET 8.0
 - Microsoft SQL Server
 - Visual Studio or another IDE that supports .NET
 - NuGet Packet Manager
 - Prometheus and Grafana (for monitoring)

**Installing Packages**

 - Open the project in your IDE (Visual Studio or another IDE that supports .NET).
 - Open the NuGet Packet Manager Console and run the following commands to install the required packages:

![image](https://github.com/FatSijarina/ISAS/assets/93129989/20247d7c-1db9-49ce-b469-e042f8d61d8c)
![image](https://github.com/FatSijarina/ISAS/assets/93129989/4311367d-5615-42a2-8f5a-12abf7b14ce2)

**Creating the Database**

 - Open SQL Server Management Studio (SSMS) or another database management tool.
 - Create a new database, for example, CaseManagementDB.

**Configuring Database Connection**

 - Open the appsettings.json file in your project.
 - Add or modify the ConnectionStrings section to include your database connection

**Automation Building**

 - Open the test.yml file and adjust the path to the .csproj file of your project

**System Monitoring**

**Setting Up Prometheus**

 - Download and install Prometheus from the official website.
 - Configure prometheus.yml to monitor your application

**Setting Up Grafana**

 - Download and install Grafana from the official website.
 - Open Grafana and add a new data source of type Prometheus.
 - Configure the dashboard to monitor your application's metrics by executing queries.
