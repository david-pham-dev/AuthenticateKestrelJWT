# .NET MAUI & ASP.NET Core (Kestrel + JWT) Project

## Overview
This project is a cross-platform mobile and desktop application built using **.NET MAUI** for the frontend and **ASP.NET Core** for the backend. The backend uses **Kestrel** as the web server and implements **JWT authentication** for secure user access.

## Features
- **User Authentication**: Secure login and registration using JWT authentication.
- **Cross-Platform Compatibility**: Runs on Android and Windows 10.
- **RESTful API**: Backend exposes endpoints for authentication and user data management.
- **Database Integration**: Stores user data securely with **MySQL**.
- **Secure API Communication**: JWT tokens are used to authenticate API requests.

## Technologies Used
### Frontend (.NET MAUI)
- .NET MAUI
- MVVM Architecture
- HttpClient for API calls

### Backend (ASP.NET Core)
- ASP.NET Core Web API
- Kestrel Web Server
- JWT Authentication
- MySQL Database
- Entity Framework Core

## Setup Instructions

### Prerequisites
- Install **.NET SDK 8.0+**
- Install **Visual Studio 2022+** with .NET MAUI and ASP.NET Core workload
- Install **MySQL Server** and MySQL Workbench

### Backend Setup
1. Clone the repository:
   ```sh
   git clone https://github.com/yourusername/your-repo.git
   cd your-repo/backend
   ```
2. Install dependencies:
   ```sh
   dotnet restore
   ```
3. Update database connection string in `appsettings.json`:

4. Apply migrations:
   ```sh
   dotnet ef database update
   ```
5. Run the backend server using Kestrel:
   ```sh
   dotnet run
   ```

### Frontend Setup
1. Navigate to the frontend project:

2. Restore dependencies:
   ```sh
   dotnet restore
   ```
3. Run the MAUI app:
   ```sh
   dotnet build
   dotnet maui run android  # or 'windows', 'ios', etc.
   ```
   Or via Ctrl + F5.

## API Endpoints
### Authentication
- `POST /api/auth/register` → Register a new user
- `POST /api/auth/login` → Authenticate and receive JWT token
- `GET /api/user/profile` → Fetch user profile (Requires JWT)

## Security Measures
- **JWT Token Expiration**: Tokens have a limited lifespan to enhance security.
- **Password Hashing**: User passwords are securely hashed using **BCrypt**.


## Future Enhancements
- Implement refresh tokens for improved security.
- Improving UI/UX design.
- Add support for social login (Google, Facebook, etc.).
- Enhance UI with animations and themes.
- Role-Based Authorization: Different access levels for users and admins.


