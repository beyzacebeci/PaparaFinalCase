# Expense Flow API

A robust .NET-based API for managing employee expense claims. This application provides functionality for expense management, user authentication, payment processing, and comprehensive reporting.

## 🚀 Features

## 🔑 Authentication

The API uses JWT Bearer authentication. To access protected endpoints:

1. Register a user using `/api/Authentication/register`
2. Login using `/api/Authentication/login` to get a token
3. Use the token in the Authorization header: `Bearer {your-token}`

## 🔐 Authorization

Two main roles are available:

- **Admin**: Full access
- **Employee**: Basic access

### Default Users

The system comes with two pre-configured users:

| Role     | Username | Password     |
| -------- | -------- | ------------ |
| Admin    | admin    | Admin123.    |
| Employee | employee | Employee123. |

- **Expense Management**

  - CRUD operations for expense claims
  - Category-based expense organization
  - Expense status tracking (Pending, Approved, Rejected, Paid)
  - Payment method tracking

- **Reporting System**
  - User-specific expense reports
  - Category-based expense reports
  - Daily, weekly, and monthly reports
  - Overall expense statistics

## 🛠 Technical Stack

- **.NET 9.0**
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
git clone https://github.com/beyzacebeci/ExpenseFlow.git
```

### 2. Update the connection string in `appsettings.Development.json`

```json
"ConnectionStrings": {
  "DefaultConnection": "Your-Connection-String"
}
```

### 3. Database Migration Steps

### Using Visual Studio Package Manager Console

1. Set App.API as startup project
2. Open Package Manager Console
3. Set App.Repositories as Default Project in PMC
4. Run these commands:

```bash
# Create initial migration
Add-Migration InitialCreate

# Apply migration to database
Update-Database
```

### Using .NET CLI

1. Open terminal in solution directory
2. Navigate to App.Repositories project:

```bash
cd App.DataAccess
```

3. Create migration:

```bash
dotnet ef migrations add InitialCreate --startup-project ../App.API
```

4. Apply migration:

```bash
dotnet ef database update --startup-project ../App.API
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
https://localhost:7231/swagger
```

## ⚡ Key Endpoints

### 🔐 Authentication

POST `/api/Authentication/register`

- Creates a new user account
- Public endpoint
- Body: UserRegistrationRequest
  - Email (string): User's email address
  - Password (string): User's password
  - FirstName (string): User's first name
  - LastName (string): User's last name
  - Role (string): User's role (Admin/Employee)
- Returns: Registration result with success/failure message

POST `/api/Authentication/login`

- Authenticates a user and returns a JWT token
- Public endpoint
- Body: UserLoginRequest
  - Email (string): User's email address
  - Password (string): User's password
- Returns: TokenResponse containing:
  - AccessToken (string): JWT access token
  - RefreshToken (string): Token for refreshing access
  - Expiration (DateTime): Token expiration time

POST `/api/Authentication/refresh`

- Refreshes the access token
- Public endpoint
- Body: TokenRefreshRequest
  - AccessToken (string): Current access token
  - RefreshToken (string): Current refresh token
- Returns: New TokenResponse with fresh tokens

### 💰 Expense Claims

GET `/api/ExpenseClaims`

- Lists all expense claims
- Authorization: Bearer token required
- Role: Admin only
- Returns all expense claims in the system with their details

GET `/api/ExpenseClaims/user-claims`

- Lists current user's expense claims
- Authorization: Bearer token required
- Role: Employee
- Returns expense claims for the authenticated user

POST `/api/ExpenseClaims`

- Creates a new expense claim
- Authorization: Bearer token required
- Role: Employee
- Body: ExpenseClaimRequest
  - ExpenseCategoryId (int): Category of the expense
  - Amount (decimal): Expense amount
  - Description (string): Detailed description
  - Location (string): Where the expense occurred
  - ExpenseDate (DateTime): Date of the expense
  - PaymentMethod (enum): CreditCard, Cash, DebitCard, Other
  - PaymentReference (string, optional): Reference number for payment

PUT `/api/ExpenseClaims/{id}/status`

- Updates expense claim status
- Authorization: Bearer token required
- Role: Admin only
- Body: ExpenseClaimStatusUpdateRequest
  - Status (enum): Pending, Approved, Rejected, Paid
  - ExpenseStatusDescription (string, optional): Reason for status change

Expense Status Types:

- Pending: Initial state of expense claim
- Approved: Expense claim has been approved
- Rejected: Expense claim has been rejected
- Paid: Expense has been paid out

Payment Methods:

- CreditCard
- Cash
- DebitCard
- Other

### 📊 Reports

GET `/api/Reports/user`

- Gets current user's expense report
- Authorization: Bearer token required
- Role: Employee
- Returns detailed expense report for the authenticated user

GET `/api/Reports/all-users`

- Gets expense reports for all users
- Authorization: Bearer token required
- Role: Admin only
- Returns comprehensive expense reports for all users in the system

GET `/api/Reports/categories`

- Gets category-based expense reports
- Authorization: Bearer token required
- Role: Admin only
- Returns expense statistics grouped by categories

GET `/api/Reports/overall`

- Gets overall expense statistics
- Authorization: Bearer token required
- Role: Admin only
- Returns general expense statistics and metrics

GET `/api/Reports/daily`

- Gets daily expense report
- Authorization: Bearer token required
- Role: Admin only
- Query Parameters: date (DateTime)
- Returns expense report for a specific day

GET `/api/Reports/weekly`

- Gets weekly expense report
- Authorization: Bearer token required
- Role: Admin only
- Query Parameters: startOfWeek (DateTime)
- Returns expense report for a specific week

GET `/api/Reports/monthly`

- Gets monthly expense report
- Authorization: Bearer token required
- Role: Admin only
- Query Parameters: year (int), month (int)
- Returns expense report for a specific month

### 💳 Payment Transactions

GET `/api/PaymentTransactions`

- Lists all payment transactions
- Authorization: Bearer token required
- Role: Admin only
- Returns: List of payment transactions with details:
  - TransactionId (string)
  - Amount (decimal)
  - Status (enum): Pending, Completed, Failed
  - PaymentDate (DateTime)
  - PaymentMethod (enum): CreditCard, Cash, DebitCard, Other
  - ReferenceNumber (string)
  - RelatedExpenseClaim (ExpenseClaim)

POST `/api/PaymentTransactions`

- Creates a new payment transaction
- Authorization: Bearer token required
- Role: Admin only
- Body: PaymentTransactionRequest
  - ExpenseClaimId (int): ID of the related expense claim
  - Amount (decimal): Payment amount
  - PaymentMethod (enum): CreditCard, Cash, DebitCard, Other
  - ReferenceNumber (string): Payment reference number
- Returns: Created payment transaction details

PUT `/api/PaymentTransactions/{id}/status`

- Updates payment transaction status
- Authorization: Bearer token required
- Role: Admin only
- Body: PaymentStatusUpdateRequest
  - Status (enum): Pending, Completed, Failed
  - StatusDescription (string, optional): Reason for status change
- Returns: Updated payment transaction

Payment Transaction Status Types:

- Pending: Initial state of payment
- Completed: Payment has been successfully processed
- Failed: Payment processing failed

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
