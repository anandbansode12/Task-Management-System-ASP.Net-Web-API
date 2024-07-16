# Task-Management-System-ASP.Net-Web-API

**Project Structure:**
The project is well-structured with separate layers for data access, business logic, and configuration settings. The Data, Models, Controllers, and Settings folders are logically placed.

**Technology Stack:**
1. .NET Core: Utilizes ASP.NET Core for building the Web API.
2. Entity Framework Core: For ORM and database interactions.
3. JWT Authentication: For securing endpoints.

**Key Features Implemented**
1. Database Context: TMSContext class to handle database operations.
2. JWT Authentication: Secure authentication mechanism with token-based authentication.
3. CRUD Operations: Implemented with corresponding endpoints.
4. Authorization Policies: Custom policies to restrict access to certain roles.

**Code Quality and Best Practices:**
1. Consistency: The code follows a consistent structure with proper naming conventions and separation of concerns.
2. Error Handling: Proper error handling and responses are implemented.
3. Security: JWT configuration and role-based authorization are well-set.

**Configuration and Settings:**
1. Configuration File: appsettings.json is used effectively for configuration management.
2. Dependency Injection: Utilizes DI for services and configuration settings.

Overall, the Web API project is robust, well-structured, and follows best practices for .NET Core development. It covers essential functionalities and includes security measures with JWT authentication and role-based access control.
