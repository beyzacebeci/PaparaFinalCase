# Expense Flow API

A robust .NET-based API for managing employee expense claims. This application provides functionality for expense management, user authentication, payment processing, and comprehensive reporting.

## 🚀 Features

- **Authentication & Authorization**

  - JWT-based authentication
  - Role-based access control (Admin/Employee roles)
  - User registration and login
  - Token refresh mechanism

- **Expense Management**

  - CRUD operations for expense claims
  - Category-based expense organization
  - Expense status tracking (Pending, Approved, Rejected, Paid)
  - Document upload support
  - Payment method tracking

- **Payment Processing**

  - Payment transaction tracking
  - Bank integration simulation
  - Payment status monitoring
  - IBAN management

- **Reporting System**
  - User-specific expense reports
  - Category-based expense reports
  - Daily, weekly, and monthly reports
  - Overall expense statistics

## 🛠 Technical Stack

- **.NET 8.0**
- **Entity Framework Core**
- **SQL Server**
- **AutoMapper**
- **FluentValidation**
- **JWT Authentication**
- **Swagger/OpenAPI**

## 🏗 Architecture

The project follows Clean Architecture principles with these main layers:

- **ExpenseFlow/**
  - **ExpenseFlow.DataAccess** — Data Access, Migrations
  - **ExpenseFlow.Application** — Business Logic, Interfaces
  - **ExpenseFlow.API** — API Controllers, Filters

## 🔧 Setup

### 1. Clone Repository

```bash
git clone https://github.com/yourusername/ExpenseFlow.git
```

### 2. Update the connection string in `appsettings.Development.json`

```json
"ConnectionStrings": {
  "DefaultConnection": "Your-Connection-String"
}
```

### 3. Database Migration

```bash
# Navigate to ExpenseFlow.DataAccess
cd ExpenseFlow.DataAccess

# Create migration
dotnet ef migrations add InitialCreate

# Apply migration
dotnet ef database update
```

### 4. Build and Run

```bash
# Navigate to API project
cd ExpenseFlow.API

# Restore dependencies
dotnet restore

# Build project
dotnet build

# Run application
dotnet run
```

## 🔑 Authentication

The API uses JWT Bearer authentication. To access protected endpoints:

1. Register a user using `/api/Authentication/register`
2. Login using `/api/Authentication/login` to get a token
3. Use the token in the Authorization header: `Bearer {your-token}`

## 🔐 Authorization

Two main roles are available:

- **Admin**: Full access to all features
- **Employee**: Access to personal expense management

## 📝 API Documentation

When running in development mode, access the Swagger documentation at:

```bash
https://localhost:5001/swagger
```

## ⚡ Key Endpoints

### 🔐 Authentication

POST `/api/Authentication/register`

- Creates a new user account
- Body: UserRegistrationRequest
- Public endpoint

POST `/api/Authentication/login`

- Authenticates a user and returns a JWT token
- Body: UserLoginRequest
- Public endpoint

POST `/api/Authentication/refresh`

- Refreshes the access token
- Body: TokenRefreshRequest
- Public endpoint

### 💰 Expense Claims

GET `/api/ExpenseClaims`

- Lists all expense claims (Admin only)
- Authorization: Bearer token required

GET `/api/ExpenseClaims/user-claims`

- Lists current user's expense claims
- Authorization: Bearer token required

POST `/api/ExpenseClaims`

- Creates a new expense claim
- Body: ExpenseClaimRequest
- Authorization: Bearer token required

PUT `/api/ExpenseClaims/{id}/status`

- Updates expense claim status
- Body: ExpenseClaimStatusUpdateRequest
- Admin only
- Authorization: Bearer token required

### 📊 Reports

GET `/api/Reports/user`

- Gets current user's expense report
- Authorization: Bearer token required

GET `/api/Reports/all-users`

- Gets expense reports for all users
- Admin only
- Authorization: Bearer token required

GET `/api/Reports/categories`

- Gets category-based expense reports
- Admin only
- Authorization: Bearer token required

GET `/api/Reports/daily`

- Gets daily expense report
- Admin only
- Authorization: Bearer token required

GET `/api/Reports/weekly`

- Gets weekly expense report
- Admin only
- Authorization: Bearer token required

GET `/api/Reports/monthly`

- Gets monthly expense report
- Admin only
- Authorization: Bearer token required

### 💳 Payment Transactions

GET `/api/PaymentTransactions`

- Lists all payment transactions
- Admin only
- Authorization: Bearer token required

## 🛡 Security Features

- Password hashing using ASP.NET Core Identity
- JWT token authentication
- Role-based authorization
- Input validation using FluentValidation
- Global exception handling

## 📦 Dependencies

Key NuGet packages:

- Microsoft.AspNetCore.Identity.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- AutoMapper
- FluentValidation
- System.IdentityModel.Tokens.Jwt

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.
