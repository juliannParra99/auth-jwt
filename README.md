# .NET Core Authentication and Authorization Module with Entity Framework

https://github.com/juliannParra99/auth-jwt/assets/104667764/a28fafc2-d02e-4196-91e9-b05b52115d49

This project is a .NET Core module that handles user authentication and authorization with Entity Framework Core for data storage. Users cannot access backend data without proper credentials.


## Features:

- **User Registration:** Users register with email and password.
- **Login:** Users log in with registered email and password.
- **JWT Token Generation:** Upon successful login, a JWT token is generated for authorization.
- **Weather Forecast API:** Users can access weather forecast information with a valid JWT token.

## Main Technology Stack:

- .NET Core
- Entity Framework Core
- ASP.NET Core Identity (for user management)
- SQLite

## API Routes:

- **/register:** User registration endpoint (POST request)
- **/login:** User login endpoint (POST request)
- **/weatherForecast:** Weather forecast information endpoint (GET request with authorization)

## Getting Started:

```bash

# Install dependencies using
dotnet restore
# Configure connection strings in appsettings.json.

# Run migrations using
dotnet ef migrations add <migration_name>
dotnet ef database update.
# Start the project using
dotnet run.
```

## Usage:

- Refer to the API documentation for detailed request and response structures for each endpoint.
- Users must register before attempting to login.
- Login with registered credentials to obtain a JWT token.
- Include the JWT token in the authorization header for subsequent requests to access protected resources (e.g., weather forecast).

## Contributing:

We welcome contributions! Help us make it even better.  Fix bugs, ✨ add features, or  share ideas.


