# webapp_api

## Description

This project is a REST API created using **.NET Core 8**. It is designed to provide a scalable and efficient backend service for handling various operations.

### Features:
- **Swagger**: The API uses Swagger for interactive documentation, which allows for easy testing and exploring of the API endpoints.
- **SQLite**: The API uses **SQLite** as the database for lightweight and embedded data storage.
- **Entity Framework Core (EF Core)**: The API uses **EF Core** as the Object-Relational Mapper (ORM) to interact with the SQLite database.
- **DTOs (Data Transfer Objects)**: The project uses **DTOs** to decouple the API models from the internal data models. This helps ensure efficient data transfer and enhances security by controlling the exposed data.
- **Extensions**: The project includes **extensions** in the **Helpers** folder to simplify common tasks and provide reusable code snippets across the application.
- **Filters**: **Filters** are used to make entry parameters more dynamic. They allow for greater flexibility and customization of API inputs, making the filtering of query parameters and other request data easier and more adaptable.
- **Microsoft Identity**: The API uses **Microsoft Identity** to manage user information, including authentication and authorization. This enables secure management of user accounts and access control.
- **JWT Authentication**: The API uses **JSON Web Tokens (JWT)** to manage authentication. JWTs are used to securely transmit user information between the client and server, ensuring that only authorized users can access protected resources.

### Folder Structure:
- **Config**: Contains configuration files and settings for the application.
- **Controllers**: Contains the API controllers that handle HTTP requests and responses.
- **Data**: Contains database context and initial data seeding logic.
- **Enums**: Contains the enumerations used throughout the application.
- **Helpers**: Contains utility functions and helper classes for the application. This folder also includes:
  - **Extensions**: Contains reusable code extensions that simplify common tasks.
- **Migrations**: Contains the database migration files for EF Core.
- **Models**: Contains the data models that represent the application's entities. This folder also includes:
  - **DTOs**: Contains the Data Transfer Objects (DTOs) for the API.
  - **Filters**: Contains the filters used for making entry parameters more dynamic and flexible.
- **Repository**: Contains the repository pattern for data access logic.
- **Services**: Contains the business logic and services that interact with the repository.

### Design Patterns:
- **Repository Pattern**: The project implements the **Repository Pattern** for abstracting data access logic, which separates the application's business logic from the data layer. This makes it easier to test and maintain.
- **Controller-Action Pattern**: The API follows the **Controller-Action** pattern, where controllers handle the routing and actions perform the necessary logic for each endpoint.
- **Scoped Services**: The project registers services with the **Scoped** lifetime (`AddScoped`), meaning that a new instance of the service is created per request. This is suitable for services that are shared during the processing of a single request but should not be reused across multiple requests.
- **Dependency Injection**: The project uses **Dependency Injection (DI)** to inject dependencies into controllers and services, promoting loose coupling and easier testability.
