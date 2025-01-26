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
