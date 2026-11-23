# 🔐 JWT Auth API (.NET 9)

A token-based authentication API built with **ASP.NET Core on .NET 9**. Users can register, log in, get a short-lived **access token** plus a long-lived **refresh token**, and call endpoints protected by authentication and by **role**.

![.NET](https://img.shields.io/badge/.NET_9-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=flat-square&logo=csharp&logoColor=white)
![EF Core](https://img.shields.io/badge/EF_Core_9-68217A?style=flat-square&logo=nuget&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=flat-square&logo=microsoftsqlserver&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-000000?style=flat-square&logo=jsonwebtokens&logoColor=white)

## ✨ Features

- **Register & login** with passwords hashed by ASP.NET Core Identity's `PasswordHasher`
- **JWT access tokens** signed with HMAC-SHA512, carrying the user's name, id and role as claims (valid for 1 day)
- **Refresh tokens**: 32-byte cryptographically random tokens, stored per user with a 7-day expiry
- **Token rotation**: every refresh issues a new access token *and* a new refresh token
- **Role-based authorization** (`[Authorize(Roles = "Admin")]`)
- Issuer, audience, lifetime and signing key are all validated
- **Interactive API docs** with OpenAPI + [Scalar](https://scalar.com/) in development

## 🧱 Project Structure

```
JwtAuthDotNet9/
├── Controllers/AuthController.cs   # HTTP endpoints
├── Services/
│   ├── IAuthService.cs             # service contract
│   └── AuthService.cs              # hashing, token creation, refresh logic
├── Entities/User.cs                # Id, Username, PasswordHash, Role, RefreshToken, expiry
├── Models/                         # UserDto, TokenResponseDto, RefreshTokenRequestDto
├── Data/UserDbContext.cs           # EF Core DbContext
├── Migrations/                     # EF Core migrations
└── Program.cs                      # DI, JWT bearer setup, OpenAPI/Scalar
```

## 📡 API Endpoints

Base route: `/api/auth`

| Method | Route | Auth | Description |
|---|---|---|---|
| `POST` | `/register` | Public | Create a user `{ "username", "password" }` |
| `POST` | `/login` | Public | Returns `{ "accessToken", "refreshToken" }` |
| `POST` | `/refresh-token` | Public | Body `{ "userId", "refreshToken" }` and returns a new token pair |
| `GET` | `/` | 🔒 Any logged-in user | Test endpoint for authenticated users |
| `GET` | `/admin-only` | 🔒 `Admin` role | Test endpoint for admins |

Send the access token as a header on protected routes:

```
Authorization: Bearer <accessToken>
```

## 🚀 Getting Started

**Prerequisites:** [.NET 9 SDK](https://dotnet.microsoft.com/download) and SQL Server (Express or LocalDB).

```bash
git clone https://github.com/Bayzeedsakib/JwtAuthDotNet9.git
cd JwtAuthDotNet9/JwtAuthDotNet9
```

1. Set your connection string and JWT settings in `appsettings.json`:

   ```json
   "ConnectionStrings": {
     "UserDatabse": "Server=localhost\\SQLEXPRESS;Database=UserDb;Trusted_Connection=true;TrustServerCertificate=true;"
   },
   "AppSettings": {
     "Token": "<a long random secret, at least 64 characters>",
     "Issuer": "MyAwesomeApp",
     "Audience": "MyAwesomeAudience"
   }
   ```

2. Create the database and run the API:

   ```bash
   dotnet tool install --global dotnet-ef   # if you don't have it
   dotnet ef database update
   dotnet run
   ```

3. Open the Scalar API reference at `https://localhost:<port>/scalar/v1` to try the endpoints.

## 🔄 Auth Flow

```
Register ──► Login ──► { accessToken, refreshToken }
                              │
             call API with Bearer accessToken
                              │
             access token expires ──► POST /refresh-token
                              │
                   new { accessToken, refreshToken }
```

## 👤 Author

**Bayzeed Sakib** · [GitHub](https://github.com/Bayzeedsakib) · [Portfolio](https://bayzeed-sakib-portfolio.vercel.app)
