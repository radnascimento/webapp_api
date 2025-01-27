# webapp_api

## Description

This project is a REST API created using **.NET Core 8**. It is designed to provide a scalable and efficient backend service for managing student lesson records. The API allows for the registration of students' notes during lessons, including details such as the lesson **level**, **topic**, **material used**, and **study notes**.

### Published URL:
The API is hosted and can be accessed at the following URL:  
**[http://reactservice.somee.com/api/](http://reactservice.somee.com/api/)**

### Features:
- **Swagger**: The API uses Swagger for interactive documentation, which allows for easy testing and exploring of the API endpoints. **Note**: Swagger is only available in the development environment and is not accessible in production.
- **SQLite**: The API uses **SQLite** as the database for lightweight and embedded data storage.
- **Entity Framework Core (EF Core)**: The API uses **EF Core** as the Object-Relational Mapper (ORM) to interact with the SQLite database.
- **DTOs (Data Transfer Objects)**: The project uses **DTOs** to decouple the API models from the internal data models. This helps ensure efficient data transfer and enhances security by controlling the exposed data.
- **Extensions**: The project includes **extensions** in the **Helpers** folder to simplify common tasks and provide reusable code snippets across the application.
- **Filters**: **Filters** are used to make entry parameters more dynamic. They allow for greater flexibility and customization of API inputs, making the filtering of query parameters and other request data easier and more adaptable.
- **Microsoft Identity**: The API uses **Microsoft Identity** to manage user information, including authentication and authorization. This enables secure management of user accounts and access control.
- **JWT Authentication**: The API uses **JSON Web Tokens (JWT)** to manage authentication. JWTs are used to securely transmit user information between the client and server, ensuring that only authorized users can access protected resources.

### Project Purpose:
The project is designed to help educators and students manage lesson records. It allows for registering detailed lesson information, such as:
- **Level**: The difficulty or stage of the lesson.
- **Topic**: The subject matter covered in the lesson.
- **Material Used**: The resources and materials used during the lesson.
- **Study Notes**: The notes taken during the lesson, allowing students to review and track their learning progress.

### Authentication:
To access the API, users must first create an account. This can be done by sending a `POST` request to the **`/api/authentication/register`** endpoint with the following properties in the request body:

- **UserName**: The username for the new user account.
- **Email**: The email address associated with the user account.
- **Password**: The password for the new user account.

Example request body:
```json
{
  "UserName": "exampleUser",
  "Email": "example@example.com",
  "Password": "yourPassword"
}
```

After registering, users can authenticate by sending a `POST` request to the **`/api/authentication/login`** endpoint. This will return a JWT, which can then be used to access protected endpoints of the API.

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

### Future Development:
The project is currently under construction, and additional features and improvements will be implemented in upcoming releases. These will include enhancements to existing functionality, new features, and optimizations to improve overall performance and user experience.

