DwarfAnimeBackend
This is the backend project developed in ASP.NET Core (C#), composed of two applications: DwarfAnimeBackend and DwarfCodeData. The main goal of this system is to provide a REST API that interacts with an SQL database, all running in the cloud through Azure Services and GitHub.

Overview
DwarfAnimeBackend
Main function: This project is designed to expose a REST API that allows communication with frontend clients or applications. The API is implemented using Controllers that define the application's functionalities.
Technologies: ASP.NET Core, C#.
Framework used: Entity Framework Core (ORM) to interact with the database without the need to write manual SQL queries.
DwarfCodeData
Main function: Responsible for managing and persisting data processed by the API into an SQL database.
Technologies: C#, Entity Framework Core.
Key components:
Models: Represent the entities in the database.
Table relationships: Defined to establish how the entities are related.
Migrations: Used to manage changes to the database schema.
Workflow
API on Azure:

The backend and API are fully deployed on Azure, providing a scalable and managed cloud environment.
Swagger is used to efficiently interact with the API and conduct testing.
Database on Azure:

The SQL database is also set up and managed in Azure, offering optimized performance and scalability for data storage.
Continuous Integration and Continuous Deployment (CI/CD):

The entire development process is automated through a CI/CD pipeline that integrates GitHub Actions with Azure Services.
Every code change goes through automated testing, building, and continuous deployment to the cloud environments.
Installation
Prerequisites
.NET Core 6 or higher.
An Azure account to view and manage the deployed services in the cloud.
Access to the GitHub repository if you want to contribute or make modifications.
Steps to run the project locally (optional)
Clone the repository:
bash
Copiar
Editar
git clone https://github.com/yourusername/DwarfAnimeBackend.git
Install dependencies: Navigate to each project's folder and restore the NuGet packages:
bash
Copiar
Editar
cd DwarfAnimeBackend
dotnet restore
cd ../DwarfCodeData
dotnet restore
Configure local database (optional if not using Azure): Modify the appsettings.json file to connect to your local SQL Server database.
Run migrations: From the DwarfCodeData folder, run the migrations:
bash
Copiar
Editar
dotnet ef database update
Run the backend: Navigate to DwarfAnimeBackend and run the project:
bash
Copiar
Editar
dotnet run
Access the API: The API will be available at http://localhost:5000.
Accessing the API on Azure
To access the API deployed on Azure, simply visit the provided URL in the Azure configuration. The production environment is fully in the cloud, so you don't need to run the project locally if you just want to interact with the API.

CI/CD with Azure and GitHub Actions
This project uses GitHub Actions to automate the Continuous Integration (CI) and Continuous Deployment (CD) process. Code changes are automatically tested, built, and deployed to Azure services.

CI/CD Flow
Development: Each time a push or pull request is made to the repository, GitHub Actions triggers a series of steps including code compilation, unit tests execution, and code quality validation.
Automatic deployment: When the tests and build are successful, the changes are automatically deployed to Azure, both for the backend and the SQL database.
Scalability: Azure services allow automatic scaling for both the backend and the database to handle higher amounts of traffic and data.
