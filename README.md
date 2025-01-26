# DwarfAnimeBackend

This is the backend project developed in ASP.NET Core (C#), composed of two applications: **DwarfAnimeBackend** and **DwarfCodeData**. The main goal of this system is to provide a REST API that interacts with an SQL database, all running in the cloud through **Azure Services** and **GitHub**.

## Overview

### **DwarfAnimeBackend**

- **Main function**: This project is designed to expose a REST API that allows communication with frontend clients or applications. The API is implemented using **Controllers** that define the application's functionalities.
- **Technologies**: ASP.NET Core, C#.
- **Framework used**: **Entity Framework Core** (ORM) to interact with the database without the need to write manual SQL queries.

### **DwarfCodeData**

- **Main function**: Responsible for managing and persisting data processed by the API into an SQL database.
- **Technologies**: C#, Entity Framework Core.
- **Key components**:
  - **Models**: Represent the entities in the database.
  - **Table relationships**: Defined to establish how the entities are related.
  - **Migrations**: Used to manage changes to the database schema.

## Workflow

1. **API on Azure**:
   - The backend and API are fully deployed on **Azure**, providing a scalable and managed cloud environment.
   - **Swagger** is used to efficiently interact with the API and conduct testing.

2. **Database on Azure**:
   - The SQL database is also set up and managed in **Azure**, offering optimized performance and scalability for data storage.

3. **Continuous Integration and Continuous Deployment (CI/CD)**:
   - The entire development process is automated through a **CI/CD** pipeline that integrates **GitHub Actions** with **Azure Services**.
   - Every code change goes through automated testing, building, and continuous deployment to the cloud environments.

## Installation

### Prerequisites

- .NET Core 6 or higher.
- An **Azure** account to view and manage the deployed services in the cloud.
- Access to the **GitHub** repository if you want to contribute or make modifications.

### Steps to run the project locally (optional)

1. **Clone the repository**:
   ```bash
   git clone https://github.com/yourusername/DwarfAnimeBackend.git
   Install dependencies: Navigate to each project's folder and restore the NuGet packages:

2. **Install dependencies:** Navigate to each project's folder and restore the NuGet packages:
    ```bash
    cd DwarfAnimeBackend
    dotnet restore
    cd ../DwarfCodeData
    dotnet restore

3. **Configure local database:**
  (optional if not using Azure): Modify the appsettings.json file to connect to your local SQL Server database.

4. **Run migrations**:
     ```bash
    dotnet ef database update
     
5. **Run migrations**:
    ```bash
    dotnet run

6. **Access the API:**
    To access the API locally, you'll need to run the backend project and ensure that **Swagger** is enabled for testing and interacting with the API in your local environment. Follow these steps:

    1. **Run the backend locally**: 
     - If you want to run the project locally, navigate to the `DwarfAnimeBackend` folder and use the following command:
     ```bash
     dotnet run
     ```
     - This will start the API on your local machine, usually accessible via `http://localhost:5000`.

    2. **Activate Swagger**:
     - Swagger is included by default for local development to interact with the API. 
     - When you run the project locally, Swagger will be available at `http://localhost:5000/swagger` (or another port, depending on your configuration).

    3. **Configure appsettings.json**:
     - In the `appsettings.json` file, configure the appropriate connection strings or settings to point to your local database if necessary. You will need to adjust the connection string to connect to your local SQL Server database (if not using Azure):
     ```json
     {
       "ConnectionStrings": {
         "DefaultConnection": "Server=localhost;Database=YourDatabaseName;Trusted_Connection=True;MultipleActiveResultSets=true"
       }
     }
     ```
     - This ensures the API uses the correct database when running locally.

By following these steps, you can easily run and test the API locally, interact with it via Swagger, and ensure it works before deploying it to Azure.
  
### Accessing the API on Azure

To access the API deployed on Azure, simply visit the provided URL in the Azure configuration. The production environment is fully in the cloud, so you don't need to run the project locally if you just want to interact with the API.

## CI/CD with Azure and GitHub Actions

This project uses **GitHub Actions** to automate the **Continuous Integration (CI)** and **Continuous Deployment (CD)** process. Code changes are automatically tested, built, and deployed to **Azure services**.

### CI/CD Flow

1. **Development**: Each time a push or pull request is made to the repository, GitHub Actions triggers a series of steps including code compilation, unit tests execution, and code quality validation.

2. **Automatic deployment**: When the tests and build are successful, the changes are automatically deployed to **Azure**, both for the backend and the SQL database.

3. **Scalability**: Azure services allow automatic scaling for both the backend and the database to handle higher amounts of traffic and data.


## Future Updates

In the upcoming releases, I plan to implement the following improvements and new features:

1. **Secure Password Hashing with PBKDF2**:
   - Currently, passwords in the application are stored without any security measures. In the future, I will implement a secure hashing algorithm using **PBKDF2** to hash passwords. This will significantly improve the security of user credentials by preventing plaintext password storage.

2. **Enhance API Functionality**:
   - I aim to add more features and endpoints to the API to improve its functionality. These will include additional resources, advanced queries, and improved user interactions, making the API more powerful and flexible for frontend integration.

3. **Integrate External API for Data Collection**:
   - Another planned feature is to integrate an external API to gather additional data that will be used within the program. This will allow the application to enrich its content, provide more dynamic responses, and offer better data to users.

These updates are planned to enhance the overall security, functionality, and data capabilities of the application, ensuring that it remains robust and scalable in the future.

 






